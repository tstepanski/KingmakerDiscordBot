using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IDisconnectedListener : IListener
{
    Task OnDisconnected(IDiscordRestClientProxy client, Exception exception, CancellationToken cancellationToken);
}