using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserLeftListener
{
    Task OnUserLeft(IDiscordRestClientProxy client, SocketGuild guild, SocketUser user,
        CancellationToken cancellationToken);
}