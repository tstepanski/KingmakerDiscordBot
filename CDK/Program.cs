using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;

namespace KingmakerDiscordBot.CDK;

public static class Program
{
    public static void Main()
    {
        var application = new App();

        _ = new BotStack(application);

        var synthesis = application.Synth();

        Console.WriteLine($"Synthesized to: {synthesis.Directory}");
    }
}