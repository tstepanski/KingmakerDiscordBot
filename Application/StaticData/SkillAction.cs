using System.Collections.Immutable;

namespace KingmakerDiscordBot.Application.StaticData;

internal sealed class SkillAction : AbstractLookup<SkillAction>, ILookup<SkillAction>, IHasTraits
{
    public static readonly SkillAction EstablishFarmland = new("Establish Farmland",
        "You plant crops and establish livestock in permanent farms, ranches, and other growing operations to create Farmland. If you're attempting to Establish Farmland in a hex that is predominantly plains, you must spend 1 RP and the check is against your Control DC. If you're targeting a hex that is predominantly hills, you must spend 2 RP and the check is against your Control DC + 5.",
        Skill.Agriculture, false,
        "Plains or hills are the predominant terrain feature in the hex; the hex is in the influence of one of your settlements.",
        "You establish two adjacent Farmland hexes instead of one. If your target hex was a hills hex, the additional hex may be a hills hex or a plains hex; otherwise, the additional hex must be a plains hex. If no appropriate hex is available, treat this result as a regular success instead.",
        "You establish one Farmland hex.", "You fail to establish a Farmland hex.",
        "You fail to establish a Farmland hex, and your attempt potentially causes the spread of a blight. At the start of each of the next two Event phases, attempt a DC 6 flat check; on a failure, your kingdom experiences a Crop Failure event in this and all adjacent hexes.",
        522, Trait.Downtime, Trait.Region);

    public static readonly SkillAction HarvestCrops = new("Harvest Crops",
        "Attempt a basic check to forage for wild edibles or gather excess crops from farms.",
        Skill.Agriculture, false, null, "Gain 1d4 Food commodities.", "Gain 1 Food commodity.",
        "Gain no Food commodities.",
        "Lose 1d4 Food commodities to spoilage; if you have no Food to lose, you instead gain 1 Unrest.", 522,
        Trait.Downtime, Trait.Region);

