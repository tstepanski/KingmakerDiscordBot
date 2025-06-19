using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ILogListener
{
    Task OnLog(IDiscordRestClientProxy client, LogMessage logMessage, CancellationToken cancellationToken);
}