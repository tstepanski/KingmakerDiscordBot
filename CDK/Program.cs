using Amazon.CDK;

namespace KingmakerDiscordBot.CDK;

public static class Program
{
    public static void Main()
    {
        var application = new App();
        var hasher = new Hasher();

        var applicationAssetsStack = new ApplicationAssetsStack(application, hasher);
        
        _ = new BotStack(application, applicationAssetsStack.Bucket, hasher);

        var synthesis = application.Synth();

        Console.WriteLine($"Synthesized to: {synthesis.Directory}");
    }
}