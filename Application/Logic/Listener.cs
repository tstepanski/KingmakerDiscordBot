using Microsoft.Extensions.Hosting;

namespace KingmakerDiscordBot.Application.Logic;

internal sealed class Listener(IDiscordClientFactory clientFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var oneMinute = TimeSpan.FromMinutes(1);
        var client = await clientFactory.CreateAsync(stoppingToken);
        
        await client.StartAsync();

        while (stoppingToken.IsCancellationRequested is false)
        {
            await Task.Delay(oneMinute, stoppingToken);
        }

        await client.StopAsync();
    }
}