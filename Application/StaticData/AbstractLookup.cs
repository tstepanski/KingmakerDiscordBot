using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace KingmakerDiscordBot.Application.StaticData;

internal abstract class AbstractLookup<T>(string name, string description, Source source, ushort page) : 
    ISourcedInformation<T> where T : AbstractLookup<T>, ILookup<T>
{
    private static readonly ImmutableSortedDictionary<string, T> ReferencesByName =
        T.GetAll().ToImmutableSortedDictionary(reference => reference.Name, reference => reference,
            StringComparer.OrdinalIgnoreCase);

    public string Name { get; } = name;

    public string Description { get; } = description;

    public Source Source { get; } = source;

    public ushort Page { get; } = page;

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