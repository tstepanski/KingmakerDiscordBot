using System.Reflection;
using System.Text.RegularExpressions;
using Amazon.CDK;
using Amazon.CDK.AWS.CloudWatch;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.S3;

namespace KingmakerDiscordBot.CDK;

internal sealed partial class BotStack : Stack
{
    private static readonly Regex TokenRegex = TokenRegexFactory();

    public BotStack(App application, IBucket bucket) : base(application, nameof(BotStack))
    {
        var vpc = CreateVpc(this);
        var securityGroup = CreateSecurityGroup(vpc);
        var logGroup = CreateLogGroup(this);
        var role = CreateRole(this);
        var launchTemplate = CreateLaunchTemplate(this, role, securityGroup);

        logGroup.GrantWrite(role);

        var instance = CreateInstance(this, launchTemplate, vpc, bucket);

        _ = CreateHeartbeatAlarm(this, instance);
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

    private static Alarm CreateHeartbeatAlarm(BotStack stack, CfnInstance instance)
    {
        const string name = $"{Constants.HeartbeatMetricName}-{nameof(Alarm)}";

        var metricProperties = new MetricProps
        {
            DimensionsMap = new Dictionary<string, string>
            {
                { "InstanceId", instance.Ref }
            },
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
            TreatMissingData = TreatMissingData.NOT_BREACHING
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

    private static CfnInstance CreateInstance(BotStack stack, LaunchTemplate launchTemplate, Vpc vpc, IBucket bucket)
    {
        var launchTemplateSpecificationProperty = new CfnInstance.LaunchTemplateSpecificationProperty
        {
            LaunchTemplateId = launchTemplate.LaunchTemplateId,
            Version = launchTemplate.LatestVersionNumber
        };

        var imageId = stack.GetContextOrThrow(Constants.ParentImageIdKey);
        var userData = GetBase64EncodedUserData(bucket);

        var properties = new CfnInstanceProps
        {
            ImageId = imageId,
            LaunchTemplate = launchTemplateSpecificationProperty,
            SubnetId = vpc.PublicSubnets[0].SubnetId,
            UserData = userData
        };

        return new CfnInstance(stack, nameof(CfnInstance), properties);
    }

    private static string GetBase64EncodedUserData(IBucket bucket)
    {
        var location = Assembly.GetExecutingAssembly().Location;
        var directory = Directory.GetParent(location);
        var path = Path.Join(directory!.FullName, "user-data.yml");

        var userData = File.ReadAllText(path);

        userData = TokenRegex.Replace(userData, match => GetTokenReplacement(match.Groups[1].Value, bucket));

        return Fn.Base64(userData);
    }

    private static string GetTokenReplacement(string token, IBucket bucket)
    {
        return token switch
        {
            "BUCKET" => bucket.BucketName,
            "HEARTBEAT_INTERVAL_IN_SECONDS" => Constants.HeartbeatIntervalInSeconds.ToString(),
            "HEARTBEAT_METRIC_NAME" => Constants.HeartbeatMetricName,
            "HEARTBEAT_NAMESPACE" => Constants.HeartbeatNamespace,
            "LOG_GROUP" => Constants.BotName,
            "REGION" => Aws.REGION,
            _ => throw new ArgumentOutOfRangeException(nameof(token), token)
        };
    }

    private static LaunchTemplate CreateLaunchTemplate(BotStack stack, Role role, SecurityGroup securityGroup)
    {
        var instanceType = InstanceType.Of(InstanceClass.T4G, InstanceSize.SMALL);
        var maximumSpotPriceRaw = stack.GetContextOrThrow(Constants.MaximumSpotPriceKey);
        var maximumSpotPriceParsed = double.Parse(maximumSpotPriceRaw);

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
            RequireImdsv2 = true,
            Role = role,
            SecurityGroup = securityGroup,
            SpotOptions = spotOptions
        };

        return new LaunchTemplate(stack, nameof(LaunchTemplate), properties);
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

    private static SecurityGroup CreateSecurityGroup(Vpc vpc)
    {
        var properties = new SecurityGroupProps
        {
            AllowAllOutbound = true,
            Description = "Allow All Output, SSH In Only",
            SecurityGroupName = Constants.BotName,
            Vpc = vpc
        };

        var securityGroup = new SecurityGroup(vpc, nameof(SecurityGroup), properties);
        var developerIp = securityGroup.GetContextOrThrow("DEV_IP");

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