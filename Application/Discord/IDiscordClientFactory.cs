namespace KingmakerDiscordBot.Application.Discord;

internal interface IDiscordClientFactory
{
    Task<ISocketClientProxy> CreateAsync(CancellationToken cancellationToken);
}