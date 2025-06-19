using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IIntegrationCreatedListener : IListener
{
    Task OnIntegrationCreated(IDiscordRestClientProxy client, IIntegration integration,
        CancellationToken cancellationToken);
}