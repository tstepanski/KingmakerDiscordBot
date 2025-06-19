using Discord;
using Discord.Rest;

namespace KingmakerDiscordBot.Application.Discord;

internal sealed class DiscordRestClientProxy(DiscordRestClient client) : IDiscordRestClientProxy
{
    private readonly IDiscordClient _clientAsContract = client;

    public void Dispose()
    {
        client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return client.DisposeAsync();
    }

    public Task StartAsync()
    {
        return _clientAsContract.StartAsync();
    }

    public Task StopAsync()
    {
        return _clientAsContract.StopAsync();
    }

    public Task<IApplication> GetApplicationInfoAsync(RequestOptions? options = null)
    {
        return _clientAsContract.GetApplicationInfoAsync(options);
    }

    public Task<IChannel> GetChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return _clientAsContract.GetChannelAsync(id, mode, options);
    }

    public Task<IReadOnlyCollection<IPrivateChannel>> GetPrivateChannelsAsync(CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return _clientAsContract.GetPrivateChannelsAsync(mode, options);
    }

    public Task<IReadOnlyCollection<IDMChannel>> GetDMChannelsAsync(CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return _clientAsContract.GetDMChannelsAsync(mode, options);
    }

    public Task<IReadOnlyCollection<IGroupChannel>> GetGroupChannelsAsync(CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return _clientAsContract.GetGroupChannelsAsync(mode, options);
    }

    public Task<IReadOnlyCollection<IConnection>> GetConnectionsAsync(RequestOptions? options = null)
    {
        return _clientAsContract.GetConnectionsAsync(options);
    }

    public Task<IApplicationCommand> GetGlobalApplicationCommandAsync(ulong id, RequestOptions? options = null)
    {
        return _clientAsContract.GetGlobalApplicationCommandAsync(id, options);
    }

    public Task<IReadOnlyCollection<IApplicationCommand>> GetGlobalApplicationCommandsAsync(
        bool withLocalizations = false, string? locale = null, RequestOptions? options = null)
    {
        return _clientAsContract.GetGlobalApplicationCommandsAsync(withLocalizations, locale, options);
    }

    public Task<IApplicationCommand> CreateGlobalApplicationCommand(ApplicationCommandProperties properties,
        RequestOptions? options = null)
    {
        return _clientAsContract.CreateGlobalApplicationCommand(properties, options);
    }

    public Task<IReadOnlyCollection<IApplicationCommand>> BulkOverwriteGlobalApplicationCommand(
        ApplicationCommandProperties[] properties, RequestOptions? options = null)
    {
        return _clientAsContract.BulkOverwriteGlobalApplicationCommand(properties, options);
    }

    public Task<IGuild> GetGuildAsync(ulong id, CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return _clientAsContract.GetGuildAsync(id, mode, options);
    }

    public Task<IReadOnlyCollection<IGuild>> GetGuildsAsync(CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return _clientAsContract.GetGuildsAsync(mode, options);
    }

    public Task<IGuild> CreateGuildAsync(string name, IVoiceRegion region, Stream? jpegIcon = null,
        RequestOptions? options = null)
    {
        return _clientAsContract.CreateGuildAsync(name, region, jpegIcon, options);
    }

    public Task<IInvite> GetInviteAsync(string inviteId, RequestOptions? options = null)
    {
        return _clientAsContract.GetInviteAsync(inviteId, options);
    }

    public Task<IUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions? options = null)
    {
        return _clientAsContract.GetUserAsync(id, mode, options);
    }

    public Task<IUser> GetUserAsync(string username, string discriminator, RequestOptions? options = null)
    {
        return _clientAsContract.GetUserAsync(username, discriminator, options);
    }

    public Task<IReadOnlyCollection<IVoiceRegion>> GetVoiceRegionsAsync(RequestOptions? options = null)
    {
        return _clientAsContract.GetVoiceRegionsAsync(options);
    }

    public Task<IVoiceRegion> GetVoiceRegionAsync(string id, RequestOptions? options = null)
    {
        return _clientAsContract.GetVoiceRegionAsync(id, options);
    }

    public Task<IWebhook> GetWebhookAsync(ulong id, RequestOptions? options = null)
    {
        return _clientAsContract.GetWebhookAsync(id, options);
    }

    public Task<int> GetRecommendedShardCountAsync(RequestOptions? options = null)
    {
        return client.GetRecommendedShardCountAsync(options);
    }

    public Task<BotGateway> GetBotGatewayAsync(RequestOptions? options = null)
    {
        return client.GetBotGatewayAsync(options);
    }

    public Task<IEntitlement> CreateTestEntitlementAsync(ulong skuId, ulong ownerId, SubscriptionOwnerType ownerType,
        RequestOptions? options = null)
    {
        return _clientAsContract.CreateTestEntitlementAsync(skuId, ownerId, ownerType, options);
    }

    public Task DeleteTestEntitlementAsync(ulong entitlementId, RequestOptions? options = null)
    {
        return client.DeleteTestEntitlementAsync(entitlementId, options);
    }

    public IAsyncEnumerable<IReadOnlyCollection<IEntitlement>> GetEntitlementsAsync(int limit = 100,
        ulong? afterId = null, ulong? beforeId = null,
        bool excludeEnded = false, ulong? guildId = null, ulong? userId = null, ulong[]? skuIds = null,
        RequestOptions? options = null, bool? excludeDeleted = null)
    {
        return client.GetEntitlementsAsync(limit, afterId, beforeId, excludeEnded, guildId, userId, skuIds, options,
            excludeDeleted);
    }

    public Task<IReadOnlyCollection<SKU>> GetSKUsAsync(RequestOptions? options = null)
    {
        return client.GetSKUsAsync(options);
    }

    public Task ConsumeEntitlementAsync(ulong entitlementId, RequestOptions? options = null)
    {
        return client.ConsumeEntitlementAsync(entitlementId, options);
    }

    public IAsyncEnumerable<IReadOnlyCollection<ISubscription>> GetSKUSubscriptionsAsync(ulong skuId, int limit = 100,
        ulong? afterId = null, ulong? beforeId = null,
        ulong? userId = null, RequestOptions? options = null)
    {
        return client.GetSKUSubscriptionsAsync(skuId, limit, afterId, beforeId, userId, options);
    }

    public Task<ISubscription> GetSKUSubscriptionAsync(ulong skuId, ulong subscriptionId,
        RequestOptions? options = null)
    {
        return _clientAsContract.GetSKUSubscriptionAsync(skuId, subscriptionId, options);
    }

    public Task<Emote> GetApplicationEmoteAsync(ulong emoteId, RequestOptions? options = null)
    {
        return client.GetApplicationEmoteAsync(emoteId, options);
    }

    public Task<IReadOnlyCollection<Emote>> GetApplicationEmotesAsync(RequestOptions? options = null)
    {
        return client.GetApplicationEmotesAsync(options);
    }

    public Task<Emote> ModifyApplicationEmoteAsync(ulong emoteId, Action<ApplicationEmoteProperties> args,
        RequestOptions? options = null)
    {
        return client.ModifyApplicationEmoteAsync(emoteId, args, options);
    }

    public Task<Emote> CreateApplicationEmoteAsync(string name, Image image, RequestOptions? options = null)
    {
        return client.CreateApplicationEmoteAsync(name, image, options);
    }

    public Task DeleteApplicationEmoteAsync(ulong emoteId, RequestOptions? options = null)
    {
        return client.DeleteApplicationEmoteAsync(emoteId, options);
    }

    public ConnectionState ConnectionState => client.ConnectionState;

    public ISelfUser CurrentUser => _clientAsContract.CurrentUser;

    public TokenType TokenType => client.TokenType;

    public bool IsValidHttpInteraction(string publicKey, string signature, string timestamp, string body)
    {
        return client.IsValidHttpInteraction(publicKey, signature, timestamp, body);
    }

    public Task<RestInteraction> ParseHttpInteractionAsync(string publicKey, string signature, string timestamp,
        string body,
        Func<InteractionProperties, bool> doApiCallOnCreation)
    {
        return client.ParseHttpInteractionAsync(publicKey, signature, timestamp, body, doApiCallOnCreation);
    }

    public Task<RestSelfUser> GetCurrentUserAsync(RequestOptions? options = null)
    {
        return client.GetCurrentUserAsync(options);
    }

    public Task<RestGuildUser> GetCurrentUserGuildMemberAsync(ulong guildId, RequestOptions? options = null)
    {
        return client.GetCurrentUserGuildMemberAsync(guildId, options);
    }

    public Task<RestApplication> GetCurrentBotInfoAsync(RequestOptions? options = null)
    {
        return client.GetCurrentBotInfoAsync(options);
    }

    public Task<RestApplication> ModifyCurrentBotApplicationAsync(Action<ModifyApplicationProperties> args,
        RequestOptions? options = null)
    {
        return client.ModifyCurrentBotApplicationAsync(args, options);
    }

    public Task<RestGuildWidget?> GetGuildWidgetAsync(ulong id, RequestOptions? options = null)
    {
        return client.GetGuildWidgetAsync(id, options);
    }

    public IAsyncEnumerable<IReadOnlyCollection<RestUserGuild>> GetGuildSummariesAsync(RequestOptions? options = null)
    {
        return client.GetGuildSummariesAsync(options);
    }

    public Task<RestGuildUser> GetGuildUserAsync(ulong guildId, ulong id, RequestOptions? options = null)
    {
        return client.GetGuildUserAsync(guildId, id, options);
    }

    public Task<RestGlobalCommand> CreateGlobalCommand(ApplicationCommandProperties properties,
        RequestOptions? options = null)
    {
        return client.CreateGlobalCommand(properties, options);
    }

    public Task<RestGuildCommand> CreateGuildCommand(ApplicationCommandProperties properties, ulong guildId,
        RequestOptions? options = null)
    {
        return client.CreateGuildCommand(properties, guildId, options);
    }

    public Task<IReadOnlyCollection<RestGlobalCommand>> GetGlobalApplicationCommands(bool withLocalizations,
        string locale, RequestOptions? options = null)
    {
        return client.GetGlobalApplicationCommands(withLocalizations, locale, options);
    }

    public Task<IReadOnlyCollection<RestGuildCommand>> GetGuildApplicationCommands(ulong guildId,
        bool withLocalizations, string locale, RequestOptions? options = null)
    {
        return client.GetGuildApplicationCommands(guildId, withLocalizations, locale, options);
    }

    public Task<IReadOnlyCollection<RestGlobalCommand>> BulkOverwriteGlobalCommands(
        ApplicationCommandProperties[] commandProperties, RequestOptions? options = null)
    {
        return client.BulkOverwriteGlobalCommands(commandProperties, options);
    }

    public Task<IReadOnlyCollection<RestGuildCommand>> BulkOverwriteGuildCommands(
        ApplicationCommandProperties[] commandProperties, ulong guildId, RequestOptions? options = null)
    {
        return client.BulkOverwriteGuildCommands(commandProperties, guildId, options);
    }

    public Task<IReadOnlyCollection<GuildApplicationCommandPermission>> BatchEditGuildCommandPermissions(ulong guildId,
        IDictionary<ulong, ApplicationCommandPermission[]> permissions, RequestOptions? options = null)
    {
        return client.BatchEditGuildCommandPermissions(guildId, permissions, options);
    }

    public Task DeleteAllGlobalCommandsAsync(RequestOptions? options = null)
    {
        return client.DeleteAllGlobalCommandsAsync(options);
    }

    public Task AddRoleAsync(ulong guildId, ulong userId, ulong roleId, RequestOptions? options = null)
    {
        return client.AddRoleAsync(guildId, userId, roleId, options);
    }

    public Task RemoveRoleAsync(ulong guildId, ulong userId, ulong roleId, RequestOptions? options = null)
    {
        return client.RemoveRoleAsync(guildId, userId, roleId, options);
    }

    public Task AddReactionAsync(ulong channelId, ulong messageId, IEmote emote, RequestOptions? options = null)
    {
        return client.AddReactionAsync(channelId, messageId, emote, options);
    }

    public Task RemoveReactionAsync(ulong channelId, ulong messageId, ulong userId, IEmote emote,
        RequestOptions? options = null)
    {
        return client.RemoveReactionAsync(channelId, messageId, userId, emote, options);
    }

    public Task RemoveAllReactionsAsync(ulong channelId, ulong messageId, RequestOptions? options = null)
    {
        return client.RemoveAllReactionsAsync(channelId, messageId, options);
    }

    public Task RemoveAllReactionsForEmoteAsync(ulong channelId, ulong messageId, IEmote emote,
        RequestOptions? options = null)
    {
        return client.RemoveAllReactionsForEmoteAsync(channelId, messageId, emote, options);
    }

    public Task<IReadOnlyCollection<RoleConnectionMetadata>> GetRoleConnectionMetadataRecordsAsync(
        RequestOptions? options = null)
    {
        return client.GetRoleConnectionMetadataRecordsAsync(options);
    }

    public Task<IReadOnlyCollection<RoleConnectionMetadata>> ModifyRoleConnectionMetadataRecordsAsync(
        ICollection<RoleConnectionMetadataProperties> metadata, RequestOptions? options = null)
    {
        return client.ModifyRoleConnectionMetadataRecordsAsync(metadata, options);
    }

    public Task<RoleConnection> GetUserApplicationRoleConnectionAsync(ulong applicationId,
        RequestOptions? options = null)
    {
        return client.GetUserApplicationRoleConnectionAsync(applicationId, options);
    }

    public Task<RoleConnection> ModifyUserApplicationRoleConnectionAsync(ulong applicationId,
        RoleConnectionProperties roleConnection, RequestOptions? options = null)
    {
        return client.ModifyUserApplicationRoleConnectionAsync(applicationId, roleConnection, options);
    }

    public Task LoginAsync(TokenType tokenType, string token, bool validateToken)
    {
        return client.LoginAsync(tokenType, token, validateToken);
    }

    public Task LogoutAsync()
    {
        return client.LogoutAsync();
    }

    public LoginState LoginState => client.LoginState;

    public event Func<LogMessage, Task> Log
    {
        add => client.Log += value;
        remove => client.Log -= value;
    }

    public event Func<Task> LoggedIn
    {
        add => client.LoggedIn += value;
        remove => client.LoggedIn -= value;
    }

    public event Func<Task> LoggedOut
    {
        add => client.LoggedOut += value;
        remove => client.LoggedOut -= value;
    }

    public event Func<string, string, double, Task> SentRequest
    {
        add => client.SentRequest += value;
        remove => client.SentRequest -= value;
    }
}