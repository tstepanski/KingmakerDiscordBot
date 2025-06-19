using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IApplicationCommandDeletedListener : IListener
{
    Task OnApplicationCommandDeleted(IDiscordRestClientProxy client, SocketApplicationCommand socketApplicationCommand,
        CancellationToken cancellationToken);
}