using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IVoiceChannelStatusUpdatedListener : IListener
{
    Task OnVoiceChannelStatusUpdated(IDiscordRestClientProxy client,
        Cacheable<SocketVoiceChannel, ulong> cachedVoiceChannel, string previousStatus, string currentStatus, 
        CancellationToken cancellationToken);
}