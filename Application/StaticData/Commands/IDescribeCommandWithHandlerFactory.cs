namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal interface IDescribeCommandWithHandlerFactory
{
    IDescribeCommandWithHandler Create<T>() where T : ILookup<T>;
}