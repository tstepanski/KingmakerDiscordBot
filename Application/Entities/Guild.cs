namespace KingmakerDiscordBot.Application.Entities;

internal sealed class Guild
{
    public string Id { get; set; } = string.Empty;

    public string CommandsKnownHash { get; set; } = string.Empty;
}