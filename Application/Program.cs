using System.Runtime.Loader;

namespace KingmakerDiscordBot.Application;

public static class Program
{
    public static async Task Main()
    {
        var cancellationTokenSource = new CancellationTokenSource();

        SetupCancellation(cancellationTokenSource);

        await RunAsync(cancellationTokenSource);
    }

    private static void SetupCancellation(CancellationTokenSource cancellationTokenSource)
    {
        AssemblyLoadContext.Default.Unloading += _ => cancellationTokenSource.Cancel();

        Console.CancelKeyPress += (_, eventArguments) =>
        {
            eventArguments.Cancel = true;
            
            cancellationTokenSource.Cancel();
        };
    }

    private static async Task RunAsync(CancellationTokenSource cancellationTokenSource)
    {
        while (cancellationTokenSource.IsCancellationRequested is false)
        {
            await Task.Delay(5000, cancellationTokenSource.Token);
            
            await Console.Out.WriteLineAsync("Hello, World!");
        }
    }
}