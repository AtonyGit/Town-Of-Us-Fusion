using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using System;
using System.Collections;
using System.Linq;
using HarmonyLib;
using Hazel;
using Il2CppSystem.Collections.Generic;
using Reactor;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.CrewmateRoles.InvestigatorMod;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.CrewmateRoles.SnitchMod;
using TownOfUsFusion.Extensions;
using AmongUs.GameOptions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;
using Reactor.Utilities;

namespace TownOfUsFusion.NeutralRoles.CursedSoulMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
public class MeetingSwap
{
    public static void Postfix(MeetingHud __instance)
    {
        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.CursedSoul)) return;
        var CursedSoulRole = Role.GetRole<CursedSoul>(PlayerControl.LocalPlayer);


        var playerList = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(RoleEnum.Haunter) && !x.Is(RoleEnum.Phantom) && !x.Is(RoleEnum.CursedSoul)).ToList();

        var soulGuarantee = UnityEngine.Random.RandomRangeInt(0, 100);

        if (CustomGameOptions.SoulSwapGuarantee > soulGuarantee || CursedSoulRole.CursedPlayer == null)
        {
            var num = UnityEngine.Random.RandomRangeInt(0, playerList.Count);
            CursedSoulRole.CursedPlayer = playerList[num];
        }
        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SOUL SWAPPED WITH: {CursedSoulRole.CursedPlayer.Data.PlayerName}");
            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.SoulSwap, SendOption.Reliable, -1);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.Write(CursedSoulRole.CursedPlayer);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            SoulSwap(CursedSoulRole, CursedSoulRole.CursedPlayer);
    }

        public static void SoulSwap(CursedSoul CursedSoulRole, PlayerControl other)
        {
            var role = Utils.GetRole(other);
            CursedSoulRole.LastSoulSwapped = DateTime.UtcNow;
            var CursedSoul = CursedSoulRole.Player;
            List<PlayerTask> tasks1, tasks2;
            List<GameData.TaskInfo> taskinfos1, taskinfos2;

            var swapTasks = true;
            var resetCursedSoul = false;

            Role newRole;

            switch (role)
            {
                case RoleEnum.Altruist:
                case RoleEnum.Aurial:
                case RoleEnum.Bartender:
                case RoleEnum.Bodyguard:
                case RoleEnum.Captain:
                case RoleEnum.Crusader:
                case RoleEnum.Detective:
                case RoleEnum.Engineer:
                case RoleEnum.Hunter:
                case RoleEnum.Imitator:
                case RoleEnum.Investigator:
                case RoleEnum.Mage:
                case RoleEnum.Mayor:
                case RoleEnum.Medic:
                case RoleEnum.Medium:
                case RoleEnum.Monarch:
                case RoleEnum.Mystic:
                case RoleEnum.Oracle:
                case RoleEnum.Prosecutor:
                case RoleEnum.Seer:
                case RoleEnum.Sheriff:
                case RoleEnum.Snitch:
                case RoleEnum.Spy:
                case RoleEnum.Swapper:
                case RoleEnum.Taskmaster:
                case RoleEnum.Tracker:
                case RoleEnum.Trapper:
                case RoleEnum.Transporter:
                case RoleEnum.Trickster:
                case RoleEnum.VampireHunter:
                case RoleEnum.Veteran:
                case RoleEnum.Vigilante:
                case RoleEnum.Crewmate:

                    var oldRole2 = Role.GetRole(other);
                    var killsList2 = (oldRole2.CorrectKills, oldRole2.IncorrectKills, oldRole2.CorrectAssassinKills, oldRole2.IncorrectAssassinKills);
                    
                    newRole = Role.GetRole(other);
                    newRole.Player = CursedSoul;
                    var roleName2 = newRole.Name;
                    var roleColor2 = newRole.Color;
                    var roleFaction2 = newRole.Faction;
                    newRole.Name = Utils.GradientColorText("79FFB3", "B579FF", newRole.Name);
                    newRole.Color = Patches.Colors.CursedSoul;
                    newRole.Faction = Faction.NeutralCursed;

                    Role.RoleDictionary.Remove(CursedSoul.PlayerId);
                    Role.RoleDictionary.Remove(other.PlayerId);

                    Role.RoleDictionary.Add(CursedSoul.PlayerId, newRole);

                        resetCursedSoul = true;
                        CursedSoulRole.Player = other;

                        Role.RoleDictionary.Remove(other.PlayerId);

                        if (PlayerControl.LocalPlayer == other)
                        {
                            var curse = new CursedSoul(PlayerControl.LocalPlayer);
                            curse.CorrectKills = killsList2.CorrectKills;
                            curse.IncorrectKills = killsList2.IncorrectKills;
                            curse.CorrectAssassinKills = killsList2.CorrectAssassinKills;
                            curse.IncorrectAssassinKills = killsList2.IncorrectAssassinKills;
                            curse.Name = "Cursed Soul";
                            curse.Color = roleColor2;
                            curse.Faction = roleFaction2;
                            curse.RegenTask();
                        }
                        else
                        {
                            if (PlayerControl.LocalPlayer == CursedSoul) newRole.RegenTask();
                            var curse = new CursedSoul(other);
                            curse.CorrectKills = killsList2.CorrectKills;
                            curse.IncorrectKills = killsList2.IncorrectKills;
                            curse.CorrectAssassinKills = killsList2.CorrectAssassinKills;
                            curse.IncorrectAssassinKills = killsList2.IncorrectAssassinKills;
                            curse.Name = "Cursed Soul";
                            curse.Color = roleColor2;
                            curse.Faction = roleFaction2;
                        }
                        swapTasks = true;


                    break;

                case RoleEnum.Amnesiac:
                case RoleEnum.Arsonist:
                case RoleEnum.Baker:
                case RoleEnum.Berserker:
                case RoleEnum.Cannibal:
                case RoleEnum.CursedSoul:
                case RoleEnum.Doomsayer:
                case RoleEnum.Executioner:
                case RoleEnum.Ghoul:
                case RoleEnum.GuardianAngel:
                case RoleEnum.Inquisitor:
                case RoleEnum.Jester:
                case RoleEnum.Joker:
                case RoleEnum.Pirate:
                case RoleEnum.Plaguebearer:
                //case RoleEnum.Puppet:
                case RoleEnum.SerialKiller:
                case RoleEnum.SoulCollector:
                case RoleEnum.Survivor:
                case RoleEnum.Glitch:
                case RoleEnum.Sentinel:
                case RoleEnum.Tyrant:
                case RoleEnum.Werewolf:

                case RoleEnum.Blackmailer:
                case RoleEnum.Bomber:
                case RoleEnum.Escapist:
                case RoleEnum.Grenadier:
                case RoleEnum.Janitor:
                case RoleEnum.Miner:
                case RoleEnum.Morphling:
                case RoleEnum.Poisoner:
                case RoleEnum.Swooper:
                case RoleEnum.Traitor:
                case RoleEnum.Undertaker:
                case RoleEnum.Venerer:
                case RoleEnum.Warlock:
                case RoleEnum.Impostor:

                    var oldRole = Role.GetRole(other);
                    var killsList = (oldRole.CorrectKills, oldRole.IncorrectKills, oldRole.CorrectAssassinKills, oldRole.IncorrectAssassinKills);

                    newRole = Role.GetRole(other);
                    newRole.Player = CursedSoul;
                    var roleName = newRole.Name;
                    var roleColor = newRole.Color;
                    var roleFaction = newRole.Faction;
                    newRole.Name = Utils.GradientColorText("79FFB3", "B579FF", newRole.Name);
                    newRole.Color = Patches.Colors.CursedSoul;
                    newRole.Faction = Faction.NeutralCursed;

                    Role.RoleDictionary.Remove(CursedSoul.PlayerId);
                    Role.RoleDictionary.Remove(other.PlayerId);

                    Role.RoleDictionary.Add(CursedSoul.PlayerId, newRole);

                        resetCursedSoul = true;
                        CursedSoulRole.Player = other;

                        Role.RoleDictionary.Remove(other.PlayerId);

                        if (PlayerControl.LocalPlayer == other)
                        {
                            var curse = new CursedSoul(PlayerControl.LocalPlayer);
                            curse.CorrectKills = killsList.CorrectKills;
                            curse.IncorrectKills = killsList.IncorrectKills;
                            curse.CorrectAssassinKills = killsList.CorrectAssassinKills;
                            curse.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
                            curse.Name = "Cursed Soul";
                            curse.Color = roleColor;
                            curse.Faction = roleFaction;
                            curse.RegenTask();
                        }
                        else
                        {
                            if (PlayerControl.LocalPlayer == CursedSoul) newRole.RegenTask();
                            var curse = new CursedSoul(other);
                            curse.CorrectKills = killsList.CorrectKills;
                            curse.IncorrectKills = killsList.IncorrectKills;
                            curse.CorrectAssassinKills = killsList.CorrectAssassinKills;
                            curse.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
                            curse.Name = "Cursed Soul";
                            curse.Color = roleColor;
                            curse.Faction = roleFaction;
                        }

                    swapTasks = false;
                    break;
                case RoleEnum.Vampire:
                case RoleEnum.Jackal:
                case RoleEnum.NeoNecromancer:
                case RoleEnum.Husk:
                case RoleEnum.Apparitionist:
                case RoleEnum.Scourge:
                case RoleEnum.Pestilence:
                case RoleEnum.War:
                case RoleEnum.Famine:
                case RoleEnum.Death:
                    Utils.MurderPlayer(CursedSoul, CursedSoul, false);
                    swapTasks = false;
                    break;
            }

            if (swapTasks)
            {
                tasks1 = other.myTasks;
                taskinfos1 = other.Data.Tasks;
                tasks2 = CursedSoul.myTasks;
                taskinfos2 = CursedSoul.Data.Tasks;

                CursedSoul.myTasks = tasks1;
                CursedSoul.Data.Tasks = taskinfos1;
                other.myTasks = tasks2;
                other.Data.Tasks = taskinfos2;

                if (resetCursedSoul) CursedSoulRole.RegenTask();
            }

            //System.Console.WriteLine(CursedSoul.Is(RoleEnum.Sheriff));
            //System.Console.WriteLine(other.Is(RoleEnum.Sheriff));
            //System.Console.WriteLine(Roles.Role.GetRole(CursedSoul));
            /*if (CursedSoul.AmOwner || other.AmOwner)
            {
                if (CursedSoul.Is(RoleEnum.Arsonist) && other.AmOwner)
                    Role.GetRole<Arsonist>(CursedSoul).IgniteButton.Destroy();
                DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
                DestroyableSingleton<HudManager>.Instance.KillButton.isActive = false;

            }*/
        }
