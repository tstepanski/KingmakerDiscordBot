using System.Collections.Immutable;
using Discord;

namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal sealed class PartitionedDescribeCommandHandler(string commandType,
    IEnumerable<SimpleDescribeCommandHandler> partitionHandlers) : IDescribeCommandHandler
{
    private readonly ImmutableSortedDictionary<string, SimpleDescribeCommandHandler> _partitionHandlers =
        partitionHandlers.ToImmutableSortedDictionary(handler => handler.CommandType, handler => handler);

    public string CommandType { get; } = commandType;
    
    public Embed GetResponse(IReadOnlyCollection<IApplicationCommandInteractionDataOption> options)
    {
        try
        {
            var partitionSelection = options.Single();
            var partitionHandler = _partitionHandlers[partitionSelection.Name];

            return partitionHandler.GetResponse(partitionSelection.Options);
        }
        catch (InvalidOperationException exception)
        {
            throw new KeyNotFoundException($"Could not determine sub-type of {CommandType}", exception);
        }
    }
}