using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IThreadMemberLeftListener : IListener
{
    Task OnThreadMemberLeft(IDiscordRestClientProxy client, SocketThreadUser socketThreadUser,
        CancellationToken cancellationToken);
}