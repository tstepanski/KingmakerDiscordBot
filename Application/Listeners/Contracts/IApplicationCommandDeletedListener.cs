using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IApplicationCommandDeletedListener
{
    Task OnApplicationCommandDeleted(IDiscordRestClientProxy client, SocketApplicationCommand socketApplicationCommand,
        CancellationToken cancellationToken);
}