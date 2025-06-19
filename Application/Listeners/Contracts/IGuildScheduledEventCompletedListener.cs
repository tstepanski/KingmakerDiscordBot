using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildScheduledEventCompletedListener
{
    Task OnGuildScheduledEventCompleted(IDiscordRestClientProxy client, SocketGuildEvent socketGuildEvent,
        CancellationToken cancellationToken);
}