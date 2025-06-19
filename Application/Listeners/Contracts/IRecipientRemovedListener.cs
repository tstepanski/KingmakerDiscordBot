using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IRecipientRemovedListener : IListener
{
    Task OnRecipientRemoved(IDiscordRestClientProxy client, SocketGroupUser socketGroupUser,
        CancellationToken cancellationToken);
}