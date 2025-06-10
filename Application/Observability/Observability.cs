using Amazon.CloudWatch;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Amazon.XRay.Recorder.Handlers.System.Net;
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
            .AddSingleton<IInstanceIdHelper, InstanceIdHelper>()
            .AddHttpClient()
            .ConfigureHttpClientDefaults(builder =>
            {
                builder.ConfigurePrimaryHttpMessageHandler(() => 
                    new HttpClientXRayTracingHandler(new HttpClientHandler()));
            })
            .AddLogging(builder =>
            {
                var loggingConfigurationSection = configuration.GetAWSLoggingConfigSection();

                builder.ClearProviders();
                builder.AddAWSProvider(loggingConfigurationSection);
            });
    }
}