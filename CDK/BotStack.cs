using System.Reflection;
using System.Text.RegularExpressions;
using Amazon.CDK;
using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.CloudWatch;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.SecretsManager;

namespace KingmakerDiscordBot.CDK;

internal sealed partial class BotStack : Stack
{
    private static readonly Regex TokenRegex = TokenRegexFactory();

    public BotStack(App application, IBucket bucket) : base(application, nameof(BotStack))
    {
        var vpc = CreateVpc(this);
        var securityGroup = CreateSecurityGroup(this, vpc);
        var logGroup = CreateLogGroup(this);
        var role = CreateRole(this);
        var tokenSecret = LookupTokenSecret(this);

        logGroup.GrantWrite(role);
        tokenSecret.GrantRead(role);

        _ = CreateAutoScalingGroup(this, bucket, role, securityGroup, vpc, tokenSecret, logGroup);
        _ = CreateHeartbeatAlarm(this);
    }

    private static ISecret LookupTokenSecret(BotStack botStack)
    {
        var tokenArn = botStack.GetContextOrThrow("DISCORD_TOKEN_ARN");

        return Secret.FromSecretCompleteArn(botStack, nameof(ISecret), tokenArn);
    }

    private static PolicyStatement CreateCloudwatchPolicyStatement()
    {
        var properties = new PolicyStatementProps
        {
            Actions = ["cloudwatch:PutMetricData"],
            Conditions = new Dictionary<string, object>
            {
                {
                    "StringEquals", new Dictionary<string, object>
                    {
                        { "cloudwatch:metricName", Constants.HeartbeatMetricName },
                        { "cloudwatch:namespace", Constants.HeartbeatNamespace }
                    }
                }
            },
            Effect = Effect.ALLOW,
            Resources = ["*"]
        };

        return new PolicyStatement(properties);
    }

