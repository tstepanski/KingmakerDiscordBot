using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserUnbannedListener : IListener
{
    Task OnUserUnbanned(IDiscordRestClientProxy client, SocketUser user, SocketGuild guild,
        CancellationToken cancellationToken);
}