using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildJoinRequestDeletedListener : IListener
{
    Task OnGuildJoinRequestDeleted(IDiscordRestClientProxy client, Cacheable<SocketGuildUser, ulong> user,
        SocketGuild guild, CancellationToken cancellationToken);
}