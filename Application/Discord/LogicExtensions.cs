using Amazon.SecretsManager;
using KingmakerDiscordBot.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InternalDiscordConfiguration = KingmakerDiscordBot.Application.Configuration.Discord; 

namespace KingmakerDiscordBot.Application.Discord;

internal static class LogicExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .Configure<InternalDiscordConfiguration>(configuration, "Discord")
            .AddSingleton<IAmazonSecretsManager, AmazonSecretsManagerClient>()
            .AddSingleton<IDiscordClientFactory, DiscordClientFactory>()
            .AddHostedService<Listener>();
    }
}