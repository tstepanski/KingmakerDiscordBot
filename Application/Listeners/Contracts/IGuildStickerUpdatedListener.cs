using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildStickerUpdatedListener : IListener
{
    Task OnGuildStickerUpdated(IDiscordRestClientProxy client, SocketCustomSticker before, SocketCustomSticker after,
        CancellationToken cancellationToken);
}