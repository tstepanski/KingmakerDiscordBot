namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class Heartland : AbstractLookup<Heartland>, ILookup<Heartland>
{
    public static readonly Heartland ForestOrSwamp = new("Forest or Swamp",
        "Your nation begins in woodlands or swamplands, so there are no shortages in natural resources or wonders to bolster your citizens' imagination and mood.",
        Ability.Culture);

    public static readonly Heartland HillOrPlain = new("Hill or Plain",
        "Your nation starts in an area that is easy to traverse. This is reflected in your citizens' temperament; they appreciate that your choice makes their lives a bit easier.",
        Ability.Loyalty);

    public static readonly Heartland LakeOrRiver = new("Lake or River",
        "By establishing your nation on the shores of a lake or river, you ensure a built-in mechanism for trade. Even before a road is built, merchants and travelers can reach your settlement with relative ease via boat.",
        Ability.Economy);

    public static readonly Heartland MountainOrRuins = new("Mountain or Ruins",
        "Your nation is founded in the mountains or includes a significant ruined location, and it uses these natural or artificial features to bolster defense. Your citizens tend to be hale and hardy, if not stubborn to a fault.",
        Ability.Stability);

    private Heartland(string name, string description, Ability boost) : base(name, description,
        Source.KingmakerAdventurePath, 509)
    {
        Boost = boost;
    }

    public Ability Boost { get; }
}