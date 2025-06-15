using System.Text.Json;
using KingmakerDiscordBot.Application.Configuration;
using KingmakerDiscordBot.Application.General;
using KingmakerDiscordBot.Application.Logic;
using KingmakerDiscordBot.Application.Observability;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace KingmakerDiscordBot.Application;

public static class Program
{
    internal static IConfigurationFactory ConfigurationFactory = new ConfigurationFactory();

    public static async Task Main(string[] arguments)
    {
        var configuration = ConfigurationFactory.Create(arguments);
        
        await LogConfigurationAsync(configuration);

        using var host = Host
            .CreateDefaultBuilder(arguments)
            .ConfigureServices(services => services
                .AddAws(configuration)
                .AddObservability(configuration)
                .AddLogic(configuration))
            .Build();

        await host.RunAsync();
    }

    private static async Task LogConfigurationAsync(IConfiguration configuration)
    {
        await using var standardOutput = Console.OpenStandardOutput();

        await JsonSerializer.SerializeAsync(standardOutput, configuration, PrettySerializationSettings.Instance);
    }
}