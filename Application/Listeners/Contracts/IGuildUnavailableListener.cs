using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildUnavailableListener
{
    Task OnGuildUnavailable(IDiscordRestClientProxy client, SocketGuild socketGuild,
        CancellationToken cancellationToken);
}