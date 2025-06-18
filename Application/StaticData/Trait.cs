namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class Trait : AbstractLookup<Trait>, ILookup<Trait>
{
    public static readonly Trait Region = new("Region",
        "This is a Kingdom skill activity that can be undertaken during the Region Activity phase.",
        Source.KingmakerAdventurePath, 517);

    public static readonly Trait Downtime = new("Downtime",
        "A mode of play in which characters are not adventuring. Days pass quickly at the table, and characters engage in long term activities.",
        Source.PlayerCore, 455);

    public static readonly Trait Leadership = new("Leadership",
        "This is a Kingdom skill activity that can be undertaken during the Leadership Activity phase.",
        Source.KingmakerAdventurePath, 517);

    public static readonly Trait Civic = new("Civic",
        "This is a Kingdom skill activity that can be undertaken during the Civic Activity phase.",
        Source.KingmakerAdventurePath, 517);

    public static readonly Trait Commerce = new("Commerce",
        "This is a Kingdom skill activity that can be undertaken during the Commerce Activity phase.",
        Source.KingmakerAdventurePath, 517);

    public static readonly Trait Fortune = new("Fortune",
        "A fortune effect beneficially alters how you roll your dice. You can never have more than one fortune effect alter a single roll. If multiple fortune effects would apply, you have to pick which to use. If a fortune effect and a misfortune effect would apply to the same roll, the two cancel each other out, and you roll normally.",
        Source.KingmakerAdventurePath, 401);

    public static readonly Trait General = new("General",
        "A fortune effect beneficially alters how you roll your dice. You can never have more than one fortune effect alter a single roll. If multiple fortune effects would apply, you have to pick which to use. If a fortune effect and a misfortune effect would apply to the same roll, the two cancel each other out, and you roll normally.",
        Source.KingmakerAdventurePath, 401);

    public static readonly Trait Kingdom = new("Kingdom",
        "A kingdom gains feats as it increases in level. Some feats are general-purpose abilities that apply all the time. Others grant benefits to specific kingdom activities or events or allow kingdoms to perform special activities. Each time a kingdom gains a feat, players can select any feat whose level does not exceed their kingdom's level and whose prerequisites their kingdom satisfies.",
        Source.KingmakerAdventurePath, 531);

    private Trait(string name, string description, Source source, ushort page) : base(name, description, source, page)
    {
    }
}