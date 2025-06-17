namespace KingmakerDiscordBot.Application.StaticData;

internal interface IHasPageSource
{
    Source Source { get; }

    ushort Page { get; }
}