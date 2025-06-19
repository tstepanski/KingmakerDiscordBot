using System.Text.Json;

namespace KingmakerDiscordBot.Application.General;

internal static class SerializationSettings
{
    public static readonly JsonSerializerOptions PrettySettingsInstance = new()
    {
        WriteIndented = true
    };

    public static readonly JsonSerializerOptions CompressSettingsInstance = new()
    {
        WriteIndented = false,
        Converters =
        {
            new OptionalJsonConverterFactory()
        }
    };
}