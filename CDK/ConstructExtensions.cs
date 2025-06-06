using Constructs;

namespace KingmakerDiscordBot.CDK;

public static class ConstructExtensions
{
    public static string GetContextOrThrow(this Construct construct, string key)
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        var value = construct.Node.TryGetContext(key)?.ToString();

        ArgumentException.ThrowIfNullOrWhiteSpace(value, key);

        return value;
    }
}