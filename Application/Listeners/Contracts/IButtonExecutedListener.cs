using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface IButtonExecutedListener
{
    Task OnButtonExecuted(IDiscordRestClientProxy client, SocketMessageComponent socketMessageComponent,
        CancellationToken cancellationToken);
}