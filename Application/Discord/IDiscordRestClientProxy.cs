using Discord;
using Discord.Rest;

namespace KingmakerDiscordBot.Application.Discord;

internal interface IDiscordRestClientProxy : IDiscordClient
{
    LoginState LoginState { get; }
    bool IsValidHttpInteraction(string publicKey, string signature, string timestamp, string body);
    Task<RestSelfUser> GetCurrentUserAsync(RequestOptions? options = null);
    Task<RestGuildUser> GetCurrentUserGuildMemberAsync(ulong guildId, RequestOptions? options = null);
    Task<RestApplication> GetCurrentBotInfoAsync(RequestOptions? options = null);
    Task<RestGuildWidget?> GetGuildWidgetAsync(ulong id, RequestOptions? options = null);
    IAsyncEnumerable<IReadOnlyCollection<RestUserGuild>> GetGuildSummariesAsync(RequestOptions? options = null);
    Task<RestGuildUser> GetGuildUserAsync(ulong guildId, ulong id, RequestOptions? options = null);
    Task DeleteAllGlobalCommandsAsync(RequestOptions? options = null);
    Task AddRoleAsync(ulong guildId, ulong userId, ulong roleId, RequestOptions? options = null);
    Task RemoveRoleAsync(ulong guildId, ulong userId, ulong roleId, RequestOptions? options = null);
    Task AddReactionAsync(ulong channelId, ulong messageId, IEmote emote, RequestOptions? options = null);
    Task LoginAsync(TokenType tokenType, string token, bool validateToken);
    Task LogoutAsync();

    Task<RestInteraction> ParseHttpInteractionAsync(string publicKey, string signature, string timestamp, string body,
        Func<InteractionProperties, bool> doApiCallOnCreation);

    Task<RestGlobalCommand> CreateGlobalCommand(ApplicationCommandProperties properties,
        RequestOptions? options = null);

    Task<IReadOnlyCollection<RestGlobalCommand>> GetGlobalApplicationCommands(bool withLocalizations, string locale,
        RequestOptions? options = null);

    Task<RestGuildCommand> CreateGuildCommand(ApplicationCommandProperties properties, ulong guildId,
        RequestOptions? options = null);

    Task<RestApplication> ModifyCurrentBotApplicationAsync(Action<ModifyApplicationProperties> args,
        RequestOptions? options = null);

    Task<IReadOnlyCollection<RestGuildCommand>> GetGuildApplicationCommands(ulong guildId, bool withLocalizations,
        string locale,
        RequestOptions? options = null);

    Task<IReadOnlyCollection<RestGlobalCommand>> BulkOverwriteGlobalCommands(
        ApplicationCommandProperties[] commandProperties, RequestOptions? options = null);

    Task<IReadOnlyCollection<RestGuildCommand>> BulkOverwriteGuildCommands(
        ApplicationCommandProperties[] commandProperties, ulong guildId,
        RequestOptions? options = null);

    Task<IReadOnlyCollection<GuildApplicationCommandPermission>> BatchEditGuildCommandPermissions(ulong guildId,
        IDictionary<ulong, ApplicationCommandPermission[]> permissions,
        RequestOptions? options = null);

    Task RemoveReactionAsync(ulong channelId, ulong messageId, ulong userId, IEmote emote,
        RequestOptions? options = null);

    Task RemoveAllReactionsAsync(ulong channelId, ulong messageId, RequestOptions? options = null);

    Task RemoveAllReactionsForEmoteAsync(ulong channelId, ulong messageId, IEmote emote,
        RequestOptions? options = null);

    Task<IReadOnlyCollection<RoleConnectionMetadata>> GetRoleConnectionMetadataRecordsAsync(
        RequestOptions? options = null);

    Task<IReadOnlyCollection<RoleConnectionMetadata>> ModifyRoleConnectionMetadataRecordsAsync(
        ICollection<RoleConnectionMetadataProperties> metadata,
        RequestOptions? options = null);

    Task<RoleConnection> GetUserApplicationRoleConnectionAsync(ulong applicationId, RequestOptions? options = null);

    Task<RoleConnection> ModifyUserApplicationRoleConnectionAsync(ulong applicationId,
        RoleConnectionProperties roleConnection, RequestOptions? options = null);

    event Func<LogMessage, Task> Log;

    event Func<Task> LoggedIn;

    event Func<Task> LoggedOut;

    event Func<string, string, double, Task> SentRequest;
}