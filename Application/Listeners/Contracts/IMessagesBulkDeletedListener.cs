using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IMessagesBulkDeletedListener
{
    Task OnMessagesBulkDeleted(IDiscordRestClientProxy client,
        IReadOnlyCollection<Cacheable<IMessage, ulong>> cachedMessages, Cacheable<IMessageChannel, ulong> channel,
        CancellationToken cancellationToken);
}