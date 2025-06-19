using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildMemberUpdatedListener : IListener
{
    Task OnGuildMemberUpdated(IDiscordRestClientProxy client, Cacheable<SocketGuildUser, ulong> cachedUser,
        SocketGuildUser updatedUser, CancellationToken cancellationToken);
}