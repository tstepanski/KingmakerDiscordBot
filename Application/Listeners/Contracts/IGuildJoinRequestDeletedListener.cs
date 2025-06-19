using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildJoinRequestDeletedListener
{
    Task OnGuildJoinRequestDeleted(IDiscordRestClientProxy client, Cacheable<SocketGuildUser, ulong> user,
        SocketGuild guild, CancellationToken cancellationToken);
}