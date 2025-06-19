using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IEntitlementCreatedListener
{
    Task OnEntitlementCreated(IDiscordRestClientProxy client, SocketEntitlement socketEntitlement,
        CancellationToken cancellationToken);
}