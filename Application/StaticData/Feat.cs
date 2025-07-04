﻿using System.Collections.Immutable;

namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class Feat : AbstractLookup<Feat>, ILookup<Feat>, IHasTraits
{
    private static readonly ImmutableSortedSet<Trait> GeneralAndKingdom = [Trait.General, Trait.Kingdom];

    public static readonly Feat CivilService = new("Civil Service",
        "Everyone has a place and a role, and as long as those roles are filled, the government functions. When you select this feat, choose one leadership role; that role is now supported by your efficient civil servants, so its vacancy penalty is no longer applicable. If you wish to change the leadership role to which Civil Service applies, you can do so using the New Leadership activity at the start of a Kingdom turn.\n\nYou gain a +2 status bonus to New Leadership checks.",
        null, 531);

    public static readonly Feat CooperativeLeadership = new("Cooperative Leadership",
        "Your leaders are skilled at working with one another. When a leader uses the Focused Attention kingdom activity to aid another leader's kingdom check, the circumstance bonus granted by a success is increased to +3.\n\nAt 11th level, your leaders' collaborative style leads them to ever greater successes when they work together. When a leader uses the Focused Attention kingdom activity to aid another leader's check, treat a critical failure on the aided check as a failure. If your kingdom has at least the expert rank in the skill used in the aided check, treat a failure on the check as a success. (This does not allow you to ever improve a critical failure to a success.)",
        null, 531);

    public static readonly Feat CrushDissent = new("Crush Dissent",
        "Your rule brooks no dissent and stamps out traitors, making harsh examples of them. Once per Kingdom turn when you gain Unrest, you can attempt to crush the dissent by attempting a basic Warfare check. On a success, the Unrest increase is canceled, but on a critical failure, the Unrest increase is doubled. In addition, you gain a +1 status bonus to checks to resolve dangerous kingdom events that involve internal bickering, such as Feud.",
        "Trained in Warfare", 531);

    public static readonly Feat FortifiedFiefs = new("Fortified Fiefs",
        "Your vassals take their duty to protect those under their stewardship seriously, and your engineers emphasize the value of a strong defense when it comes to building settlements and fortifications. You gain a +2 circumstance bonus to checks attempted as part of the Fortify Hex activity and on activities to build or repair a Barracks, Castle, Garrison, Keep, Stone Wall, or Wooden Wall. In addition, you gain a +1 status bonus to all kingdom checks attempted during dangerous events that directly impact your settlements' defenses.",
        "Trained in Defense", 531);

    public static readonly Feat InsiderTrading = new("Insider Trading",
        "Your leading citizens share valuable business information with one another and with associates in other lands, and they hire one another's workers to supply the labor they need to fuel their production. You gain a +1 status bonus to Establish Work Site, Establish Trade Agreement, and Trade Commodities activities. In addition, gain 1 bonus Resource Die at the start of each Kingdom turn.",
        "Trained in Industry", 531);

    public static readonly Feat KingdomAssurance = new("Kingdom Assurance",
        "Even when things go poorly in other areas, you can count on consistency in carrying out kingdom activities with a chosen skill. Choose one Kingdom skill in which your kingdom is trained. Once per Kingdom turn, when you would attempt a skill check for that skill, you can forgo rolling and instead take a result equal to 10 + your proficiency bonus; do not apply any other bonuses, penalties, or modifiers to this result.\n\nSpecial You can select this feat multiple times. Each time, choose a different skill and gain the benefits of this feat for that skill.",
        "Trained in at least three skills", 532, Trait.Fortune);

    public static readonly Feat MuddleThrough = new("Muddle Through",
        "Your people are independent-minded and take care of the small things around the kingdom, not letting them pile up into bigger problems. Increase two of your Ruin thresholds by 1 and one of them by 2.",
        "Trained in Wilderness", 532);

    public static readonly Feat PracticalMagic = new("Practical Magic",
        "Magic has an honored place in your society, and your people incorporate it into their everyday work to make life easier. You gain a +1 status bonus to Magic checks, and you can use Magic checks in place of Engineering checks. In addition, as magic-wielding NPCs find your nation a comfortable place to live and work, you reduce the cost of using the Hire Adventurers activity to 1 RP.",
        "Trained in Magic", 532);

    public static readonly Feat PullTogether = new("Pull Together",
        "Your people are very reliable, and their swift decision-making keeps most projects from getting too far off track. Once per Kingdom turn when you roll a critical failure on a Kingdom skill check, attempt a DC 11 flat check. If this succeeds, your citizens heed the call to put in extra work to mitigate the disaster; treat the Kingdom skill check result as failure instead. The DC of this flat check increases by 5 each time you subsequently use it, but it decreases by 1 (to a minimum of 11) for each Kingdom turn that passes when you do not use it.",
        "Trained in Politics", 532);

    public static readonly Feat SkillTrainingKingdom = new("Skill Training (Kingdom)",
        "Your kingdom receives the trained proficiency rank in a Kingdom skill of your choice. You can select this feat multiple times, choosing a new skill each time.",
        null, 532);

    public static readonly Feat EndureAnarchy = new("Endure Anarchy",
        "Your kingdom holds together even in the midst of extreme peril. If your kingdom's Unrest is 6 or higher and you use a kingdom activity that decreases Unrest, decrease the Unrest by an additional 1. You do not fall into anarchy unless your kingdom's Unrest reaches 24.",
        "Loyalty 14", 531);

    public static readonly Feat InspiringEntertainment = new("Inspiring Entertainment",
        "Your kingdom's artists and entertainers are talented and prolific, and there's never a shortage of new plays, operas, novels, music, sculptures, paintings, or other forms of distraction to entertain the citizens, even during times of upheaval. When you check for Unrest during the Upkeep phase of a Kingdom turn, you may roll a Culture-based check rather than a Loyalty-based check to determine the outcome. Your kingdom also gains a +2 status bonus to all Culture-based skill checks whenever your kingdom has at least 1 Unrest.",
        "Culture 14", 532);

    public static readonly Feat LiquidateResources = new("Liquidate Resources",
        "Your kingdom's economy can liquidate resources in an emergency when funding runs out. The first time during a Kingdom turn in which you are forced to spend RP as the result of a failed skill check or a dangerous event, and that expense reduces you to 0 RP, you may instead reduce your RP to 1 and treat the expense as if it were paid in full. At the start of your next Kingdom turn, roll 4 fewer Resource Dice than normal.",
        "Economy 14", 532);

    public static readonly Feat QuickRecovery = new("Quick Recovery (Kingdom)",
        "Your kingdom recovers more quickly from danger and disaster. Whenever you attempt a skill check to end an ongoing harmful kingdom event, you gain a +4 status bonus to the check.",
        "Stability 14", 532);

    public static readonly Feat FreeAndFair = new("Free and Fair",
        "Your reputation for transparency and fairness in conducting elections, appointments, and other changes in government inspires tremendous public trust. You gain a +2 circumstance bonus to Loyalty-based checks attempted as part of the New Leadership and Pledge of Fealty activities. If you fail or critically fail such a check, you can spend 2 RP to reroll the check (but without the +2 circumstance bonus); attempting this adds the Fortune trait. You must take the result of the second roll, even if it is worse than the original roll.",
        null, 531);

    public static readonly Feat QualityOfLife = new("Quality of Life",
        "Your kingdom's robust economy makes the creature comforts of civilization more readily available to all, and even finer luxuries are more easily had. The first time you gain Luxury Commodities in a Kingdom turn, increase the total gained by 1. All of your settlements are treated as 1 level higher than their actual level for the purposes of determining what sorts of magic items might be offered for sale at their markets and shops.",
        null, 532);

    public static readonly Feat FameAndFortune = new("Fame and Fortune",
        "Your kingdom's reputation has spread far and wide, bringing in visitors to behold the spectacle of your greatness and pay their respects. Whenever you achieve a critical success on any Kingdom skill check during the Activity phase of a Kingdom turn, gain 1 bonus Resource Die at the start of your next Kingdom turn.",
        null, 531);

    private Feat(string name, string description, string? prerequisite, ushort page, Trait? extraTrait = null) :
        base(name, description, Source.KingmakerAdventurePath, page)
    {
        Prerequisite = prerequisite;

        Traits = extraTrait is null
            ? GeneralAndKingdom
            : GeneralAndKingdom.Add(extraTrait);
    }

    public string? Prerequisite { get; }

    public ImmutableSortedSet<Trait> Traits { get; }
}