/*
    public static string PlayerReportFeedback(PlayerControl player)
    {
        if (player.Is(RoleEnum.Aurial) || player.Is(RoleEnum.Imitator) || StartImitate.ImitatingPlayer == player
            || player.Is(RoleEnum.Morphling) || player.Is(RoleEnum.Mystic) || player.Is(RoleEnum.Husk)
             || player.Is(RoleEnum.Spy) || player.Is(RoleEnum.Glitch))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} has an altered perception of reality";

        else if (player.Is(RoleEnum.Blackmailer) || player.Is(RoleEnum.Detective) || player.Is(RoleEnum.CursedSoul)
             || player.Is(RoleEnum.Oracle) || player.Is(RoleEnum.Snitch) || player.Is(RoleEnum.Trapper))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} has an insight for private information";

        else if (player.Is(RoleEnum.Altruist) || player.Is(RoleEnum.Amnesiac) || player.Is(RoleEnum.Janitor) || player.Is(RoleEnum.NeoNecromancer)
             || player.Is(RoleEnum.Medium) || player.Is(RoleEnum.Undertaker) || player.Is(RoleEnum.Vampire) || player.Is(RoleEnum.Apparitionist))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} has an unusual obsession with dead bodies";

        else if (player.Is(RoleEnum.Investigator) || player.Is(RoleEnum.Swooper) || player.Is(RoleEnum.Tracker) || player.Is(RoleEnum.Scourge)
            || player.Is(RoleEnum.VampireHunter) || player.Is(RoleEnum.Venerer) || player.Is(RoleEnum.Werewolf) || player.Is(RoleEnum.Cannibal))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} is well trained in hunting down prey";

        else if (player.Is(RoleEnum.Arsonist) || player.Is(RoleEnum.Miner) || player.Is(RoleEnum.Plaguebearer)
              || player.Is(RoleEnum.Prosecutor) || player.Is(RoleEnum.Seer) || player.Is(RoleEnum.Transporter))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} spreads fear amonst the group";

        else if (player.Is(RoleEnum.Engineer) || player.Is(RoleEnum.Escapist) || player.Is(RoleEnum.Grenadier)
            || player.Is(RoleEnum.GuardianAngel) || player.Is(RoleEnum.Medic) || player.Is(RoleEnum.Survivor))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} hides to guard themself or others";

        else if (player.Is(RoleEnum.Executioner) || player.Is(RoleEnum.Jester) || player.Is(RoleEnum.Mayor) || player.Is(RoleEnum.Tyrant)
             || player.Is(RoleEnum.Swapper) || player.Is(RoleEnum.Traitor) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Joker))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} has a trick up their sleeve";

        else if (player.Is(RoleEnum.Bomber) || player.Is(RoleEnum.Berserker) || player.Is(RoleEnum.Pestilence) || player.Is(RoleEnum.Poisoner)
             || player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Vigilante) || player.Is(RoleEnum.Warlock))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} is capable of performing relentless attacks";

        else if (player.Is(RoleEnum.Crewmate) || player.Is(RoleEnum.Impostor))
            return $"You observe that {player.GetDefaultOutfit().PlayerName} appears to be roleless";
        else
            return "Error";
    }

    public static string RoleReportFeedback(PlayerControl player)
    {
        if (player.Is(RoleEnum.Aurial) || player.Is(RoleEnum.Imitator) || StartImitate.ImitatingPlayer == player
            || player.Is(RoleEnum.Morphling) || player.Is(RoleEnum.Mystic) || player.Is(RoleEnum.Husk)
             || player.Is(RoleEnum.Spy) || player.Is(RoleEnum.Glitch))
            return "(Aurial, Husk, Imitator, Morphling, Mystic, Spy or The Glitch)";

        else if (player.Is(RoleEnum.Blackmailer) || player.Is(RoleEnum.Detective) || player.Is(RoleEnum.CursedSoul)
             || player.Is(RoleEnum.Oracle) || player.Is(RoleEnum.Snitch) || player.Is(RoleEnum.Trapper))
            return "(Blackmailer, Detective, CursedSoul, Oracle, Snitch or Trapper)";

        else if (player.Is(RoleEnum.Altruist) || player.Is(RoleEnum.Amnesiac) || player.Is(RoleEnum.Janitor) || player.Is(RoleEnum.NeoNecromancer)
             || player.Is(RoleEnum.Medium) || player.Is(RoleEnum.Undertaker) || player.Is(RoleEnum.Vampire) || player.Is(RoleEnum.Apparitionist))
            return "(Altruist, Amnesiac, Janitor, Medium, Necromancer, Apparitionist, Undertaker or Vampire)";

        else if (player.Is(RoleEnum.Investigator) || player.Is(RoleEnum.Swooper) || player.Is(RoleEnum.Tracker) || player.Is(RoleEnum.Scourge)
            || player.Is(RoleEnum.VampireHunter) || player.Is(RoleEnum.Venerer) || player.Is(RoleEnum.Werewolf) || player.Is(RoleEnum.Cannibal))
            return "(Cannibal, Investigator, Scourge, Swooper, Tracker, Vampire Hunter, Venerer or Werewolf)";

        else if (player.Is(RoleEnum.Arsonist) || player.Is(RoleEnum.Miner) || player.Is(RoleEnum.Plaguebearer)
              || player.Is(RoleEnum.Prosecutor) || player.Is(RoleEnum.Seer) || player.Is(RoleEnum.Transporter))
            return "(Arsonist, Miner, Plaguebearer, Prosecutor, Seer or Transporter)";
            
        else if (player.Is(RoleEnum.Engineer) || player.Is(RoleEnum.Escapist) || player.Is(RoleEnum.Grenadier)
            || player.Is(RoleEnum.GuardianAngel) || player.Is(RoleEnum.Medic) || player.Is(RoleEnum.Survivor))
            return "(Engineer, Escapist, Grenadier, Guardian Angel, Medic or Survivor)";

        else if (player.Is(RoleEnum.Executioner) || player.Is(RoleEnum.Jester) || player.Is(RoleEnum.Mayor) || player.Is(RoleEnum.Tyrant)
             || player.Is(RoleEnum.Swapper) || player.Is(RoleEnum.Traitor) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Joker))
            return "(Executioner, Jester, Joker, Mayor, Tyrant, Swapper, Traitor or Veteran)";
            
        else if (player.Is(RoleEnum.Bomber) || player.Is(RoleEnum.Berserker) || player.Is(RoleEnum.Pestilence) || player.Is(RoleEnum.Poisoner)
             || player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Vigilante) || player.Is(RoleEnum.Warlock))
            return "(Bomber, Berserker, Pestilence, Poisoner, Sheriff, Vigilante or Warlock)";

        else if (player.Is(RoleEnum.Crewmate) || player.Is(RoleEnum.Impostor))
            return "(Crewmate or Impostor)";

        else return "Error";
    }*/
}
}