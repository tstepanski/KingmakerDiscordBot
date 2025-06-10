namespace KingmakerDiscordBot.Application.Configuration;

internal sealed class Aws
{
    public string LogGroup { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;
}