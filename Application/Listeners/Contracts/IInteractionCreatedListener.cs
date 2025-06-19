using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IInteractionCreatedListener
{
    Task OnInteractionCreated(IDiscordRestClientProxy client, SocketInteraction socketInteraction,
        CancellationToken cancellationToken);
}