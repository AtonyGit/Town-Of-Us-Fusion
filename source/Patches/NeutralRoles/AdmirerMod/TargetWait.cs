using System;
using AmongUs.GameOptions;
using HarmonyLib;
using TownOfUsFusion.CrewmateRoles.HaunterMod;
using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using TownOfUsFusion.CrewmateRoles.TrackerMod;
using TownOfUsFusion.CrewmateRoles.TrapperMod;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.ImpostorRoles.BomberMod;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.AdmirerMod
{

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class AdmirerTargetWait
    {
        private static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Admirer)) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;

            var role = Role.GetRole<Admirer>(PlayerControl.LocalPlayer);

            if (!role.AdmiredPlayer.Data.IsDead && !role.AdmiredPlayer.Data.Disconnected) return;

            if (AmongUsClient.Instance.AmHost)
            {
                Utils.Rpc(CustomRPC.AdmirerSetRole, PlayerControl.LocalPlayer.PlayerId, role.AdmiredPlayer.PlayerId, (byte)1);
                AdmirerSetRole(role, role.AdmiredPlayer);
            }
            else Utils.Rpc(CustomRPC.AdmirerSetRole, PlayerControl.LocalPlayer.PlayerId, role.AdmiredPlayer.PlayerId, (byte)0);

            //return false;
        }

    public static Sprite Sprite => TownOfUsFusion.Arrow;
        public static void AdmirerSetRole(Admirer amneRole, PlayerControl other)
        {
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Lookout))
            {
                var lookout = Role.GetRole<Lookout>(PlayerControl.LocalPlayer);
                if (lookout.Watching.ContainsKey(other.PlayerId))
                {
                    if (!lookout.Watching[other.PlayerId].Contains(RoleEnum.Admirer)) lookout.Watching[other.PlayerId].Add(RoleEnum.Admirer);
                }
            }

            var role = Utils.GetRole(other);
            var amnesiac = amneRole.Player;

            var rememberImp = true;
            var rememberNeut = true;

            Role newRole;

            switch (role)
            {
                case RoleEnum.Crewmate:

                case RoleEnum.Aurial:
                case RoleEnum.Investigator:
                case RoleEnum.Lookout:
                case RoleEnum.Medium:
                case RoleEnum.Operative:
                case RoleEnum.Psychic:
                case RoleEnum.Tracker:
                case RoleEnum.Trapper:

                case RoleEnum.Deputy:
                case RoleEnum.Hunter:
                case RoleEnum.Sheriff:
                case RoleEnum.Veteran:
                case RoleEnum.Vigilante:

                case RoleEnum.Altruist:
                case RoleEnum.Bodyguard:
                case RoleEnum.Medic:
                case RoleEnum.MirrorMaster:
                case RoleEnum.Oracle:

                case RoleEnum.Captain:
                case RoleEnum.Jailor:
                case RoleEnum.Politician:
                case RoleEnum.Prosecutor:
                case RoleEnum.Mayor:
                case RoleEnum.Swapper:

                case RoleEnum.Engineer:
                case RoleEnum.Imitator:
                case RoleEnum.TimeLord:
                case RoleEnum.Transporter:

                    rememberImp = false;
                    rememberNeut = false;

                    break;

                case RoleEnum.Admirer:
                case RoleEnum.Amnesiac:
                case RoleEnum.GuardianAngel:
                case RoleEnum.Lawyer:
                case RoleEnum.Survivor:

                case RoleEnum.Jester:
                case RoleEnum.Executioner:
                case RoleEnum.Doomsayer:

                case RoleEnum.Cannibal:
                case RoleEnum.CursedSoul:
                case RoleEnum.Tyrant:

                case RoleEnum.Arsonist:
                case RoleEnum.Glitch:
                case RoleEnum.SerialKiller:
                case RoleEnum.Werewolf:
                
                case RoleEnum.Vampire:
                case RoleEnum.Jackal:

                case RoleEnum.Juggernaut:
                case RoleEnum.Armageddon:
                case RoleEnum.Plaguebearer:
                case RoleEnum.Pestilence:
                case RoleEnum.SoulCollector:
                case RoleEnum.Death:

                    rememberImp = false;

                    break;
            }

            newRole = Role.GetRole(other);
            newRole.Player = amnesiac;

            if ((role == RoleEnum.Glitch || role == RoleEnum.Juggernaut || role == RoleEnum.Pestilence ||
                role == RoleEnum.SerialKiller || role == RoleEnum.Armageddon ||
                role == RoleEnum.Werewolf) && PlayerControl.LocalPlayer == other)
            {
                HudManager.Instance.KillButton.buttonLabelText.gameObject.SetActive(false);
            }

            if (role == RoleEnum.Tracker) Footprint.DestroyAll(Role.GetRole<Tracker>(other));

            if (role == RoleEnum.Operative) CompleteTask.Postfix(amnesiac);

            if (role == RoleEnum.Investigator && PlayerControl.LocalPlayer == other)
            {
                var detecRole = Role.GetRole<Investigator>(other);
                foreach (GameObject scene in detecRole.CrimeScenes)
                {
                    UnityEngine.Object.Destroy(scene);
                }
            }

            if (role == RoleEnum.SoulCollector && PlayerControl.LocalPlayer == other)
            {
                var scRole = Role.GetRole<SoulCollector>(other);
                foreach (GameObject soul in scRole.Souls)
                {
                    UnityEngine.Object.Destroy(soul);
                }
            }

            if (role == RoleEnum.Bomber && PlayerControl.LocalPlayer.Data.IsImpostor())
            {
                if (BombTeammate.TempBomb != null)
                {
                    try { BombExtentions.ClearBomb(BombTeammate.TempBomb); }
                    catch { }
                }
            }

            Role.RoleDictionary.Remove(amnesiac.PlayerId);
            Role.RoleDictionary.Remove(other.PlayerId);
            Role.RoleDictionary.Add(amnesiac.PlayerId, newRole);

            if (!(amnesiac.Is(RoleEnum.Crewmate) || amnesiac.Is(RoleEnum.Impostor))) newRole.RegenTask();

            if (other == StartImitate.ImitatingPlayer)
            {
                StartImitate.ImitatingPlayer = amneRole.Player;
                newRole.AddToRoleHistory(RoleEnum.Imitator);
            }
            else newRole.AddToRoleHistory(newRole.RoleType);

            if (rememberImp == false)
            {
                if (rememberNeut == false)
                {
                    new Crewmate(other);
                }
                else
                {
                    // If role is not Vampire, turn dead player into Survivor
                    if (role != RoleEnum.Vampire)
                    {
                        var survivor = new Survivor(other);
                        survivor.RegenTask();
                    }
                    // If role is Vampire, keep dead player as Vampire
                    if (role == RoleEnum.Vampire)
                    {
                        var vampire = new Vampire(other);
                        vampire.RegenTask();
                    }

                    if (role == RoleEnum.Arsonist || role == RoleEnum.Glitch || role == RoleEnum.Plaguebearer ||
                            role == RoleEnum.Pestilence || role == RoleEnum.Werewolf || role == RoleEnum.Juggernaut
                             || role == RoleEnum.Vampire || role == RoleEnum.SerialKiller || role == RoleEnum.Armageddon)
                    {
                        if (CustomGameOptions.AmneTurnNeutAssassin) new Assassin(amnesiac);
                        if (other.Is(AbilityEnum.Assassin)) Ability.AbilityDictionary.Remove(other.PlayerId);
                    }
                }
            }
            else if (rememberImp == true)
            {
                new Impostor(other);
                amnesiac.Data.Role.TeamType = RoleTeamTypes.Impostor;
                RoleManager.Instance.SetRole(amnesiac, RoleTypes.Impostor);
                amnesiac.SetKillTimer(GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (player.Data.IsImpostor() && PlayerControl.LocalPlayer.Data.IsImpostor())
                    {
                        player.nameText().color = Patches.Colors.Impostor;
                    }
                }
                if (CustomGameOptions.AmneTurnImpAssassin) new Assassin(amnesiac);
            }

            if (role == RoleEnum.Operative)
            {
                var operativeRole = Role.GetRole<Operative>(amnesiac);
                operativeRole.ImpArrows.DestroyAll();
                operativeRole.OperativeArrows.Values.DestroyAll();
                operativeRole.OperativeArrows.Clear();
                CompleteTask.Postfix(amnesiac);
                if (other.AmOwner)
                    foreach (var player in PlayerControl.AllPlayerControls)
                        player.nameText().color = Color.white;
                DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
            }

            else if (role == RoleEnum.Sheriff)
            {
                var sheriffRole = Role.GetRole<Sheriff>(amnesiac);
                sheriffRole.LastKilled = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Engineer)
            {
                var engiRole = Role.GetRole<Engineer>(amnesiac);
                engiRole.UsesLeft = CustomGameOptions.MaxFixes;
            }

            else if (role == RoleEnum.Medic)
            {
                var medicRole = Role.GetRole<Medic>(amnesiac);
                if (amnesiac != StartImitate.ImitatingPlayer) medicRole.UsedAbility = false;
                else medicRole.UsedAbility = true;
                medicRole.StartingCooldown = medicRole.StartingCooldown.AddSeconds(-10f);
            }

            else if (role == RoleEnum.Mayor)
            {
                var mayorRole = Role.GetRole<Mayor>(amnesiac);
                mayorRole.Revealed = false;
                DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
            }

            else if (role == RoleEnum.Politician)
            {
                var pnRole = Role.GetRole<Politician>(amnesiac);
                pnRole.CampaignedPlayers.RemoveRange(0, pnRole.CampaignedPlayers.Count);
                pnRole.LastCampaigned = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Prosecutor)
            {
                var prosRole = Role.GetRole<Prosecutor>(amnesiac);
                prosRole.Prosecuted = false;
                DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
            }

            else if (role == RoleEnum.Vigilante)
            {
                var vigiRole = Role.GetRole<Vigilante>(amnesiac);
                vigiRole.RemainingKills = CustomGameOptions.VigilanteKills;
                DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
            }

            else if (role == RoleEnum.Veteran)
            {
                var vetRole = Role.GetRole<Veteran>(amnesiac);
                vetRole.UsesLeft = CustomGameOptions.MaxAlerts;
                vetRole.LastAlerted = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Hunter)
            {
                var hunterRole = Role.GetRole<Hunter>(amnesiac);
                hunterRole.UsesLeft = CustomGameOptions.HunterStalkUses;
                hunterRole.LastStalked = DateTime.UtcNow;
                hunterRole.LastKilled = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Tracker)
            {
                var trackerRole = Role.GetRole<Tracker>(amnesiac);
                trackerRole.TrackerArrows.Values.DestroyAll();
                trackerRole.TrackerArrows.Clear();
                trackerRole.UsesLeft = CustomGameOptions.MaxTracks;
                trackerRole.LastTracked = DateTime.UtcNow;
                trackerRole.SeeOnlyTrackedPrints = CustomGameOptions.SeeOnlyTrackedPrints;
            }

            else if (role == RoleEnum.Lookout)
            {
                var loRole = Role.GetRole<Lookout>(amnesiac);
                loRole.UsesLeft = CustomGameOptions.MaxWatches;
                loRole.Watching.Clear();
                loRole.LastWatched = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Aurial)
            {
                var aurialRole = Role.GetRole<Aurial>(amnesiac);
                aurialRole.SenseArrows.Values.DestroyAll();
                aurialRole.SenseArrows.Clear();
                DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
            }

            else if (role == RoleEnum.Deputy)
            {
                var deputyRole = Role.GetRole<Deputy>(amnesiac);
                deputyRole.Camping = null;
                deputyRole.Killer = null;
                deputyRole.CampedThisRound = false;
                deputyRole.StartingCooldown = deputyRole.StartingCooldown.AddSeconds(-10f);
            }

            else if (role == RoleEnum.Investigator)
            {
                var investigatorRole = Role.GetRole<Investigator>(amnesiac);
                investigatorRole.LastExamined = DateTime.UtcNow;
                investigatorRole.CurrentTarget = null;
            }

            else if (role == RoleEnum.SoulCollector)
            {
                var scRole = Role.GetRole<SoulCollector>(amnesiac);
                scRole.LastReaped = DateTime.UtcNow;
                scRole.SoulsCollected = 1;
                scRole.CollectedSouls = false;
            }

            else if (role == RoleEnum.Transporter)
            {
                var tpRole = Role.GetRole<Transporter>(amnesiac);
                tpRole.TransportPlayer1 = null;
                tpRole.TransportPlayer2 = null;
                tpRole.LastTransported = DateTime.UtcNow;
                tpRole.UsesLeft = CustomGameOptions.TransportMaxUses;
            }

            else if (role == RoleEnum.Medium)
            {
                var medRole = Role.GetRole<Medium>(amnesiac);
                medRole.MediatedPlayers.Values.DestroyAll();
                medRole.MediatedPlayers.Clear();
                medRole.BodyArrows.Values.DestroyAll();
                medRole.BodyArrows.Clear();
                medRole.LastMediated = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Psychic)
            {
                var psychicRole = Role.GetRole<Psychic>(amnesiac);
                psychicRole.Investigated.RemoveRange(0, psychicRole.Investigated.Count);
                psychicRole.LastInvestigated = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Jailor)
            {
                var jailorRole = Role.GetRole<Jailor>(amnesiac);
                jailorRole.LastJailed = DateTime.UtcNow;
                jailorRole.Jailed = null;
                jailorRole.Executes = CustomGameOptions.MaxExecutes;
                jailorRole.CanJail = true;
            }

            else if (role == RoleEnum.Oracle)
            {
                var oracleRole = Role.GetRole<Oracle>(amnesiac);
                oracleRole.BlessedPlayer = null;
                oracleRole.LastBlessed = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Arsonist)
            {
                var arsoRole = Role.GetRole<Arsonist>(amnesiac);
                arsoRole.DousedPlayers.RemoveRange(0, arsoRole.DousedPlayers.Count);
                arsoRole.LastDoused = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Survivor)
            {
                var survRole = Role.GetRole<Survivor>(amnesiac);
                survRole.LastVested = DateTime.UtcNow;
                survRole.UsesLeft = CustomGameOptions.MaxVests;
            }

            else if (role == RoleEnum.GuardianAngel)
            {
                var gaRole = Role.GetRole<GuardianAngel>(amnesiac);
                gaRole.LastProtected = DateTime.UtcNow;
                gaRole.UsesLeft = CustomGameOptions.MaxProtects;
            }

            else if (role == RoleEnum.Glitch)
            {
                var glitchRole = Role.GetRole<Glitch>(amnesiac);
                glitchRole.LastKilled = DateTime.UtcNow;
                glitchRole.LastHacked = DateTime.UtcNow;
                glitchRole.LastMimiced = DateTime.UtcNow;
                glitchRole.Hacked = null;
            }

            else if (role == RoleEnum.Juggernaut)
            {
                var juggRole = Role.GetRole<Juggernaut>(amnesiac);
                juggRole.JuggKills = 0;
                juggRole.LastKilled = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Grenadier)
            {
                var grenadeRole = Role.GetRole<Grenadier>(amnesiac);
                grenadeRole.LastFlashed = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Morphling)
            {
                var morphlingRole = Role.GetRole<Morphling>(amnesiac);
                morphlingRole.LastMorphed = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Escapist)
            {
                var escapistRole = Role.GetRole<Escapist>(amnesiac);
                escapistRole.LastEscape = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Swooper)
            {
                var swooperRole = Role.GetRole<Swooper>(amnesiac);
                swooperRole.LastSwooped = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Venerer)
            {
                var venererRole = Role.GetRole<Venerer>(amnesiac);
                venererRole.LastCamouflaged = DateTime.UtcNow;
                venererRole.KillsAtStartAbility = 0;
            }

            else if (role == RoleEnum.Blackmailer)
            {
                var blackmailerRole = Role.GetRole<Blackmailer>(amnesiac);
                blackmailerRole.LastBlackmailed = DateTime.UtcNow;
                blackmailerRole.Blackmailed = null;
            }

            else if (role == RoleEnum.Hypnotist)
            {
                var hypnotistRole = Role.GetRole<Hypnotist>(amnesiac);
                hypnotistRole.LastHypnotised = DateTime.UtcNow;
                hypnotistRole.HypnotisedPlayers.RemoveRange(0, hypnotistRole.HypnotisedPlayers.Count);
                hypnotistRole.HysteriaActive = false;
            }

            else if (role == RoleEnum.Miner)
            {
                var minerRole = Role.GetRole<Miner>(amnesiac);
                minerRole.LastMined = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Undertaker)
            {
                var dienerRole = Role.GetRole<Undertaker>(amnesiac);
                dienerRole.LastDragged = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Werewolf)
            {
                var wwRole = Role.GetRole<Werewolf>(amnesiac);
                wwRole.LastRampaged = DateTime.UtcNow;
                wwRole.LastKilled = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Doomsayer)
            {
                var doomRole = Role.GetRole<Doomsayer>(amnesiac);
                doomRole.LastObserved = DateTime.UtcNow;
                doomRole.LastObservedPlayer = null;
            }

            else if (role == RoleEnum.Plaguebearer)
            {
                var plagueRole = Role.GetRole<Plaguebearer>(amnesiac);
                plagueRole.InfectedPlayers.RemoveRange(0, plagueRole.InfectedPlayers.Count);
                plagueRole.InfectedPlayers.Add(amnesiac.PlayerId);
                plagueRole.LastInfected = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Pestilence)
            {
                var pestRole = Role.GetRole<Pestilence>(amnesiac);
                pestRole.LastKilled = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Vampire)
            {
                var vampRole = Role.GetRole<Vampire>(amnesiac);
                vampRole.LastBitten = DateTime.UtcNow;
            }

            else if (role == RoleEnum.Trapper)
            {
                var trapperRole = Role.GetRole<Trapper>(amnesiac);
                trapperRole.LastTrapped = DateTime.UtcNow;
                trapperRole.UsesLeft = CustomGameOptions.MaxTraps;
                trapperRole.trappedPlayers.Clear();
                trapperRole.traps.ClearTraps();
            }

            else if (role == RoleEnum.Bomber)
            {
                var bomberRole = Role.GetRole<Bomber>(amnesiac);
                bomberRole.Bomb.ClearBomb();
            }

            else if (!(amnesiac.Is(RoleEnum.Altruist) || amnesiac.Is(RoleEnum.Admirer) || amnesiac.Is(Faction.Impostors)))
            {
                DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
            }

            var killsList = (newRole.Kills, newRole.CorrectKills, newRole.IncorrectKills, newRole.CorrectAssassinKills, newRole.IncorrectAssassinKills);
            var otherRole = Role.GetRole(other);
            newRole.Kills = otherRole.Kills;
            newRole.CorrectKills = otherRole.CorrectKills;
            newRole.IncorrectKills = otherRole.IncorrectKills;
            newRole.CorrectAssassinKills = otherRole.CorrectAssassinKills;
            newRole.IncorrectAssassinKills = otherRole.IncorrectAssassinKills;
            otherRole.Kills = killsList.Kills;
            otherRole.CorrectKills = killsList.CorrectKills;
            otherRole.IncorrectKills = killsList.IncorrectKills;
            otherRole.CorrectAssassinKills = killsList.CorrectAssassinKills;
            otherRole.IncorrectAssassinKills = killsList.IncorrectAssassinKills;

            if (amnesiac.Is(Faction.Impostors) && (!amnesiac.Is(RoleEnum.Traitor) || CustomGameOptions.OperativeSeesTraitor))
            {
                foreach (var operative in Role.GetRoles(RoleEnum.Operative))
                {
                    var operativeRole = (Operative)operative;
                    if (operativeRole.TasksDone && PlayerControl.LocalPlayer.Is(RoleEnum.Operative))
                    {
                        var gameObj = new GameObject();
                        var arrow = gameObj.AddComponent<ArrowBehaviour>();
                        gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                        var renderer = gameObj.AddComponent<SpriteRenderer>();
                        renderer.sprite = Sprite;
                        arrow.image = renderer;
                        gameObj.layer = 5;
                        operativeRole.OperativeArrows.Add(amnesiac.PlayerId, arrow);
                    }
                    else if (operativeRole.Revealed && PlayerControl.LocalPlayer == amnesiac)
                    {
                        var gameObj = new GameObject();
                        var arrow = gameObj.AddComponent<ArrowBehaviour>();
                        gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                        var renderer = gameObj.AddComponent<SpriteRenderer>();
                        renderer.sprite = Sprite;
                        arrow.image = renderer;
                        gameObj.layer = 5;
                        operativeRole.ImpArrows.Add(arrow);
                    }
                }
            }
        }
    }
}