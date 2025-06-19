using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildScheduledEventUpdatedListener
{
    Task OnGuildScheduledEventUpdated(IDiscordRestClientProxy client, Cacheable<SocketGuildEvent, ulong> cachedEvent,
        SocketGuildEvent updatedEvent, CancellationToken cancellationToken);
}