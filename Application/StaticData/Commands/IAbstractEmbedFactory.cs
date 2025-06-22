using Discord;

namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal interface IAbstractEmbedFactory
{
    Func<T, Embed> Create<T>() where T : ILookup<T>;
}