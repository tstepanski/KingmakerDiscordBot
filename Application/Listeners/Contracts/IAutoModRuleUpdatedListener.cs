using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IAutoModRuleUpdatedListener : IListener
{
    Task OnAutoModRuleUpdated(IDiscordRestClientProxy client, Cacheable<SocketAutoModRule, ulong> cachedRule,
        SocketAutoModRule updatedRule, CancellationToken cancellationToken);
}