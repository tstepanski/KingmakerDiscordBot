using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IAuditLogCreatedListener : IListener
{
    Task OnAuditLogCreated(IDiscordRestClientProxy client, SocketAuditLogEntry entry, SocketGuild guild,
        CancellationToken cancellationToken);
}