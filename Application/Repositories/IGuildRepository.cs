using KingmakerDiscordBot.Application.Entities;

namespace KingmakerDiscordBot.Application.Repositories;

internal interface IGuildRepository
{
    Task CreateNew(ulong guildId, CancellationToken cancellationToken);
    
    Task<DateTime?> GetCommandsLastUpdatedOnAsync(ulong guildId, CancellationToken cancellationToken);
}