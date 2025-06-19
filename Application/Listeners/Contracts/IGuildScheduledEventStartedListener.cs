using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildScheduledEventStartedListener
{
    Task OnGuildScheduledEventStarted(IDiscordRestClientProxy client, SocketGuildEvent socketGuildEvent,
        CancellationToken cancellationToken);
}