using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using KingmakerDiscordBot.Application.Discord;
using KingmakerDiscordBot.Application.Listeners.Contracts;

namespace KingmakerDiscordBot.Application.Listeners;

internal sealed partial class ListenerRegistrar(IEnumerable<IListener> listeners, ILogger<ListenerRegistrar> logger) : 
    IListenerRegistrar
{
    private static readonly Regex NameFormat = NameFormatFactory();
    private static readonly string ListenerNamespace = typeof(IListener).Namespace!;
    private static readonly Type ClientProxyType = typeof(IDiscordRestClientProxy);
    private static readonly Type CancellationTokenType = typeof(CancellationToken);

    private static readonly ImmutableSortedDictionary<string, EventInfo> EventsByName = typeof(ISocketClientProxy)
        .GetEvents(BindingFlags.Public | BindingFlags.Instance)
        .Where(@event => @event.AddMethod is not null)
        .Where(@event => @event.RemoveMethod is not null)
        .ToImmutableSortedDictionary(@event => @event.Name, @event => @event);

    public void RegisterAll(ISocketClientProxy socketClientProxy, CancellationToken cancellationToken)
    {
        var restClient = socketClientProxy.Rest;
        var restClientExpression = Expression.Constant(restClient, ClientProxyType);
        var tokenExpression = Expression.Constant(cancellationToken, CancellationTokenType);

        foreach (var listener in listeners)
        {
            var listenerType = listener.GetType();
            var listenerExpression = Expression.Constant(listener, listenerType);
            var eventsToHandle = GetEventsToHandle(listenerType);

            foreach (var (@event, handler) in eventsToHandle)
            {
                logger.LogInformation("Setting up {listenerType} to subscribe to {@event}", listenerType.Name, 
                    @event.Name);
                
                var eventHandler = CreateEventHandler(@event, handler, listenerExpression, restClientExpression,
                    tokenExpression);

                @event.AddEventHandler(socketClientProxy, eventHandler);

                cancellationToken.Register(() => @event.RemoveEventHandler(socketClientProxy, eventHandler));
            }
        }
    }

    private static Delegate CreateEventHandler(EventInfo @event, MethodInfo handler,
        ConstantExpression listenerExpression, ConstantExpression restClientExpression,
        ConstantExpression tokenExpression)
    {
        var delegateType = @event.EventHandlerType!;

        var eventParameters = delegateType
            .GetMethod("Invoke")!
            .GetParameters()
            .Select(parameter => Expression.Parameter(parameter.ParameterType, parameter.Name!))
            .ToImmutableArray();

        var methodArguments = eventParameters
            .Cast<Expression>()
            .Prepend(restClientExpression)
            .Append(tokenExpression);

        var body = Expression.Call(listenerExpression, handler, methodArguments);

        return Expression
            .Lambda(delegateType, body, eventParameters)
            .Compile();
    }

    private static IEnumerable<(EventInfo @event, MethodInfo handler)> GetEventsToHandle(Type listenerType)
    {
        return listenerType
            .GetInterfaces()
            .Where(interfaceType => ListenerNamespace.Equals(interfaceType.Namespace))
            .Select(interfaceType => (interfaceType, nameMatch: NameFormat.Match(interfaceType.Name)))
            .Where(pair => pair.nameMatch.Success)
            .Select(pair =>
            {
                var eventName = pair.nameMatch.Groups[1].Value;
                var handler = pair.interfaceType.GetMethod($"On{eventName}");
                var parameters = handler?.GetParameters();

                return (eventName, handler, parameters);
            })
            .Where(method => method.handler is not null)
            .Select(method => (method.eventName, method.handler, method.parameters,
                eventFound: EventsByName.TryGetValue(method.eventName, out var @event), @event))
            .Where(method => method.eventFound)
            .Select(method => (handler: method.handler!, parameters: method.parameters!, @event: method.@event!))
            .Where(method => method.parameters.Length >= 2)
            .Where(method => method.parameters[0].ParameterType == ClientProxyType)
            .Where(method => method.parameters[^1].ParameterType == CancellationTokenType)
            .Select(method => (method.@event, method.handler));
    }

    [GeneratedRegex("I([a-zA-Z]+)Listener", RegexOptions.Compiled)]
    private static partial Regex NameFormatFactory();
}