using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IAutoModRuleCreatedListener
{
    Task OnAutoModRuleCreated(IDiscordRestClientProxy client, SocketAutoModRule socketAutoModRule,
        CancellationToken cancellationToken);
}