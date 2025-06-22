using KingmakerDiscordBot.Application.Listeners;
using Microsoft.Extensions.Hosting;

namespace KingmakerDiscordBot.Application.Discord;

internal sealed class Listener(IDiscordClientFactory clientFactory,
    IListenerRegistrar listenerRegistrar) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var oneMinute = TimeSpan.FromMinutes(1);
        var client = await clientFactory.CreateAsync(stoppingToken);

        listenerRegistrar.RegisterAll(client, stoppingToken);

        await client.StartAsync();

        await Task.Delay(Timeout.Infinite, stoppingToken);

        await client.StopAsync();
    }
}