using UnityEngine;

namespace TownOfUsFusion.Patches
{
    class Colors
{

    // Crew Colors
    public readonly static Color Crewmate = Color.white;
    public readonly static Color Mayor = new(0.44f, 0.31f, 0.66f, 1f);
    public readonly static Color Sheriff = Color.yellow;
    public readonly static Color Engineer = new(1f, 0.65f, 0.04f, 1f);
    public readonly static Color Swapper = new(0.4f, 0.9f, 0.4f, 1f);
    public readonly static Color Investigator = new(0f, 0.7f, 0.7f, 1f);
    public readonly static Color Medic = new(0f, 0.4f, 0f, 1f);
    public readonly static Color Seer = new(1f, 0.8f, 0.5f, 1f);
    public readonly static Color Spy = new(0.8f, 0.64f, 0.8f, 1f);
    public readonly static Color Snitch = new(0.83f, 0.69f, 0.22f, 1f);
    public readonly static Color Altruist = new(0.4f, 0f, 0f, 1f);
    public readonly static Color Vigilante = new(1f, 1f, 0.6f, 1f);
    public readonly static Color Veteran = new(0.6f, 0.5f, 0.25f, 1f);
    public readonly static Color Haunter = new(0.83f, 0.83f, 0.83f, 1f);
    public readonly static Color Tracker = new(0f, 0.6f, 0f, 1f);
    public readonly static Color Transporter = new(0f, 0.93f, 1f, 1f);
    public readonly static Color Medium = new(0.65f, 0.5f, 1f, 1f);
    public readonly static Color Mystic = new(0.3f, 0.6f, 0.9f, 1f);
    public readonly static Color Trapper = new(0.65f, 0.82f, 0.7f, 1f);
    public readonly static Color Detective = new(0.3f, 0.3f, 1f, 1f);
    public readonly static Color Chameleon = new(0.5f, 0.5f, 0f, 1f);
    public readonly static Color Imitator = new(0.7f, 0.85f, 0.3f, 1f);
    public readonly static Color VampireHunter = new(0.7f, 0.7f, 0.9f, 1f);
    public readonly static Color Trickster = new(0.92f, 0.64f, 0.6f, 1f);
    public readonly static Color Prosecutor = new(0.7f, 0.5f, 0f, 1f);
    public readonly static Color Oracle = new(0.75f, 0f, 0.75f, 1f);
    public readonly static Color Aurial = new(0.7f, 0.3f, 0.6f, 1f);
    public readonly static Color Hunter = new Color(0.16f, 0.67f, 0.53f, 1f);

    // Neutral Colors
    public readonly static Color Jester = new(1f, 0.75f, 0.8f, 1f);
    public readonly static Color Executioner = new(0.55f, 0.25f, 0.02f, 1f);
    public readonly static Color Glitch = Color.green;
    public readonly static Color Arsonist = new(1f, 0.3f, 0f);
    public readonly static Color Phantom = new(0.4f, 0.16f, 0.38f, 1f);
    public readonly static Color Amnesiac = new(0.5f, 0.7f, 1f, 1f);
    public readonly static Color Survivor = new(1f, 0.9f, 0.3f, 1f);
    public readonly static Color GuardianAngel = new(0.7f, 1f, 1f, 1f);
    public readonly static Color Werewolf = new(0.66f, 0.4f, 0.16f, 1f);
    public readonly static Color Doomsayer = new(0f, 1f, 0.5f, 1f);
    public readonly static Color Vampire = new(0.15f, 0.15f, 0.15f, 1f);
    // TOU FUSION ROLES
    
    public readonly static Color Bodyguard = new(0.5f, 0.83f, 0.67f, 1f);
    public readonly static Color Taskmaster = new(0.77f, 1f, 1f, 1f);
    public readonly static Color Mage = Palette.ImpostorRed;
    public readonly static Color Captain = Palette.ImpostorRed;
    public readonly static Color Monarch = Palette.ImpostorRed;
    public readonly static Color Sentinel = new(0.56f, 0.64f, 0.55f, 1f);
    public readonly static Color Bartender = Palette.ImpostorRed;
    
    public readonly static Color Joker = new(0.75f, 1f, 0.52f, 1f);
    public readonly static Color Pirate = Palette.ImpostorRed;
    public readonly static Color CursedSoul = new(0.64f, 0.64f, 0.91f, 1f);
    public readonly static Color Fraud = Palette.ImpostorRed;
    public readonly static Color Tempest = Palette.ImpostorRed;
    public readonly static Color Tyrant = new(0.92f, 0.33f, 0.36f, 1f);
    public readonly static Color Cannibal = new(0.55f, 0.27f, 0.07f, 1f);
    public readonly static Color Witch = Palette.ImpostorRed;
    public readonly static Color Inquisitor = new(0.85f, 0.26f, 0.57f, 1f);
    public readonly static Color Ghoul = Palette.ImpostorRed;
    public readonly static Color Mercenary = Palette.ImpostorRed;
    public readonly static Color SerialKiller = Palette.ImpostorRed;
    
    public readonly static Color NeoNecromancer = new(0.89f, 0.15f, 0.35f, 1f);

    //Base Colors
    public readonly static Color Impostor = Palette.ImpostorRed;
    public readonly static Color RegularApoc = new(1f, 0.91f, 0.7f, 1f);
    public readonly static Color TrueApoc = new(0.42f, 0.36f, 0.23f, 1f);

    //Modifiers
    public readonly static Color Bait = new(0f, 0.7f, 0.7f, 1f);
    public readonly static Color Oblivious = new(0.6f, 0.6f, 0.6f, 1f);
    public readonly static Color Drunk = new(0.46f, 0.5f, 0f, 1f);
    public readonly static Color Aftermath = new(0.65f, 1f, 0.65f, 1f);
    public readonly static Color Diseased = Color.grey;
    public readonly static Color Torch = new(1f, 1f, 0.6f, 1f);
    public readonly static Color ButtonBarry = new(0.9f, 0f, 1f, 1f);
    public readonly static Color Dwarf = new(1f, 0.5f, 0.5f, 1f);
    public readonly static Color Eclipsed = new(0.85f, 0.56f, 0.44f, 1f);
    public readonly static Color Giant = new(1f, 0.7f, 0.3f, 1f);
    public readonly static Color Sleuth = new(0.5f, 0.2f, 0.2f, 1f);
    public readonly static Color Tiebreaker = new(0.6f, 0.9f, 0.6f, 1f);
    public readonly static Color Radar = new(1f, 0f, 0.5f, 1f);
    public readonly static Color Multitasker = new(1f, 0.5f, 0.3f, 1f);
    public readonly static Color Frosty = new(0.6f, 1f, 1f, 1f);
    // ALLIANCE COLORS
    public readonly static Color Recruit = new(0.31f, 0.3f, 0.36f, 1f);
    public readonly static Color Lovers = new(1f, 0.4f, 0.53f, 1f);
    public readonly static Color Crewpocalypse = new(1f, 0.4f, 0.8f, 1f);
    public readonly static Color Egotist = new(0.23f, 0.38f, 0.25f, 1f);
    // OTHER STUFF
    public readonly static Color Role = Color.white;
    public readonly static Color Modifier = Color.white;
    public readonly static Color Neutral = Color.white;
    public readonly static Color Faction = Color.white;
    public readonly static Color Alignment = Color.white;
}
}
