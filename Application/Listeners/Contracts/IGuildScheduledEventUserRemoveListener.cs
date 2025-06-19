using Discord;
using Discord.Rest;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IGuildScheduledEventUserRemoveListener : IListener
{
    Task OnGuildScheduledEventUserRemove(IDiscordRestClientProxy client,
        Cacheable<SocketUser, RestUser, IUser, ulong> user, SocketGuildEvent socketGuildEvent,
        CancellationToken cancellationToken);
}