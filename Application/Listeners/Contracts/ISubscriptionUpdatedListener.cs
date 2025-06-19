using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISubscriptionUpdatedListener
{
    Task OnSubscriptionUpdated(IDiscordRestClientProxy client, Cacheable<SocketSubscription, ulong> cachedSubscription,
        SocketSubscription subscription, CancellationToken cancellationToken);
}