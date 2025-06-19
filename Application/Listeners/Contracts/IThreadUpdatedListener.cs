using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IThreadUpdatedListener
{
    Task OnThreadUpdated(IDiscordRestClientProxy client, Cacheable<SocketThreadChannel, ulong> cachedChannel,
        SocketThreadChannel updatedChannel, CancellationToken cancellationToken);
}