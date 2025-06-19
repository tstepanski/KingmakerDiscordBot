using Discord;
using Discord.Rest;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildScheduledEventUserAddListener : IListener
{
    Task OnGuildScheduledEventUserAdd(IDiscordRestClientProxy client,
        Cacheable<SocketUser, RestUser, IUser, ulong> user, SocketGuildEvent socketGuildEvent,
        CancellationToken cancellationToken);
}