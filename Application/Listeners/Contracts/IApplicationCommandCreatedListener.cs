using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IApplicationCommandCreatedListener
{
    Task OnApplicationCommandCreated(IDiscordRestClientProxy client, SocketApplicationCommand socketApplicationCommand,
        CancellationToken cancellationToken);
}