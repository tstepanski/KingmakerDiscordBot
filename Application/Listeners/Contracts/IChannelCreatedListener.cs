using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IChannelCreatedListener
{
    Task OnChannelCreated(IDiscordRestClientProxy client, SocketChannel socketChannel,
        CancellationToken cancellationToken);
}