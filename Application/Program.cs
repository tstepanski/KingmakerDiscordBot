using System.Text.Json;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using KingmakerDiscordBot.Application.Configuration;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.General;
using KingmakerDiscordBot.Application.Observability;
using KingmakerDiscordBot.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace KingmakerDiscordBot.Application;

public static class Program
{
    internal static IConfigurationFactory ConfigurationFactory = new ConfigurationFactory();

    public static async Task Main(string[] arguments)
    {
        var configuration = ConfigurationFactory.Create(arguments);
        
        AWSXRayRecorder.InitializeInstance(configuration);
        AWSSDKHandler.RegisterXRayForAllServices();
        
        await LogConfigurationAsync(configuration);

        using var host = Host
            .CreateDefaultBuilder(arguments)
            .ConfigureServices(services => services.RegisterAll(configuration))
            .Build();

        await host.RunAsync();
    }

    private static async Task LogConfigurationAsync(IConfiguration configuration)
    {
        await using var standardOutput = Console.OpenStandardOutput();

        await JsonSerializer.SerializeAsync(standardOutput, configuration, SerializationSettings.PrettySettingsInstance);
    }
}