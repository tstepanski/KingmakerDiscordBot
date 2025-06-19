using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IInviteCreatedListener : IListener
{
    Task OnInviteCreated(IDiscordRestClientProxy client, SocketInvite socketInvite,
        CancellationToken cancellationToken);
}