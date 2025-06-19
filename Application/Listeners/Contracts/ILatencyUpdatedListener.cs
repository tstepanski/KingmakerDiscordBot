using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ILatencyUpdatedListener : IListener
{
    Task OnLatencyUpdated(IDiscordRestClientProxy client, int oldLatency, int newLatency,
        CancellationToken cancellationToken);
}