using System.Collections.Immutable;

namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class Skill : AbstractLookup<Skill>, ILookup<Skill>
{
    public static readonly Skill Agriculture = new("Agriculture",
        "Agriculture measures the kingdom's ability to cultivate the land, bringing forth crops, flocks, and livestock.",
        Ability.Stability, 522);

    public static readonly Skill Arts = new("Arts",
        "Arts measure the kingdom's devotion to entertainment, artwork, and public works such as monuments.",
        Ability.Culture, 522);

    public static readonly Skill Boating = new("Boating",
        "Boating reflects the kingdom’s affinity for navigating rivers and lakes, or for using waterways to bolster trade, exploration, or even conquest.",
        Ability.Economy, 522);

    public static readonly Skill Defense = new("Defense",
        "Defense measures the kingdom’s ability to police and protect itself and its citizens from bandits, monsters, criminals, outside incursions, and pestilence or plague, but not natural disasters. It includes both physical fortifications and barriers as well as dedicated individuals guarding the land.",
        Ability.Stability, 522);

    public static readonly Skill Engineering = new("Engineering",
        "Engineering measures the kingdom’s ability to alter the physical landscape of its territory.",
        Ability.Stability, 523);

    public static readonly Skill Exploration = new("Exploration",
        "Exploration measures the kingdom’s ability to look outward and see what lies beyond its own borders, and to closely examine claimed territory to discover secrets.",
        Ability.Economy, 524);

    public static readonly Skill Folklore = new("Folklore",
        "Folklore measures the kingdom’s connection with faiths and customs of all kinds. It also indicates how deeply tradition affects public life and the prominence of faith, worship, and culturally traditional activities.",
        Ability.Culture, 524);

    public static readonly Skill Industry = new("Industry",
        "Industry measures the kingdom’s devotion to the business of building and making things, from basic necessities to luxury goods for trade. It puts people to work creating a prosperous future.",
        Ability.Economy, 525);

    public static readonly Skill Intrigue = new("Intrigue",
        "Intrigue measures the kingdom’s mastery of the hidden forces of politics. It includes manipulation of factions within a country and espionage beyond its borders, as well as investigations into criminal activities.",
        Ability.Loyalty, 526);

    public static readonly Skill Magic = new("Magic",
        "Magic measures the kingdom’s affinity for the mystic arts, whether arcane, divine, occult, or primal. It may reflect the breadth of natural magical talent among the people or it may represent the study of ancient secrets and magical theory.",
        Ability.Culture, 526);

    public static readonly Skill Politics = new("Politics",
        "Politics measures a kingdom’s embrace of civic life of all kinds, from deeply rooted local traditions to cosmopolitan cross-cultural connections. It also reflects the importance of the citizenry’s shared values, whether they are dedicated to freedom and justice or to more unsavory ethics.",
        Ability.Loyalty, 527);

    public static readonly Skill Scholarship = new("Scholarship",
        "Scholarship measures the kingdom’s interest in teaching and training its citizens to learn about the world around them. It also includes researching answers to problems in every field, from history and medicine to alchemy and philosophy.",
        Ability.Culture, 527);

    public static readonly Skill Statecraft = new("Statecraft",
        "Statecraft measures the kingdom’s ability to engage and interact with other nations, Freeholds, and political powers in above-the board political manners, including its trustworthiness in the eyes of other nations and its own citizens.",
        Ability.Loyalty, 528);

    public static readonly Skill Trade = new("Trade",
        "Trade measures a kingdom’s involvement in commerce of every kind, but especially in moving goods from one place to another and in the health of its market. You take a cumulative –1 item penalty on Trade checks for each settlement in your kingdom that has no Land Borders, unless it has at least one Water Border with a Bridge.",
        Ability.Economy, 529);

    public static readonly Skill Warfare = new("Warfare",
        "Warfare reflects a kingdom’s readiness to mobilize its military forces against its enemies—be they lone rampaging monsters or entire armies bent on invasion. Warfare has no exclusive skill activities, though it can be used with some general skill activities. Warfare is used extensively to resolve mass combat.",
        Ability.Loyalty, 530);

    public static readonly Skill Wilderness = new("Wilderness",
        "Wilderness measures how well the kingdom manages its natural resources, integrates with the natural ecosystem, and handles dangerous wildlife. It also reflects the kingdom’s ability to anticipate, prevent, and recover from natural disasters, in much the same way the Defense skill protects against other threats.",
        Ability.Stability, 530);

    private static readonly ImmutableSortedDictionary<Ability, ImmutableSortedSet<Skill>> SkillsByAbility = GetAll()
        .GroupBy(skill => skill.RelatedAbility)
        .ToImmutableSortedDictionary(skills => skills.Key, skills => skills.ToImmutableSortedSet());

    private Skill(string name, string description, Ability relatedAbility, ushort page) : base(name, description,
        Source.KingmakerAdventurePath, page)
    {
        RelatedAbility = relatedAbility;
    }

    public Ability RelatedAbility { get; }

    public static IEnumerable<Skill> GetAllByAbility(Ability ability)
    {
        return SkillsByAbility.GetValueOrDefault(ability, ImmutableSortedSet<Skill>.Empty);
    }

    public static IEnumerable<Skill> GetAll()
    {
        yield return Agriculture;
        yield return Arts;
        yield return Boating;
        yield return Defense;
        yield return Engineering;
        yield return Exploration;
        yield return Folklore;
        yield return Industry;
        yield return Intrigue;
        yield return Magic;
        yield return Politics;
        yield return Scholarship;
        yield return Statecraft;
        yield return Trade;
        yield return Warfare;
        yield return Wilderness;
    }
}