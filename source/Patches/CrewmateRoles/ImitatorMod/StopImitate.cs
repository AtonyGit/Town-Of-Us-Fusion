using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using TownOfUsFusion.CrewmateRoles.TrackerMod;
using TownOfUsFusion.CrewmateRoles.TrapperMod;
using System.Collections.Generic;
using System.Linq;

namespace TownOfUsFusion.CrewmateRoles.ImitatorMod
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.StartMeeting))]
    class StartMeetingPatch
    {
        public static void Prefix(PlayerControl __instance, [HarmonyArgument(0)] NetworkedPlayerInfo meetingTarget)
        {
            if (__instance == null)
            {
                return;
            }
            if (StartImitate.ImitatingPlayer != null && !StartImitate.ImitatingPlayer.Is(RoleEnum.Traitor))
            {
                List<RoleEnum> trappedPlayers = null;
                Dictionary<byte, List<RoleEnum>> seenPlayers = null;
                PlayerControl confessingPlayer = null;
                PlayerControl jailedPlayer = null;

                if (PlayerControl.LocalPlayer == StartImitate.ImitatingPlayer)
                {

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer))
                    {
                        var engineerRole = Role.GetRole<Engineer>(PlayerControl.LocalPlayer);
                        Object.Destroy(engineerRole.UsesText);
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
                    {
                        var trackerRole = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);
                        trackerRole.TrackerArrows.Values.DestroyAll();
                        trackerRole.TrackerArrows.Clear();
                        Footprint.DestroyAll(trackerRole);
                        Object.Destroy(trackerRole.UsesText);
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Lookout))
                    {
                        var loRole = Role.GetRole<Lookout>(PlayerControl.LocalPlayer);
                        Object.Destroy(loRole.UsesText);
                        seenPlayers = loRole.Watching;
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
                        Object.Destroy(transporterRole.UsesText);
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
                    {
                        var veteranRole = Role.GetRole<Veteran>(PlayerControl.LocalPlayer);
                        Object.Destroy(veteranRole.UsesText);
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Trapper))
                    {
                        var trapperRole = Role.GetRole<Trapper>(PlayerControl.LocalPlayer);
                        Object.Destroy(trapperRole.UsesText);
                        trapperRole.traps.ClearTraps();
                        trappedPlayers = trapperRole.trappedPlayers;
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Oracle))
                    {
                        var oracleRole = Role.GetRole<Oracle>(PlayerControl.LocalPlayer);
                        oracleRole.ClosestPlayer = null;
                        confessingPlayer = oracleRole.BlessedPlayer;
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Deputy))
                    {
                        var deputyRole = Role.GetRole<Deputy>(PlayerControl.LocalPlayer);
                        deputyRole.ClosestPlayer = null;
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator))
                    {
                        var detecRole = Role.GetRole<Investigator>(PlayerControl.LocalPlayer);
                        detecRole.ClosestPlayer = null;
                        detecRole.ExamineButton.gameObject.SetActive(false);
                        foreach (GameObject scene in detecRole.CrimeScenes)
                        {
                            UnityEngine.Object.Destroy(scene);
                        }
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Hunter))
                    {
                        var hunterRole = Role.GetRole<Hunter>(PlayerControl.LocalPlayer);
                        Object.Destroy(hunterRole.UsesText);
                        hunterRole.ClosestPlayer = null;
                        hunterRole.ClosestStalkPlayer = null;
                        hunterRole.StalkButton.SetTarget(null);
                        hunterRole.StalkButton.gameObject.SetActive(false);
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Politician))
                    {
                        var politicianRole = Role.GetRole<Politician>(PlayerControl.LocalPlayer);
                        politicianRole.ClosestPlayer = null;
                    }

                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Jailor))
                    {
                        var jailorRole = Role.GetRole<Jailor>(PlayerControl.LocalPlayer);
                        jailorRole.ClosestPlayer = null;
                    }

                    try
                    {
                        DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
                    }
                    catch { }
                }

                if (StartImitate.ImitatingPlayer.Is(RoleEnum.Medium))
                {
                    var medRole = Role.GetRole<Medium>(StartImitate.ImitatingPlayer);
                    medRole.MediatedPlayers.Values.DestroyAll();
                    medRole.MediatedPlayers.Clear();
                    medRole.BodyArrows.Values.DestroyAll();
                    medRole.BodyArrows.Clear();
                }

                if (StartImitate.ImitatingPlayer.Is(RoleEnum.Spy))
                {
                    var spyRole = Role.GetRole<Spy>(StartImitate.ImitatingPlayer);
                    spyRole.SpyArrows.Values.DestroyAll();
                    spyRole.SpyArrows.Clear();
                    spyRole.ImpArrows.DestroyAll();
                    spyRole.ImpArrows.Clear();
                }

                if (StartImitate.ImitatingPlayer.Is(RoleEnum.Jailor))
                {
                    var jailorRole = Role.GetRole<Jailor>(StartImitate.ImitatingPlayer);
                    jailedPlayer = jailorRole.Jailed;
                }

                var role = Role.GetRole(StartImitate.ImitatingPlayer);
                var killsList = (role.Kills, role.CorrectKills, role.IncorrectKills, role.CorrectAssassinKills, role.IncorrectAssassinKills);
                Role.RoleDictionary.Remove(StartImitate.ImitatingPlayer.PlayerId);
                var imitator = new Imitator(StartImitate.ImitatingPlayer);
                imitator.trappedPlayers = trappedPlayers;
                imitator.confessingPlayer = confessingPlayer;
                imitator.watchedPlayers = seenPlayers;
                imitator.jailedPlayer = jailedPlayer;
                var newRole = Role.GetRole(StartImitate.ImitatingPlayer);
                newRole.RemoveFromRoleHistory(newRole.RoleType);
                newRole.Kills = killsList.Kills;
                newRole.CorrectKills = killsList.CorrectKills;
                newRole.IncorrectKills = killsList.IncorrectKills;
                newRole.CorrectAssassinKills = killsList.CorrectAssassinKills;
                newRole.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
                Role.GetRole<Imitator>(StartImitate.ImitatingPlayer).ImitatePlayer = null;
                StartImitate.ImitatingPlayer = null;
            }
            return;
        }
    }
}
