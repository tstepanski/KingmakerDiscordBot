using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IPresenceUpdatedListener : IListener
{
    Task OnPresenceUpdated(IDiscordRestClientProxy client, SocketUser user, SocketPresence before, SocketPresence after,
        CancellationToken cancellationToken);
}