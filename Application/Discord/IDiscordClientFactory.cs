using Discord;

namespace KingmakerDiscordBot.Application.Discord;

internal interface IDiscordClientFactory
{
    Task<IDiscordClient> CreateAsync(CancellationToken cancellationToken);
}