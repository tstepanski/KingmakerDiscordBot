using KingmakerDiscordBot.Application.General;
using KingmakerDiscordBot.Application.Listeners.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KingmakerDiscordBot.Application.Tests.General;

public sealed class ServiceCollectionExtensionsTests
{
    [Fact]
    public void RegisterAll_GivenConfiguration_ReturnsIServiceCollection()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            ["AWS:Region"] = "us-east-1"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var serviceCollection = new ServiceCollection();
        var result = serviceCollection.RegisterAll(configuration);

        Assert.IsType<IServiceCollection>(result, false);
    }

    [Fact]
    public void RegisterAll_GivenConfiguration_Provides1Listener()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            ["AWS:Region"] = "us-east-1"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var serviceProvider = new ServiceCollection()
            .RegisterAll(configuration)
            .BuildServiceProvider();

        var result = serviceProvider
            .GetServices<IListener>()
            .Count();

        Assert.Equal(2, result);
    }
}