using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IThreadMemberJoinedListener
{
    Task OnThreadMemberJoined(IDiscordRestClientProxy client, SocketThreadUser socketThreadUser,
        CancellationToken cancellationToken);
}