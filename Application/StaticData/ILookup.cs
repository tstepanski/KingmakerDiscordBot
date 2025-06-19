using System.Diagnostics.CodeAnalysis;
using Discord;

namespace KingmakerDiscordBot.Application.StaticData;

internal interface ILookup<T> : ISourcedInformation<T>
{
    static abstract string TypeCommandName { get; }

    static abstract string TypePrettyName { get; }

    static virtual ManyCommandPartition<T>? ManyCommandPartition { get; } = null;

    static abstract T FromName(string name);

    static abstract IEnumerable<T> GetAll();

    static abstract SlashCommandProperties SetupSlashCommand();

    static abstract bool TryParse(string? name, [NotNullWhen(true)] out T? value);
}