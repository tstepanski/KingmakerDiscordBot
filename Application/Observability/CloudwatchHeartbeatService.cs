using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using KingmakerDiscordBot.Application.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KingmakerDiscordBot.Application.Observability;

internal sealed class CloudwatchHeartbeatService(IAmazonCloudWatch cloudWatch, IOptions<Heartbeat> settings,
    ILogger<CloudwatchHeartbeatService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            await SendHeartbeat(stoppingToken);

            try
            {
                await Task.Delay(settings.Value.Interval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Expected
            }
        }
    }

    private async Task SendHeartbeat(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
            
        using var scope = logger.BeginScope($"Heartbeat: {now:O}");
            
        logger.LogDebug("Sending");
        
        var datum = new MetricDatum
        {
            MetricName = settings.Value.MetricName,
            Timestamp = now,
            Unit = StandardUnit.Count,
            Value = 1
        };
            
        var request = new PutMetricDataRequest
        {
            MetricData = [datum],
            Namespace = settings.Value.Namespace
        };
            
        await cloudWatch.PutMetricDataAsync(request, cancellationToken);
            
        logger.LogDebug("Sent");
    }
}