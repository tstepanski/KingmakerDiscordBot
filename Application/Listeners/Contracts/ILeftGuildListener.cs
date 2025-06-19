using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ILeftGuildListener
{
    Task OnLeftGuild(IDiscordRestClientProxy client, SocketGuild socketGuild, CancellationToken cancellationToken);
}