using KingmakerDiscordBot.Application.StaticData;
using KingmakerDiscordBot.Application.StaticData.Commands;

namespace KingmakerDiscordBot.Application.Tests.StaticData.Commands;

public sealed class AbstractEmbedFactoryTests
{
    [Fact]
    public void Create_GivenSkillAction_ReturnsFactoryThatProvidesTraitsAsACommaDelimitedList()
    {
        var factory = new AbstractEmbedFactory();
        var lambda = factory.Create<SkillAction>();
        var embed = lambda(SkillAction.CelebrateHoliday);
        var field = embed.Fields.Single(field => field.Name == nameof(SkillAction.Traits));
        
        Assert.Equal("Downtime, Leadership", field.Value);
    }
}