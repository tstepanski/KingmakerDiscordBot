using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Repositories;
using KingmakerDiscordBot.Application.StaticData;
using Microsoft.Extensions.Hosting;

namespace KingmakerDiscordBot.Application.Listeners;

internal sealed class GuildAvailabilityListener(IDiscordClientFactory clientFactory, IGuildRepository guildRepository) : 
    BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = await clientFactory.CreateAsync(stoppingToken);

        Func<SocketGuild, Task> onGuildAvailable = @event => HandleGuildAvailableAsync(@event, stoppingToken);
        Func<SocketGuild, Task> onGuildJoined = @event => HandleGuildJoinedAsync(@event, stoppingToken);
        
        client.GuildAvailable += onGuildAvailable;
        client.JoinedGuild += onGuildJoined;

        stoppingToken.Register(() =>
        {
            client.GuildAvailable -= onGuildAvailable;
            client.JoinedGuild -= onGuildJoined;
        });
    }

    private Task HandleGuildAvailableAsync(SocketGuild guildAvailableEvent, CancellationToken cancellationToken)
    {
        
    }

    private async Task HandleGuildJoinedAsync(SocketGuild guildJoinedEvent, CancellationToken cancellationToken)
    {
        await guildRepository.CreateNew(guildJoinedEvent.Id, cancellationToken);
        
        Ability.
    }
}