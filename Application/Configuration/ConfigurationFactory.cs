using Microsoft.Extensions.Configuration;

namespace KingmakerDiscordBot.Application.Configuration;

internal sealed class ConfigurationFactory : IConfigurationFactory
{
    public IConfiguration Create(string[] arguments)
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables()
            .AddCommandLine(arguments)
            .Build();
    }
}