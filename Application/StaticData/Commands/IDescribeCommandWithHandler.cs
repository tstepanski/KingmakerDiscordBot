using Discord;

namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal interface IDescribeCommandWithHandler
{
    SlashCommandProperties SlashCommand { get; }
    
    IDescribeCommandHandler Handler { get; }

    void Deconstruct(out SlashCommandProperties slashCommand, out IDescribeCommandHandler handler);
}