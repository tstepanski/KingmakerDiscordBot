using Discord;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Listeners;
using KingmakerDiscordBot.Application.StaticData.Commands;
using Moq;

namespace KingmakerDiscordBot.Application.Tests.IntegrationTest;

public sealed class DescribeCommandFullCircleTest
{
    [Fact]
    public async Task DescribeEstablishWorkSiteSkillAction()
    {
        var abstractEmbedFactory = new AbstractEmbedFactory();
        var describeCommandWithHandlerFactory = new DescribeCommandWithHandlerFactory(abstractEmbedFactory);
        var commandsPayloadGenerator = new CommandsPayloadGenerator(describeCommandWithHandlerFactory);
        var describeCommandListener = new DescribeCommandListener(commandsPayloadGenerator);
        var restClientMock = new Mock<IDiscordRestClientProxy>();
        var commandMock = new Mock<ISlashCommandInteraction>();
        var interactionDataMock = new Mock<IApplicationCommandInteractionData>();
        var optionMock = new Mock<IApplicationCommandInteractionDataOption>();
        var subcommandOptionMock = new Mock<IApplicationCommandInteractionDataOption>();

        commandMock
            .Setup(interaction => interaction.Data)
            .Returns(interactionDataMock.Object);

        interactionDataMock
            .SetupGet(data => data.Name)
            .Returns("describe-skill-action");

        interactionDataMock
            .SetupGet(data => data.Options)
            .Returns([optionMock.Object]);

        optionMock
            .SetupGet(option => option.Name)
            .Returns("engineering");

        optionMock
            .SetupGet(option => option.Options)
            .Returns([subcommandOptionMock.Object]);

        subcommandOptionMock
            .SetupGet(option => option.Name)
            .Returns("skill-action");

        subcommandOptionMock
            .SetupGet(option => option.Value)
            .Returns("Establish Work Site");

        commandMock
            .Setup(interaction => interaction.RespondAsync(It.IsAny<string?>(), It.IsAny<Embed[]?>(), It.IsAny<bool>(),
                It.IsAny<bool>(), It.IsAny<AllowedMentions?>(), It.IsAny<MessageComponent?>(), It.IsAny<Embed?>(),
                It.IsAny<RequestOptions?>(), It.IsAny<PollProperties?>()))
            .Callback<string?, Embed[]?, bool, bool, AllowedMentions?, MessageComponent?, Embed?, RequestOptions?,
                PollProperties?>((_, _, _, _, _, _, embed, _, _) =>
            {
                Assert.IsType<Embed>(embed);
                Assert.IsType<string>(embed.Title);
            });

        await describeCommandListener.OnSlashCommandExecuted(restClientMock.Object, commandMock.Object);
    }
}