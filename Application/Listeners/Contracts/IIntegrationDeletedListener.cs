using Discord;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IIntegrationDeletedListener : IListener
{
    Task OnIntegrationDeleted(IDiscordRestClientProxy client, IGuild guild, ulong integrationId,
        Optional<ulong> applicationId, CancellationToken cancellationToken);
}