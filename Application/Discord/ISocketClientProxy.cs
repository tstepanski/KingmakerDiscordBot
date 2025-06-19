using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace KingmakerDiscordBot.Application.Discord;

internal interface ISocketClientProxy : IDiscordClient
{
    IDiscordRestClientProxy Rest { get; }
    
    event Func<Task> Connected;
    
    event Func<Task> Ready;
    
    event Func<Exception, Task> Disconnected;

    event Func<int, int, Task> LatencyUpdated;

    event Func<LogMessage, Task> Log;

    event Func<Task> LoggedIn;

    event Func<Task> LoggedOut;

    event Func<string, string, double, Task> SentRequest;

    event Func<SocketChannel, Task> ChannelCreated;

    event Func<SocketChannel, Task> ChannelDestroyed;

    event Func<SocketChannel, SocketChannel, Task> ChannelUpdated;

    event Func<Cacheable<SocketVoiceChannel, ulong>, string, string, Task> VoiceChannelStatusUpdated;

    event Func<SocketMessage, Task> MessageReceived;

    event Func<Cacheable<IMessage, ulong>, Cacheable<IMessageChannel, ulong>, Task> MessageDeleted;

    event Func<IReadOnlyCollection<Cacheable<IMessage, ulong>>, Cacheable<IMessageChannel, ulong>, Task>
        MessagesBulkDeleted;

    event Func<Cacheable<IMessage, ulong>, SocketMessage, ISocketMessageChannel, Task> MessageUpdated;

    event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, SocketReaction, Task> ReactionAdded;

    event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, SocketReaction, Task> ReactionRemoved;

    event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, Task> ReactionsCleared;

    event Func<Cacheable<IUserMessage, ulong>, Cacheable<IMessageChannel, ulong>, IEmote, Task>
        ReactionsRemovedForEmote;

    event Func<Cacheable<IUser, ulong>, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong>,
        Cacheable<IUserMessage, ulong>, Cacheable<SocketGuild, RestGuild, IGuild, ulong>?, ulong, Task> PollVoteAdded;

    event Func<Cacheable<IUser, ulong>, Cacheable<ISocketMessageChannel, IRestMessageChannel, IMessageChannel, ulong>,
        Cacheable<IUserMessage, ulong>, Cacheable<SocketGuild, RestGuild, IGuild, ulong>?, ulong, Task> PollVoteRemoved;

    event Func<SocketRole, Task> RoleCreated;

    event Func<SocketRole, Task> RoleDeleted;

    event Func<SocketRole, SocketRole, Task> RoleUpdated;

    event Func<SocketGuild, Task> JoinedGuild;

    event Func<SocketGuild, Task> LeftGuild;

    event Func<SocketGuild, Task> GuildAvailable;

    event Func<SocketGuild, Task> GuildUnavailable;

    event Func<SocketGuild, Task> GuildMembersDownloaded;

    event Func<SocketGuild, SocketGuild, Task> GuildUpdated;

    event Func<Cacheable<SocketGuildUser, ulong>, SocketGuild, Task> GuildJoinRequestDeleted;

    event Func<SocketGuildEvent, Task> GuildScheduledEventCreated;

    event Func<Cacheable<SocketGuildEvent, ulong>, SocketGuildEvent, Task> GuildScheduledEventUpdated;

    event Func<SocketGuildEvent, Task> GuildScheduledEventCancelled;

    event Func<SocketGuildEvent, Task> GuildScheduledEventCompleted;

    event Func<SocketGuildEvent, Task> GuildScheduledEventStarted;

    event Func<Cacheable<SocketUser, RestUser, IUser, ulong>, SocketGuildEvent, Task> GuildScheduledEventUserAdd;

    event Func<Cacheable<SocketUser, RestUser, IUser, ulong>, SocketGuildEvent, Task> GuildScheduledEventUserRemove;

    event Func<IIntegration, Task> IntegrationCreated;

    event Func<IIntegration, Task> IntegrationUpdated;

    event Func<IGuild, ulong, Optional<ulong>, Task> IntegrationDeleted;

    event Func<SocketGuildUser, Task> UserJoined;

    event Func<SocketGuild, SocketUser, Task> UserLeft;

    event Func<SocketUser, SocketGuild, Task> UserBanned;

