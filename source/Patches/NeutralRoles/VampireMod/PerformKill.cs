﻿using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;
using TownOfUsFusion.CrewmateRoles.TrackerMod;
using TownOfUsFusion.CrewmateRoles.TrapperMod;
using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using System.Linq;
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.Patches.NeutralRoles;
using Hazel;

namespace TownOfUsFusion.NeutralRoles.VampireMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class Bite
    {
        public static bool Prefix(KillButton __instance)
        {
            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Vampire);
            if (!flag) return true;
            var role = Role.GetRole<Vampire>(PlayerControl.LocalPlayer);
            if (!PlayerControl.LocalPlayer.CanMove || role.ClosestPlayer == null) return false;
            var flag2 = role.BiteTimer() == 0f;
            if (!flag2) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            if (role.ClosestPlayer == null) return false;

            var vamps = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(RoleEnum.Vampire)).ToList();
            foreach (var phantom in Role.GetRoles(RoleEnum.Phantom))
            {
                var phantomRole = (Phantom)phantom;
                if (phantomRole.formerRole == RoleEnum.Vampire) vamps.Add(phantomRole.Player);
            }
            var aliveVamps = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(RoleEnum.Vampire) && !x.Data.IsDead && !x.Data.Disconnected).ToList();
            if ((role.ClosestPlayer.Is(Faction.Crewmates) || (role.ClosestPlayer.Is(Faction.NeutralBenign)
                && CustomGameOptions.CanBiteNeutralBenign) || (role.ClosestPlayer.Is(Faction.NeutralEvil)
                && CustomGameOptions.CanBiteNeutralEvil)) && !role.ClosestPlayer.Is(AllianceEnum.Lover) &&
                aliveVamps.Count == 1 && vamps.Count < CustomGameOptions.MaxVampiresPerGame)
            {
                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
                if (interact[6] == true)
                {
                        role.BittenPlayer = role.ClosestPlayer;
                        __instance.SetTarget(null);
                        DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);

                        role.TimeRemaining = CustomGameOptions.BiteDuration;
                        __instance.SetCoolDown(role.TimeRemaining, CustomGameOptions.BiteDuration);
                        var writer4 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                            (byte)CustomRPC.RemoteBite,
                        SendOption.Reliable, -1);
                        Convert(role.ClosestPlayer);
                        //Utils.Rpc(CustomRPC.Bite, role.ClosestPlayer.PlayerId);

                        writer4.Write(PlayerControl.LocalPlayer.PlayerId);
                        writer4.Write(role.BittenPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer4);
                        return false;
                }
                else if (interact[0] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    return false;
                }
                else if (interact[5] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    return false;
                }
                else if (interact[4] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    return false;
                }
                else if (interact[1] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    role.LastBitten = role.LastBitten.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[3] == true) return false;
                return false;
            }
            else
            {
                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
                if (interact[6] == true) 
                {
                    role.BittenPlayer = role.ClosestPlayer;
                    __instance.SetTarget(null);
                    DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);
                    role.TimeRemaining = CustomGameOptions.BiteDuration;
                    __instance.SetCoolDown(role.TimeRemaining, CustomGameOptions.BiteDuration);
                    var writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte)CustomRPC.RemoteBite,
                    SendOption.Reliable, -1);
                    writer2.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer2.Write(role.BittenPlayer.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer2);
                    return false;
                }
                else if (interact[0] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    return false;
                }
                else if (interact[5] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    return false;
                }
                else if (interact[4] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    return false;
                }
                else if (interact[1] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    role.LastBitten = role.LastBitten.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[2] == true)
                {
                    role.LastBitten = DateTime.UtcNow;
                    role.LastBitten = role.LastBitten.AddSeconds(CustomGameOptions.VestKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[3] == true) return false;
                return false;
            }
        }

        public static void Convert(PlayerControl newVamp)
        {
            var oldRole = Role.GetRole(newVamp);
            var killsList = (oldRole.CorrectKills, oldRole.IncorrectKills, oldRole.CorrectAssassinKills, oldRole.IncorrectAssassinKills);

            if (newVamp.Is(RoleEnum.Spy))
            {
                var spy = Role.GetRole<Spy>(newVamp);
                spy.SpyArrows.Values.DestroyAll();
                spy.SpyArrows.Clear();
                spy.ImpArrows.DestroyAll();
                spy.ImpArrows.Clear();
            }

            if (newVamp == StartImitate.ImitatingPlayer) StartImitate.ImitatingPlayer = null;

            if (newVamp.Is(RoleEnum.GuardianAngel))
            {
                var ga = Role.GetRole<GuardianAngel>(newVamp);
                ga.UnProtect();
            }

            if (newVamp.Is(RoleEnum.Medium))
            {
                var medRole = Role.GetRole<Medium>(newVamp);
                medRole.MediatedPlayers.Values.DestroyAll();
                medRole.MediatedPlayers.Clear();
                medRole.BodyArrows.Values.DestroyAll();
                medRole.BodyArrows.Clear();
            }

            if (PlayerControl.LocalPlayer == newVamp)
            {
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator)) Role.GetRole<Investigator>(PlayerControl.LocalPlayer).ExamineButton.SetTarget(null);
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Hunter)) Role.GetRole<Hunter>(PlayerControl.LocalPlayer).StalkButton.SetTarget(null);
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Altruist)) CrewmateRoles.AltruistMod.KillButtonTarget.SetTarget(HudManager.Instance.KillButton, null, Role.GetRole<Altruist>(PlayerControl.LocalPlayer));
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Amnesiac)) AmnesiacMod.KillButtonTarget.SetTarget(HudManager.Instance.KillButton, null, Role.GetRole<Amnesiac>(PlayerControl.LocalPlayer));

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Sheriff)) HudManager.Instance.KillButton.buttonLabelText.gameObject.SetActive(false);

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer))
                {
                    var engineerRole = Role.GetRole<Engineer>(PlayerControl.LocalPlayer);
                    //UnityEngine.Object.Destroy(engineerRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
                {
                    var trackerRole = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);
                    trackerRole.TrackerArrows.Values.DestroyAll();
                    trackerRole.TrackerArrows.Clear();
                    //UnityEngine.Object.Destroy(trackerRole.UsesText);
                    Footprint.DestroyAll(trackerRole);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Lookout))
                {
                    var loRole = Role.GetRole<Lookout>(PlayerControl.LocalPlayer);
                    //UnityEngine.Object.Destroy(loRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Aurial))
                {
                    var aurialRole = Role.GetRole<Aurial>(PlayerControl.LocalPlayer);
                    aurialRole.SenseArrows.Values.DestroyAll();
                    aurialRole.SenseArrows.Clear();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
                {
                    var transporterRole = Role.GetRole<Transporter>(PlayerControl.LocalPlayer);
                    //UnityEngine.Object.Destroy(transporterRole.UsesText);
                    try
                    {
                        PlayerMenu.singleton.Menu.Close();
                    }
                    catch { }
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
                {
                    var veteranRole = Role.GetRole<Veteran>(PlayerControl.LocalPlayer);
                    //UnityEngine.Object.Destroy(veteranRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Trapper))
                {
                    var trapperRole = Role.GetRole<Trapper>(PlayerControl.LocalPlayer);
                    //UnityEngine.Object.Destroy(trapperRole.UsesText);
                    trapperRole.traps.ClearTraps();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator))
                {
                    var detecRole = Role.GetRole<Investigator>(PlayerControl.LocalPlayer);
                    detecRole.ExamineButton.gameObject.SetActive(false);
                    foreach (GameObject scene in detecRole.CrimeScenes)
                    {
                        UnityEngine.Object.Destroy(scene);
                    }
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Hunter))
                {
                    var hunterRole = Role.GetRole<Hunter>(PlayerControl.LocalPlayer);
                    //UnityEngine.Object.Destroy(hunterRole.UsesText);
                    hunterRole.StalkButton.SetTarget(null);
                    hunterRole.StalkButton.gameObject.SetActive(false);
                    HudManager.Instance.KillButton.buttonLabelText.gameObject.SetActive(false);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Survivor))
                {
                    var survRole = Role.GetRole<Survivor>(PlayerControl.LocalPlayer);
                    //UnityEngine.Object.Destroy(survRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel))
                {
                    var gaRole = Role.GetRole<GuardianAngel>(PlayerControl.LocalPlayer);
                    //UnityEngine.Object.Destroy(gaRole.UsesText);
                }
            }

            Role.RoleDictionary.Remove(newVamp.PlayerId);

            if (PlayerControl.LocalPlayer == newVamp)
            {
                var role = new Vampire(PlayerControl.LocalPlayer);
                role.CorrectKills = killsList.CorrectKills;
                role.IncorrectKills = killsList.IncorrectKills;
                role.CorrectAssassinKills = killsList.CorrectAssassinKills;
                role.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
                role.RegenTask();
            }
            else
            {
                var role = new Vampire(newVamp);
                role.CorrectKills = killsList.CorrectKills;
                role.IncorrectKills = killsList.IncorrectKills;
                role.CorrectAssassinKills = killsList.CorrectAssassinKills;
                role.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
            }

            if (CustomGameOptions.NewVampCanAssassin) new Assassin(newVamp);
        }
    }
}
