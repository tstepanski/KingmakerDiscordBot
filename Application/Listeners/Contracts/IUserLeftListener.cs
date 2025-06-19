using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserLeftListener : IListener
{
    Task OnUserLeft(IDiscordRestClientProxy client, SocketGuild guild, SocketUser user,
        CancellationToken cancellationToken);
}