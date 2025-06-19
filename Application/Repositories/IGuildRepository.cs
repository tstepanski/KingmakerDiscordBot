namespace KingmakerDiscordBot.Application.Repositories;

internal interface IGuildRepository
{
    Task CreateNew(ulong guildId, CancellationToken cancellationToken);
    
    Task<string?> GetKnownCommandsHashAsync(ulong guildId, CancellationToken cancellationToken);

    Task UpdateKnownCommandsHashAsync(ulong guildId, string knownCommandsHash, CancellationToken cancellationToken);
}