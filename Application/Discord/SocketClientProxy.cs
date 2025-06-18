using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace KingmakerDiscordBot.Application.Discord;

internal sealed class SocketClientProxy(DiscordSocketClient client) : ISocketClientProxy
{
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
        return client.StartAsync();
    }

    public Task StopAsync()
    {
        return client.StopAsync();
    }

    public async Task<IApplication> GetApplicationInfoAsync(RequestOptions? options = null)
    {
        return await client.GetApplicationInfoAsync(options);
    }

    public async Task<IChannel> GetChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetChannelAsync(id, mode, options);
    }

    public async Task<IReadOnlyCollection<IPrivateChannel>> GetPrivateChannelsAsync(
        CacheMode mode = CacheMode.AllowDownload, RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetPrivateChannelsAsync(mode, options);
    }

    public async Task<IReadOnlyCollection<IDMChannel>> GetDMChannelsAsync(CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetDMChannelsAsync(mode, options);
    }

    public async Task<IReadOnlyCollection<IGroupChannel>> GetGroupChannelsAsync(
        CacheMode mode = CacheMode.AllowDownload, RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetGroupChannelsAsync(mode, options);
    }

    public async Task<IReadOnlyCollection<IConnection>> GetConnectionsAsync(RequestOptions? options = null)
    {
        return await client.GetConnectionsAsync(options);
    }

    public async Task<IApplicationCommand> GetGlobalApplicationCommandAsync(ulong id, RequestOptions? options = null)
    {
        return await client.GetGlobalApplicationCommandAsync(id, options);
    }

    public async Task<IReadOnlyCollection<IApplicationCommand>> GetGlobalApplicationCommandsAsync(
        bool withLocalizations = false, string? locale = null, RequestOptions? options = null)
    {
        return await client.GetGlobalApplicationCommandsAsync(withLocalizations, locale, options);
    }

    public async Task<IApplicationCommand> CreateGlobalApplicationCommand(ApplicationCommandProperties properties,
        RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).CreateGlobalApplicationCommand(properties, options);
    }

    public async Task<IReadOnlyCollection<IApplicationCommand>> BulkOverwriteGlobalApplicationCommand(
        ApplicationCommandProperties[] properties, RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).BulkOverwriteGlobalApplicationCommand(properties, options);
    }

    public async Task<IGuild> GetGuildAsync(ulong id, CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetGuildAsync(id, mode, options);
    }

    public async Task<IReadOnlyCollection<IGuild>> GetGuildsAsync(CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetGuildsAsync(mode, options);
    }

    public async Task<IGuild> CreateGuildAsync(string name, IVoiceRegion region, Stream? jpegIcon = null,
        RequestOptions? options = null)
    {
        return await client.CreateGuildAsync(name, region, jpegIcon, options);
    }

    public async Task<IInvite> GetInviteAsync(string inviteId, RequestOptions? options = null)
    {
        return await client.GetInviteAsync(inviteId, options);
    }

    public async Task<IUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload,
        RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetUserAsync(id, mode, options);
    }

    public async Task<IUser> GetUserAsync(string username, string discriminator, RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetUserAsync(username, discriminator, options);
    }

    public async Task<IReadOnlyCollection<IVoiceRegion>> GetVoiceRegionsAsync(RequestOptions? options = null)
    {
        return await client.GetVoiceRegionsAsync(options);
    }

    public async Task<IVoiceRegion> GetVoiceRegionAsync(string id, RequestOptions? options = null)
    {
        return await client.GetVoiceRegionAsync(id, options);
    }

    public async Task<IWebhook> GetWebhookAsync(ulong id, RequestOptions? options = null)
    {
        return await ((IDiscordClient)client).GetWebhookAsync(id, options);
    }

    public Task<int> GetRecommendedShardCountAsync(RequestOptions? options = null)
    {
        return client.GetRecommendedShardCountAsync(options);
    }

    public Task<BotGateway> GetBotGatewayAsync(RequestOptions? options = null)
    {
        return client.GetBotGatewayAsync(options);
    }

    public async Task<IEntitlement> CreateTestEntitlementAsync(ulong skuId, ulong ownerId,
        SubscriptionOwnerType ownerType, RequestOptions? options = null)
    {
        return await client.CreateTestEntitlementAsync(skuId, ownerId, ownerType, options);
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

    public async Task<ISubscription> GetSKUSubscriptionAsync(ulong skuId, ulong subscriptionId,
        RequestOptions? options = null)
    {
        return await client.GetSKUSubscriptionAsync(skuId, subscriptionId, options);
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

    public ISelfUser CurrentUser => ((IDiscordClient)client).CurrentUser;

    public TokenType TokenType => client.TokenType;

    public event Func<Task> Connected
    {
        add => client.Connected += value;
        remove => client.Connected -= value;
    }

    public event Func<Task> Ready
    {
        add => client.Ready += value;
        remove => client.Ready -= value;
    }

    public event Func<Exception, Task> Disconnected
    {
        add => client.Disconnected += value;
        remove => client.Disconnected -= value;
    }

    public event Func<int, int, Task> LatencyUpdated
    {
        add => client.LatencyUpdated += value;
        remove => client.LatencyUpdated -= value;
    }

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

    public event Func<SocketChannel, Task> ChannelCreated
    {
        add => client.ChannelCreated += value;
        remove => client.ChannelCreated -= value;
    }

    public event Func<SocketChannel, Task> ChannelDestroyed
    {
        add => client.ChannelDestroyed += value;
        remove => client.ChannelDestroyed -= value;
    }

    public event Func<SocketChannel, SocketChannel, Task> ChannelUpdated
    {
        add => client.ChannelUpdated += value;
        remove => client.ChannelUpdated -= value;
    }

    public event Func<Cacheable<SocketVoiceChannel, ulong>, string, string, Task> VoiceChannelStatusUpdated
    {
        add => client.VoiceChannelStatusUpdated += value;
        remove => client.VoiceChannelStatusUpdated -= value;
    }

    public event Func<SocketMessage, Task> MessageReceived
    {
        add => client.MessageReceived += value;
        remove => client.MessageReceived -= value;
    }

    public event Func<Cacheable<IMessage, ulong>, Cacheable<IMessageChannel, ulong>, Task> MessageDeleted
    {
        add => client.MessageDeleted += value;
        remove => client.MessageDeleted -= value;
    }

    public event Func<IReadOnlyCollection<Cacheable<IMessage, ulong>>, Cacheable<IMessageChannel, ulong>, Task>
        MessagesBulkDeleted
        {
            add => client.MessagesBulkDeleted += value;
            remove => client.MessagesBulkDeleted -= value;
        }

    public event Func<Cacheable<IMessage, ulong>, SocketMessage, ISocketMessageChannel, Task> MessageUpdated
    {
        add => client.MessageUpdated += value;
        remove => client.MessageUpdated -= value;
    }

    public event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, SocketReaction, Task>
        ReactionAdded
        {
            add => client.ReactionAdded += value;
            remove => client.ReactionAdded -= value;
        }

    public event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, SocketReaction, Task>
        ReactionRemoved
        {
            add => client.ReactionRemoved += value;
            remove => client.ReactionRemoved -= value;
        }

    public event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, Task> ReactionsCleared
    {
        add => client.ReactionsCleared += value;
        remove => client.ReactionsCleared -= value;
    }

    public event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, IEmote, Task>
        ReactionsRemovedForEmote
        {
            add => client.ReactionsRemovedForEmote += value;
            remove => client.ReactionsRemovedForEmote -= value;
        }

    public event
        Func<Cacheable<IUser, ulong>, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong>,
            Cacheable<IUserMessage, ulong>, Cacheable<SocketGuild, RestGuild, IGuild, ulong>?, ulong, Task>
        PollVoteAdded
        {
            add => client.PollVoteAdded += value;
            remove => client.PollVoteAdded -= value;
        }

    public event
        Func<Cacheable<IUser, ulong>, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong>,
            Cacheable<IUserMessage, ulong>, Cacheable<SocketGuild, RestGuild, IGuild, ulong>?, ulong, Task>
        PollVoteRemoved
        {
            add => client.PollVoteRemoved += value;
            remove => client.PollVoteRemoved -= value;
        }

    public event Func<SocketRole, Task> RoleCreated
    {
        add => client.RoleCreated += value;
        remove => client.RoleCreated -= value;
    }

    public event Func<SocketRole, Task> RoleDeleted
    {
        add => client.RoleDeleted += value;
        remove => client.RoleDeleted -= value;
    }

    public event Func<SocketRole, SocketRole, Task> RoleUpdated
    {
        add => client.RoleUpdated += value;
        remove => client.RoleUpdated -= value;
    }

    public event Func<SocketGuild, Task> JoinedGuild
    {
        add => client.JoinedGuild += value;
        remove => client.JoinedGuild -= value;
    }

    public event Func<SocketGuild, Task> LeftGuild
    {
        add => client.LeftGuild += value;
        remove => client.LeftGuild -= value;
    }

    public event Func<SocketGuild, Task> GuildAvailable
    {
        add => client.GuildAvailable += value;
        remove => client.GuildAvailable -= value;
    }

    public event Func<SocketGuild, Task> GuildUnavailable
    {
        add => client.GuildUnavailable += value;
        remove => client.GuildUnavailable -= value;
    }

    public event Func<SocketGuild, Task> GuildMembersDownloaded
    {
        add => client.GuildMembersDownloaded += value;
        remove => client.GuildMembersDownloaded -= value;
    }

    public event Func<SocketGuild, SocketGuild, Task> GuildUpdated
    {
        add => client.GuildUpdated += value;
        remove => client.GuildUpdated -= value;
    }

    public event Func<Cacheable<SocketGuildUser, ulong>, SocketGuild, Task> GuildJoinRequestDeleted
    {
        add => client.GuildJoinRequestDeleted += value;
        remove => client.GuildJoinRequestDeleted -= value;
    }

    public event Func<SocketGuildEvent, Task> GuildScheduledEventCreated
    {
        add => client.GuildScheduledEventCreated += value;
        remove => client.GuildScheduledEventCreated -= value;
    }

    public event Func<Cacheable<SocketGuildEvent, ulong>, SocketGuildEvent, Task> GuildScheduledEventUpdated
    {
        add => client.GuildScheduledEventUpdated += value;
        remove => client.GuildScheduledEventUpdated -= value;
    }

    public event Func<SocketGuildEvent, Task> GuildScheduledEventCancelled
    {
        add => client.GuildScheduledEventCancelled += value;
        remove => client.GuildScheduledEventCancelled -= value;
    }

    public event Func<SocketGuildEvent, Task> GuildScheduledEventCompleted
    {
        add => client.GuildScheduledEventCompleted += value;
        remove => client.GuildScheduledEventCompleted -= value;
    }

    public event Func<SocketGuildEvent, Task> GuildScheduledEventStarted
    {
        add => client.GuildScheduledEventStarted += value;
        remove => client.GuildScheduledEventStarted -= value;
    }

    public event Func<Cacheable<SocketUser, RestUser, IUser, ulong>, SocketGuildEvent, Task> GuildScheduledEventUserAdd
    {
        add => client.GuildScheduledEventUserAdd += value;
        remove => client.GuildScheduledEventUserAdd -= value;
    }

    public event Func<Cacheable<SocketUser, RestUser, IUser, ulong>, SocketGuildEvent, Task>
        GuildScheduledEventUserRemove
        {
            add => client.GuildScheduledEventUserRemove += value;
            remove => client.GuildScheduledEventUserRemove -= value;
        }

    public event Func<IIntegration, Task> IntegrationCreated
    {
        add => client.IntegrationCreated += value;
        remove => client.IntegrationCreated -= value;
    }

    public event Func<IIntegration, Task> IntegrationUpdated
    {
        add => client.IntegrationUpdated += value;
        remove => client.IntegrationUpdated -= value;
    }

    public event Func<IGuild, ulong, Optional<ulong>, Task> IntegrationDeleted
    {
        add => client.IntegrationDeleted += value;
        remove => client.IntegrationDeleted -= value;
    }

    public event Func<SocketGuildUser, Task> UserJoined
    {
        add => client.UserJoined += value;
        remove => client.UserJoined -= value;
    }

    public event Func<SocketGuild, SocketUser, Task> UserLeft
    {
        add => client.UserLeft += value;
        remove => client.UserLeft -= value;
    }

    public event Func<SocketUser, SocketGuild, Task> UserBanned
    {
        add => client.UserBanned += value;
        remove => client.UserBanned -= value;
    }

    public event Func<SocketUser, SocketGuild, Task> UserUnbanned
    {
        add => client.UserUnbanned += value;
        remove => client.UserUnbanned -= value;
    }

    public event Func<SocketUser, SocketUser, Task> UserUpdated
    {
        add => client.UserUpdated += value;
        remove => client.UserUpdated -= value;
    }

    public event Func<Cacheable<SocketGuildUser, ulong>, SocketGuildUser, Task> GuildMemberUpdated
    {
        add => client.GuildMemberUpdated += value;
        remove => client.GuildMemberUpdated -= value;
    }

    public event Func<SocketUser, SocketVoiceState, SocketVoiceState, Task> UserVoiceStateUpdated
    {
        add => client.UserVoiceStateUpdated += value;
        remove => client.UserVoiceStateUpdated -= value;
    }

    public event Func<SocketVoiceServer, Task> VoiceServerUpdated
    {
        add => client.VoiceServerUpdated += value;
        remove => client.VoiceServerUpdated -= value;
    }

    public event Func<SocketSelfUser, SocketSelfUser, Task> CurrentUserUpdated
    {
        add => client.CurrentUserUpdated += value;
        remove => client.CurrentUserUpdated -= value;
    }

    public event Func<Cacheable<IUser, ulong>, Cacheable<IMessageChannel, ulong>, Task> UserIsTyping
    {
        add => client.UserIsTyping += value;
        remove => client.UserIsTyping -= value;
    }

    public event Func<SocketGroupUser, Task> RecipientAdded
    {
        add => client.RecipientAdded += value;
        remove => client.RecipientAdded -= value;
    }

    public event Func<SocketGroupUser, Task> RecipientRemoved
    {
        add => client.RecipientRemoved += value;
        remove => client.RecipientRemoved -= value;
    }

    public event Func<SocketUser, SocketPresence, SocketPresence, Task> PresenceUpdated
    {
        add => client.PresenceUpdated += value;
        remove => client.PresenceUpdated -= value;
    }

    public event Func<SocketInvite, Task> InviteCreated
    {
        add => client.InviteCreated += value;
        remove => client.InviteCreated -= value;
    }

    public event Func<SocketGuildChannel, string, Task> InviteDeleted
    {
        add => client.InviteDeleted += value;
        remove => client.InviteDeleted -= value;
    }

    public event Func<SocketInteraction, Task> InteractionCreated
    {
        add => client.InteractionCreated += value;
        remove => client.InteractionCreated -= value;
    }

    public event Func<SocketMessageComponent, Task> ButtonExecuted
    {
        add => client.ButtonExecuted += value;
        remove => client.ButtonExecuted -= value;
    }

    public event Func<SocketMessageComponent, Task> SelectMenuExecuted
    {
        add => client.SelectMenuExecuted += value;
        remove => client.SelectMenuExecuted -= value;
    }

    public event Func<SocketSlashCommand, Task> SlashCommandExecuted
    {
        add => client.SlashCommandExecuted += value;
        remove => client.SlashCommandExecuted -= value;
    }

    public event Func<SocketUserCommand, Task> UserCommandExecuted
    {
        add => client.UserCommandExecuted += value;
        remove => client.UserCommandExecuted -= value;
    }

    public event Func<SocketMessageCommand, Task> MessageCommandExecuted
    {
        add => client.MessageCommandExecuted += value;
        remove => client.MessageCommandExecuted -= value;
    }

    public event Func<SocketAutocompleteInteraction, Task> AutocompleteExecuted
    {
        add => client.AutocompleteExecuted += value;
        remove => client.AutocompleteExecuted -= value;
    }

    public event Func<SocketModal, Task> ModalSubmitted
    {
        add => client.ModalSubmitted += value;
        remove => client.ModalSubmitted -= value;
    }

    public event Func<SocketApplicationCommand, Task> ApplicationCommandCreated
    {
        add => client.ApplicationCommandCreated += value;
        remove => client.ApplicationCommandCreated -= value;
    }

    public event Func<SocketApplicationCommand, Task> ApplicationCommandUpdated
    {
        add => client.ApplicationCommandUpdated += value;
        remove => client.ApplicationCommandUpdated -= value;
    }

    public event Func<SocketApplicationCommand, Task> ApplicationCommandDeleted
    {
        add => client.ApplicationCommandDeleted += value;
        remove => client.ApplicationCommandDeleted -= value;
    }

    public event Func<SocketThreadChannel, Task> ThreadCreated
    {
        add => client.ThreadCreated += value;
        remove => client.ThreadCreated -= value;
    }

    public event Func<Cacheable<SocketThreadChannel, ulong>, SocketThreadChannel, Task> ThreadUpdated
    {
        add => client.ThreadUpdated += value;
        remove => client.ThreadUpdated -= value;
    }

    public event Func<Cacheable<SocketThreadChannel, ulong>, Task> ThreadDeleted
    {
        add => client.ThreadDeleted += value;
        remove => client.ThreadDeleted -= value;
    }

    public event Func<SocketThreadUser, Task> ThreadMemberJoined
    {
        add => client.ThreadMemberJoined += value;
        remove => client.ThreadMemberJoined -= value;
    }

    public event Func<SocketThreadUser, Task> ThreadMemberLeft
    {
        add => client.ThreadMemberLeft += value;
        remove => client.ThreadMemberLeft -= value;
    }

    public event Func<SocketStageChannel, Task> StageStarted
    {
        add => client.StageStarted += value;
        remove => client.StageStarted -= value;
    }

    public event Func<SocketStageChannel, Task> StageEnded
    {
        add => client.StageEnded += value;
        remove => client.StageEnded -= value;
    }

    public event Func<SocketStageChannel, SocketStageChannel, Task> StageUpdated
    {
        add => client.StageUpdated += value;
        remove => client.StageUpdated -= value;
    }

    public event Func<SocketStageChannel, SocketGuildUser, Task> RequestToSpeak
    {
        add => client.RequestToSpeak += value;
        remove => client.RequestToSpeak -= value;
    }

    public event Func<SocketStageChannel, SocketGuildUser, Task> SpeakerAdded
    {
        add => client.SpeakerAdded += value;
        remove => client.SpeakerAdded -= value;
    }

    public event Func<SocketStageChannel, SocketGuildUser, Task> SpeakerRemoved
    {
        add => client.SpeakerRemoved += value;
        remove => client.SpeakerRemoved -= value;
    }

    public event Func<SocketCustomSticker, Task> GuildStickerCreated
    {
        add => client.GuildStickerCreated += value;
        remove => client.GuildStickerCreated -= value;
    }

    public event Func<SocketCustomSticker, SocketCustomSticker, Task> GuildStickerUpdated
    {
        add => client.GuildStickerUpdated += value;
        remove => client.GuildStickerUpdated -= value;
    }

    public event Func<SocketCustomSticker, Task> GuildStickerDeleted
    {
        add => client.GuildStickerDeleted += value;
        remove => client.GuildStickerDeleted -= value;
    }

    public event Func<SocketGuild, SocketChannel, Task> WebhooksUpdated
    {
        add => client.WebhooksUpdated += value;
        remove => client.WebhooksUpdated -= value;
    }

    public event Func<SocketAuditLogEntry, SocketGuild, Task> AuditLogCreated
    {
        add => client.AuditLogCreated += value;
        remove => client.AuditLogCreated -= value;
    }

    public event Func<SocketAutoModRule, Task> AutoModRuleCreated
    {
        add => client.AutoModRuleCreated += value;
        remove => client.AutoModRuleCreated -= value;
    }

    public event Func<Cacheable<SocketAutoModRule, ulong>, SocketAutoModRule, Task> AutoModRuleUpdated
    {
        add => client.AutoModRuleUpdated += value;
        remove => client.AutoModRuleUpdated -= value;
    }

    public event Func<SocketAutoModRule, Task> AutoModRuleDeleted
    {
        add => client.AutoModRuleDeleted += value;
        remove => client.AutoModRuleDeleted -= value;
    }

    public event Func<SocketGuild, AutoModRuleAction, AutoModActionExecutedData, Task> AutoModActionExecuted
    {
        add => client.AutoModActionExecuted += value;
        remove => client.AutoModActionExecuted -= value;
    }

    public event Func<SocketEntitlement, Task> EntitlementCreated
    {
        add => client.EntitlementCreated += value;
        remove => client.EntitlementCreated -= value;
    }

    public event Func<Cacheable<SocketEntitlement, ulong>, SocketEntitlement, Task> EntitlementUpdated
    {
        add => client.EntitlementUpdated += value;
        remove => client.EntitlementUpdated -= value;
    }

    public event Func<Cacheable<SocketEntitlement, ulong>, Task> EntitlementDeleted
    {
        add => client.EntitlementDeleted += value;
        remove => client.EntitlementDeleted -= value;
    }

    public event Func<SocketSubscription, Task> SubscriptionCreated
    {
        add => client.SubscriptionCreated += value;
        remove => client.SubscriptionCreated -= value;
    }

    public event Func<Cacheable<SocketSubscription, ulong>, SocketSubscription, Task> SubscriptionUpdated
    {
        add => client.SubscriptionUpdated += value;
        remove => client.SubscriptionUpdated -= value;
    }

    public event Func<Cacheable<SocketSubscription, ulong>, Task> SubscriptionDeleted
    {
        add => client.SubscriptionDeleted += value;
        remove => client.SubscriptionDeleted -= value;
    }

    public event Func<string, JToken, Task> UnknownDispatchReceived
    {
        add => client.UnknownDispatchReceived += value;
        remove => client.UnknownDispatchReceived -= value;
    }
}