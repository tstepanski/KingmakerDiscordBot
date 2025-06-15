using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InternalDiscordConfiguration = KingmakerDiscordBot.Application.Configuration.Discord; 

namespace KingmakerDiscordBot.Application.Logic;

internal static class LogicExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var discordConfiguration = configuration.GetSection("Discord");
        
        return serviceCollection
            .Configure<InternalDiscordConfiguration>(discordConfiguration)
            .AddSingleton<IAmazonSecretsManager, AmazonSecretsManagerClient>()
            .AddSingleton<IDiscordClientFactory, DiscordClientFactory>()
            .AddHostedService<Listener>();
    }
}