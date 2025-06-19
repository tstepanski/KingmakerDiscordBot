using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserCommandExecutedListener : IListener
{
    Task OnUserCommandExecuted(IDiscordRestClientProxy client, SocketUserCommand socketUserCommand,
        CancellationToken cancellationToken);
}