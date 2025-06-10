using Amazon.CloudWatch;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KingmakerDiscordBot.Application.Observability;

internal static class ObservabilityExtensions
{
    public static IServiceCollection AddObservability(this IServiceCollection serviceCollection, 
        IConfiguration configuration)
    {
        AWSXRayRecorder.InitializeInstance(configuration);
        AWSSDKHandler.RegisterXRayForAllServices();
        
        return serviceCollection
            .AddAWSService<IAmazonCloudWatch>()
            .AddHostedService<CloudwatchHeartbeatService>()
            .AddLogging(builder =>
            {
                var loggingConfigurationSection = configuration.GetAWSLoggingConfigSection();

                builder.ClearProviders();
                builder.AddAWSProvider(loggingConfigurationSection);
            });
    }
}