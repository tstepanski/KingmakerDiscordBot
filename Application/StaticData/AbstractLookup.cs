using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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

            if (all.IsEmpty)
            {
                throw new InvalidOperationException($"{typeName} has no discernible values");
            }

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
        if (name is not null)
        {
            return ReferencesByName.Value.TryGetValue(name, out value);
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
    
    public static string TypeCommandName { get; }

    public static string TypePrettyName { get; }
    // ReSharper restore StaticMemberInGenericType
}