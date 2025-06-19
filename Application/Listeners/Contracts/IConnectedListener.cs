namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IConnectedListener
{
    Task OnConnected(CancellationToken cancellationToken);
}