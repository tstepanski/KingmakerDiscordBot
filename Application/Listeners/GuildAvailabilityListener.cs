using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Listeners.Contracts;
using KingmakerDiscordBot.Application.Repositories;

namespace KingmakerDiscordBot.Application.Listeners;

internal sealed class GuildAvailabilityListener(IGuildRepository repository,
    ICommandsPayloadGenerator commandsPayloadGenerator) : IJoinedGuildListener, IGuildAvailableListener
{
    public async Task OnGuildAvailable(IDiscordRestClientProxy client, SocketGuild guildAvailableEvent,
        CancellationToken cancellationToken)
    {
        var knownCommandsHash = await repository.GetKnownCommandsHashAsync(guildAvailableEvent.Id, cancellationToken);

        if (knownCommandsHash != commandsPayloadGenerator.CurrentHashCode)
        {
            await RefreshCommandsAsync(client, guildAvailableEvent, cancellationToken);
        }
    }

    public async Task OnJoinedGuild(IDiscordRestClientProxy client, SocketGuild guildJoinedEvent,
        CancellationToken cancellationToken)
    {
        await repository.CreateNew(guildJoinedEvent.Id, cancellationToken);

        await RefreshCommandsAsync(client, guildJoinedEvent, cancellationToken);
    }

    private async Task RefreshCommandsAsync(IDiscordRestClientProxy client, SocketGuild guild, CancellationToken
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

        await client.BulkOverwriteGuildCommands(commands, guild.Id, requestOptions);

        await repository.UpdateKnownCommandsHashAsync(guild.Id, commandsPayloadGenerator.CurrentHashCode,
            cancellationToken);
    }
}