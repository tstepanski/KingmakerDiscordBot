using System.Reflection;
using System.Text.RegularExpressions;
using Amazon.CDK;
using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.CloudWatch;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.SecretsManager;

namespace KingmakerDiscordBot.CDK;

internal sealed partial class BotStack : Stack
{
    private static readonly Regex TokenRegex = TokenRegexFactory();

    public BotStack(App application, IBucket bucket, IVpc vpc, ISecurityGroup securityGroup, ITableV2 guildsTable,
        Hasher hasher) : base(application, nameof(BotStack))
    {
        var logGroup = CreateLogGroup(this);
        var role = CreateRole(this);
        var tokenSecret = LookupTokenSecret(this);
        var userData = CreateUserData(this, bucket, logGroup, tokenSecret, guildsTable, hasher);
        var launchTemplate = CreateLaunchTemplate(this, role, securityGroup, userData, hasher);

        bucket.GrantRead(role);
        guildsTable.GrantReadWriteData(role);
        tokenSecret.GrantRead(role);

        _ = CreateAutoScalingGroup(this, launchTemplate, vpc);
        _ = CreateHeartbeatAlarm(this);
    }

    private static UserData CreateUserData(BotStack stack, IBucket bucket, LogGroup logGroup, ISecret tokenSecret,
        ITableV2 guildsTable, Hasher hasher)
    {
        var region = stack.GetContextOrThrow(Constants.AwsRegion);
        var location = Assembly.GetExecutingAssembly().Location;
        var directory = Directory.GetParent(location);
        var path = Path.Join(directory!.FullName, "user-data.yml");

        var userData = File.ReadAllText(path);

        userData = TokenRegex.Replace(userData,
            match => GetTokenReplacement(match.Groups[1].Value, bucket, region, tokenSecret, logGroup, guildsTable));

        hasher.AddRaw(userData);

        return UserData.Custom(userData);
    }

    private static ISecret LookupTokenSecret(BotStack botStack)
    {
        var tokenArn = botStack.GetContextOrThrow("DISCORD_TOKEN_ARN");

        return Secret.FromSecretCompleteArn(botStack, nameof(ISecret), tokenArn);
    }

    private static Alarm CreateHeartbeatAlarm(BotStack stack)
    {
        const string name = $"{Constants.HeartbeatMetricName}-{nameof(Alarm)}";

        var metricProperties = new MetricProps
        {
            MetricName = Constants.HeartbeatMetricName,
            Namespace = Constants.HeartbeatNamespace,
            Period = Duration.Seconds(Constants.HeartbeatIntervalInSeconds),
            Statistic = "Sum",
            Unit = Unit.COUNT
        };

        var metric = new Metric(metricProperties);

        var properties = new AlarmProps
        {
            ActionsEnabled = true,
            AlarmDescription = "Verifies application is running normally",
            AlarmName = name,
            ComparisonOperator = ComparisonOperator.LESS_THAN_THRESHOLD,
            DatapointsToAlarm = 2,
            EvaluationPeriods = 2,
            Metric = metric,
            Threshold = 1,
            TreatMissingData = TreatMissingData.BREACHING
        };

        return new Alarm(stack, name, properties);
    }

    private static LogGroup CreateLogGroup(BotStack stack)
    {
        var properties = new LogGroupProps
        {
            LogGroupClass = LogGroupClass.STANDARD,
            LogGroupName = Constants.BotName,
            RemovalPolicy = RemovalPolicy.DESTROY,
            Retention = RetentionDays.THREE_DAYS
        };

        return new LogGroup(stack, nameof(LogGroup), properties);
    }

