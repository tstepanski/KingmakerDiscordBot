using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserUpdatedListener : IListener
{
    Task OnUserUpdated(IDiscordRestClientProxy client, SocketUser before, SocketUser after,
        CancellationToken cancellationToken);
}