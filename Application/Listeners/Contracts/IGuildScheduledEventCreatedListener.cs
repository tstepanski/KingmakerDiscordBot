using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildScheduledEventCreatedListener
{
    Task OnGuildScheduledEventCreated(IDiscordRestClientProxy client, SocketGuildEvent socketGuildEvent,
        CancellationToken cancellationToken);
}