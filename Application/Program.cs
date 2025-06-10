using KingmakerDiscordBot.Application.Configuration;
using KingmakerDiscordBot.Application.General;
using KingmakerDiscordBot.Application.Observability;
using Microsoft.Extensions.Hosting;

namespace KingmakerDiscordBot.Application;

public static class Program
{
    internal static IConfigurationFactory ConfigurationFactory = new ConfigurationFactory();

    public static async Task Main(string[] arguments)
    {
        var configuration = ConfigurationFactory.Create(arguments);

        using var host = Host
            .CreateDefaultBuilder(arguments)
            .ConfigureServices(services => services
                .AddAws(configuration)
                .AddObservability(configuration))
            .Build();

        await host.RunAsync();
    }
}