namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IConnectedListener : IListener
{
    Task OnConnected(CancellationToken cancellationToken);
}