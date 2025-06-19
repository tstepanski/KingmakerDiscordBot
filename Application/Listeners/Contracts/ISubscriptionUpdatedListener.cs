using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISubscriptionUpdatedListener : IListener
{
    Task OnSubscriptionUpdated(IDiscordRestClientProxy client, Cacheable<SocketSubscription, ulong> cachedSubscription,
        SocketSubscription subscription, CancellationToken cancellationToken);
}