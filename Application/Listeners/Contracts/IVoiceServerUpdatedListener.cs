using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IVoiceServerUpdatedListener : IListener
{
    Task OnVoiceServerUpdated(IDiscordRestClientProxy client, SocketVoiceServer socketVoiceServer,
        CancellationToken cancellationToken);
}