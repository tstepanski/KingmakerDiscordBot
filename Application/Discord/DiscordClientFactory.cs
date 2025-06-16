using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Discord;
using Discord.Net.Rest;
using Discord.Net.WebSockets;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Observability;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InternalDiscordConfiguration = KingmakerDiscordBot.Application.Configuration.Discord; 

namespace KingmakerDiscordBot.Application.Discord;

internal sealed class DiscordClientFactory(WebSocketProvider webSocketProvider, RestClientProvider restClientProvider,
    IAmazonSecretsManager secretsManager, IOptions<InternalDiscordConfiguration> configuration, 
    ILogger<DiscordSocketClient> logger) : IDiscordClientFactory, IDisposable
{
    private IDiscordClient? _cachedInstance;
    private readonly SemaphoreSlim _lock = new(1, 1);
    
    public async Task<IDiscordClient> CreateAsync(CancellationToken cancellationToken)
    {
        if (_cachedInstance is not null)
        {
            return _cachedInstance;
        }

        await _lock.WaitAsync(cancellationToken);

        try
        {
            _cachedInstance ??= await CreateNewAsync(cancellationToken);
            
            logger.LogInformation("Created new discord client");

            return _cachedInstance;
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<IDiscordClient> CreateNewAsync(CancellationToken cancellationToken)
    {
        var request = new GetSecretValueRequest
        {
            
            SecretId = configuration.Value.TokenArn
        };
        
        var response = await secretsManager.GetSecretValueAsync(request, cancellationToken);
        var token = response.SecretString.Trim();

        var discordConfiguration = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.None,
            LogGatewayIntentWarnings = false,
            LogLevel = LogSeverity.Critical,
            RestClientProvider = restClientProvider,
            WebSocketProvider = webSocketProvider
        };

        var client = new DiscordSocketClient(discordConfiguration);

        client.Log += logger.Log;

        await client.LoginAsync(TokenType.Bot, token);

        client.Ready += () =>
        {
            logger.LogInformation("Client ready");

            return Task.CompletedTask;
        };

        return client;
    }

    public void Dispose()
    {
        _cachedInstance?.Dispose();
        _lock.Dispose();
    }
}