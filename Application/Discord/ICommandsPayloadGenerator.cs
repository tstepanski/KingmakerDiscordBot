using Discord;
using KingmakerDiscordBot.Application.StaticData.Commands;

namespace KingmakerDiscordBot.Application.Discord;

internal interface ICommandsPayloadGenerator
{
    string CurrentHashCode { get; }

    IEnumerable<SlashCommandProperties> GetAllCommands();

    IEnumerable<IDescribeCommandHandler> GetAllDescribeCommandHandlers();
}