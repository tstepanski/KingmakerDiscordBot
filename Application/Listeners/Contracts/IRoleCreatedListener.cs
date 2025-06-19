using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IRoleCreatedListener : IListener
{
    Task OnRoleCreated(IDiscordRestClientProxy client, SocketRole socketRole, CancellationToken cancellationToken);
}