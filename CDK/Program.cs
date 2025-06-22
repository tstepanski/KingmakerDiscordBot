using Amazon.CDK;

namespace KingmakerDiscordBot.CDK;

public static class Program
{
    public static void Main()
    {
        var application = new App();
        var hasher = new Hasher();

        var vpcStack = new VpcStack(application);
        var applicationAssetsStack = new ApplicationAssetsStack(application, hasher);
        var databaseStack = new DatabaseStack(application);

        _ = new BotStack(application, applicationAssetsStack.Bucket, vpcStack.Vpc, vpcStack.SecurityGroup, 
            databaseStack.GuildsTable, hasher);

        var synthesis = application.Synth();

        Console.WriteLine($"Synthesized to: {synthesis.Directory}");
    }
}