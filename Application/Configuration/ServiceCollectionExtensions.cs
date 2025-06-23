namespace KingmakerDiscordBot.Application.Configuration;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection Configure<T>(this IServiceCollection serviceCollection,
        IConfiguration configuration, string sectionName) where T : class
    {
        var configurationSection = configuration.GetSection(sectionName);

        return serviceCollection.Configure<T>(configurationSection);
    }
}