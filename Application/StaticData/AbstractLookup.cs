using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Discord;
using KingmakerDiscordBot.Application.General;

namespace KingmakerDiscordBot.Application.StaticData;

internal abstract class AbstractLookup<T>(string name, string description, Source source, ushort page) :
    ISourcedInformation<T> where T : AbstractLookup<T>, ILookup<T>
{
    private static readonly Type ThisType = typeof(T);

    // ReSharper disable StaticMemberInGenericType
    private static readonly string TypeName = ThisType.Name;

    private static readonly ImmutableSortedSet<T> All = ThisType
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
        .Where(field => field.IsInitOnly)
        .Where(field => field.FieldType == ThisType)
        .Select(field => field.GetValue(null))
        .Cast<T>()
        .ToImmutableSortedSet();

    private static readonly ImmutableSortedDictionary<string, T> ReferencesByName =
        All.ToImmutableSortedDictionary(reference => reference.Name, reference => reference,
            StringComparer.OrdinalIgnoreCase);
    // ReSharper restore StaticMemberInGenericType

    static AbstractLookup()
    {
        if (All.IsEmpty)
        {
            throw new InvalidOperationException($"{TypeName} has no discernible values");
        }
        
        if (All.Count > 25)
        {
            throw new InvalidOperationException($"{TypeName} has more than 25 values");
        }
    }

    public string Name { get; } = name;

    public string Description { get; } = description;

    public Source Source { get; } = source;

    public ushort Page { get; } = page;

    // ReSharper disable StaticMemberInGenericType
    public static string TypeCommandName { get; } = TypeName.Commandify();

    public static string TypePrettyName { get; } = TypeName.Prettify();
    // ReSharper restore StaticMemberInGenericType

    public static SlashCommandProperties SetupSlashCommand()
    {
        var options = new SlashCommandOptionBuilder()
            .WithName(TypeCommandName)
            .WithDescription($"The {TypePrettyName.ToLower()} of which you wish described")
            .WithRequired(true)
            .WithType(ApplicationCommandOptionType.String);
        
        foreach (var reference in All)
        {
            options.AddChoice(reference.Name, reference.Name);
        }

        return new SlashCommandBuilder()
            .WithName($"describe-{TypeCommandName}")
            .WithDescription($"Describes a specific {TypePrettyName.ToLower()}")
            .WithContextTypes(InteractionContextType.Guild)
            .WithIntegrationTypes(ApplicationIntegrationType.GuildInstall)
            .AddOption(options)
            .Build();
    }

    public bool Equals(T? other)
    {
        return other is not null &&
               ReferenceEquals(this, other);
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
        return All;
    }

    public static bool TryParse(string? name, [NotNullWhen(true)] out T? value)
    {
        if (name is not null)
        {
            return ReferencesByName.TryGetValue(name, out value);
        }

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

    public int CompareTo(T? other)
    {
        return StringComparer.OrdinalIgnoreCase.Compare(Name, other?.Name);
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
}