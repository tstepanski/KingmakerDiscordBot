using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IStageStartedListener : IListener
{
    Task OnStageStarted(IDiscordRestClientProxy client, SocketStageChannel socketStageChannel,
        CancellationToken cancellationToken);
}