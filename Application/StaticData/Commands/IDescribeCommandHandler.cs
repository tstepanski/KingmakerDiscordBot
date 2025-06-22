using Discord;

namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal interface IDescribeCommandHandler
{
    string CommandType { get; }
    
    Embed GetResponse(IReadOnlyCollection<IApplicationCommandInteractionDataOption> options);
}