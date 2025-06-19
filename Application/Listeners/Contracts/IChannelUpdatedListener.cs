using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IChannelUpdatedListener
{
    Task OnChannelUpdated(IDiscordRestClientProxy client, SocketChannel before, SocketChannel after,
        CancellationToken cancellationToken);
}