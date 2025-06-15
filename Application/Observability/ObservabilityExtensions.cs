using System.Net;
using System.Net.Http.Headers;
using Amazon.CloudWatch;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Amazon.XRay.Recorder.Handlers.System.Net;
using Discord.Net.Rest;
using Discord.Net.WebSockets;
using KingmakerDiscordBot.Application.Configuration;
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

        var heartbeatConfiguration = configuration.GetSection("Heartbeat");

        return serviceCollection
            .Configure<Heartbeat>(heartbeatConfiguration)
            .AddAWSService<IAmazonCloudWatch>()
            .AddHostedService<CloudwatchHeartbeatService>()
            .AddSingleton<IInstanceIdHelper, InstanceIdHelper>()
            .AddHttpClient()
            .AddSingleton<WebSocketProvider>(_ =>
            {
                var webSocketProvider = DefaultWebSocketProvider.Create();
        
                return () =>
                {
                    var defaultClient = webSocketProvider();
            
                    return new TracedWebSocketClient(defaultClient);
                };
            })
            .AddSingleton<RestClientProvider>(provider =>
            {
                return baseUrl =>
                {
                    var httpClient = provider.GetRequiredService<HttpClient>();

                    return new TracedRestClient(baseUrl, httpClient);
                };
            })
            .ConfigureHttpClientDefaults(builder =>
            {
                var httpMessageHandler = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UseCookies = false
                };

                var tracingHandler = new HttpClientXRayTracingHandler(httpMessageHandler);

                builder
                    .ConfigurePrimaryHttpMessageHandler(() => tracingHandler)
                    .ConfigureHttpClient(client =>
                    {
                        client.DefaultRequestHeaders.AcceptEncoding.Clear();
                        
                        AddEncodingCompression("gzip");
                        AddEncodingCompression("deflate");
                        
                        return;

                        void AddEncodingCompression(string method)
                        {
                            var headerValue = StringWithQualityHeaderValue.Parse(method);
                            
                            client.DefaultRequestHeaders.AcceptEncoding.Add(headerValue);
                        }
                    });
            });
    }
}