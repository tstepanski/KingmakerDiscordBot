using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildStickerDeletedListener : IListener
{
    Task OnGuildStickerDeleted(IDiscordRestClientProxy client, SocketCustomSticker socketCustomSticker,
        CancellationToken cancellationToken);
}