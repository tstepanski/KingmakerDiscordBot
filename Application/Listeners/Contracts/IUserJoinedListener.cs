using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserJoinedListener : IListener
{
    Task OnUserJoined(IDiscordRestClientProxy client, SocketGuildUser socketGuildUser,
        CancellationToken cancellationToken);
}