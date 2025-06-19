using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildAvailableListener : IListener
{
    Task OnGuildAvailable(IDiscordRestClientProxy client, SocketGuild socketGuild, CancellationToken cancellationToken);
}