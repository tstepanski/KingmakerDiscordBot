using Amazon;
using Amazon.Extensions.NETCore.Setup;
using KingmakerDiscordBot.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        return serviceCollection.AddDefaultAWSOptions(awsOptions);
    }
}