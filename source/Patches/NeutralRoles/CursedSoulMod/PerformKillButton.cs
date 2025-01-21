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

namespace TownOfUsFusion.NeutralRoles.CursedSoulMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class PerformKill
{
    public static bool Prefix(KillButton __instance)
    {
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.CursedSoul);
        if (!flag) return true;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        if (!PlayerControl.LocalPlayer.CanMove) return false;
        var role = Role.GetRole<CursedSoul>(PlayerControl.LocalPlayer);
        if (role.SoulSwapTimer() != 0) return false;

        if (role.ClosestPlayer == null) return false;
        var distBetweenPlayers = Utils.GetDistBetweenPlayers(PlayerControl.LocalPlayer, role.ClosestPlayer);
        var flag3 = distBetweenPlayers <
                    GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        if (!flag3) return false;
        var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
        if (interact[4] == true)
        {
            role.CursedPlayer = role.ClosestPlayer;
            role.LastSoulSwapped = DateTime.UtcNow;
        }
        if (interact[0] == true)
        {
            role.CursedPlayer = role.ClosestPlayer;
            role.LastSoulSwapped = DateTime.UtcNow;
            return false;
        }
        else if (interact[1] == true)
        {
            role.CursedPlayer = role.ClosestPlayer;
            role.LastSoulSwapped = DateTime.UtcNow;
            return false;
        }
        else if (interact[3] == true) return false;
        return false;
    }
}
/*
    public enum ShiftEnum
    {
        NonImpostors,
        RegularCrewmates,
        Nobody
    }

    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKillButton

    {
        public static bool Prefix(KillButton __instance)
        {
            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.CursedSoul);
            if (!flag) return true;
            var role = Role.GetRole<CursedSoul>(PlayerControl.LocalPlayer);
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var flag2 = role.CursedSoulShiftTimer() == 0f;
            if (!flag2) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
            if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            if (role.ClosestPlayer == null) return false;
            var playerId = role.ClosestPlayer.PlayerId;
            if (role.ClosestPlayer.isShielded())
            {
                var medic = role.ClosestPlayer.getMedic().Player.PlayerId;

                var writer1 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.AttemptSound, SendOption.Reliable, -1);
                writer1.Write(medic);
                writer1.Write(role.ClosestPlayer.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer1);
                if (CustomGameOptions.ShieldBreaks) role.LastShifted = DateTime.UtcNow;
                StopKill.BreakShield(medic, role.ClosestPlayer.PlayerId, CustomGameOptions.ShieldBreaks);

                return false;
            }

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.SoulSwap, SendOption.Reliable, -1);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.Write(playerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            SoulSwap(role, role.ClosestPlayer);
            return false;
        }

        public static void SoulSwap(CursedSoul CursedSoulRole, PlayerControl other)
        {
            var role = Utils.GetRole(other);
            //System.Console.WriteLine(role);
            //TODO - Shift Animation
            CursedSoulRole.LastShifted = DateTime.UtcNow;
            var CursedSoul = CursedSoulRole.Player;
            List<PlayerTask> tasks1, tasks2;
            List<GameData.TaskInfo> taskinfos1, taskinfos2;

            var swapTasks = true;
            var resetCursedSoul = false;
            var snitch = false;

            Role newRole;

            switch (role)
            {
                case RoleEnum.Sheriff:
                case RoleEnum.Jester:
                case RoleEnum.Engineer:
                case RoleEnum.Mayor:
                case RoleEnum.Swapper:
                case RoleEnum.Investigator:
                case RoleEnum.Medic:
                case RoleEnum.Seer:
                case RoleEnum.Executioner:
                case RoleEnum.Spy:
                case RoleEnum.Snitch:
                case RoleEnum.Arsonist:
                case RoleEnum.Crewmate:
                case RoleEnum.Altruist:

                    if (role == RoleEnum.Investigator) Footprint.DestroyAll(Role.GetRole<Investigator>(other));


                    newRole = Role.GetRole(other);
                    newRole.Player = CursedSoul;

                    if (role == RoleEnum.Snitch) CompleteTask.Postfix(CursedSoul);

                    var modifier = Modifier.GetModifier(other);
                    var modifier2 = Modifier.GetModifier(CursedSoul);
                    if (modifier != null && modifier2 != null)
                    {
                        modifier.Player = CursedSoul;
                        modifier2.Player = other;
                        Modifier.ModifierDictionary.Remove(other.PlayerId);
                        Modifier.ModifierDictionary.Remove(CursedSoul.PlayerId);
                        Modifier.ModifierDictionary.Add(CursedSoul.PlayerId, modifier);
                        Modifier.ModifierDictionary.Add(other.PlayerId, modifier2);
                    }
                    else if (modifier2 != null)
                    {
                        modifier2.Player = other;
                        Modifier.ModifierDictionary.Remove(CursedSoul.PlayerId);
                        Modifier.ModifierDictionary.Add(other.PlayerId, modifier2);
                    }
                    else if (modifier != null)
                    {
                        modifier.Player = CursedSoul;
                        Modifier.ModifierDictionary.Remove(other.PlayerId);
                        Modifier.ModifierDictionary.Add(CursedSoul.PlayerId, modifier);
                    }


                    Role.RoleDictionary.Remove(CursedSoul.PlayerId);
                    Role.RoleDictionary.Remove(other.PlayerId);

                    Role.RoleDictionary.Add(CursedSoul.PlayerId, newRole);
                    snitch = role == RoleEnum.Snitch;

                    foreach (var exeRole in Role.AllRoles.Where(x => x.RoleType == RoleEnum.Executioner))
                    {
                        var executioner = (Executioner) exeRole;
                        var target = executioner.target;
                        if (other == target)
                        {
                            executioner.target.nameText.color = Color.white;
                            ;
                            executioner.target = CursedSoul;

                            executioner.RegenTask();
                        }
                    }

                    if (CustomGameOptions.WhoShifts == ShiftEnum.NonImpostors ||
                        role == RoleEnum.Crewmate && CustomGameOptions.WhoShifts == ShiftEnum.RegularCrewmates)
                    {
                        resetCursedSoul = true;
                        CursedSoulRole.Player = other;
                        Role.RoleDictionary.Add(other.PlayerId, CursedSoulRole);
                    }
                    else
                    {
                        new Crewmate(other);
                    }


                    break;

                case RoleEnum.Undertaker:
                case RoleEnum.Swooper:
                case RoleEnum.Miner:
                case RoleEnum.Morphling:
                case RoleEnum.Janitor:
                case RoleEnum.Impostor:
                case RoleEnum.Glitch:
                case RoleEnum.CursedSoul:
                    CursedSoul.Data.IsImpostor = true;
                    CursedSoul.MurderPlayer(CursedSoul);
                    CursedSoul.Data.IsImpostor = false;
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

                if (other.AmOwner) Coroutines.Start(ShowShift());

                if (snitch)
                {
                    var snitchRole = Role.GetRole<Snitch>(CursedSoul);
                    snitchRole.ImpArrows.DestroyAll();
                    snitchRole.SnitchArrows.DestroyAll();
                    snitchRole.SnitchTargets.Clear();
                    CompleteTask.Postfix(CursedSoul);
                    if (other.AmOwner)
                        foreach (var player in PlayerControl.AllPlayerControls)
                            player.nameText.color = Color.white;
                }

                if (resetCursedSoul) CursedSoulRole.RegenTask();
            }

            //System.Console.WriteLine(CursedSoul.Is(RoleEnum.Sheriff));
            //System.Console.WriteLine(other.Is(RoleEnum.Sheriff));
            //System.Console.WriteLine(Roles.Role.GetRole(CursedSoul));
            if (CursedSoul.AmOwner || other.AmOwner)
            {
                if (CursedSoul.Is(RoleEnum.Arsonist) && other.AmOwner)
                    Role.GetRole<Arsonist>(CursedSoul).IgniteButton.Destroy();
                DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
                DestroyableSingleton<HudManager>.Instance.KillButton.isActive = false;

                Lights.SetLights();
            }
        }
    }*/

}
