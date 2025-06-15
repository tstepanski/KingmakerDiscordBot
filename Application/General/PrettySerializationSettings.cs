using System.Text.Json;

namespace KingmakerDiscordBot.Application.General;

public static class PrettySerializationSettings
{
    public static readonly JsonSerializerOptions Instance = new()
    {
        WriteIndented = true
    };
}