using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IAutoModActionExecutedListener : IListener
{
    Task OnAutoModActionExecuted(IDiscordRestClientProxy client, SocketGuild guild, AutoModRuleAction action,
        AutoModActionExecutedData data,
        CancellationToken cancellationToken);
}