namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class Ability : AbstractLookup<Ability>, ILookup<Ability>
{
    public static readonly Ability Culture = new("Culture",
        "Culture measures the interest and dedication of your nation and its people to the arts and sciences, to religion and reason, and to the subjects that your society chooses to learn about and to teach. Are your people well-versed in rhetoric and philosophy? Do they value learning and research, music and dance? Do they embrace society in all its diverse splendor? If they do, your kingdom likely has a robust Culture score.");

    public static readonly Ability Economy = new("Economy",
        "Economy measures the practical day-to-day workings of your society as it comes together to do the work of making and building, buying and selling. How industrious are your citizenry? Are they devoted to building more, higher, and better, trading in goods, services, and ideas? If so, your kingdom likely has a robust Economy score.");

    public static readonly Ability Loyalty = new("Loyalty",
        "Loyalty measures the collective will, spirit, and sense of camaraderie the citizens of your nation possess. How much do they trust and depend on one another? How do they respond when you sound the call to arms or enact new laws? How do they react when other nations send spies or provocateurs into your lands to make trouble? If they support the kingdom's leadership, the kingdom itself has a robust Loyalty score.");

    public static readonly Ability Stability = new("Stability",
        "Stability measures the physical health and well-being of your nation. This includes its infrastructure and buildings, the welfare of its people, and how well things are protected and maintained under your rule. How carefully do you maintain your stores and reserves, repair things that are broken, and provide for the necessities of life? How quickly can you mobilize to shield your citizens from harm? A kingdom that can handle both prosperity and disaster efficiently and effectively has a robust Stability score.");

    private Ability(string name, string description) : base(name, description, Source.KingmakerAdventurePath, 507)
    {
    }
}