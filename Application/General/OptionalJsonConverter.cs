using System.Text.Json;
using System.Text.Json.Serialization;
using Discord;

namespace KingmakerDiscordBot.Application.General;

internal sealed class OptionalJsonConverter<T> : JsonConverter<Optional<T>>
{
    public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = JsonSerializer.Deserialize<T>(ref reader, options)!;

        return new Optional<T>(value);
    }

    public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
    {
        if (value.IsSpecified)
            JsonSerializer.Serialize(writer, value.Value, options);
        else
            writer.WriteNullValue();
    }
}