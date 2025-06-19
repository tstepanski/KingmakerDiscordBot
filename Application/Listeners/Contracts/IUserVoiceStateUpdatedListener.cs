using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserVoiceStateUpdatedListener : IListener
{
    Task OnUserVoiceStateUpdated(IDiscordRestClientProxy client, SocketUser user, SocketVoiceState beforeState,
        SocketVoiceState afterState, CancellationToken cancellationToken);
}