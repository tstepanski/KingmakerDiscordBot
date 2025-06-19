using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IWebhooksUpdatedListener
{
    Task OnWebhooksUpdated(IDiscordRestClientProxy client, SocketGuild guild, SocketChannel channel,
        CancellationToken cancellationToken);
}