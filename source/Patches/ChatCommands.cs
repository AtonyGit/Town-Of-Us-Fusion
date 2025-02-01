using HarmonyLib;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;

namespace TownOfUsFusion.Patches
{
    [HarmonyPatch]
    public static class ChatCommands
    {
        public static bool JailorMessage = false;

        [HarmonyPatch(typeof(ChatController), nameof(ChatController.AddChat))]
        public static class PrivateJaileeChat
        {
            public static bool Prefix(ChatController __instance, [HarmonyArgument(0)] ref PlayerControl sourcePlayer, ref string chatText)
            {
                if (sourcePlayer == PlayerControl.LocalPlayer)
                {
                    string text = chatText.ToLower().Trim();
                    if (text.StartsWith("/help"))
                    {
                        AddCustomMessage("help");
                        return false;
                    }
                    if (text.StartsWith("/crewroles") || text.StartsWith("/crewmateroles"))
                    {
                        AddCustomMessage("crewroles");
                        return false;
                    }
                    if (text.StartsWith("/neutralroles") || text.StartsWith("/neutroles"))
                    {
                        AddCustomMessage("neutralroles");
                        return false;
                    }
                    if (text.StartsWith("/impostorroles") || text.StartsWith("/improles") || text.StartsWith("/imposterroles"))
                    {
                        AddCustomMessage("improles");
                        return false;
                    }
                    if (text.StartsWith("/allmod"))
                    {
                        AddCustomMessage("modifiers");
                        return false;
                    }
                    if (text.StartsWith("/allall"))
                    {
                        AddCustomMessage("alliances");
                        return false;
                    }
                    if (text.StartsWith("/crew"))
                    {
                        AddRoleMessage(RoleEnum.Crewmate);
                        return false;
                    }
                    else if (text.StartsWith("/imp"))
                    {
                        AddRoleMessage(RoleEnum.Impostor);
                        return false;
                    }
                    else if (text.StartsWith("/alt"))
                    {
                        AddRoleMessage(RoleEnum.Altruist);
                        return false;
                    }
                    else if (text.StartsWith("/engi"))
                    {
                        AddRoleMessage(RoleEnum.Engineer);
                        return false;
                    }
                    else if (text.StartsWith("/invest"))
                    {
                        AddRoleMessage(RoleEnum.Investigator);
                        return false;
                    }
                    else if (text.StartsWith("/mayor"))
                    {
                        AddRoleMessage(RoleEnum.Mayor);
                        return false;
                    }
                    else if (text.StartsWith("/medic"))
                    {
                        AddRoleMessage(RoleEnum.Medic);
                        return false;
                    }
                    else if (text.StartsWith("/sher"))
                    {
                        AddRoleMessage(RoleEnum.Sheriff);
                        return false;
                    }
                    else if (text.StartsWith("/swap"))
                    {
                        AddRoleMessage(RoleEnum.Swapper);
                        return false;
                    }
                    else if (text.StartsWith("/psy"))
                    {
                        AddRoleMessage(RoleEnum.Psychic);
                        return false;
                    }
                    else if (text.StartsWith("/spy"))
                    {
                        AddRoleMessage(RoleEnum.Spy);
                        return false;
                    }
                    else if (text.StartsWith("/vig"))
                    {
                        AddRoleMessage(RoleEnum.Vigilante);
                        return false;
                    }
                    else if (text.StartsWith("/hunt"))
                    {
                        AddRoleMessage(RoleEnum.Hunter);
                        return false;
                    }
                    else if (text.StartsWith("/arso"))
                    {
                        AddRoleMessage(RoleEnum.Arsonist);
                        return false;
                    }
                    else if (text.StartsWith("/exe"))
                    {
                        AddRoleMessage(RoleEnum.Executioner);
                        return false;
                    }
                    else if (text.StartsWith("/glitch") ||
                        text.StartsWith("/theglitch") ||
                        text.StartsWith("/the glitch"))
                    {
                        AddRoleMessage(RoleEnum.Glitch);
                        return false;
                    }
                    else if (text.StartsWith("/jest"))
                    {
                        AddRoleMessage(RoleEnum.Jester);
                        return false;
                    }
                    else if (text.StartsWith("/phan"))
                    {
                        AddRoleMessage(RoleEnum.Phantom);
                        return false;
                    }
                    else if (text.StartsWith("/gren"))
                    {
                        AddRoleMessage(RoleEnum.Grenadier);
                        return false;
                    }
                    else if (text.StartsWith("/jan"))
                    {
                        AddRoleMessage(RoleEnum.Janitor);
                        return false;
                    }
                    else if (text.StartsWith("/mini"))
                    {
                        AddModifierMessage(ModifierEnum.Mini);
                        return false;
                    }
                    else if (text.StartsWith("/miner"))
                    {
                        AddRoleMessage(RoleEnum.Miner);
                        return false;
                    }
                    else if (text.StartsWith("/morph"))
                    {
                        AddRoleMessage(RoleEnum.Morphling);
                        return false;
                    }
                    else if (text.StartsWith("/swoop"))
                    {
                        AddRoleMessage(RoleEnum.Swooper);
                        return false;
                    }
                    else if (text.StartsWith("/utaker") || 
                        text.StartsWith("/undertaker"))
                    {
                        AddRoleMessage(RoleEnum.Undertaker);
                        return false;
                    }
                    else if (text.StartsWith("/haunt"))
                    {
                        AddRoleMessage(RoleEnum.Haunter);
                        return false;
                    }
                    else if (text.StartsWith("/vet"))
                    {
                        AddRoleMessage(RoleEnum.Veteran);
                        return false;
                    }
                    else if (text.StartsWith("/amne"))
                    {
                        AddRoleMessage(RoleEnum.Amnesiac);
                        return false;
                    }
                    else if (text.StartsWith("/jugg"))
                    {
                        AddRoleMessage(RoleEnum.Juggernaut);
                        return false;
                    }
                    else if (text.StartsWith("/track"))
                    {
                        AddRoleMessage(RoleEnum.Tracker);
                        return false;
                    }
                    else if (text.StartsWith("/trans"))
                    {
                        AddRoleMessage(RoleEnum.Transporter);
                        return false;
                    }
                    else if (text.StartsWith("/trait"))
                    {
                        AddRoleMessage(RoleEnum.Traitor);
                        return false;
                    }
                    else if (text.StartsWith("/med"))
                    {
                        AddRoleMessage(RoleEnum.Medium);
                        return false;
                    }
                    else if (text.StartsWith("/trap"))
                    {
                        AddRoleMessage(RoleEnum.Trapper);
                        return false;
                    }
                    else if (text.StartsWith("/surv"))
                    {
                        AddRoleMessage(RoleEnum.Survivor);
                        return false;
                    }
                    else if (text.StartsWith("/ga") ||
                        text.StartsWith("/guardian"))
                    {
                        AddRoleMessage(RoleEnum.GuardianAngel);
                        return false;
                    }
                    else if (text.StartsWith("/bmer") ||
                        text.StartsWith("/black"))
                    {
                        AddRoleMessage(RoleEnum.Blackmailer);
                        return false;
                    }
                    else if (text.StartsWith("/pb") ||
                        text.StartsWith("/plague"))
                    {
                        AddRoleMessage(RoleEnum.Plaguebearer);
                        return false;
                    }
                    else if (text.StartsWith("/pest"))
                    {
                        AddRoleMessage(RoleEnum.Pestilence);
                        return false;
                    }
                    else if (text.StartsWith("/ww") ||
                        text.StartsWith("/were"))
                    {
                        AddRoleMessage(RoleEnum.Werewolf);
                        return false;
                    }
                    else if (text.StartsWith("/escap"))
                    {
                        AddRoleMessage(RoleEnum.Escapist);
                        return false;
                    }
                    else if (text.StartsWith("/imitat"))
                    {
                        AddRoleMessage(RoleEnum.Imitator);
                        return false;
                    }
                    else if (text.StartsWith("/bomb"))
                    {
                        AddRoleMessage(RoleEnum.Bomber);
                        return false;
                    }
                    else if (text.StartsWith("/doom"))
                    {
                        AddRoleMessage(RoleEnum.Doomsayer);
                        return false;
                    }
                    else if (text.StartsWith("/cann"))
                    {
                        AddRoleMessage(RoleEnum.Cannibal);
                        return false;
                    }
                    else if (text.StartsWith("/ty"))
                    {
                        AddRoleMessage(RoleEnum.Tyrant);
                        return false;
                    }
                    else if (text.StartsWith("/bg") || text.StartsWith("/body"))
                    {
                        AddRoleMessage(RoleEnum.Bodyguard);
                        return false;
                    }
                    else if (text.StartsWith("/mirror") || text.StartsWith("/mm"))
                    {
                        AddRoleMessage(RoleEnum.MirrorMaster);
                        return false;
                    }
                    else if (text.StartsWith("/capt"))
                    {
                        AddRoleMessage(RoleEnum.Captain);
                        return false;
                    }
                    else if (text.StartsWith("/law"))
                    {
                        AddRoleMessage(RoleEnum.Lawyer);
                        return false;
                    }
                    else if (text.StartsWith("/inquis"))
                    {
                        AddRoleMessage(RoleEnum.Inquisitor);
                        return false;
                    }
                    else if (text.StartsWith("/pois"))
                    {
                        AddRoleMessage(RoleEnum.Poisoner);
                        return false;
                    }
                    else if (text.StartsWith("/vamp"))
                    {
                        AddRoleMessage(RoleEnum.Vampire);
                        return false;
                    }
                    else if (text.StartsWith("/pros"))
                    {
                        AddRoleMessage(RoleEnum.Prosecutor);
                        return false;
                    }
                    else if (text.StartsWith("/war"))
                    {
                        AddRoleMessage(RoleEnum.Warlock);
                        return false;
                    }
                    else if (text.StartsWith("/ora"))
                    {
                        AddRoleMessage(RoleEnum.Oracle);
                        return false;
                    }
                    else if (text.StartsWith("/ven"))
                    {
                        AddRoleMessage(RoleEnum.Venerer);
                        return false;
                    }
                    else if (text.StartsWith("/aur"))
                    {
                        AddRoleMessage(RoleEnum.Aurial);
                        return false;
                    }
                    else if (text.StartsWith("/poli"))
                    {
                        AddRoleMessage(RoleEnum.Politician);
                        return false;
                    }
                    else if (text.StartsWith("/hypno"))
                    {
                        AddRoleMessage(RoleEnum.Hypnotist);
                        return false;
                    }
                    else if (text.StartsWith("/jailor"))
                    {
                        AddRoleMessage(RoleEnum.Jailor);
                        return false;
                    }
                    else if (text.StartsWith("/sk") || text.StartsWith("/seri"))
                    {
                        AddRoleMessage(RoleEnum.SerialKiller);
                        return false;
                    }
                    else if (text.StartsWith("/sc") ||
                        text.StartsWith("/soul"))
                    {
                        AddRoleMessage(RoleEnum.SoulCollector);
                        return false;
                    }
                    else if (text.StartsWith("/dep"))
                    {
                        AddRoleMessage(RoleEnum.Deputy);
                        return false;
                    }
                    else if (text.StartsWith("/love"))
                    {
                        AddAllianceMessage(AllianceEnum.Lover);
                        return false;
                    }
                    else if (text.StartsWith("/rec"))
                    {
                        AddAllianceMessage(AllianceEnum.Recruit);
                        return false;
                    }
                    else if (text.StartsWith("/crewpost"))
                    {
                        AddAllianceMessage(AllianceEnum.Crewpostor);
                        return false;
                    }
                    else if (text.StartsWith("/crewpoc"))
                    {
                        AddAllianceMessage(AllianceEnum.Crewpocalypse);
                        return false;
                    }
                    else if (text.StartsWith("/ego"))
                    {
                        AddAllianceMessage(AllianceEnum.Egotist);
                        return false;
                    }
                    else if (text.StartsWith("/lo"))
                    {
                        AddRoleMessage(RoleEnum.Lookout);
                        return false;
                    }
                    else if (text.StartsWith("/giant"))
                    {
                        AddModifierMessage(ModifierEnum.Giant);
                        return false;
                    }
                    else if (text.StartsWith("/drunk"))
                    {
                        AddModifierMessage(ModifierEnum.Drunk);
                        return false;
                    }
                    else if (text.StartsWith("/Obliv"))
                    {
                        AddModifierMessage(ModifierEnum.Oblivious);
                        return false;
                    }
                    else if (text.StartsWith("/button"))
                    {
                        AddModifierMessage(ModifierEnum.ButtonBarry);
                        return false;
                    }
                    else if (text.StartsWith("/after"))
                    {
                        AddModifierMessage(ModifierEnum.Aftermath);
                        return false;
                    }
                    else if (text.StartsWith("/bait"))
                    {
                        AddModifierMessage(ModifierEnum.Bait);
                        return false;
                    }
                    else if (text.StartsWith("/dis"))
                    {
                        AddModifierMessage(ModifierEnum.Diseased);
                        return false;
                    }
                    else if (text.StartsWith("/flash"))
                    {
                        AddModifierMessage(ModifierEnum.Flash);
                        return false;
                    }
                    else if (text.StartsWith("/tie"))
                    {
                        AddModifierMessage(ModifierEnum.Tiebreaker);
                        return false;
                    }
                    else if (text.StartsWith("/torch"))
                    {
                        AddModifierMessage(ModifierEnum.Torch);
                        return false;
                    }
                    else if (text.StartsWith("/sleuth"))
                    {
                        AddModifierMessage(ModifierEnum.Sleuth);
                        return false;
                    }
                    else if (text.StartsWith("/radar"))
                    {
                        AddModifierMessage(ModifierEnum.Radar);
                        return false;
                    }
                    else if (text.StartsWith("/dis"))
                    {
                        AddModifierMessage(ModifierEnum.Disperser);
                        return false;
                    }
                    else if (text.StartsWith("/multi"))
                    {
                        AddModifierMessage(ModifierEnum.Multitasker);
                        return false;
                    }
                    else if (text.StartsWith("/double"))
                    {
                        AddModifierMessage(ModifierEnum.DoubleShot);
                        return false;
                    }
                    else if (text.StartsWith("/udog") ||
                        text.StartsWith("/underdog"))
                    {
                        AddModifierMessage(ModifierEnum.Underdog);
                        return false;
                    }
                    else if (text.StartsWith("/frost"))
                    {
                        AddModifierMessage(ModifierEnum.Frosty);
                        return false;
                    }
                    else if ((text.StartsWith("/sense")) ||
                        text.StartsWith("/sixth"))
                    {
                        AddModifierMessage(ModifierEnum.SixthSense);
                        return false;
                    }
                    else if (text.StartsWith("/shy"))
                    {
                        AddModifierMessage(ModifierEnum.Shy);
                        return false;
                    }
                    else if (text.StartsWith("/sab"))
                    {
                        AddModifierMessage(ModifierEnum.Saboteur);
                        return false;
                    }
                    else if (text.StartsWith("/ass"))
                    {
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                            "The Assassin is an ability which is given to killers to guess other player's roles during mettings. If they guess correctly they kill the other player, if not, they die instead.");
                        return false;
                    }
                    else if (text.StartsWith("/r") || text.StartsWith("/role"))
                    {
                        var role = Role.GetRole(PlayerControl.LocalPlayer);
                        if (role != null) AddRoleMessage(role.RoleType);
                        else if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started) DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "You do not have a role.");
                        else DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "Invalid Command.");
                        return false;
                    }
                    else if (text.StartsWith("/m") || text.StartsWith("/modifier"))
                    {
                        var modifier = Modifier.GetModifier(PlayerControl.LocalPlayer);
                        if (modifier != null) AddModifierMessage(modifier.ModifierType);
                        else if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started) DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "You do not have a modifier.");
                        else DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "Invalid Command.");
                        return false;
                    }
                }
                if ((chatText.ToLower().Trim().StartsWith("/jail")) && sourcePlayer.Is(RoleEnum.Jailor) && MeetingHud.Instance)
                {
                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Jailor) || PlayerControl.LocalPlayer.IsJailed())
                    {
                        if (chatText.ToLower().StartsWith("/jail")) chatText = chatText[5..];
                        else if (chatText.ToLower().StartsWith("/jail ")) chatText = chatText[6..];
                        else if (chatText.ToLower().StartsWith("/ jail")) chatText = chatText[6..];
                        else if (chatText.ToLower().StartsWith("/ jail ")) chatText = chatText[7..];
                        JailorMessage = true;
                        if (sourcePlayer != PlayerControl.LocalPlayer && PlayerControl.LocalPlayer.IsJailed() && !sourcePlayer.Data.IsDead) sourcePlayer = PlayerControl.LocalPlayer;
                        return true;
                    }
                    else return false;
                }
                if (chatText.ToLower().Trim().StartsWith("/"))
                {
                    if (sourcePlayer == PlayerControl.LocalPlayer) DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "Invalid Command.");
                    return false;
                }
                if (sourcePlayer.IsJailed() && MeetingHud.Instance)
                {
                    if (PlayerControl.LocalPlayer == sourcePlayer || PlayerControl.LocalPlayer.Is(RoleEnum.Jailor)) return true;
                    else return false;
                }
                if (PlayerControl.LocalPlayer.IsJailed() && MeetingHud.Instance) return false;
                return true;
            }

            public static void AddCustomMessage(string type)
            {
                if (type == "help") DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "By using /role, you may see what your role does in-game (or you may type a role). This applies to modifiers and alliances as well, but you may see all the commands by running the following: \n/crewroles | /neutroles | /improles | /allmodifiers | /allalliances");
                else if (type == "crewroles") DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "--------------- CREW INVESTIGATIVE ---------------\nAurial, Haunter, Investigator, Lookout, Medium, Spy, Psychic, Tracker, Trapper\n--------------------- CREW KILLING ---------------------\nDeputy, Hunter, Jailor, Sheriff, Veteran, Vigilante\n----------------- CREW PROTECTIVE -----------------\nAltruist, Bodyguard, Medic, Mirror Master, Oracle\n----------------- CREW SOVEREIGN ------------------\nCaptain, Politician, Prosecutor, Swapper\n--------------------- CREW UTILITY ---------------------\nEngineer, Imitator, Transporter");
                else if (type == "neutralroles") DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "------------------- NEUTRAL BENIGN ------------------\nAmnesiac, Guardian Angel, Lawyer, Survivor\n--------------------- NEUTRAL EVIL ---------------------\nDoomsayer, Executioner, Jester, Phantom\n------------------- NEUTRAL CHAOS -------------------\nCannibal, Inquisitor, Tyrant\n------------------- NEUTRAL KILLING ------------------\nArsonist, Serial Killer, The Glitch, Werewolf\n---------------- NEUTRAL NEOPHYTE ----------------\nJackal, Necromancer, Vampire\n-------------- neutral apocalypse --------------\nJuggernaut, Armaggeddon, Plaguebearer, Pestilence, Soul Collector, Death");
                else if (type == "improles") DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "------------- IMPOSTOR CONCEALING -------------\nEscapist, Grenadier, Morphling, Swooper, Venerer\n----------------- IMPOSTOR KILLING ------------------\nBomber, Poisoner, Traitor, Warlock\n---------------- IMPOSTOR SUPPORT ----------------\nBlackmailer, Hypnotist, Janitor, Miner, Undertaker");
                else if (type == "modifiers") DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "-------------- CREWMATE MODIFIERS --------------\nAftermath, Bait, Diseased, Frosty, Multitasker, Torch\n----------------- GLOBAL MODIFIERS -----------------\nButton Barry, Drunk, Oblivious, Flash, Giant, Mini, Radar, Shy, Sixth Sense, Sleuth, Tiebreaker\n--------------- IMPOSTOR MODIFIERS ---------------\nDisperser, Double Shot, Saboteur, Underdog");
                else if (type == "alliances") DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "-------------- CREWMATE ALLIANCES --------------\nCrewpostor, Crewpocalypse, Egotist\n----------------- GLOBAL ALLIANCES -----------------\nLover, Recruit");
            }
            public static void AddRoleMessage(RoleEnum role)
            {
                switch (role)
                {
                    case RoleEnum.Crewmate: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Crewmate is a vanilla Crewmate.");
                    break;
                    case RoleEnum.Impostor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Impostor a vanilla Impostor.");
                    break;
                    case RoleEnum.Altruist: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Altruist is a crewmate with the ability to revive other players, at the cost of death when they have one revive remaining");
                    break;
                    case RoleEnum.Engineer: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Engineer is a crewmate with the ability to vent and fix sabotages.");
                    break;
                    case RoleEnum.Mayor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Mayor is a crewmate with 3 votes and their role is revealed to everyone.");
                    break;
                    case RoleEnum.Medic: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Medic is a crewmate who can place a shield on another player.");
                    break;
                    case RoleEnum.Sheriff: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Sheriff is a crewmate who can kill other players. If the other player is good, they will self-kill instead.");
                    break;
                    case RoleEnum.Swapper: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Swapper is a crewmate who can swap the votes of 2 players during meetings.");
                    break;
                    case RoleEnum.Psychic: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Psychic is a crewmate who can reveal the alliance of other players, and check if they are good or evil via a vision.");
                    break;
                    case RoleEnum.Spy: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Spy is a crewmate who can see the colours of players on the admin table and can see who the Impostors are once they complete all their tasks.");
                    break;
                    case RoleEnum.Vigilante: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Vigilante is a crewmate who can guess other people's roles during meetings. If they guess correctly they kill the other player, if not, they die instead.");
                    break;
                    case RoleEnum.Hunter: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Hunter is a crewmate who can stalk other players. If the stalked player uses an ability, the Hunter will then be permitted to kill them. The Hunter has no punishment for killing incorrectly.");
                    break;
                    case RoleEnum.Arsonist: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Arsonist is a neutral killer with the goal to kill everyone. To do so they must douse players and once enough people are doused they can ignite, killing all doused players immediately.");
                    break;
                    case RoleEnum.Executioner: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Executioner is a neutral evil role with the goal to vote out a specific player.");
                    break;
                    case RoleEnum.Glitch: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Glitch is a neutral killer with the goal to kill everyone. In addition to killing, they can also hack players, disabling abilities and mimic players, changing their appearance to look like others.");
                    break;
                    case RoleEnum.Jester: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Jester is a neutral evil role with the goal to be voted out.");
                    break;
                    case RoleEnum.Phantom: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Phantom is a neutral evil role with the goal to complete all their tasks without being clicked.");
                    break;
                    case RoleEnum.Grenadier: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Grenadier is an impostor who can use smoke grenades to blind other players.");
                    break;
                    case RoleEnum.Janitor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Janitor is an impostor who can remove bodies from the map.");
                    break;
                    case RoleEnum.Miner: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Miner is an impostor who can place new vents to create a new vent network.");
                    break;
                    case RoleEnum.Morphling: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Morphling is an impostor who can morph into other players.");
                    break;
                    case RoleEnum.Swooper: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Swooper is an impostor who can turn invisible.");
                    break;
                    case RoleEnum.Undertaker: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Undertaker is an impostor who can drag bodies to different locations.");
                    break;
                    case RoleEnum.Haunter: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Haunter is a crewmate who can reveal all Impostors on completion of their tasks.");
                    break;
                    case RoleEnum.Veteran: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Veteran is a crewmate who can alert to kill anyone who interacts with them.");
                    break;
                    case RoleEnum.Amnesiac: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Amnesiac is a neutral benign role that needs to find a body in order to remember a new role.");
                    break;
                    case RoleEnum.Juggernaut: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Juggernaut is a neutral apocalypse role with the goal to kill everyone. Every kill they make reduces their kill cooldown.");
                    break;
                    case RoleEnum.Tracker: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Tracker is a crewmate who can track multiple other players, as well as see their footprints.");
                    break;
                    case RoleEnum.Transporter: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Transporter is a crewmate who can swap the locations of 2 other players.");
                    break;
                    case RoleEnum.Traitor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Traitor is an impostor who was originally a Crewmate but switched sides.");
                    break;
                    case RoleEnum.Medium: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Medium is a crewmate who can see dead players the round that they die & gets an alert when a player dies.");
                    break;
                    case RoleEnum.Trapper: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Trapper is a crewmate who can place traps around the map. All players who stand in these traps will reveal their role to the Trapper as long as enough players trigger the trap.");
                    break;
                    case RoleEnum.Survivor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Survivor is a neutral benign role that needs to live to win.");
                    break;
                    case RoleEnum.GuardianAngel: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Guardian Angel is a neutral benign role that needs their target to win to win themselves.");
                    break;
                    case RoleEnum.Blackmailer: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Blackmailer is an impostor who can silence other players.");
                    break;
                    case RoleEnum.Plaguebearer: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Plaguebearer is a neutral apocalypse role with the goal to kill everyone. To do this they must infect everyone to turn into Pestilence.");
                    break;
                    case RoleEnum.Pestilence: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Pestilence is a neutral apocalypse role with the goal to kill everyone. In addition to being able to kill, they are invincible and anyone who interacts with them will die.");
                    break;
                    case RoleEnum.Werewolf: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Werewolf is a neutral killer with the goal to kill everyone. In order to kill, they must rampage which gives them a short kill cooldown to kill people in bursts.");
                    break;
                    case RoleEnum.Investigator: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Investigator is a crewmate that can inspect crime scenes. Any player who has walked over this crime scene is suspicious. They can then examine players to see who has been at the crime scene.");
                    break;
                    case RoleEnum.Escapist: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Escapist is an impostor who can mark a location and recall to it later.");
                    break;
                    case RoleEnum.Imitator: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Imitator is a crewmate who can select dead crew roles to use during meetings. The following round they become this new role.");
                    break;
                    case RoleEnum.Bomber: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Bomber is an impostor who can place bombs, these kill anyone in the area a short duration later.");
                    break;
                    case RoleEnum.Doomsayer: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Doomsayer is a neutral evil role with the goal to guess 3 other player's roles simultaneously.");
                    break;
                    case RoleEnum.Cannibal: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Cannibal is a neutral chaos role with the goal to eat multiple corpses in order to win by themselves. However, some bodies may be staked by a Hunter, which will not count.");
                    break;
                    case RoleEnum.Vampire: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Vampire is a neutral neophyte role with the goal to kill everyone. The first crewmate the original Vampire bites will turn into a Vampire, the rest will die.");
                    break;
                    case RoleEnum.Prosecutor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Prosecutor is a crewmate who can exile a player of their choosing in a meeting.");
                    break;
                    case RoleEnum.Warlock: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Warlock is an impostor who can charge their kill button to kill multiple people at once.");
                    break;
                    case RoleEnum.Oracle: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Oracle is a crewmate who can bless other players. Blessed players cannot be interacted with, and cannot be voted out.");
                    break;
                    case RoleEnum.Venerer: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Venerer is an impostor who improves their ability with each kill. First kill is camouflage, second is speed and third is global slow.");
                    break;
                    case RoleEnum.Aurial: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Aurial is a crewmate who can sense ability uses nearby.");
                    break;
                    case RoleEnum.Politician: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Politician is a crewmate who can campaign in order to become the Mayor. During meetings they can attempt to reveal as the Mayor, if they have campaigned over half the crewmates they will be successful, if not they are unable to campaign the following round.");
                    break;
                    case RoleEnum.Hypnotist: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Hypnotist is an impostor who can hypnotise other players. During meetings they can then release mass hysteria which makes all hypnotised players see everyone else as either morphed as themself, camouflaged or invisible.");
                    break;
                    case RoleEnum.Jailor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Jailor is a crewmate who can jail other players. Jailed players cannot have meeting abilities used on them and cannot use meeting abilities themself. The Jailor and jailee may also privately talk to each other and the Jailor may also execute their jailee. If they execute a crewmate they lose the ability to jail players.");
                    break;
                    case RoleEnum.SoulCollector: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Soul Collector is a neutral apocalypse role with the goal to collect souls. In order to obtain them they must reap players, once those players die they can pick their soul up off the ground.");
                    break;
                    case RoleEnum.Lookout: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(
                        PlayerControl.LocalPlayer, "The Lookout is a crewmate who can watch other players. They will see all players who interact with each player they watch, as well as expand their eyesight with their Eagle Eye ability.");
                    break;
                    case RoleEnum.SerialKiller: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Serial Killer is an neutral killer who must hunt down prey. Once their kill cooldown is up they are given a target to kill and begin their hunt. If they kill that target they get a reduced kill cooldown and regenerate their bloodlust duration. If they don't kill their target they are given an increased kill cooldown.");
                    break;
                    case RoleEnum.Deputy: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Deputy is a crewmate who can camp other players. If the player is killed they will receive an alert notifying them of their death. During the following meeting they may then shoot anyone. If they shoot the killer, they die unless fortified or invincible, if they are wrong nothing happens.");
                    break;
                    case RoleEnum.Bodyguard: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Bodyguard is a crewmate who may select a new target to protect every round. They may only protect the player a few times, and during this brief period of time, they will take the hit from their attacker and attack them back as well.");
                    break;
                    case RoleEnum.MirrorMaster: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Mirror Master is a crewmate who places magic mirrors on other players in order to absorb attacks. By absorbing these attacks, they may unleash the attack on another player the following round.");
                    break;
                    case RoleEnum.Lawyer: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Lawyer is a neutral benign role with the goal of keeping their target alive till the end of the game. They win with them, similarly to the guardian angel.");
                    break;
                    case RoleEnum.Inquisitor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Inquisitor is a neutral chaos role with the task of vanquishing three heretics. They are only shown their roles, however, and they may inquire players to see if they are heretics. If they win, they stay in the game with the choice of causing any chaos they wish");
                    break;
                    case RoleEnum.Tyrant: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Tyrant is a neutral chaos role with the intention of helping neutrals win. They have a vote bank which they can unleash at any time, and they win with Neutral Evils and all other Neutral Killers.");
                    break;
                    case RoleEnum.Poisoner: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                        "The Poisoner is an Impostor role who can poison a player, killing them after a few seconds.");
                    break;
                    default: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                    "Sorry, the entry for this role is not complete yet!");
                break;
                }
            }

            public static void AddAllianceMessage(AllianceEnum alliance)
            {
                switch(alliance)
                {
                case AllianceEnum.Lover: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                    "The Lover is a global alliance that life links 2 players. They also gain an extra win condition of surviving until final 3 together.");
                break;
                    case AllianceEnum.Recruit: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                    "The Recruit is a global alliance that life links 2 players, which are lead by an unknown Jackal. They must eliminate all other players to win.");
                break;
                    case AllianceEnum.Crewpocalypse: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                    "The Crewpocalypse is a crewmate alliance that makes the player a part of the Apocalypse faction.");
                break;
                    case AllianceEnum.Crewpostor: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                    "The Crewpostor is a crewmate alliance that makes the player a part of the Impostor faction.");
                break;
                    case AllianceEnum.Egotist: DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer,
                    "The Egotist is a global alliance that makes the player win with any killer, as long as they survive.");
                break;
                }
            }

            public static void AddModifierMessage(ModifierEnum modifier)
            {
                switch (modifier)
                {
                    case ModifierEnum.Giant:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Giant is a global modifier that increases the size of a player.");
                        break;
                    case ModifierEnum.Mini:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Mini is a global modifier that decreases the size of a player.");
                        break;
                    case ModifierEnum.Drunk:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Drunk is a global modifier that inverts the controls of a player.");
                        break;
                    case ModifierEnum.Oblivious:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Oblivious is a global modifier that disables or hides the report button of a player.");
                        break;
                    case ModifierEnum.ButtonBarry:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Button Barry is a global modifier that allows the player to button from anywhere on the map.");
                        break;
                    case ModifierEnum.Aftermath:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Aftermath is a crewmate modifier that forces their killer to instantly use their ability.");
                        break;
                    case ModifierEnum.Bait:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Bait is a crewmate modifier that forces their killer to report their body.");
                        break;
                    case ModifierEnum.Diseased:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Diseased is a crewmate modifier that increases their killer's kill cooldown.");
                        break;
                    case ModifierEnum.Flash:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Flash is a global modifier that increases the speed of a player.");
                        break;
                    case ModifierEnum.Tiebreaker:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Tiebreaker is a global modifier that allows a player's vote to break ties in meetings.");
                        break;
                    case ModifierEnum.Torch:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Torch is a crewmate modifier that allows them to see when lights are off.");
                        break;
                    case ModifierEnum.Sleuth:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Sleuth is a global modifier that allows a player to see roles of dead bodies that they report.");
                        break;
                    case ModifierEnum.Radar:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Radar is a global modifier that shows an arrow pointing to the closest player.");
                        break;
                    case ModifierEnum.Disperser:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Disperser is an impostor modifier that gives an extra ability to Impostors. This being once per game sending every player to a random vent on the map.");
                        break;
                    case ModifierEnum.Multitasker:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Multitasker is a crewmate modifier that makes their tasks slightly transparent.");
                        break;
                    case ModifierEnum.DoubleShot:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Double Shot is an impostor modifier that gives Assassins an extra life when assassinating.");
                        break;
                    case ModifierEnum.Underdog:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Underdog is an impostor modifier that grants Impostors a reduced kill cooldown when alone.");
                        break;
                    case ModifierEnum.Frosty:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Frosty is a crewmate modifier that reduces the speed of their killer temporarily.");
                        break;
                    case ModifierEnum.SixthSense:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Sixth Sense is a global modifier that alerts players to when someone interacts with them.");
                        break;
                    case ModifierEnum.Shy:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Shy is a global modifier that makes the player slightly transparent when they stand still.");
                        break;
                    case ModifierEnum.Saboteur:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "The Saboteur is an impostor modifier that passively reduces non-door sabotage cooldowns.");
                        break;
                    default:
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "Invalid modifier entry.");
                        break;
                }
            }
        }

        [HarmonyPatch(typeof(ChatBubble), nameof(ChatBubble.SetName))]
        public static class SetName
        {
            public static void Postfix(ChatBubble __instance, [HarmonyArgument(0)] string playerName)
            {
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Jailor) && MeetingHud.Instance)
                {
                    var jailor = Role.GetRole<Jailor>(PlayerControl.LocalPlayer);
                    if (jailor.Jailed != null && jailor.Jailed.Data.PlayerName == playerName)
                    {
                        __instance.NameText.color = jailor.Color;
                        __instance.NameText.text = playerName + " (Jailed)";
                    }
                    else if (JailorMessage)
                    {
                        __instance.NameText.color = jailor.Color;
                        __instance.NameText.text = "Jailor";
                        JailorMessage = false;
                    }
                }
                if (PlayerControl.LocalPlayer.IsJailed() && MeetingHud.Instance)
                {
                    if (JailorMessage)
                    {
                        __instance.NameText.color = Colors.Jailor;
                        __instance.NameText.text = "Jailor";
                        JailorMessage = false;
                    }
                }
            }
        }
    }
}