    public static readonly SkillAction CraftLuxuries = new("Craft Luxuries",
        "You encourage your artisans to craft luxury goods and may even aid them in this pursuit. Roll 1 Resource Die and spend RP equal to the result. Then attempt a basic check.",
        Skill.Arts, false, null,
        "Your artisans exceed expectations and craft extravagant goods. Gain 1d4 Luxury Commodities.",
        "Your artisans produce some delightful goods. Gain 1 Luxury Commodity.",
        "Your artisans fail to produce anything noteworthy.",
        "Your artisans not only fail to produce anything noteworthy, but some took advantage of the opportunity to push their own agendas or earn more for themselves by selling to underground markets. Increase one of your Ruins by 1.",
        522, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction CreateMasterpiece = new("Create a Masterpiece",
        "You encourage your kingdom's artists to create and display a masterful work of art to bolster your kingdom's reputation. Attempt a basic check; the result affects either Fame or Infamy (depending on the type of kingdom you're running). Create a Masterpiece may be attempted only once per Kingdom turn regardless of the number of leaders pursuing activities.",
        Skill.Arts, true, null,
        "Gain 1 Fame or Infamy point immediately, and at the start of your next Kingdom turn, gain 1 additional Fame or Infamy point. Immediately roll 2 Resource Dice. Gain RP equal to the result.",
        "Gain 1 Fame or Infamy point immediately.", "Your attempt to create a masterpiece fails.",
        "Not only does your attempt to create a masterpiece fail, it does so in a dramatic and humiliating way. Lose 1 Fame or Infamy point; if you have no Fame or Infamy points to lose, instead gain 1d4 Unrest.",
        522, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction GoFishing = new("Go Fishing",
        "Attempt a basic check to fish for food from the rivers and lakes in your kingdom.", Skill.Boating, false,
        "Must have at least one claimed hex that includes river or lake terrain.", "Gain 1d4 Food commodities.",
        "Gain 1 Food commodity.", "Gain no Food commodities.",
        "You lose some fishers to tragic accidents; gain 1 Unrest.", 522, Trait.Downtime, Trait.Region);

    public static readonly SkillAction FortifyHex = new("Fortify Hex",
        "Your command your engineers to construct a protected encampment, such as a fort or barbican, to serve as a defensive post in the hex. Spend RP as determined by the hex's most inhospitable terrain. Then attempt a basic check. A fortified hex grants an additional bonus in warfare, but also gives traveling PCs a place to rest that prevents wandering monsters from interrupting their rest.",
        Skill.Defense, false, "The target hex must be claimed by your kingdom and must not have a settlement in it.",
        "You find a defensible position for your fortification and finish construction efficiently. Gain a refund of half the RP you spent to build in the hex, then reduce Unrest by 1.",
        "You establish your fortification in the hex. Reduce Unrest by 1.", "You fail to fortify the hex.",
        "Your attempt ends in disaster. Not only do you fail to build a structure, but you lose several workers to an accident, banditry, a vicious monster, or some other unforeseen occurrence. Gain 1 Unrest.",
        523, Trait.Downtime, Trait.Region);

    public static readonly SkillAction ProvideCare = new("Provide Care",
        "Attempt a basic check to organize and encourage your settlements' healers, apothecaries, medics, and other caregivers to provide care and support for citizens in need.",
        Skill.Defense, false, null,
        "You provide unexpectedly compassionate support for the people. Reduce Unrest by 1 and reduce one Ruin of your choice by 1.",
        "Your care soothes the worries and fears of the populace; reduce Unrest by 1.",
        "You don't provide any notable care for the citizens, but at least you don't make things worse.",
        "Your attempt to provide care backfires. Increase your Unrest or a Ruin of your choice by 1.", 523,
        Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction BuildRoads = new("Build Roads",
        "Your order your kingdom's engineers to construct a network of robust roads through the hex. Travel along roads uses a terrain type one step better than the surrounding terrain; for example, roads through forest hexes—normally difficult terrain—allow travel as if it were open terrain. Spend RP as determined by the hex's most inhospitable terrain (if the hex includes any rivers that cross the hex from one hex side to any other, you must spend double the normal RP cost to also build bridges; this adds the Bridge structure to that hex). Then attempt a basic check. Work with the GM to determine where your roads appear on the map.",
        Skill.Engineering, false, "The hex in which you seek to build roads must be claimed by your kingdom.",
        "You build roads into the target hex and one adjacent claimed hex that doesn't yet have roads and whose terrain features are at least as hospitable as those of the target hex. If no adjacent hex is appropriate, treat this result as a Success instead.",
        "You build roads in the hex.", "You fail to build roads in the hex.",
        "Your attempt to build roads ends in disaster. Not only do you fail to build roads, but you lose several workers to an accident, banditry, a vicious monster, or some other unforeseen occurrence. Gain 1 Unrest.",
        523, Trait.Downtime, Trait.Region);

    public static readonly SkillAction Demolish = new("Demolish",
        "Choose a single occupied lot in one of your settlements and attempt a basic check to reduce it to Rubble and then clear the Rubble away to make ready for a new structure. For multiple-lot structures, you'll need to perform multiple Demolish activities (or critically succeed at the activity) to fully clear all of the lots. As soon as you begin Demolishing a multiple-lot structure, all of the lots occupied by that structure no longer function.",
        Skill.Engineering, false, null,
        "Choose one of the following effects: you demolish an entire multiple-lot structure all at once and clear all of the lots it occupied, or you recover 1d6 Commodities (chosen from lumber, stone, and ore) from the Rubble of a single-lot demolition.",
        "You demolish the lot successfully.",
        "You fail to demolish the lot. It remains in Rubble and cannot be used for further construction until you successfully Demolish it.",
        "As failure, but accidents during the demolition cost you the lives of some of your workers. Gain 1 Unrest.",
        523, Trait.Civic, Trait.Downtime);

    public static readonly SkillAction EstablishWorkSite = new("Establish Work Site",
        "Your hire a crew of workers to travel to a hex that contains Lumber, Ore, or Stone to be harvested. Spend RP as determined by the hex's most inhospitable terrain. Then attempt a basic check. Lumber camps can be established in any hex that contains a significant amount of forest terrain. Mines and quarries can be established in any hex that contains a significant amount of hill or mountain terrain.",
        Skill.Engineering, false, null,
        "You establish a Work Site in the hex and proceed to discover an unexpectedly rich supply of high quality Commodities. All Commodity yields granted by this site are doubled until the end of the next Kingdom turn.",
        "You establish a Work Site in the hex.", "You fail to establish a Work Site in the hex.",
        "Not only do you fail to establish a Work Site, but you lose several workers to an accident, banditry, a vicious monster, or some other unforeseen occurrence. Gain 1 Unrest.",
        524, Trait.Downtime, Trait.Region);

    public static readonly SkillAction Irrigation = new("Irrigation",
        "You send excavators to build waterways, canals, or drainage systems to convey water from areas that have natural access to a river or lake. Spend RP as determined by the hex's most inhospitable terrain feature. Then attempt a basic check.",
        Skill.Engineering, true,
        "You control a hex adjacent to a river or lake that itself does not contain a river or lake.",
        "The hex gains a river or lake terrain feature (or you change the effects of a previous critical failure at Irrigation in this hex into a failure); work with your GM to determine where these features appear in the hex. In addition, your workers were efficient and quick, and you regain half the RP you spent building the waterways.",
        "As success, but without regaining any RP.",
        "You fail to build workable systems or to restore a previous critical failure, and the hex does not gain the river or lake terrain feature.",
        "As failure, but your attempts at Irrigation are so completely useless that they become breeding grounds for disease. Gain 1 Unrest. From this point onward, at the start of your Kingdom turn's Event phase, attempt a DC 4 flat check. This flat check's DC increases by 1 for each hex in your kingdom that contains a critically failed attempt at Irrigation. If you fail this flat check, your kingdom suffers a Plague event in addition to any other event it might have. You can attempt this activity again in a later Kingdom turn to undo a critically failed Irrigation attempt.",
        524, Trait.Downtime, Trait.Region);

    public static readonly SkillAction HireAdventurers = new("Hire Adventurers",
        "While the PCs can strike out themselves to deal with ongoing events, it's often more efficient to Hire Adventurers. When you Hire Adventurers to help end an ongoing event, the DC is equal to your Control DC adjusted by the event's level modifier. Roll 1 Resource Die and spend RP equal to the result each time you attempt this activity.",
        Skill.Exploration, false, null, "You end the continuous event.",
        "The continuous event doesn't end, but you gain a +2 circumstance bonus to resolve the event during the next Event phase.",
        "You fail to end the continuous event. If you try to end the continuous event again, the cost in RP increases to 2 Resource Dice.",
        "As failure, but word spreads quickly through the region—you can no longer attempt to end this continuous event by Hiring Adventurers.",
        524, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction CelebrateHoliday = new("Celebrate Holiday",
        "You declare a day of celebration. Holidays may be religious, historical, martial, or simply festive, but all relieve your citizens from their labors and give them a chance to make merry at the kingdom's expense. Attempt a basic check, but if your kingdom Celebrated a Holiday the previous turn, the DC increases by 4, as your kingdom hasn't had a chance to recover from the previous gala.",
        Skill.Folklore, false, null,
        "Your holidays are a delight to your people. The event is expensive, but incidental income from the celebrants covers the cost. You gain a +2 circumstance bonus to Loyalty-based checks until the end of your next Kingdom turn.",
        "Your holidays are a success, but they're also expensive. You gain a +1 circumstance bonus to Loyalty-based checks until the end of your next Kingdom turn. Immediately roll 1 Resource Die and spend RP equal to the result. If you can't afford this cost, treat this result as a Critical Failure instead.",
        "The holiday passes with little enthusiasm, but is still expensive. Immediately roll 1 Resource Die and spend RP equal to the result. If you can't afford this cost, treat this result as a Critical Failure instead.",
        "Your festival days are poorly organized, and the citizens actively mock your failed attempt to celebrate. During the next turn, reduce your Resource Dice total by 4. The failure also causes you to take a –1 circumstance penalty to Loyalty-based checks until the end of the next Kingdom turn.",
        524, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction TradeCommodities = new("Trade Commodities",
        "There are five different categories of Commodities: Food, Lumber, Luxuries, Ore, and Stone. When you Trade Commodities, select one Commodity that your kingdom currently stockpiles and reduce that Commodity's stockpile by up to 4. Then attempt a basic check. If you trade with a group that you've established diplomatic relations with, you gain a +1 circumstance bonus to the check.",
        Skill.Industry, false, null,
        "At the beginning of the next Kingdom turn, you gain 2 bonus Resource Dice per point of stockpile expended from your Commodity now.",
        "At the beginning of your next Kingdom turn, you gain 1 bonus Resource Die per point of stockpile expended from your Commodity now.",
        "You gain 1 bonus Resource Die at the beginning of your next Kingdom turn.",
        "You gain no bonus Resource Dice (though the Commodity remains depleted). If you Traded Commodities the previous turn, gain 1 Unrest.",
        525, Trait.Commerce, Trait.Downtime);

    public static readonly SkillAction RelocateCapital = new("Relocate Capital",
        "The kingdom leaders announce that they are uprooting the seat of government from its current home and reestablishing it in another settlement. Attempt a check with a DC equal to the kingdom's Control DC + 5. You cannot Relocate your Capital again for at least 3 Kingdom turns.",
        Skill.Industry, true,
        "One of your settlements that is not your current capital must contain a Castle, Palace, or Town Hall. All leaders must spend all of their leadership activities during the Activity phase of a Kingdom turn on this activity.",
        "The move goes off splendidly, with people excited about the new capital and celebrating the leadership's wisdom.",
        "The move goes smoothly and with minimal disruption, but some folks are upset or homesick. Increase Unrest by 1.",
        "The move causes unhappiness. Gain 1 Unrest and increase two Ruins of your choice by 1.",
        "The people reject the idea of the new capital and demand you move it back. The move is unsuccessful, and your capital remains unchanged. Gain 1d4 Unrest. Increase three Ruins of your choice by 1 and the fourth Ruin by 3.",
        525, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction Infiltration = new("Infiltration",
        "You send spies out to gather intelligence on a neighboring nation, a cult or thieves' guild within your borders, an unclaimed Freehold, or even an unexplored adventure site. Alternately, you can simply send your spies out to investigate the current health of your kingdom. Attempt a basic check.",
        Skill.Intrigue, false, null,
        "You learn something valuable or helpful. If you were infiltrating a specific target, the GM decides what is learned, but the information is exact and precise. For example, if you were infiltrating an unexplored ruin, you might learn that the site is infested with web lurkers and spider swarms. If you were investigating your kingdom's health, your spies reveal easy methods to address citizen dissatisfaction, allowing you to choose one of the following: reduce Unrest by 1d4 or reduce a Ruin of your choice by 1.",
        "You learn something helpful about the target, but the information is vague and imprecise. For example, if you were infiltrating the same ruin mentioned in the critical success above, you might learn that some sort of aberration uses the ruins as its lair. If you were investigating your kingdom's health, your spies learn enough that you can take action. Reduce your kingdom's Unrest by 1.",
        "Your spies fail to learn anything of import, but they are not themselves compromised.",
        "You never hear from your spies again, but someone certainly does! You take a –2 circumstance penalty on all kingdom checks until the end of the next Kingdom turn as counter-infiltration from an unknown enemy tampers with your kingdom's inner workings.",
        526, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction ClandestineBusiness = new("Clandestine Business",
        "You know there are criminals in your kingdom, and they know you know. You encourage them to send kickbacks in the form of resources and Commodities to the government, but the common citizens will be more than upset if they find out! This starts as a basic check against your Control DC, but every subsequent Kingdom turn you pursue Clandestine Business, the DC increases by 2. Every Kingdom turn that passes without Clandestine Business reduces the DC by 1 (until you reach your Control DC).",
        Skill.Intrigue, true, null,
        "Immediately roll 2 Resource Dice. Gain RP equal to the result. In addition, you gain 1d4 Luxury Commodities. The public is none the wiser.",
        "Either immediately roll 2 Resource Dice and gain RP equal to the result, or gain 1d4 Luxury Commodities. Regardless of your choice, rumors spread about where the government is getting these \"gifts.\" Increase Unrest by 1.",
        "Immediately roll 1 Resource Die and gain RP equal to the result. Rumors are backed up with eyewitness accounts. Increase Unrest by 1 and Corruption by 1.",
        "You gain nothing from the Clandestine Business but angry citizens. Increase Unrest by 1d6, Corruption by 2, and one other Ruin of your choice by 1.",
        526, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction SupernaturalSolution = new("Supernatural Solution",
        "Your spellcasters try to resolve issues when mundane solutions just aren't enough. Attempt a basic check.",
        Skill.Magic, false, null,
        "You can call upon your spellcasters' supernatural solution to aid in resolving any Kingdom skill check made during the remainder of this Kingdom turn. Do so just before a Kingdom skill check is rolled (by yourself or any other PC). Attempt a Magic check against the same DC in addition to the Kingdom skill check, and take whichever of the two results you prefer. If you don't use your Supernatural Solution by the end of this Kingdom turn, this benefit ends and you gain 10 kingdom XP instead.",
        "As critical success, but the solution costs the kingdom 1d4 RP to research. This cost is paid now, whether or not you use your supernatural solution.",
        "Your attempt at researching a supernatural solution costs the kingdom 2d6 RP, but is ultimately a failure, providing no advantage.",
        "As failure, but your spellcasters' resources and morale are impacted such that you cannot attempt a Supernatural Solution again for 2 Kingdom turns. Special You cannot influence a check with Supernatural Solution and Creative Solution simultaneously",
        526, Trait.Downtime, Trait.Fortune, Trait.Leadership);

    public static readonly SkillAction Prognostication = new("Prognostication",
        "Your kingdom's spellcasters read the omens and provide advice on how best to prepare for near-future events. Attempt a basic check.",
        Skill.Magic, true, null,
        "If you have a random kingdom event this turn, roll twice to determine the event that takes place. The players choose which of the two results occurs, and the kingdom gains a +2 circumstance bonus to the check to resolve the event.",
        "Gain a +1 circumstance bonus to checks made to resolve random kingdom events this turn.",
        "Your spellcasters divine no aid.",
        "Your spellcasters provide inaccurate readings of the future. You automatically have a random kingdom event this turn. Roll twice to determine the event that takes place; the GM decides which of the two results occurs.",
        527, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction ImproveLifestyle = new("Improve Lifestyle",
        "Attempt a basic check to draw upon your kingdom's treasury to enhance the quality of life for your citizens. This activity can be taken only during the Commerce phase of a Kingdom turn.",
        Skill.Politics, false, null,
        "Your push to Improve Lifestyles affords your citizens significant free time to pursue recreational activities. For the remainder of the Kingdom turn, you gain a +2 circumstance bonus to Culture-based checks.",
        "Your push to Improve Lifestyles helps your citizens enjoy life. For the remainder of the Kingdom turn, you gain a +1 circumstance bonus to Culture-based checks.",
        "As success, but you've strained your treasury. Take a –1 circumstance penalty to Economy-based checks for the remainder of this Kingdom turn.",
        "Your attempt to Improve Lifestyles backfires horribly as criminal elements in your kingdom abuse your generosity. You take a –1 circumstance penalty to Economy-based checks for the remainder of the Kingdom turn, gain 1 Unrest, and add 1 to a Ruin of your choice.",
        527, Trait.Commerce, Trait.Downtime);

    public static readonly SkillAction CreativeSolution = new("Creative Solution",
        "You work with your kingdom's scholars, thinkers, and practitioners of magical and mundane experimentation to come up with new ways to resolve issues when business as usual is just not working. Attempt a basic check.",
        Skill.Scholarship, false, null,
        "You can call upon the solution to aid in resolving any Kingdom skill check made during the remainder of this Kingdom turn. Do so when a Kingdom skill check is rolled, but before you learn the result. Immediately reroll that check with a +2 circumstance bonus; you must take the new result. If you don't use your Creative Solution by the end of this turn, you lose this benefit and gain 10 kingdom XP instead.",
        "As critical success, but the Creative Solution costs the kingdom 1d4 RP to research. This cost is paid now, whether or not you use your Creative Solution.",
        "Your attempt at researching a Creative Solution costs the kingdom 2d6 RP but is ultimately a failure. It provides no advantage.",
        "As failure, but your scholars and thinkers are so frustrated that you take a –1 circumstance penalty to Culture-based checks until the end of the next Kingdom turn.",
        527, Trait.Downtime, Trait.Fortune, Trait.Leadership);

    public static readonly SkillAction RequestForeignAid = new("Request Foreign Aid",
        "When disaster strikes, you send out a call for help to another nation with whom you have diplomatic relations. The DC of this check is equal to the other group's Negotiation DC +2.",
        Skill.Statecraft, true, "You have diplomatic relations with the group you are requesting aid from.",
        "Your ally's aid grants a +4 circumstance bonus to any one Kingdom skill check attempted during the remainder of this Kingdom turn. You can choose to apply this bonus to any Kingdom skill check after the die is rolled, but must do so before the result is known. In addition, immediately roll 2 Resource Dice and gain RP equal to the result; this RP does not accrue into XP at the end of the turn if you don't spend it.",
        "As success, but choose the benefit given by the aid: either roll 1 Resource Die and gain RP equal to the result or gain a +2 circumstance bonus to a check.",
        "Your ally marshals its resources but cannot get aid to you in time to deal with your current situation. At the start of your next Kingdom turn, gain 1d4 RP.",
        "Your ally is tangled up in its own problems and is unable to assist you, is insulted by your request for aid, or might even have an interest in seeing your kingdom struggle against one of your ongoing events. Whatever the case, your pleas for aid make your kingdom look desperate. You gain no aid, but you do increase Unrest by 1d4.",
        528, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction SendDiplomaticEnvoy = new("Send Diplomatic Envoy",
        "You send emissaries to another group to foster positive relations and communication. The DC of this check is the group's Negotiation DC. Attempts to Send a Diplomatic Envoy to a nation with which your kingdom is at war take a –4 circumstance penalty to the check and have the result worsened one degree.",
        Skill.Statecraft, true, null,
        "Your envoys are received quite warmly and make a good first impression. You establish diplomatic relations with the group and gain a +2 circumstance bonus to all checks made with that group until the next Kingdom turn.",
        "You establish diplomatic relations.",
        "Your envoys are received, but the target organization isn't ready to engage in diplomatic relations. If you attempt to Send a Diplomatic Envoy to the group next Kingdom turn, you gain a +2 circumstance bonus to that check.",
        "Disaster! Your envoy fails to reach their destination, is turned back at the border, or is taken prisoner or executed. The repercussions on your kingdom's morale and reputation are significant. Choose one of the following results: gain 1d4 Unrest, add 1 to a Ruin of your choice, or immediately roll 2 Resource Dice and spend RP equal to the result. In any event, you cannot attempt to Send a Diplomatic Envoy to this same target for the next 3 Kingdom turns.",
        528, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction CapitalInvestment = new("Capital Investment",
        "You contribute funds from your personal wealth for the good of the kingdom, including coinage, gems, jewelry, weapons and armor salvaged from enemies, magical or alchemical items, and so on. Your contribution generates economic activity in the form of RP that can be used during your current Kingdom turn or on the next Kingdom turn (your choice).  You can use Capital Investment to repay funds from Tap Treasury. In this case, no roll is needed and you simply deduct the appropriate amount of funds from your personal wealth to pay back that which was borrowed. When you use Capital Investment to generate RP, the amount of gp required to make an investment is set by your kingdom's level. Investments below this amount cause your attempt at Capital Investment to suffer an automatic critical failure, while investments above this amount are lost. The investment required is equal to the value listed on Table 10–9: Party Treasure by Level in the Pathfinder Core Rulebook (page 509); use the value for your kingdom's level under the “Currency per Additional PC” as the required investment value. This is a basic check.",
        Skill.Trade, false, "You must be within the influence of a settlement that contains at least one Bank.",
        "Your kingdom reaps the benefits of your investment. Immediately roll 4 Resource Dice. Gain RP equal to the result.",
        "Your investment helps the economy. Immediately roll 2 Resource Dice. Gain RP equal to the result.",
        "Your investment ends up being used to shore up shortfalls elsewhere. Gain 1d4 RP.",
        "Your investment is embezzled, lost, or otherwise misappropriated. Choose one of the following: either roll 1 Resource Die and gain RP equal to the result and also increase your Crime by an equal amount, or gain 0 RP and increase Crime by 1.",
        529, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction ManageTradeAgreements = new("Manage Trade Agreements",
        "You send agents out to attend to established trade agreements. Spend 2 RP per Trade Agreement you wish to manage. Then attempt a basic check. If you Managed Trade Agreements on the previous turn, increase this DC by 5.",
        Skill.Trade, false, null,
        "At the start of your next Kingdom turn, you gain 1 bonus Resource Die per trade agreement, and 1 Commodity of your choice per trade agreement (no more than half of these Commodities may be Luxuries).",
        "As critical success, but you must choose between gaining Resource Dice or Commodities.",
        "You gain 1 RP per trade agreement at the start of your next turn.",
        "You gain no benefit, as your traders and merchants met with bad luck on the road. You can't Manage Trade Agreements for 1 Kingdom turn.",
        529, Trait.Commerce, Trait.Downtime);

    public static readonly SkillAction PurchaseCommodities = new("Purchase Commodities",
        "You can spend RP to Purchase Commodities, but doing so is more expensive than gathering them or relying upon trade agreements. When you Purchase Commodities, select the Commodity you wish to purchase (Food, Lumber, Luxuries, Ore, or Stone). Expend 8 RP if you're purchasing Luxuries or 4 RP if you're purchasing any other Commodity. Then attempt a basic check.",
        Skill.Trade, false, null,
        "You immediately gain 4 Commodities of the chosen type and 2 Commodities of any other type (except Luxuries).",
        "You gain 2 Commodities of the chosen type.", "You gain 1 Commodity of the chosen type.",
        "You gain no Commodities.", 529, Trait.Downtime, Trait.Leadership);

    public static readonly SkillAction CollectTaxes = new("Collect Taxes",
        "Tax collectors travel through the lands to collect funds for the betterment of the kingdom. Attempt a basic check.",
        Skill.Trade, true, null,
        "Your tax collectors are wildly successful! For the remainder of the Kingdom turn, gain a +2 circumstance bonus to Economy-based checks.",
        "Your tax collectors gather enough to grant you a +1 circumstance bonus to Economy-based checks for the remainder of the Kingdom turn. If you attempted to Collect Taxes during the previous turn, increase Unrest by 1.",
        "As success, but the people are unhappy about taxes—increase Unrest by 1 (or by 2 if you attempted to Collect Taxes the previous turn).",
        "Your tax collectors encounter resistance from the citizens and their attempts to gather taxes are rebuffed. While the tax collectors still manage to gather enough taxes to support essential government needs, they have angered the kingdom's citizens and encouraged rebellious acts. Increase Unrest by 2, and choose one Ruin to increase by 1.",
        530, Trait.Commerce, Trait.Downtime);

    public static readonly SkillAction GatherLivestock = new("Gather Livestock",
        "Attempt a basic check to gather excess livestock from local wildlife, ranches, and farms. This generates a number of Food commodities.",
        Skill.Wilderness, false, null, "Gain 1d4 Food commodities.", "Gain 1 Food commodity.",
        "Gain no Food commodities.",
        "Lose 1d4 Food commodities to spoilage. If you have no Food to lose, you instead gain 1 Unrest.", 530,
        Trait.Downtime, Trait.Region);

    private static readonly Lazy<ImmutableSortedDictionary<Skill, ImmutableSortedSet<SkillAction>>> ActionsBySkill =
        new(() => GetAll()
                .GroupBy(action => action.RelatedSkill)
                .ToImmutableSortedDictionary(actions => actions.Key, actions => actions.ToImmutableSortedSet()),
            LazyThreadSafetyMode.ExecutionAndPublication);

    private SkillAction(string name, string description, Skill relatedSkill, bool trained, string? requirements,
        string criticalSuccess, string success, string failure, string criticalFailure, ushort page,
        params Trait[] traits) : base(name, description, Source.KingmakerAdventurePath, page)
    {
        RelatedSkill = relatedSkill;
        Trained = trained;
        Requirements = requirements;
        CriticalSuccess = criticalSuccess;
        Success = success;
        Failure = failure;
        CriticalFailure = criticalFailure;
        Traits = traits.ToImmutableSortedSet();
    }

    public Skill RelatedSkill { get; }

    public bool Trained { get; }

    public string? Requirements { get; }

    public string CriticalSuccess { get; }

    public string Success { get; }

    public string Failure { get; }

    public string CriticalFailure { get; }

    public ImmutableSortedSet<Trait> Traits { get; }

    public static ManyCommandPartition<SkillAction>? ManyCommandPartition { get; } = new()
    {
        Name = "Related Skill",
        ValueOfFunction = action => action.RelatedSkill.Name
    };

    public void Deconstruct(out string name, out string description, out Source source, out ushort page,
        out Skill relatedSkill, out bool trained, out string? requirements, out string criticalSuccess,
        out string success, out string failure, out string criticalFailure, out ImmutableSortedSet<Trait> traits)
    {
        name = Name;
        description = Description;
        source = Source;
        page = Page;
        relatedSkill = RelatedSkill;
        trained = Trained;
        requirements = Requirements;
        criticalSuccess = CriticalSuccess;
        success = Success;
        failure = Failure;
        criticalFailure = CriticalFailure;
        traits = Traits;
    }

    public static IEnumerable<SkillAction> GetAllBySkill(Skill skill)
    {
        return ActionsBySkill.Value.GetValueOrDefault(skill, ImmutableSortedSet<SkillAction>.Empty);
    }
}