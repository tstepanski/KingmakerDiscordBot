using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Discord;
using KingmakerDiscordBot.Application.General;

namespace KingmakerDiscordBot.Application.StaticData;

internal abstract class AbstractLookup<T>(string name, string description, Source source, ushort page) :
    ISourcedInformation<T> where T : AbstractLookup<T>, ILookup<T>
{
    static AbstractLookup()
    {
        var thisType = typeof(T);
        var typeName = thisType.Name;

        All = new Lazy<ImmutableSortedSet<T>>(() =>
        {
            var all = thisType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(field => field.IsInitOnly)
                .Where(field => field.FieldType == thisType)
                .Select(field => field.GetValue(null))
                .Cast<T>()
                .ToImmutableSortedSet();

            if (all.IsEmpty) throw new InvalidOperationException($"{typeName} has no discernible values");

            return all;
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        ReferencesByName = new Lazy<ImmutableSortedDictionary<string, T>>(() =>
            All.Value.ToImmutableSortedDictionary(reference => reference.Name, reference => reference,
                StringComparer.OrdinalIgnoreCase), LazyThreadSafetyMode.ExecutionAndPublication);

        TypeCommandName = typeName.Commandify();
        TypePrettyName = typeName.Prettify();
    }

    public string Name { get; } = name;

    public string Description { get; } = description;

    public Source Source { get; } = source;

    public ushort Page { get; } = page;

    public bool Equals(T? other)
    {
        return other is not null &&
               ReferenceEquals(this, other);
    }

    public int CompareTo(T? other)
    {
        return StringComparer.OrdinalIgnoreCase.Compare(Name, other?.Name);
    }

    public static SlashCommandProperties SetupSlashCommand()
    {
        var commandBuilder = new SlashCommandBuilder()
            .WithName($"describe-{TypeCommandName}")
            .WithDescription($"Describes a specific {TypePrettyName.ToLower()}")
            .WithContextTypes(InteractionContextType.Guild)
            .WithIntegrationTypes(ApplicationIntegrationType.GuildInstall);

        if (T.ManyCommandPartition is null)
        {
            var options = CreateOptions(All.Value);

            commandBuilder.AddOption(options);
        }
        else
        {
            var options = All
                .Value
                .GroupBy(T.ManyCommandPartition.ValueOfFunction)
                .Select(group => CreatePartitionOption(group.Key, group));

            foreach (var option in options) commandBuilder.AddOption(option);
        }

        return commandBuilder.Build();
    }

    private static SlashCommandOptionBuilder CreatePartitionOption(string partitionKey,
        IEnumerable<T> references)
    {
        var commandName = partitionKey.Commandify();

        var subCommand = new SlashCommandOptionBuilder()
            .WithName(commandName)
            .WithDescription($"Describe a {partitionKey.ToLower()} {TypePrettyName.ToLower()}")
            .WithType(ApplicationCommandOptionType.SubCommand);

        var options = CreateOptions(references);

        subCommand.AddOption(options);

        return subCommand;
    }

    private static SlashCommandOptionBuilder CreateOptions(IEnumerable<T> references)
    {
        var options = new SlashCommandOptionBuilder()
            .WithName(TypeCommandName)
            .WithDescription($"The {TypePrettyName.ToLower()} of which you wish described")
            .WithRequired(true)
            .WithType(ApplicationCommandOptionType.String);

        foreach (var reference in references) options.AddChoice(reference.Name, reference.Name);

        return options;
    }

    public static T FromName(string name)
    {
        var result = TryParse(name, out var value);

        return result
            ? value!
            : throw new ArgumentOutOfRangeException(nameof(name), name);
    }

    public static IEnumerable<T> GetAll()
    {
        return All.Value;
    }

    public static bool TryParse(string? name, [NotNullWhen(true)] out T? value)
    {
        if (name is not null) return ReferencesByName.Value.TryGetValue(name, out value);

        value = null;

        return false;
    }

    public sealed override string ToString()
    {
        return Name;
    }

    public sealed override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public sealed override bool Equals(object? other)
    {
        return Equals(other as T);
    }

    public static bool operator ==(AbstractLookup<T>? left, AbstractLookup<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AbstractLookup<T>? left, AbstractLookup<T>? right)
    {
        return !Equals(left, right);
    }

    // ReSharper disable StaticMemberInGenericType
    private static readonly Lazy<ImmutableSortedSet<T>> All;

    private static readonly Lazy<ImmutableSortedDictionary<string, T>> ReferencesByName;
    // ReSharper restore StaticMemberInGenericType

    // ReSharper disable StaticMemberInGenericType
    public static string TypeCommandName { get; }

    public static string TypePrettyName { get; }
    // ReSharper restore StaticMemberInGenericType
}