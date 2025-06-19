using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IStageUpdatedListener : IListener
{
    Task OnStageUpdated(IDiscordRestClientProxy client, SocketStageChannel before, SocketStageChannel after,
        CancellationToken cancellationToken);
}