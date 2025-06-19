using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISpeakerRemovedListener
{
    Task OnSpeakerRemoved(IDiscordRestClientProxy client, SocketStageChannel stageChannel, SocketGuildUser user,
        CancellationToken cancellationToken);
}