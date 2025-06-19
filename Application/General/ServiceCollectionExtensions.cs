using System.Net;
using System.Net.Http.Headers;
using Amazon;
using Amazon.CloudWatch;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SecretsManager;
using Amazon.XRay.Recorder.Handlers.System.Net;
using AWS.Logger;
using Discord.Net.Rest;
using Discord.Net.WebSockets;
using KingmakerDiscordBot.Application.Configuration;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Listeners;
using KingmakerDiscordBot.Application.Listeners.Contracts;
using KingmakerDiscordBot.Application.Observability;
using KingmakerDiscordBot.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using InternalDiscordConfiguration = KingmakerDiscordBot.Application.Configuration.Discord;

namespace KingmakerDiscordBot.Application.General;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterAll(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var awsConfiguration = configuration.GetSection("AWS").Get<Aws>()!;
        var region = RegionEndpoint.GetBySystemName(awsConfiguration.Region);

        var awsOptions = new AWSOptions
        {
            Region = region
        };

        var listenerType = typeof(IListener);

        var listenerImplementationTypes = listenerType
            .Assembly
            .GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.IsAbstract is false)
            .Where(type => listenerType.IsAssignableFrom(type));

        return listenerImplementationTypes
            .Aggregate(serviceCollection, (reference, listenerImplementationType) =>
                reference.AddSingleton(listenerType, listenerImplementationType))
            .AddDefaultAWSOptions(awsOptions)
            .Configure<InternalDiscordConfiguration>(configuration, "Discord")
            .Configure<Tables>(configuration, "Tables")
            .Configure<Heartbeat>(configuration, "Heartbeat")
            .AddSingleton<IGuildRepository, GuildRepository>()
            .AddSingleton<IAmazonSecretsManager, AmazonSecretsManagerClient>()
            .AddSingleton<IDiscordClientFactory, DiscordClientFactory>()
            .AddSingleton<ICommandsPayloadGenerator, CommandsPayloadGenerator>()
            .AddSingleton<IInstanceIdHelper, InstanceIdHelper>()
            .AddSingleton<ICommandsPayloadGenerator, CommandsPayloadGenerator>()
            .AddSingleton<IListenerRegistrar, ListenerRegistrar>()
            .AddAWSService<IAmazonCloudWatch>()
            .AddAWSService<IAmazonDynamoDB>()
            .AddHttpClient()
            .AddHostedService<Listener>()
            .AddHostedService<CloudwatchHeartbeatService>()
            .AddSingleton<WebSocketProvider>(_ =>
            {
                var webSocketProvider = DefaultWebSocketProvider.Create();

                return () =>
                {
                    var defaultClient = webSocketProvider();

                    return new TracedWebSocketClient(defaultClient);
                };
            })
            .AddSingleton<RestClientProvider>(provider =>
            {
                return baseUrl =>
                {
                    var httpClient = provider.GetRequiredService<HttpClient>();

                    return new TracedRestClient(baseUrl, httpClient);
                };
            })
            .ConfigureHttpClientDefaults(builder =>
            {
                var httpMessageHandler = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UseCookies = false
                };

                var tracingHandler = new HttpClientXRayTracingHandler(httpMessageHandler);

                builder
                    .ConfigurePrimaryHttpMessageHandler(() => tracingHandler)
                    .ConfigureHttpClient(client =>
                    {
                        client.DefaultRequestHeaders.AcceptEncoding.Clear();

                        AddEncodingCompression("gzip");
                        AddEncodingCompression("deflate");

                        return;

                        void AddEncodingCompression(string method)
                        {
                            var headerValue = StringWithQualityHeaderValue.Parse(method);

                            client.DefaultRequestHeaders.AcceptEncoding.Add(headerValue);
                        }
                    });
            })
            .AddLogging(builder =>
            {
                var loggerConfiguration = new AWSLoggerConfig
                {
                    LogGroup = awsConfiguration.LogGroup,
                    Region = awsConfiguration.Region,
                    FlushTimeout = TimeSpan.FromSeconds(5),
                };

                builder
                    .ClearProviders()
                    .AddConsole()
                    .AddAWSProvider(loggerConfiguration);
            });
    }
}