using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IApplicationCommandCreatedListener : IListener
{
    Task OnApplicationCommandCreated(IDiscordRestClientProxy client, SocketApplicationCommand socketApplicationCommand,
        CancellationToken cancellationToken);
}