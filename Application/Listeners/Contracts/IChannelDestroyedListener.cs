using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IChannelDestroyedListener
{
    Task OnChannelDestroyed(IDiscordRestClientProxy client, SocketChannel socketChannel,
        CancellationToken cancellationToken);
}