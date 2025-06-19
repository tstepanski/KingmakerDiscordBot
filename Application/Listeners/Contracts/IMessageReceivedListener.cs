using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IMessageReceivedListener : IListener
{
    Task OnMessageReceived(IDiscordRestClientProxy client, SocketMessage socketMessage,
        CancellationToken cancellationToken);
}