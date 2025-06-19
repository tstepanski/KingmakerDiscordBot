using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IReactionRemovedListener : IListener
{
    Task OnReactionRemoved(IDiscordRestClientProxy client, Cacheable<IUserMessage, ulong> cachedMessage,
        Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction, CancellationToken cancellationToken);
}