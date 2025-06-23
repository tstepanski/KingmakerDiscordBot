using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Listeners;
using KingmakerDiscordBot.Application.Listeners.Contracts;
using Microsoft.Extensions.Logging;
using Moq;

namespace KingmakerDiscordBot.Application.Tests.Listeners;

public sealed class ListenerRegistrarTests
{
    [Fact]
    public async Task RegisterAll_GivenGuildAvailabilityListener_CorrectlyAttachesInvokeMethod()
    {
        var socketClientProxyMock = new Mock<ISocketClientProxy>();
        var loggerMock = new Mock<ILogger<ListenerRegistrar>>();
        var fakeListener = new FakeGuildAvailabilityListener();
        var registrar = new ListenerRegistrar([fakeListener], loggerMock.Object);
        
        registrar.RegisterAll(socketClientProxyMock.Object, CancellationToken.None);

        await socketClientProxyMock.RaiseAsync(proxy => proxy.GuildAvailable += null, (SocketGuild) null!);
        
        Assert.True(fakeListener.Called);
    }
    
    private sealed class FakeGuildAvailabilityListener : IGuildAvailableListener
    {
        public bool Called { get; private set; }
        
        public Task OnGuildAvailable(IDiscordRestClientProxy _, SocketGuild __, CancellationToken ___)
        {
            Called = true;
            
            return Task.CompletedTask;
        }
    }
}