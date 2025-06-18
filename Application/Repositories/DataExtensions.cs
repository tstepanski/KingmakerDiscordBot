using KingmakerDiscordBot.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KingmakerDiscordBot.Application.Repositories;

internal static class DataExtensions
{
    public static IServiceCollection AddData(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .Configure<Tables>(configuration, "Tables")
            .AddSingleton<IGuildRepository, GuildRepository>();
    }
}