using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using KingmakerDiscordBot.Application.Configuration;
using KingmakerDiscordBot.Application.Entities;
using Microsoft.Extensions.Options;

namespace KingmakerDiscordBot.Application.Repositories;

internal sealed class GuildRepository(IAmazonDynamoDB client, IOptions<Tables> configuration) : IGuildRepository
{
    private const string LastUpdatedOnAttributeName = nameof(Guild.CommandsKnownHash);
    private const string IdAttributeName = nameof(Guild.Id);
    private string TableName => configuration.Value.Guild;

    public Task CreateNew(ulong guildId, CancellationToken cancellationToken)
    {
        var request = new PutItemRequest
        {
            Item = new Dictionary<string, AttributeValue>
            {
                [IdAttributeName] = new(guildId.ToString())
            },
            TableName = TableName
        };

        return client.PutItemAsync(request, cancellationToken);
    }

    public async Task<string?> GetKnownCommandsHashAsync(ulong guildId, CancellationToken cancellationToken)
    {
        var key = CreateKey(guildId);

        var request = new GetItemRequest
        {
            Key = key,
            ProjectionExpression = LastUpdatedOnAttributeName,
            TableName = TableName
        };

        var response = await client.GetItemAsync(request, cancellationToken);

        return response.IsItemSet && response.Item.TryGetValue(LastUpdatedOnAttributeName, out var hashCodeAttribute)
            ? hashCodeAttribute.S
            : null;
    }

    public async Task UpdateKnownCommandsHashAsync(ulong guildId, string knownCommandsHash,
        CancellationToken cancellationToken)
    {
        var key = CreateKey(guildId);

        var request = new UpdateItemRequest
        {
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                [":value"] = new(knownCommandsHash)
            },
            Key = key,
            TableName = TableName,
            UpdateExpression = $"SET {LastUpdatedOnAttributeName} = :value"
        };

        await client.UpdateItemAsync(request, cancellationToken);
    }

    private static Dictionary<string, AttributeValue> CreateKey(ulong guildId)
    {
        return new Dictionary<string, AttributeValue>
        {
            [IdAttributeName] = new(guildId.ToString())
        };
    }
}