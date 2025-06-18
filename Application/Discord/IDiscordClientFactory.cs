using Discord.WebSocket;

namespace KingmakerDiscordBot.Application.Discord;

internal interface IDiscordClientFactory
{
    Task<DiscordSocketClient> CreateAsync(CancellationToken cancellationToken);
}