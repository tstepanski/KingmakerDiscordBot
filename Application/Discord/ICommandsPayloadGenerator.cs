using Discord;

namespace KingmakerDiscordBot.Application.Discord;

internal interface ICommandsPayloadGenerator
{
    string CurrentHashCode { get; }

    IEnumerable<SlashCommandProperties> GetAllCommands();
}