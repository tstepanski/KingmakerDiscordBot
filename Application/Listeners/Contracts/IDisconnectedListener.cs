using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IDisconnectedListener
{
    Task OnDisconnected(IDiscordRestClientProxy client, Exception exception, CancellationToken cancellationToken);
}