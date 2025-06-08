using System.Reflection;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.S3;

namespace KingmakerDiscordBot.CDK;

internal sealed class BotStack : Stack
{
    public BotStack(App application, IBucket bucket) : base(application, nameof(BotStack))
    {
        var vpc = CreateVpc(this);
        var securityGroup = CreateSecurityGroup(vpc);
        var role = CreateRole(this);
        var launchTemplate = CreateLaunchTemplate(this, role, securityGroup);

        _ = CreateInstance(this, launchTemplate, vpc, bucket);
    }

    private static CfnInstance CreateInstance(BotStack instance, LaunchTemplate launchTemplate, Vpc vpc, IBucket bucket)
    {
        var launchTemplateSpecificationProperty = new CfnInstance.LaunchTemplateSpecificationProperty
        {
            LaunchTemplateId = launchTemplate.LaunchTemplateId,
            Version = launchTemplate.LatestVersionNumber
        };

        var imageId = instance.GetContextOrThrow(Constants.ParentImageIdKey);
        var userData = GetBase64EncodedUserData(bucket);

        var properties = new CfnInstanceProps
        {
            ImageId = imageId,
            LaunchTemplate = launchTemplateSpecificationProperty,
            SubnetId = vpc.PublicSubnets[0].SubnetId,
            UserData = userData
        };

        return new CfnInstance(instance, nameof(CfnInstance), properties);
    }

    private static string GetBase64EncodedUserData(IBucket bucket)
    {
        var location = Assembly.GetExecutingAssembly().Location;
        var directory = Directory.GetParent(location);
        var path = Path.Join(directory!.FullName, "user-data.yml");
        var userData = File.ReadAllText(path).Replace("{{ BUCKET }}", bucket.BucketName);
        
        return Fn.Base64(userData);
    }

    private static LaunchTemplate CreateLaunchTemplate(BotStack instance, Role role, SecurityGroup securityGroup)
    {
        var instanceType = InstanceType.Of(InstanceClass.T4G, InstanceSize.SMALL);
        var maximumSpotPriceRaw = instance.GetContextOrThrow(Constants.MaximumSpotPriceKey);
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

        return new LaunchTemplate(instance, nameof(LaunchTemplate), properties);
    }

    private static Role CreateRole(BotStack instance)
    {
        var servicePrincipal = new ServicePrincipal("ec2.amazonaws.com");
        var instanceCorePolicy = ManagedPolicy.FromAwsManagedPolicyName("AmazonSSMManagedInstanceCore");

        var properties = new RoleProps
        {
            AssumedBy = servicePrincipal,
            ManagedPolicies = [instanceCorePolicy],
            RoleName = Constants.BotName
        };

        return new Role(instance, nameof(Role), properties);
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

    private static Vpc CreateVpc(BotStack instance)
    {
        var cidr = instance.GetContextOrThrow(Constants.VirtualPrivateCloudClasslessInterDomainRoutingBlock);

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

        return new Vpc(instance, nameof(Vpc), properties);
    }
}