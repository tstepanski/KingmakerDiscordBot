namespace KingmakerDiscordBot.Application.Observability;

internal interface IInstanceIdHelper
{
    Task<string> GetIdAsync(CancellationToken cancellationToken);
}