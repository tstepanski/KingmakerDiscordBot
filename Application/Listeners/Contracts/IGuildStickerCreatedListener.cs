using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildStickerCreatedListener
{
    Task OnGuildStickerCreated(IDiscordRestClientProxy client, SocketCustomSticker socketCustomSticker,
        CancellationToken cancellationToken);
}