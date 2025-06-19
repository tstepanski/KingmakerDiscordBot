using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildScheduledEventCancelledListener
{
    Task OnGuildScheduledEventCancelled(IDiscordRestClientProxy client, SocketGuildEvent socketGuildEvent,
        CancellationToken cancellationToken);
}