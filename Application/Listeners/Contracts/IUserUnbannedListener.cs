using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserUnbannedListener
{
    Task OnUserUnbanned(IDiscordRestClientProxy client, SocketUser user, SocketGuild guild,
        CancellationToken cancellationToken);
}