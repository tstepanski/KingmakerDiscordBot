namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class ManyCommandPartition<T>
{
    public required string Name { get; init; }

    public required Func<T, string> ValueOfFunction { get; init; }

    public void Deconstruct(out string name, out Func<T, string> valueOfFunction)
    {
        name = Name;
        valueOfFunction = ValueOfFunction;
    }
}