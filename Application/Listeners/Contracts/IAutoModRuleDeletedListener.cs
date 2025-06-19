using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IAutoModRuleDeletedListener : IListener
{
    Task OnAutoModRuleDeleted(IDiscordRestClientProxy client, SocketAutoModRule socketAutoModRule,
        CancellationToken cancellationToken);
}