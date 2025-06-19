using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IAutocompleteExecutedListener : IListener
{
    Task OnAutocompleteExecuted(IDiscordRestClientProxy client,
        SocketAutocompleteInteraction socketAutocompleteInteraction, CancellationToken cancellationToken);
}