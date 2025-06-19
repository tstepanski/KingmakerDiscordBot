using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IReadyListener
{
    Task OnReady(IDiscordRestClientProxy client, CancellationToken cancellationToken);
}