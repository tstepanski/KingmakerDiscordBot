using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildStickerCreatedListener : IListener
{
    Task OnGuildStickerCreated(IDiscordRestClientProxy client, SocketCustomSticker socketCustomSticker,
        CancellationToken cancellationToken);
}