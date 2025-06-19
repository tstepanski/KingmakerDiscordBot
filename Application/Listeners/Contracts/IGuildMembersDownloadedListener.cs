using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildMembersDownloadedListener : IListener
{
    Task OnGuildMembersDownloaded(IDiscordRestClientProxy client, SocketGuild socketGuild,
        CancellationToken cancellationToken);
}