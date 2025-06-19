using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IStageUpdatedListener
{
    Task OnStageUpdated(IDiscordRestClientProxy client, SocketStageChannel before, SocketStageChannel after,
        CancellationToken cancellationToken);
}