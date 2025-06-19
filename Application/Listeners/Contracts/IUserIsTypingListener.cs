using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUserIsTypingListener : IListener
{
    Task OnUserIsTyping(IDiscordRestClientProxy client, Cacheable<IUser, ulong> user,
        Cacheable<IMessageChannel, ulong> channel, CancellationToken cancellationToken);
}