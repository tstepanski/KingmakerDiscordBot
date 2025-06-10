using Microsoft.Extensions.Configuration;

namespace KingmakerDiscordBot.Application.Configuration;

internal interface IConfigurationFactory
{
    IConfiguration Create(string[] arguments);
}