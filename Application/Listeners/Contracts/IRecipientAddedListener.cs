using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IRecipientAddedListener : IListener
{
    Task OnRecipientAdded(IDiscordRestClientProxy client, SocketGroupUser socketGroupUser,
        CancellationToken cancellationToken);
}