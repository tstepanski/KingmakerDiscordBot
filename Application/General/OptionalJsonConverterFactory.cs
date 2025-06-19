using System.Text.Json;
using System.Text.Json.Serialization;
using Discord;

namespace KingmakerDiscordBot.Application.General;

internal sealed class OptionalJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType &&
               typeToConvert.GetGenericTypeDefinition() == typeof(Optional<>);
    }

    public override JsonConverter? CreateConverter(Type type, JsonSerializerOptions options)
    {
        var elementType = type.GetGenericArguments()[0];
        var converterType = typeof(OptionalJsonConverter<>).MakeGenericType(elementType);

        return (JsonConverter?)Activator.CreateInstance(converterType);
    }
}