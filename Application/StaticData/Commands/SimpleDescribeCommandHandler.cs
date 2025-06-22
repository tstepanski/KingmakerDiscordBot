using Discord;
using Responses = System.Collections.Immutable.IImmutableDictionary<string, Discord.Embed>;

namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal sealed class SimpleDescribeCommandHandler(string commandType, Responses responsesByName) : 
    IDescribeCommandHandler
{
    public string CommandType { get; } = commandType;

    public Embed GetResponse(IReadOnlyCollection<IApplicationCommandInteractionDataOption> options)
    {
        try
        {
            var selection = options
                .Single()
                .Value
                .ToString()!;

            return responsesByName[selection];
        }
        catch (InvalidOperationException exception)
        {
            throw new KeyNotFoundException($"Could not determine type of {CommandType}", exception);
        }
    }
}