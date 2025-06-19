using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISubscriptionDeletedListener
{
    Task OnSubscriptionDeleted(IDiscordRestClientProxy client, Cacheable<SocketSubscription, ulong> cachedSubscription,
        CancellationToken cancellationToken);
}