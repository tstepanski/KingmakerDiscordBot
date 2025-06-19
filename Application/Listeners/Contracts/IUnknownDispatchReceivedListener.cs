using KingmakerDiscordBot.Application.Discord;
using Newtonsoft.Json.Linq;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUnknownDispatchReceivedListener
{
    Task OnUnknownDispatchReceived(IDiscordRestClientProxy client, string eventName, JToken payload,
        CancellationToken cancellationToken);
}