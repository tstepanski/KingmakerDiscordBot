namespace KingmakerDiscordBot.Application.Observability;

public interface IInstanceIdHelper
{
    Task<string> GetIdAsync(CancellationToken cancellationToken);
}