using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISpeakerAddedListener : IListener
{
    Task OnSpeakerAdded(IDiscordRestClientProxy client, SocketStageChannel stageChannel, SocketGuildUser user,
        CancellationToken cancellationToken);
}