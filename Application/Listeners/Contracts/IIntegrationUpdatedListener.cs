using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IIntegrationUpdatedListener
{
    Task OnIntegrationUpdated(IDiscordRestClientProxy client, IIntegration integration,
        CancellationToken cancellationToken);
}