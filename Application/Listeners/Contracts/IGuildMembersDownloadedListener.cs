using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildMembersDownloadedListener
{
    Task OnGuildMembersDownloaded(IDiscordRestClientProxy client, SocketGuild socketGuild,
        CancellationToken cancellationToken);
}