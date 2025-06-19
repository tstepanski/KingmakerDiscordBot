using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildStickerDeletedListener
{
    Task OnGuildStickerDeleted(IDiscordRestClientProxy client, SocketCustomSticker socketCustomSticker,
        CancellationToken cancellationToken);
}