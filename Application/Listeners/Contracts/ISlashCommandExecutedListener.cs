using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISlashCommandExecutedListener : IListener
{
    Task OnSlashCommandExecuted(IDiscordRestClientProxy client, SocketSlashCommand socketSlashCommand,
        CancellationToken cancellationToken);
}