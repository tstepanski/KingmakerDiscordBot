using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using KingmakerDiscordBot.Application.Configuration;
using KingmakerDiscordBot.Application.General;
using KingmakerDiscordBot.Application.Logic;
using KingmakerDiscordBot.Application.Observability;
using Microsoft.Extensions.Hosting;

namespace KingmakerDiscordBot.Application;

public static class Program
{
    internal static IConfigurationFactory ConfigurationFactory = new ConfigurationFactory();

    public static async Task Main(string[] arguments)
    {
        try
        {
            var configuration = ConfigurationFactory.Create(arguments);

            using var host = Host
                .CreateDefaultBuilder(arguments)
                .ConfigureServices(services => services
                    .AddAws(configuration)
                    .AddObservability(configuration)
                    .AddLogic(configuration))
                .Build();

            await host.RunAsync();
        }
        catch (Exception exception)
        {
            await LogExceptionToCloudWatchAsync(exception);
        }
    }

    private static async Task LogExceptionToCloudWatchAsync(Exception exception)
    {
        const string logGroupName = "kingmaker-discord-bot";
        const string logStreamName = "exceptions";
        using var client = new AmazonCloudWatchLogsClient();

        var logStreamRequest = new CreateLogStreamRequest
        {
            LogGroupName = logGroupName,
            LogStreamName = logStreamName
        };

        await client.CreateLogStreamAsync(logStreamRequest);

        var logEvent = new InputLogEvent
        {
            Message = exception.ToString(),
            Timestamp = DateTime.UtcNow
        };

        var eventsRequest = new PutLogEventsRequest
        {
            LogGroupName = logGroupName,
            LogStreamName = logStreamName,
            LogEvents = [logEvent]
        };

        await client.PutLogEventsAsync(eventsRequest);
    }
}