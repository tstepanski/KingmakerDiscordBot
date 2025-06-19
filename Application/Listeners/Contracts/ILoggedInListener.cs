using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ILoggedInListener : IListener
{
    Task OnLoggedIn(IDiscordRestClientProxy client, CancellationToken cancellationToken);
}