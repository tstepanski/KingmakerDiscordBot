using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IThreadCreatedListener : IListener
{
    Task OnThreadCreated(IDiscordRestClientProxy client, SocketThreadChannel socketThreadChannel,
        CancellationToken cancellationToken);
}