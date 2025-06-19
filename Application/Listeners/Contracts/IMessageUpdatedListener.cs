using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IMessageUpdatedListener : IListener
{
    Task OnMessageUpdated(IDiscordRestClientProxy client, Cacheable<IMessage, ulong> cachedMessage,
        SocketMessage updatedMessage, ISocketMessageChannel channel, CancellationToken cancellationToken);
}