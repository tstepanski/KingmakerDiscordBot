using Discord.WebSocket;
using KingmakerDiscordBot.Application.Discord;

namespace KingmakerDiscordBot.Application.Listeners.Contracts;

internal interface ISelectMenuExecutedListener
{
    Task OnSelectMenuExecuted(IDiscordRestClientProxy client, SocketMessageComponent socketMessageComponent,
        CancellationToken cancellationToken);
}