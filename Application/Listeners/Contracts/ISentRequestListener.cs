using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISentRequestListener
{
    Task OnSentRequest(IDiscordRestClientProxy client, string endpoint, string jsonContent, double elapsedMilliseconds,
        CancellationToken cancellationToken);
}