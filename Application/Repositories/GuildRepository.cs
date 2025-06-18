using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using KingmakerDiscordBot.Application.Configuration;
using KingmakerDiscordBot.Application.Entities;
using Microsoft.Extensions.Options;

namespace KingmakerDiscordBot.Application.Repositories;

internal sealed class GuildRepository(IAmazonDynamoDB client, IOptions<Tables> configuration) : IGuildRepository
{
    private string TableName => configuration.Value.Guild;
    
    public Task CreateNew(ulong guildId, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow.ToString("O");
        
        var request = new PutItemRequest
        {
            Item = new Dictionary<string, AttributeValue>
            {
                [nameof(Guild.Id)] = new(guildId.ToString()),
                [nameof(Guild.CommandsLastUpdatedOn)] = new(now)
            },
            TableName = TableName
        };
        
        return client.PutItemAsync(request, cancellationToken);
    }

    public async Task<DateTime?> GetCommandsLastUpdatedOnAsync(ulong guildId, CancellationToken cancellationToken)
    {
        const string attributeName = nameof(Guild.CommandsLastUpdatedOn);
        
        var request = new GetItemRequest
        {
            Key = new Dictionary<string, AttributeValue>
            {
                [nameof(Guild.Id)] = new(guildId.ToString())
            },
            ProjectionExpression = attributeName,
            TableName = TableName
        };

        var response = await client.GetItemAsync(request, cancellationToken);

        return response.IsItemSet
            ? response.Item.DateTimeOrDefault(attributeName)
            : null;
    }
}