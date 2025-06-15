using Discord;

namespace KingmakerDiscordBot.Application.Logic;

internal interface IDiscordClientFactory
{
    Task<IDiscordClient> CreateAsync(CancellationToken cancellationToken);
}