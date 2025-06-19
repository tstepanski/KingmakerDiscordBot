using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserBannedListener
{
    Task OnUserBanned(IDiscordRestClientProxy client, SocketUser user, SocketGuild guild,
        CancellationToken cancellationToken);
}