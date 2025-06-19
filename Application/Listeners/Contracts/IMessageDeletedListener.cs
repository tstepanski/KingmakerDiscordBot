using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IMessageDeletedListener : IListener
{
    Task OnMessageDeleted(IDiscordRestClientProxy client, Cacheable<IMessage, ulong> cachedMessage,
        Cacheable<IMessageChannel, ulong> channel, CancellationToken cancellationToken);
}