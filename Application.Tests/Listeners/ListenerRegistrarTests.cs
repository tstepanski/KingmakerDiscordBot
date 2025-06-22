using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Listeners;
using KingmakerDiscordBot.Application.Listeners.Contracts;
using Moq;

namespace KingmakerDiscordBot.Application.Tests.Listeners;

public sealed class ListenerRegistrarTests
{
    [Fact]
    public async Task RegisterAll_GivenGuildAvailabilityListener_CorrectlyAttachesInvokeMethod()
    {
        var socketClientProxyMock = new Mock<ISocketClientProxy>();
        var fakeListener = new FakeGuildAvailabilityListener();
        var registrar = new ListenerRegistrar([fakeListener]);
        
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