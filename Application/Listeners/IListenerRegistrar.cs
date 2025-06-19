using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners;

internal interface IListenerRegistrar
{
    void RegisterAll(ISocketClientProxy socketClientProxy, CancellationToken cancellationToken);
}