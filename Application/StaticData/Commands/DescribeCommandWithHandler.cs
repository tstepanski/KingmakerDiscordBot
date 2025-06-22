using Discord;

namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal sealed class DescribeCommandWithHandler(SlashCommandProperties slashCommand, IDescribeCommandHandler handler)
    : IDescribeCommandWithHandler
{
    public SlashCommandProperties SlashCommand { get; } = slashCommand;
    public IDescribeCommandHandler Handler { get; } = handler;

    public void Deconstruct(out SlashCommandProperties slashCommand, out IDescribeCommandHandler handler)
    {
        slashCommand = SlashCommand;
        handler = Handler;
    }
}