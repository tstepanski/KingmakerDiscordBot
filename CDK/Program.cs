using Amazon.CDK;

namespace KingmakerDiscordBot.CDK;

public static class Program
{
    public static void Main()
    {
        var application = new App();

        var applicationAssetsStack = new ApplicationAssetsStack(application);
        
        //_ = new BotStack(application, applicationAssetsStack.Bucket);

        var synthesis = application.Synth();

        Console.WriteLine($"Synthesized to: {synthesis.Directory}");
    }
}