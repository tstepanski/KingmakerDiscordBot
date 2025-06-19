using KingmakerDiscordBot.Application.Discord;
using Newtonsoft.Json.Linq;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IUnknownDispatchReceivedListener : IListener
{
    Task OnUnknownDispatchReceived(IDiscordRestClientProxy client, string eventName, JToken payload,
        CancellationToken cancellationToken);
}