    private static Alarm CreateHeartbeatAlarm(BotStack stack)
    {
        const string name = $"{Constants.HeartbeatMetricName}-{nameof(Alarm)}";
        
        var metricProperties = new MetricProps
        {
            MetricName = Constants.HeartbeatMetricName,
            Namespace = Constants.HeartbeatNamespace,
            Period = Duration.Seconds(Constants.HeartbeatIntervalInSeconds),
            Statistic = "Minimum"
        };

        var metric = new Metric(metricProperties);

        var properties = new AlarmProps
        {
            ActionsEnabled = true,
            AlarmDescription = "Verifies application is running normally",
            AlarmName = name,
            ComparisonOperator = ComparisonOperator.GREATER_THAN_OR_EQUAL_TO_THRESHOLD,
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

    private static AutoScalingGroup CreateAutoScalingGroup(BotStack stack, IBucket bucket, Role role,
        SecurityGroup securityGroup, Vpc vpc, ISecret tokenSecret, LogGroup logGroup)
    {
        var imageId = stack.GetContextOrThrow(Constants.ParentImageIdKey);
        var region = stack.GetContextOrThrow(Constants.AwsRegion);
        var userDataContent = GetBase64EncodedUserData(stack, bucket, tokenSecret, logGroup);
        var userData = UserData.Custom(userDataContent);
        var instanceType = InstanceType.Of(InstanceClass.T4G, InstanceSize.SMALL);
        var maximumSpotPrice = stack.GetContextOrThrow(Constants.MaximumSpotPriceKey);

        var machineImage = MachineImage.GenericLinux(new Dictionary<string, string>
        {
            { region, imageId }
        });

        var subnetSelection = new SubnetSelection
        {
            Subnets = vpc.PublicSubnets
        };
        
        var properties = new AutoScalingGroupProps
        {
            InstanceType = instanceType,
            MachineImage = machineImage,
            MaxCapacity = 1,
            MinCapacity = 1,
            RequireImdsv2 = true,
            Role = role,
            SecurityGroup = securityGroup,
            SpotPrice = maximumSpotPrice,
            UserData = userData,
            Vpc = vpc,
            VpcSubnets = subnetSelection
        };
        
        return new AutoScalingGroup(stack, nameof(AutoScalingGroup), properties);
    }

    private static string GetBase64EncodedUserData(BotStack stack, IBucket bucket, ISecret tokenSecret, 
        ILogGroup logGroup)
    {
        var region = stack.GetContextOrThrow(Constants.AwsRegion);
        var location = Assembly.GetExecutingAssembly().Location;
        var directory = Directory.GetParent(location);
        var path = Path.Join(directory!.FullName, "user-data.yml");

        var userData = File.ReadAllText(path);

        userData = TokenRegex.Replace(userData, 
            match => GetTokenReplacement(match.Groups[1].Value, bucket, region, tokenSecret, logGroup));

        return Fn.Base64(userData);
    }

    private static string GetTokenReplacement(string token, IBucket bucket, string region, ISecret tokenSecret,
        ILogGroup logGroup)
    {
        return token switch
        {
            "BUCKET" => bucket.BucketName,
            "DISCORD_TOKEN_ARN" => tokenSecret.SecretFullArn!,
            "HEARTBEAT_INSTANCE_DIMENSION_NAME" => Constants.HeartbeatInstanceDimensionName,
            "HEARTBEAT_INTERVAL_IN_SECONDS" => Constants.HeartbeatIntervalInSeconds.ToString(),
            "HEARTBEAT_METRIC_NAME" => Constants.HeartbeatMetricName,
            "HEARTBEAT_NAMESPACE" => Constants.HeartbeatNamespace,
            "LOG_GROUP" => logGroup.LogGroupName,
            "REGION" => region,
            _ => throw new ArgumentOutOfRangeException(nameof(token), token)
        };
    }

    private static Role CreateRole(BotStack stack)
    {
        var servicePrincipal = new ServicePrincipal("ec2.amazonaws.com");
        var instanceCorePolicy = ManagedPolicy.FromAwsManagedPolicyName("AmazonSSMManagedInstanceCore");
        var xrayWritePolicy = ManagedPolicy.FromAwsManagedPolicyName("AWSXrayWriteOnlyAccess");
        var cloudwatchPolicyStatement = CreateCloudwatchPolicyStatement();

        var properties = new RoleProps
        {
            AssumedBy = servicePrincipal,
            ManagedPolicies =
            [
                instanceCorePolicy,
                xrayWritePolicy
            ],
            RoleName = Constants.BotName
        };

        var role = new Role(stack, nameof(Role), properties);

        role.AddToPolicy(cloudwatchPolicyStatement);

        return role;
    }

    private static SecurityGroup CreateSecurityGroup(BotStack stack, Vpc vpc)
    {
        var properties = new SecurityGroupProps
        {
            AllowAllOutbound = true,
            Description = "Allow All Output, SSH In Only",
            SecurityGroupName = Constants.BotName,
            Vpc = vpc
        };

        var securityGroup = new SecurityGroup(vpc, nameof(SecurityGroup), properties);
        var developerIp = stack.GetContextOrThrow("DEV_IP");

        securityGroup.AddIngressRule(Peer.Ipv4(developerIp), Port.SSH, "Allow SSH for Development");

        return securityGroup;
    }

    private static Vpc CreateVpc(BotStack stack)
    {
        var cidr = stack.GetContextOrThrow(Constants.VirtualPrivateCloudClasslessInterDomainRoutingBlock);

        var subnetConfiguration = new SubnetConfiguration
        {
            Name = "Public",
            CidrMask = 24,
            SubnetType = SubnetType.PUBLIC
        };

        var properties = new VpcProps
        {
            IpAddresses = IpAddresses.Cidr(cidr),
            MaxAzs = 1,
            NatGateways = 0,
            SubnetConfiguration = [subnetConfiguration],
            VpcName = Constants.BotName,
        };

        return new Vpc(stack, nameof(Vpc), properties);
    }

    [GeneratedRegex(@"{{\s*([^\s]+)\s*}}", RegexOptions.Compiled)]
    private static partial Regex TokenRegexFactory();
}