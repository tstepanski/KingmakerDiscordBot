using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IInviteDeletedListener : IListener
{
    Task OnInviteDeleted(IDiscordRestClientProxy client, SocketGuildChannel channel, string inviteCode,
        CancellationToken cancellationToken);
}