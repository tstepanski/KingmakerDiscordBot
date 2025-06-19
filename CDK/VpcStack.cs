using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace KingmakerDiscordBot.CDK;

internal sealed class VpcStack : Stack
{
    public VpcStack(App application) : base(application, nameof(VpcStack))
    {
        Vpc = CreateVpc(this);
        SecurityGroup = CreateSecurityGroup(this, Vpc);
    }

    public SecurityGroup SecurityGroup { get; }

    public Vpc Vpc { get; }

    private static Vpc CreateVpc(VpcStack stack)
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
            VpcName = Constants.BotName
        };

        return new Vpc(stack, nameof(Vpc), properties);
    }

    private static SecurityGroup CreateSecurityGroup(VpcStack stack, Vpc vpc)
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
}