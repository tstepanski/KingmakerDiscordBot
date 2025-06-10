using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using KingmakerDiscordBot.Application.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KingmakerDiscordBot.Application.Observability;

internal sealed class CloudwatchHeartbeatService(IAmazonCloudWatch cloudWatch, IOptions<Heartbeat> settings,
    IInstanceIdHelper instanceIdHelper, ILogger<CloudwatchHeartbeatService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var instanceId = await instanceIdHelper.GetIdAsync(stoppingToken);

        while (stoppingToken.IsCancellationRequested is false)
        {
            await SendHeartbeat(instanceId, stoppingToken);

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

    private async Task SendHeartbeat(string instanceId, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
            
        using var scope = logger.BeginScope($"Heartbeat: {now:O}");
            
        logger.LogDebug("Sending");

        var dimension = new Dimension
        {
            Name = settings.Value.InstanceDimensionName,
            Value = instanceId
        };
        
        var datum = new MetricDatum
        {
            Dimensions = [dimension],
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