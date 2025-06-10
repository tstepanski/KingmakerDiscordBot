namespace KingmakerDiscordBot.CDK;

internal static class Constants
{
    public const string ArtifactPathKey = "ARTIFACT_PATH";

    public const string AwsRegion = "AWS_REGION";

    public const int HeartbeatIntervalInSeconds = 60;

    public const string HeartbeatInstanceDimensionName = "InstanceId";

    public const string HeartbeatMetricName = "StatusCheckFailed_Instance";

    public const string HeartbeatNamespace = "AWS/EC2";
    
    public const string ParentImageIdKey = "AWS_LINUX_AMI_ID";
    
    public const string BotName = "kingmaker-discord-bot";

    public const string MaximumSpotPriceKey = "MAX_SPOT_PRICE";
    
    public const string VirtualPrivateCloudClasslessInterDomainRoutingBlock = "VPC_CIDR";
}