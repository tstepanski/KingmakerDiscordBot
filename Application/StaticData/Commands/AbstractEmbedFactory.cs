using System.Collections;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using Discord;
using KingmakerDiscordBot.Application.General;

namespace KingmakerDiscordBot.Application.StaticData.Commands;

internal sealed class AbstractEmbedFactory : IAbstractEmbedFactory
{
    private static readonly ImmutableSortedSet<string> ExcludedPropertyNames =
    [
        nameof(ILookup<object>.Description),
        nameof(ILookup<object>.Name),
        nameof(ILookup<object>.Page),
        nameof(ILookup<object>.Source)
    ];

    private const string NameOfAddMethod = nameof(EmbedBuilder.AddField);
    private const string NameOfBuildMethod = nameof(EmbedBuilder.Build);
    private const string NameOfWithDescriptionMethod = nameof(EmbedBuilder.WithDescription);
    private const string NameOfWithTitleMethod = nameof(EmbedBuilder.WithTitle);
    private const string NameOfJoinMethod = nameof(Join);
    private const string NameOfToString = nameof(ToString);
    private static readonly Type TypeOfString = typeof(string);
    private static readonly Type TypeOfEmbedBuilder = typeof(EmbedBuilder);
    private static readonly Type TypeOfObject = typeof(object);
    private static readonly Type TypeOfBoolean = typeof(bool);
    private static readonly Type TypeOfEnumerable = typeof(IEnumerable);
    private static readonly Type ThisType = typeof(AbstractEmbedFactory);
    private static readonly MethodInfo ToStringMethod = TypeOfObject.GetMethod(NameOfToString, [])!;
    private static readonly MethodInfo BuildMethod = TypeOfEmbedBuilder.GetMethod(NameOfBuildMethod, [])!;
    private static readonly MethodInfo JoinMethod = ThisType.GetMethod(NameOfJoinMethod, [TypeOfEnumerable])!;
    private static readonly NewExpression ConstructBuilder = Expression.New(TypeOfEmbedBuilder);

    private static readonly MethodInfo WithDescriptionMethod =
        TypeOfEmbedBuilder.GetMethod(NameOfWithDescriptionMethod, [TypeOfString])!;

    private static readonly MethodInfo WithTitleMethod =
        TypeOfEmbedBuilder.GetMethod(NameOfWithTitleMethod, [TypeOfString])!;

    private static readonly MethodInfo AddMethod =
        TypeOfEmbedBuilder.GetMethod(NameOfAddMethod, [TypeOfString, TypeOfObject, TypeOfBoolean])!;

    private static readonly ConstantExpression FalseValue = Expression.Constant(false, TypeOfBoolean);
    
    public Func<T, Embed> Create<T>() where T : ILookup<T>
    {
        var typeOfT = typeof(T);
        var parameter = Expression.Parameter(typeOfT, "reference");
        var builderVariable = Expression.Variable(TypeOfEmbedBuilder, "embedBuilder");
        var assignBuilder = Expression.Assign(builderVariable, ConstructBuilder);
        var build = Expression.Call(builderVariable, BuildMethod);

        var allProperties = typeOfT
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(property => property.CanRead)
            .ToImmutableSortedDictionary(property => property.Name, property => property);

        var nameProperty = allProperties[nameof(ILookup<T>.Name)];
        var descriptionProperty = allProperties[nameof(ILookup<T>.Description)];
        var sourceProperty = allProperties[nameof(ILookup<T>.Source)];
        var pageProperty = allProperties[nameof(ILookup<T>.Page)];
        var titleSet = AddSimpleSet(parameter, builderVariable, WithTitleMethod, nameProperty);
        var descriptionSet = AddSimpleSet(parameter, builderVariable, WithDescriptionMethod, descriptionProperty);

        var expressions = allProperties
            .ExceptBy(ExcludedPropertyNames, property => property.Key)
            .Select(property => property.Value)
            .Concat([sourceProperty, pageProperty])
            .Select(property => AddField(parameter, builderVariable, property))
            .Concat([titleSet, descriptionSet])
            .Prepend(assignBuilder)
            .Append(build);

        var block = Expression.Block([builderVariable], expressions);

        return Expression
            .Lambda<Func<T, Embed>>(block, parameter)
            .Compile();
    }

    private static MethodCallExpression AddSimpleSet(ParameterExpression parameter, ParameterExpression builderVariable,
        MethodInfo method, PropertyInfo property)
    {
        var value = Expression.Property(parameter, property);

        return Expression.Call(builderVariable, method, value);
    }

    private static Expression AddField(ParameterExpression parameter, ParameterExpression builderVariable,
        PropertyInfo property)
    {
        var propertyPrettyName = property.Name.Prettify();
        var propertyAccess = Expression.Property(parameter, property);
        var key = Expression.Constant(propertyPrettyName, TypeOfString);
        var serializableValue = GetSerializableValue(property, propertyAccess);
        var addField = Expression.Call(builderVariable, AddMethod, key, serializableValue, FalseValue);

        if (property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) is null)
        {
            return addField;
        }

        var nullValue = Expression.Constant(null, property.PropertyType);
        var notEqualToNull = Expression.NotEqual(propertyAccess, nullValue);
            
        return Expression.IfThen(notEqualToNull, addField);
    }

    private static MethodCallExpression GetSerializableValue(PropertyInfo property, MemberExpression expression)
    {
        var propertyType = property.PropertyType;

        if (TypeOfEnumerable.IsAssignableFrom(propertyType) is false || propertyType == TypeOfString)
        {
            return Expression.Call(expression, ToStringMethod);
        }

        var convert = Expression.Convert(expression, TypeOfEnumerable);

        return Expression.Call(null, JoinMethod, convert);
    }

    public static string Join(IEnumerable enumerable)
    {
        return string.Join(", ", enumerable);
    }
}