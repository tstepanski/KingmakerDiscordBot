using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text.Json;
using Discord;
using KingmakerDiscordBot.Application.General;
using KingmakerDiscordBot.Application.StaticData;
using KingmakerDiscordBot.Application.StaticData.Commands;

namespace KingmakerDiscordBot.Application.Discord;

internal sealed class CommandsPayloadGenerator : ICommandsPayloadGenerator
{
    private readonly ImmutableArray<SlashCommandProperties> _commands;
    private readonly ImmutableArray<IDescribeCommandHandler> _describeCommandHandlers;

    public CommandsPayloadGenerator(IDescribeCommandWithHandlerFactory describeCommandWithHandlerFactory)
    {
        var commandsList = new List<SlashCommandProperties>();
        var describeCommandHandlers = new List<IDescribeCommandHandler>();
        
        Add<Ability>();
        Add<Charter>();
        Add<Feat>();
        Add<Government>();
        Add<Heartland>();
        Add<Skill>();
        Add<SkillAction>();
        Add<Trait>();

        _commands = [..commandsList];
        _describeCommandHandlers = [..describeCommandHandlers];

        using var memory = new MemoryStream();

        JsonSerializer.Serialize(memory, _commands, SerializationSettings.CompressSettingsInstance);

        memory.Position = 0;

        var hashBytes = SHA256.HashData(memory);

        CurrentHashCode = Convert.ToBase64String(hashBytes);

        return;

        void Add<T>() where T : ILookup<T>
        {
            var (command, handler) = describeCommandWithHandlerFactory.Create<T>();
        
            commandsList.Add(command);
            describeCommandHandlers.Add(handler);
        }
    }

    public string CurrentHashCode { get; }

    public IEnumerable<SlashCommandProperties> GetAllCommands()
    {
        return _commands;
    }

    public IEnumerable<IDescribeCommandHandler> GetAllDescribeCommandHandlers()
    {
        return _describeCommandHandlers;
    }
}