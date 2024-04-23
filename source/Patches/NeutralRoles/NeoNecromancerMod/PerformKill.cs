using HarmonyLib;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Roles;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;
using System;
using AmongUs.GameOptions;

namespace TownOfUsFusion.NeutralRoles.NeoNecromancerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class PerformKill
{
    [HarmonyPriority(Priority.First)]
    public static bool Prefix(KillButton __instance)
    {
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.NeoNecromancer);
        if (!flag) return true;
        if (!PlayerControl.LocalPlayer.CanMove) return false;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        var role = Role.GetRole<NeoNecromancer>(PlayerControl.LocalPlayer);
        
        if (role.NecroKillTimer() != 0) return false;
        if (!role.CanKill) return false;
        if (__instance == role.ResurrectButton) return true;
        if (!__instance.isActiveAndEnabled || __instance.isCoolingDown) return false;
        if (role.ClosestPlayer == null) return false;
        var distBetweenPlayers = Utils.GetDistBetweenPlayers(PlayerControl.LocalPlayer, role.ClosestPlayer);
        var flag3 = distBetweenPlayers <
                    GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        if (!flag3) return false;

        var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
        if (interact[4] == true) return false;
        else if (interact[0] == true)
        {
            role.LastKilled = DateTime.UtcNow;
            role.CanKill = false;
            return false;
        }
        else if (interact[1] == true)
        {
            role.LastKilled = DateTime.UtcNow;
            role.LastKilled = role.LastKilled.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.NecroKillCooldown);
            return false;
        }
        else if (interact[2] == true)
        {
            role.LastKilled = DateTime.UtcNow;
            role.LastKilled = role.LastKilled.AddSeconds(CustomGameOptions.VestKCReset - CustomGameOptions.NecroKillCooldown);
            return false;
        }
        else if (interact[3] == true) return false;
        return false;
    }
}
}