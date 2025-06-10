namespace KingmakerDiscordBot.Application.Configuration;

internal sealed class Cloudwatch
{
    public double HeartbeatIntervalInSeconds { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public string Namespace { get; set; } = string.Empty;

    public TimeSpan HeartbeatInterval => TimeSpan.FromSeconds(HeartbeatIntervalInSeconds);
}