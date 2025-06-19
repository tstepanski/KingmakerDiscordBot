using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Attribute = Amazon.CDK.AWS.DynamoDB.Attribute;

namespace KingmakerDiscordBot.CDK;

internal sealed class DatabaseStack : Stack
{
    public DatabaseStack(App application) : base(application, nameof(DatabaseStack))
    {
        var partitionKey = new Attribute
        {
            Name = "Id",
            Type = AttributeType.STRING
        };
        
        var maxThroughput = new MaxThroughputProps
        {
            MaxReadRequestUnits = 50,
            MaxWriteRequestUnits = 10
        };

        var billing = Billing.OnDemand(maxThroughput);
        
        var properties = new TablePropsV2
        {
            Billing = billing,
            PartitionKey = partitionKey,
            RemovalPolicy = RemovalPolicy.RETAIN,
            TableClass = TableClass.STANDARD,
            TableName = Constants.BotName
        };

        Table = new TableV2(this, nameof(TableV2), properties);
    }

    public ITableV2 Table { get; }
}