    event Func<SocketUser, SocketGuild, Task> UserUnbanned;

    event Func<SocketUser, SocketUser, Task> UserUpdated;

    event Func<Cacheable<SocketGuildUser, ulong>, SocketGuildUser, Task> GuildMemberUpdated;

    event Func<SocketUser, SocketVoiceState, SocketVoiceState, Task> UserVoiceStateUpdated;

    event Func<SocketVoiceServer, Task> VoiceServerUpdated;

    event Func<SocketSelfUser, SocketSelfUser, Task> CurrentUserUpdated;

    event Func<Cacheable<IUser, ulong>, Cacheable<IMessageChannel, ulong>, Task> UserIsTyping;

    event Func<SocketGroupUser, Task> RecipientAdded;

    event Func<SocketGroupUser, Task> RecipientRemoved;

    event Func<SocketUser, SocketPresence, SocketPresence, Task> PresenceUpdated;

    event Func<SocketInvite, Task> InviteCreated;

    event Func<SocketGuildChannel, string, Task> InviteDeleted;

    event Func<SocketInteraction, Task> InteractionCreated;

    event Func<SocketMessageComponent, Task> ButtonExecuted;

    event Func<SocketMessageComponent, Task> SelectMenuExecuted;

    event Func<SocketSlashCommand, Task> SlashCommandExecuted;

    event Func<SocketUserCommand, Task> UserCommandExecuted;

    event Func<SocketMessageCommand, Task> MessageCommandExecuted;

    event Func<SocketAutocompleteInteraction, Task> AutocompleteExecuted;

    event Func<SocketModal, Task> ModalSubmitted;

    event Func<SocketApplicationCommand, Task> ApplicationCommandCreated;

    event Func<SocketApplicationCommand, Task> ApplicationCommandUpdated;

    event Func<SocketApplicationCommand, Task> ApplicationCommandDeleted;

    event Func<SocketThreadChannel, Task> ThreadCreated;

    event Func<Cacheable<SocketThreadChannel, ulong>, SocketThreadChannel, Task> ThreadUpdated;

    event Func<Cacheable<SocketThreadChannel, ulong>, Task> ThreadDeleted;

    event Func<SocketThreadUser, Task> ThreadMemberJoined;

    event Func<SocketThreadUser, Task> ThreadMemberLeft;

    event Func<SocketStageChannel, Task> StageStarted;

    event Func<SocketStageChannel, Task> StageEnded;

    event Func<SocketStageChannel, SocketStageChannel, Task> StageUpdated;

    event Func<SocketStageChannel, SocketGuildUser, Task> RequestToSpeak;

    event Func<SocketStageChannel, SocketGuildUser, Task> SpeakerAdded;

    event Func<SocketStageChannel, SocketGuildUser, Task> SpeakerRemoved;

    event Func<SocketCustomSticker, Task> GuildStickerCreated;

    event Func<SocketCustomSticker, SocketCustomSticker, Task> GuildStickerUpdated;

    event Func<SocketCustomSticker, Task> GuildStickerDeleted;

    event Func<SocketGuild, SocketChannel, Task> WebhooksUpdated;

    event Func<SocketAuditLogEntry, SocketGuild, Task> AuditLogCreated;

    event Func<SocketAutoModRule, Task> AutoModRuleCreated;

    event Func<Cacheable<SocketAutoModRule, ulong>, SocketAutoModRule, Task> AutoModRuleUpdated;

    event Func<SocketAutoModRule, Task> AutoModRuleDeleted;

    event Func<SocketGuild, AutoModRuleAction, AutoModActionExecutedData, Task> AutoModActionExecuted;

    event Func<SocketEntitlement, Task> EntitlementCreated;

    event Func<Cacheable<SocketEntitlement, ulong>, SocketEntitlement, Task> EntitlementUpdated;

    event Func<Cacheable<SocketEntitlement, ulong>, Task> EntitlementDeleted;

    event Func<SocketSubscription, Task> SubscriptionCreated;

    event Func<Cacheable<SocketSubscription, ulong>, SocketSubscription, Task> SubscriptionUpdated;

    event Func<Cacheable<SocketSubscription, ulong>, Task> SubscriptionDeleted;

    event Func<string, JToken, Task> UnknownDispatchReceived;
}