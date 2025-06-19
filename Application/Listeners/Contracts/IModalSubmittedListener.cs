using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IModalSubmittedListener : IListener
{
    Task OnModalSubmitted(IDiscordRestClientProxy client, SocketModal socketModal, CancellationToken cancellationToken);
}