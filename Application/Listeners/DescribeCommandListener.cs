using System.Collections.Immutable;
using System.Text.Json;
using Discord;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Listeners.Contracts;
using KingmakerDiscordBot.Application.StaticData.Commands;

namespace KingmakerDiscordBot.Application.Listeners;

internal sealed class DescribeCommandListener(ICommandsPayloadGenerator commandsPayloadGenerator, 
    ILogger<DescribeCommandListener> logger) : ISlashCommandExecutedListener
{
    private readonly ImmutableSortedDictionary<string, IDescribeCommandHandler> _describeCommandHandlers =
        commandsPayloadGenerator
            .GetAllDescribeCommandHandlers()
            .ToImmutableSortedDictionary(handler => $"describe-{handler.CommandType}", handler => handler);

    public async Task OnSlashCommandExecuted(IDiscordRestClientProxy _, ISlashCommandInteraction command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var commandName = command.Data.Name;
            var json = JsonSerializer.Serialize(command.Data);

            logger.LogInformation("Received {commandName} command from {guildId}: '{payload}'", commandName,
                command.GuildId, json);

            if (commandName.StartsWith("describe-") is false)
            {
                return;
            }

            var handler = _describeCommandHandlers[commandName];
            var response = handler.GetResponse(command.Data.Options);

            var requestOptions = new RequestOptions
            {
                CancelToken = cancellationToken
            };

            await command.RespondAsync(embed: response, options: requestOptions);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to process command");
        }
    }

    Task ISlashCommandExecutedListener.OnSlashCommandExecuted(IDiscordRestClientProxy client, 
        SocketSlashCommand socketSlashCommand, CancellationToken cancellationToken)
    {
        return OnSlashCommandExecuted(client, socketSlashCommand, cancellationToken);
    }
}