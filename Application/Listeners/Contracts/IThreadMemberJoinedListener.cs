using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IThreadMemberJoinedListener : IListener
{
    Task OnThreadMemberJoined(IDiscordRestClientProxy client, SocketThreadUser socketThreadUser,
        CancellationToken cancellationToken);
}