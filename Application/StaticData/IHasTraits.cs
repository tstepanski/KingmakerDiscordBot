using System.Collections.Immutable;

namespace KingmakerDiscordBot.Application.StaticData;

internal interface IHasTraits
{
    ImmutableSortedSet<Trait> Traits { get; }
}