    private static AutoScalingGroup CreateAutoScalingGroup(BotStack stack, LaunchTemplate launchTemplate,
        IVpc vpc)
    {
        var subnetSelection = new SubnetSelection
        {
            Subnets = vpc.PublicSubnets
        };

        var properties = new AutoScalingGroupProps
        {
            AutoScalingGroupName = Constants.BotName,
            LaunchTemplate = launchTemplate,
            MaxCapacity = 1,
            MinCapacity = 1,
            Vpc = vpc,
            VpcSubnets = subnetSelection
        };

        return new AutoScalingGroup(stack, nameof(AutoScalingGroup), properties);
    }

    private static string GetTokenReplacement(string token, IBucket bucket, string region, ISecret tokenSecret,
        LogGroup logGroup, ITableV2 guildsTable)
    {
        return token switch
        {
            "BUCKET" => bucket.BucketName,
            "DISCORD_TOKEN_ARN" => tokenSecret.SecretFullArn!,
            "GUILD_TABLE_NAME" => guildsTable.TableArn,
            "HEARTBEAT_INSTANCE_DIMENSION_NAME" => Constants.HeartbeatInstanceDimensionName,
            "HEARTBEAT_INTERVAL_IN_SECONDS" => Constants.HeartbeatIntervalInSeconds.ToString(),
            "HEARTBEAT_METRIC_NAME" => Constants.HeartbeatMetricName,
            "HEARTBEAT_NAMESPACE" => Constants.HeartbeatNamespace,
            "LOG_GROUP" => logGroup.LogGroupName,
            "REGION" => region,
            _ => throw new ArgumentOutOfRangeException(nameof(token), token)
        };
    }

    private static LaunchTemplate CreateLaunchTemplate(BotStack stack, Role role, ISecurityGroup securityGroup,
        UserData userData, Hasher hasher)
    {
        var instanceType = InstanceType.Of(InstanceClass.T4G, InstanceSize.SMALL);
        var maximumSpotPriceRaw = stack.GetContextOrThrow(Constants.MaximumSpotPriceKey);
        var imageId = stack.GetContextOrThrow(Constants.ParentImageIdKey);
        var region = stack.GetContextOrThrow(Constants.AwsRegion);
        var maximumSpotPriceParsed = double.Parse(maximumSpotPriceRaw);

        var machineImage = MachineImage.GenericLinux(new Dictionary<string, string>
        {
            { region, imageId }
        });

        var spotOptions = new LaunchTemplateSpotOptions
        {
            InterruptionBehavior = SpotInstanceInterruption.TERMINATE,
            MaxPrice = maximumSpotPriceParsed,
            RequestType = SpotRequestType.ONE_TIME
        };

        var properties = new LaunchTemplateProps
        {
            InstanceType = instanceType,
            LaunchTemplateName = Constants.BotName,
            MachineImage = machineImage,
            RequireImdsv2 = true,
            Role = role,
            SecurityGroup = securityGroup,
            SpotOptions = spotOptions,
            UserData = userData,
            VersionDescription = $"Hash: {hasher}"
        };

        return new LaunchTemplate(stack, nameof(LaunchTemplate), properties);
    }

    private static Role CreateRole(BotStack stack)
    {
        var servicePrincipal = new ServicePrincipal("ec2.amazonaws.com");
        var instanceCorePolicy = ManagedPolicy.FromAwsManagedPolicyName("AmazonSSMManagedInstanceCore");
        var xrayWritePolicy = ManagedPolicy.FromAwsManagedPolicyName("AWSXrayWriteOnlyAccess");
        var cloudWatchAgentServerPolicy = ManagedPolicy.FromAwsManagedPolicyName("CloudWatchAgentServerPolicy");

        var properties = new RoleProps
        {
            AssumedBy = servicePrincipal,
            ManagedPolicies =
            [
                cloudWatchAgentServerPolicy,
                instanceCorePolicy,
                xrayWritePolicy
            ],
            RoleName = Constants.BotName
        };

        return new Role(stack, nameof(Role), properties);
    }

    [GeneratedRegex(@"{{\s*([^\s]+)\s*}}", RegexOptions.Compiled)]
    private static partial Regex TokenRegexFactory();
}