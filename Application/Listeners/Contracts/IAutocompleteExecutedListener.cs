using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IAutocompleteExecutedListener
{
    Task OnAutocompleteExecuted(IDiscordRestClientProxy client, 
        SocketAutocompleteInteraction socketAutocompleteInteraction, CancellationToken cancellationToken);
}