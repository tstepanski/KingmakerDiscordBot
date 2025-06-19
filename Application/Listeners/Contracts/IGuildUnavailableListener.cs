using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildUnavailableListener : IListener
{
    Task OnGuildUnavailable(IDiscordRestClientProxy client, SocketGuild socketGuild,
        CancellationToken cancellationToken);
}