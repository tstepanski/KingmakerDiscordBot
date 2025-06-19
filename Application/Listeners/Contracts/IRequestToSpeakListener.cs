using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IRequestToSpeakListener
{
    Task OnRequestToSpeak(IDiscordRestClientProxy client, SocketStageChannel stageChannel, SocketGuildUser user,
        CancellationToken cancellationToken);
}