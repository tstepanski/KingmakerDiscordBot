using Discord;
using Discord.Rest;
using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IPollVoteAddedListener
{
    Task OnPollVoteAdded(IDiscordRestClientProxy client, Cacheable<IUser, ulong> user,
        Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong> channel,
        Cacheable<IUserMessage, ulong> message, Cacheable<SocketGuild, RestGuild, IGuild, ulong>? guild, ulong emojiId,
        CancellationToken cancellationToken);
}