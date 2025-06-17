namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class Charter : AbstractLookup<Charter>, ILookup<Charter>
{
    public static readonly Charter Conquest = new("Conquest",
        "Your sponsors have conquered an area and its former leaders have been routed or even killed. This charter places you in charge of some portion of this conquered territory (or land abandoned by the defeated enemy) and commands you to hold and pacify it in the name of your patron. The people are particularly devoted and supportive of your rule (if partially out of fear), but the constant threat of potential war hinders the arts and makes it difficult for citizens to truly relax. If you opt for this charter, you are asked to set up your kingdom against Pitax.",
        Ability.Loyalty, Ability.Culture);

    public static readonly Charter Expansion = new("Expansion",
        "Your patron places you in charge of a domain adjacent to already settled lands with the expectation that your nation will remain a strong ally. The greater support from your patron's nation helps to bolster your own kingdom's society, but this increased reliance means that fluctuations in your ally's fortunes can impede your own kingdom's security. If you select this charter, Lady Jamandi expects you to remain strong allies with Restov.",
        Ability.Culture, Ability.Stability);

    public static readonly Charter Exploration = new("Exploration",
        "Your sponsor wants you to explore, clear, and settle a wilderness area along the border of the sponsor's own territory. Your charter helps to secure initial structures (or supplies to create them), at the cost of incurring financial debt.",
        Ability.Stability, Ability.Economy);

    public static readonly Charter Grant = new("Grant",
        "Your patron grants a large amount of funding and other resources without restriction on the nature of your kingdom's development—but they do require you to employ many of their citizens and allies. Your nation's wealth and supplies are secure, but a portion of your kingdom's residents have split allegiances between your nation and that of your sponsor.",
        Ability.Economy, Ability.Loyalty);

    public static readonly Charter Open = new("Open",
        " If you would prefer to be truly free agents and trailblazers staking your own claim, you can simply choose an open charter with no restrictions—and no direct support. In this case, Lady Jamandi applauds your bravery and self-confidence, but warns that establishing a kingdom is no small task. An open charter grants a single ability boost to any ability score, and the new nation has no built-in ability flaw.",
        null, null, 509);

    private Charter(string name, string description, Ability? boost, Ability? flaws, ushort page = 508) : base(name, 
        description, Source.KingmakerAdventurePath, page)
    {
        Boost = boost;
        Flaws = flaws;
    }

    public Ability? Boost { get; }

    public Ability? Flaws { get; }

    public static IEnumerable<Charter> GetAll()
    {
        yield return Conquest;
        yield return Expansion;
        yield return Exploration;
        yield return Grant;
        yield return Open;
    }

    public void Deconstruct(out string name, out Ability? boost, out Ability? flaws, out string description)
    {
        name = Name;
        boost = Boost;
        flaws = Flaws;
        description = Description;
    }
}