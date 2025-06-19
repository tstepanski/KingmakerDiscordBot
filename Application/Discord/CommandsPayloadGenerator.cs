using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text.Json;
using Discord;
using KingmakerDiscordBot.Application.StaticData;

namespace KingmakerDiscordBot.Application.Discord;

internal sealed class CommandsPayloadGenerator : ICommandsPayloadGenerator
{
    private static readonly ImmutableArray<SlashCommandProperties> Commands =
    [
        ..CalculateAllCommands().OrderBy(command => command.Name)
    ];

    public CommandsPayloadGenerator()
    {
        using var memory = new MemoryStream();

        JsonSerializer.Serialize(memory, Commands);

        memory.Position = 0;

        var hashBytes = SHA256.HashData(memory);

        CurrentHashCode = Convert.ToBase64String(hashBytes);
    }

    public string CurrentHashCode { get; }

    public IEnumerable<SlashCommandProperties> GetAllCommands()
    {
        return Commands;
    }

    private static IEnumerable<SlashCommandProperties> CalculateAllCommands()
    {
        yield return Ability.SetupSlashCommand();
        yield return Charter.SetupSlashCommand();
        yield return Feat.SetupSlashCommand();
        yield return Government.SetupSlashCommand();
        yield return Heartland.SetupSlashCommand();
        yield return Skill.SetupSlashCommand();
        yield return SkillAction.SetupSlashCommand();
        yield return Trait.SetupSlashCommand();
    }
}