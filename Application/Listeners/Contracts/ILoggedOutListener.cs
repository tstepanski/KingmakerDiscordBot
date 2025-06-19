using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ILoggedOutListener
{
    Task OnLoggedOut(IDiscordRestClientProxy client, CancellationToken cancellationToken);
}