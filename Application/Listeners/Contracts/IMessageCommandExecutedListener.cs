using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IMessageCommandExecutedListener : IListener
{
    Task OnMessageCommandExecuted(IDiscordRestClientProxy client, SocketMessageCommand socketMessageCommand,
        CancellationToken cancellationToken);
}