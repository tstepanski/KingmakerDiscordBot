using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildUpdatedListener : IListener
{
    Task OnGuildUpdated(IDiscordRestClientProxy client, SocketGuild before, SocketGuild after,
        CancellationToken cancellationToken);
}