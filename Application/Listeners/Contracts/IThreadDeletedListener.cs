using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IThreadDeletedListener : IListener
{
    Task OnThreadDeleted(IDiscordRestClientProxy client, Cacheable<SocketThreadChannel, ulong> channel,
        CancellationToken cancellationToken);
}