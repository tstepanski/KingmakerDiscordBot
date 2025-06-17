namespace KingmakerDiscordBot.Application.StaticData;

internal interface ILookup<T> : ISourcedInformation<T>
{
    static abstract IEnumerable<T> GetAll();
}