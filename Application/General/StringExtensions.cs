using System.Text.RegularExpressions;

namespace KingmakerDiscordBot.Application.General;

internal static partial class StringExtensions
{
    private static readonly Regex NamePrettifier = NamePrettifierFactory();

    // ReSharper disable once IdentifierTypo
    public static string Commandify(this string value)
    {
        return NamePrettifier.Replace(value, "-$1").ToLower();
    }

    public static string Prettify(this string value)
    {
        return NamePrettifier.Replace(value, " $1");
    }

    [GeneratedRegex("(?<!^)([A-Z])", RegexOptions.Compiled)]
    private static partial Regex NamePrettifierFactory();
}