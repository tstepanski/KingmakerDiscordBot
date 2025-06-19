using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IRoleDeletedListener : IListener
{
    Task OnRoleDeleted(IDiscordRestClientProxy client, SocketRole socketRole, CancellationToken cancellationToken);
}