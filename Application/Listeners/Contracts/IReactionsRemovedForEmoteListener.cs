using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IReactionsRemovedForEmoteListener
{
    Task OnReactionsRemovedForEmote(IDiscordRestClientProxy client, Cacheable<IUserMessage, ulong> cachedMessage,
        Cacheable<IMessageChannel, ulong> channel, IEmote emote, CancellationToken cancellationToken);
}