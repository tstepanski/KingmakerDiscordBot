using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IApplicationCommandUpdatedListener : IListener
{
    Task OnApplicationCommandUpdated(IDiscordRestClientProxy client, SocketApplicationCommand before,
        SocketApplicationCommand after, CancellationToken cancellationToken);
}