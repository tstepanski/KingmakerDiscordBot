using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IJoinedGuildListener : IListener
{
    Task OnJoinedGuild(IDiscordRestClientProxy client, SocketGuild socketGuild, CancellationToken cancellationToken);
}