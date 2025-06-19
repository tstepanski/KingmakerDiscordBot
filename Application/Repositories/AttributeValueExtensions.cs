using Amazon.DynamoDBv2.Model;

namespace KingmakerDiscordBot.Application.Repositories;

public static class AttributeValueExtensions
{
    public static DateTime DateTime(this AttributeValue attributeValue)
    {
        return System.DateTime.Parse(attributeValue.S);
    }

    public static DateTime? DateTimeOrDefault(this Dictionary<string, AttributeValue> attributeValues,
        string attributeName)
    {
        if (attributeValues.TryGetValue(attributeName, out var attributeValue))
        {
            return attributeValue.DateTime();
        }

        return null;
    }
}