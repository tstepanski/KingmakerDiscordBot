namespace KingmakerDiscordBot.Application.Configuration;

internal sealed class Heartbeat
{
    public string InstanceDimensionName { get; set; } = string.Empty;

    public double IntervalInSeconds { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public string Namespace { get; set; } = string.Empty;

    public TimeSpan Interval => TimeSpan.FromSeconds(IntervalInSeconds);
}