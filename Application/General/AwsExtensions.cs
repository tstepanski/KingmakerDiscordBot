using Amazon;
using Amazon.Extensions.NETCore.Setup;
using AWS.Logger;
using KingmakerDiscordBot.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KingmakerDiscordBot.Application.General;

internal static class AwsExtensions
{
    public static IServiceCollection AddAws(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var awsConfiguration = configuration.GetSection("AWS").Get<Aws>()!;
        var region = RegionEndpoint.GetBySystemName(awsConfiguration.Region);

        var awsOptions = new AWSOptions
        {
            Region = region
        };

        return serviceCollection
            .AddDefaultAWSOptions(awsOptions)
            .AddLogging(builder =>
            {
                var loggerConfiguration = new AWSLoggerConfig
                {
                    LogGroup = awsConfiguration.LogGroup,
                    Region = awsConfiguration.Region,
                    FlushTimeout = TimeSpan.FromSeconds(5),
                };
                
                builder
                    .ClearProviders()
                    .AddConsole()
                    .AddAWSProvider(loggerConfiguration);
            });
    }
}