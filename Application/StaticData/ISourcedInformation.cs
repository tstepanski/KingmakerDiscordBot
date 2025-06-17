namespace KingmakerDiscordBot.Application.StaticData;

internal interface ISourcedInformation<T> : IEquatable<T>, IComparable<T>, IHasPageSource
{
    string Name { get; }
    
    string Description { get; }
    
}