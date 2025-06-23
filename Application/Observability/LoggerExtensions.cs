using Discord;

namespace KingmakerDiscordBot.Application.Observability;

internal static class LoggerExtensions
{
    public static Task Log(this ILogger logger, LogMessage message)
    {
        var logLevel = Convert(message.Severity);

        logger.Log(logLevel, message.Exception, "Discord.Net: {Source}: {Message}", message.Source, message.Message);

        return Task.CompletedTask;
    }

    private static LogLevel Convert(LogSeverity severity)
    {
        return severity switch
        {
            LogSeverity.Critical => LogLevel.Critical,
            LogSeverity.Error => LogLevel.Error,
            LogSeverity.Warning => LogLevel.Warning,
            LogSeverity.Info => LogLevel.Information,
            LogSeverity.Verbose => LogLevel.Debug,
            LogSeverity.Debug => LogLevel.Trace,
            _ => throw new ArgumentOutOfRangeException(nameof(severity), severity, $"Unknown {nameof(LogSeverity)}")
        };
    }
}