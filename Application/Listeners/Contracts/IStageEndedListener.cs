using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IStageEndedListener
{
    Task OnStageEnded(IDiscordRestClientProxy client, SocketStageChannel socketStageChannel,
        CancellationToken cancellationToken);
}