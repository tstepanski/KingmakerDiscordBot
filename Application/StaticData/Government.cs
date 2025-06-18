using System.Collections.Immutable;

namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class Government : AbstractLookup<Government>, ILookup<Government>
{
    public static readonly Government Despotism = new("Despotism",
        "Your nation's rule is centered around a single individual who seized or inherited command and whose authority is absolute. The ruler of this kingdom still retains advisors and assistants, but only when they obey the ruler's whims.",
        Ability.Stability, Ability.Economy, Skill.Intrigue, Skill.Warfare, Feat.CrushDissent, 509);

    public static readonly Government Feudalism = new("Feudalism",
        "Your nation's rule is vested in a dynastic royal family, though much of the real power is distributed among their vassals and fiefdoms.",
        Ability.Stability, Ability.Culture, Skill.Defense, Skill.Trade, Feat.FortifiedFiefs);

    public static readonly Government Oligarchy = new("Oligarchy",
        "Your nation's rule is determined by a council of influential leaders who make decisions for all others.",
        Ability.Loyalty, Ability.Economy, Skill.Arts, Skill.Industry, Feat.InsiderTrading);

    public static readonly Government Republic = new("Republic",
        "Your nation draws its leadership from its own citizens. Elected representatives meet in parliamentary bodies to guide the nation.",
        Ability.Stability, Ability.Loyalty, Skill.Engineering, Skill.Politics, Feat.PullTogether);

    public static readonly Government Thaumocracy = new("Thaumocracy",
        "Your nation is governed by those most skilled in magic, using their knowledge and power to determine the best ways to rule. While the type of magic wielded by the nation's rulers can adjust its themes (or even its name—a thaumocracy run by divine spellcasters would be a theocracy, for example), the details below remain the same whether it's arcane, divine, occult, primal, or any combination of the four.",
        Ability.Economy, Ability.Culture, Skill.Folklore, Skill.Magic, Feat.PracticalMagic);

    public static readonly Government Yeomanry = new("Yeomanry",
        "Your nation is decentralized and relies on local leaders and citizens to handle government issues, sending representatives to each other as needed to deal with issues that concern more than one locality.",
        Ability.Loyalty, Ability.Culture, Skill.Agriculture, Skill.Wilderness, Feat.MuddleThrough);

    private Government(string name, string description, Ability firstAbilityBoost, Ability secondAbilityBoost,
        Skill firstSkillProficiency, Skill secondSkillProficiency, Feat bonusFeat, ushort page = 510) : base(name,
        description, Source.KingmakerAdventurePath, page)
    {
        Boosts = [firstAbilityBoost, secondAbilityBoost];
        Proficiencies = [firstSkillProficiency, secondSkillProficiency];
        BonusFeat = bonusFeat;
    }

    public ImmutableSortedSet<Ability> Boosts { get; }

    public ImmutableSortedSet<Skill> Proficiencies { get; }

    public Feat BonusFeat { get; }
}