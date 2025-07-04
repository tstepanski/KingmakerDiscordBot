using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ICurrentUserUpdatedListener : IListener
{
    Task OnCurrentUserUpdated(IDiscordRestClientProxy client, SocketSelfUser before, SocketSelfUser after,
        CancellationToken cancellationToken);
}