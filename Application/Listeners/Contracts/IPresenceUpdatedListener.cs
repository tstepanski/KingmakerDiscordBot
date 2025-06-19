using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IPresenceUpdatedListener
{
    Task OnPresenceUpdated(IDiscordRestClientProxy client, SocketUser user, SocketPresence before, SocketPresence after,
        CancellationToken cancellationToken);
}