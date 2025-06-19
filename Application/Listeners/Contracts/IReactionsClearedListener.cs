using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IReactionsClearedListener
{
    Task OnReactionsCleared(IDiscordRestClientProxy client, Cacheable<IUserMessage, ulong> cachedMessage,
        Cacheable<IMessageChannel, ulong> channel, CancellationToken cancellationToken);
}