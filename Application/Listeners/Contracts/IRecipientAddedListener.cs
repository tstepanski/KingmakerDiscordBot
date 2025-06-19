using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IRecipientAddedListener
{
    Task OnRecipientAdded(IDiscordRestClientProxy client, SocketGroupUser socketGroupUser,
        CancellationToken cancellationToken);
}