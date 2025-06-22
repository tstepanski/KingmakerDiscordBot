using System.Collections.Immutable;
using Discord;
using KingmakerDiscordBot.Application.General;

namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal sealed class DescribeCommandWithHandlerFactory(IAbstractEmbedFactory abstractEmbedFactory) : 
    IDescribeCommandWithHandlerFactory
{
    public const string DescribeCommandPrefix = "describe-";

    public IDescribeCommandWithHandler Create<T>() where T : ILookup<T>
    {
        var commandBuilder = new SlashCommandBuilder()
            .WithName($"{DescribeCommandPrefix}{T.TypeCommandName}")
            .WithDescription($"Describes a specific {T.TypePrettyName.ToLower()}")
            .WithContextTypes(InteractionContextType.Guild)
            .WithIntegrationTypes(ApplicationIntegrationType.GuildInstall);

        var allValues = T.GetAll();
        var embedFactory = abstractEmbedFactory.Create<T>();
        IDescribeCommandHandler handler;
        
        if (T.ManyCommandPartition is null)
        {
            (handler, var options) = CreateOptions(T.TypeCommandName, allValues, embedFactory);

            commandBuilder.AddOption(options);
        }
        else
        {
            var pairs = allValues
                .GroupBy(T.ManyCommandPartition.ValueOfFunction)
                .Select(group => CreatePartitionOption(group.Key, group, embedFactory));

            var handlers = new List<SimpleDescribeCommandHandler>();

            foreach (var (partitionHandler, option) in pairs)
            {
                commandBuilder.AddOption(option);
                
                handlers.Add(partitionHandler);
            }

            handler = new PartitionedDescribeCommandHandler(T.TypeCommandName, handlers);
        }

        var command = commandBuilder.Build();

        return new DescribeCommandWithHandler(command, handler);
    }

    private static (SimpleDescribeCommandHandler handler, SlashCommandOptionBuilder optionsBuilder) 
        CreatePartitionOption<T>(string partitionKey, IEnumerable<T> references, Func<T, Embed> embedFactory)
        where T : ILookup<T>
    {
        var commandName = partitionKey.Commandify();

        var subCommand = new SlashCommandOptionBuilder()
            .WithName(commandName)
            .WithDescription($"Describe a {partitionKey.ToLower()} {T.TypePrettyName.ToLower()}")
            .WithType(ApplicationCommandOptionType.SubCommand);

        var (handler, options) = CreateOptions(commandName, references, embedFactory);

        subCommand.AddOption(options);
        
        return (handler, subCommand);
    }

    private static (SimpleDescribeCommandHandler handler, SlashCommandOptionBuilder optionsBuilder)
        CreateOptions<T>(string commandName, IEnumerable<T> references, Func<T, Embed> embedFactory) 
        where T : ILookup<T>
    {
        var options = new SlashCommandOptionBuilder()
            .WithName(T.TypeCommandName)
            .WithDescription($"The {T.TypePrettyName.ToLower()} of which you wish described")
            .WithRequired(true)
            .WithType(ApplicationCommandOptionType.String);

        var responsesBuilder = ImmutableSortedDictionary.CreateBuilder<string, Embed>();

        foreach (var reference in references)
        {
            var response = embedFactory(reference);

            options.AddChoice(reference.Name, reference.Name);
            responsesBuilder.Add(reference.Name, response);
        }

        var responses = responsesBuilder.ToImmutable();
        var handler = new SimpleDescribeCommandHandler(commandName, responses);

        return (handler, options);
    }
}