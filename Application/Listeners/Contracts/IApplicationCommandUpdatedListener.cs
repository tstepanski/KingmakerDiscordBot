using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IApplicationCommandUpdatedListener
{
    Task OnApplicationCommandUpdated(IDiscordRestClientProxy client, SocketApplicationCommand before,
        SocketApplicationCommand after, CancellationToken cancellationToken);
}