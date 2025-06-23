using KingmakerDiscordBot.Application.Listeners;

namespace KingmakerDiscordBot.Application.Discord;

internal sealed class Listener(IDiscordClientFactory clientFactory, IListenerRegistrar listenerRegistrar, 
    ILogger<Listener> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = await clientFactory.CreateAsync(stoppingToken);

        listenerRegistrar.RegisterAll(client, stoppingToken);

        await client.StartAsync();

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Application shutting down, stopping client");
        }

        await client.StopAsync();
    }
}