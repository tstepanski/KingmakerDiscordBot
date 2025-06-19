using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IInviteCreatedListener
{
    Task OnInviteCreated(IDiscordRestClientProxy client, SocketInvite socketInvite,
        CancellationToken cancellationToken);
}