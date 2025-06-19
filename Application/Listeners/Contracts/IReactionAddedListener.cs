using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IReactionAddedListener
{
    Task OnReactionAdded(IDiscordRestClientProxy client, Cacheable<IUserMessage, ulong> cachedMessage,
        Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction, CancellationToken cancellationToken);
}