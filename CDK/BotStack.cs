using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;

namespace KingmakerDiscordBot.CDK;

public sealed class BotStack : Stack
{
    private const string BotName = "KingmakerDiscordBot";
    
    public BotStack(App application) : base(application, nameof(BotStack))
    {
        var vpc = CreateVpc(this);
        var securityGroup = CreateSecurityGroup(vpc);
        var machineImage = GetMachineImage(this);
        var role = CreateRole(this);
        var launchTemplate = CreateLaunchTemplate(this, machineImage, role, securityGroup);

        _ = CreateInstance(this, launchTemplate, vpc);
    }

    private static CfnInstance CreateInstance(BotStack stack, LaunchTemplate launchTemplate, Vpc vpc)
    {
        var launchTemplateSpecificationProperty = new CfnInstance.LaunchTemplateSpecificationProperty
        {
            LaunchTemplateId = launchTemplate.LaunchTemplateId,
            Version = launchTemplate.LatestVersionNumber
        };
        
        var properties = new CfnInstanceProps
        {
            LaunchTemplate = launchTemplateSpecificationProperty,
            SubnetId = vpc.PublicSubnets[0].SubnetId
        };

        return new CfnInstance(stack, nameof(CfnInstance), properties);
    }

    private static LaunchTemplate CreateLaunchTemplate(BotStack instance, IMachineImage machineImage, Role role,
        SecurityGroup securityGroup)
    {
        var instanceType = InstanceType.Of(InstanceClass.T4G, InstanceSize.SMALL);

        var spotOptions = new LaunchTemplateSpotOptions
        {
            InterruptionBehavior = SpotInstanceInterruption.TERMINATE,
            MaxPrice = 0.02,
            RequestType = SpotRequestType.ONE_TIME
        };

        var properties = new LaunchTemplateProps
        {
            InstanceType = instanceType,
            LaunchTemplateName = BotName,
            Role = role,
            SecurityGroup = securityGroup,
            SpotOptions = spotOptions,
            MachineImage = machineImage,
            RequireImdsv2 = true
        };

        return new LaunchTemplate(instance, nameof(LaunchTemplate), properties);
    }

    private static Role CreateRole(BotStack stack)
    {
        var servicePrincipal = new ServicePrincipal("ec2.amazonaws.com");
        var instanceCorePolicy = ManagedPolicy.FromAwsManagedPolicyName("AmazonSSMManagedInstanceCore");

        var properties = new RoleProps
        {
            AssumedBy = servicePrincipal,
            ManagedPolicies = [instanceCorePolicy],
            RoleName = BotName
        };

        return new Role(stack, nameof(Role), properties);
    }

    private static IMachineImage GetMachineImage(BotStack stack)
    {
        var region = stack.GetContextOrThrow("AWS_REGION");
        var amiId = stack.GetContextOrThrow("AMI_ID");

        var amiMap = new Dictionary<string, string>
        {
            { region, amiId }
        };

        return MachineImage.GenericLinux(amiMap);
    }

    private static SecurityGroup CreateSecurityGroup(Vpc vpc)
    {
        var properties = new SecurityGroupProps
        {
            AllowAllOutbound = true,
            Description = "Allow All Output, SSH In Only",
            SecurityGroupName = BotName,
            Vpc = vpc
        };

        var securityGroup = new SecurityGroup(vpc, nameof(SecurityGroup), properties);
        var developerIp = securityGroup.GetContextOrThrow("DEV_IP");

        securityGroup.AddIngressRule(Peer.Ipv4(developerIp), Port.SSH, "Allow SSH for Development");

        return securityGroup;
    }

    private static Vpc CreateVpc(BotStack stack)
    {
        var cidr = stack.GetContextOrThrow("VPC_CIDR");

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
            VpcName = BotName,
        };

        return new Vpc(stack, nameof(Vpc), properties);
    }
    
    private string GetContextOrThrow(string key)
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        var value = Node.TryGetContext(key)?.ToString();

        ArgumentException.ThrowIfNullOrWhiteSpace(value, key);

        return value;
    }
}