using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IRoleUpdatedListener : IListener
{
    Task OnRoleUpdated(IDiscordRestClientProxy client, SocketRole before, SocketRole after,
        CancellationToken cancellationToken);
}