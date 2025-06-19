using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ILoggedOutListener : IListener
{
    Task OnLoggedOut(IDiscordRestClientProxy client, CancellationToken cancellationToken);
}