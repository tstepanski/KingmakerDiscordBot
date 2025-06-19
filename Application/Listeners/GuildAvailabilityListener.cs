using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Repositories;
using Microsoft.Extensions.Hosting;

namespace KingmakerDiscordBot.Application.Listeners;

internal sealed class GuildAvailabilityListener(IDiscordClientFactory clientFactory, IGuildRepository repository,
    ICommandsPayloadGenerator commandsPayloadGenerator) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = await clientFactory.CreateAsync(stoppingToken);

        Func<SocketGuild, Task> onGuildAvailable = @event => HandleGuildAvailableAsync(client, @event, stoppingToken);
        Func<SocketGuild, Task> onGuildJoined = @event => HandleGuildJoinedAsync(client, @event, stoppingToken);

        client.GuildAvailable += onGuildAvailable;
        client.JoinedGuild += onGuildJoined;

        stoppingToken.Register(() =>
        {
            client.GuildAvailable -= onGuildAvailable;
            client.JoinedGuild -= onGuildJoined;
        });
    }

    private async Task HandleGuildAvailableAsync(ISocketClientProxy client, SocketGuild guildAvailableEvent, 
        CancellationToken cancellationToken)
    {
        var knownCommandsHash = await repository.GetKnownCommandsHashAsync(guildAvailableEvent.Id, cancellationToken);

        if (knownCommandsHash != commandsPayloadGenerator.CurrentHashCode)
        {
            await RefreshCommandsAsync(client, guildAvailableEvent, cancellationToken);
        }
    }

    private async Task HandleGuildJoinedAsync(ISocketClientProxy client, SocketGuild guildJoinedEvent, 
        CancellationToken cancellationToken)
    {
        await repository.CreateNew(guildJoinedEvent.Id, cancellationToken);
        
        await RefreshCommandsAsync(client, guildJoinedEvent, cancellationToken);
    }

    private async Task RefreshCommandsAsync(ISocketClientProxy client, SocketGuild guild, CancellationToken 
        cancellationToken)
    {
        var commands = commandsPayloadGenerator
            .GetAllCommands()
            .Cast<ApplicationCommandProperties>()
            .ToArray();

        var requestOptions = new RequestOptions
        {
            CancelToken = cancellationToken,
            RetryMode = RetryMode.RetryRatelimit
        };
        
        await client.Rest.BulkOverwriteGuildCommands(commands, guild.Id, requestOptions);

        await repository.UpdateKnownCommandsHashAsync(guild.Id, commandsPayloadGenerator.CurrentHashCode,
            cancellationToken);
    }
}