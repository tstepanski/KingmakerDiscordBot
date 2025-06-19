using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserJoinedListener
{
    Task OnUserJoined(IDiscordRestClientProxy client, SocketGuildUser socketGuildUser,
        CancellationToken cancellationToken);
}