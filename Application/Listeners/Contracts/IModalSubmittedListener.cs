using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IModalSubmittedListener
{
    Task OnModalSubmitted(IDiscordRestClientProxy client, SocketModal socketModal, CancellationToken cancellationToken);
}