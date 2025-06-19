using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISlashCommandExecutedListener
{
    Task OnSlashCommandExecuted(IDiscordRestClientProxy client, SocketSlashCommand socketSlashCommand,
        CancellationToken cancellationToken);
}