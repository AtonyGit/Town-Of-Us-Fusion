using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;

namespace TownOfUsFusion.CrewmateRoles.OracleMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKill
    {
        public static bool Prefix(KillButton __instance)
        {
            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Oracle);
            if (!flag) return true;
            var role = Role.GetRole<Oracle>(PlayerControl.LocalPlayer);
            if (!PlayerControl.LocalPlayer.CanMove || role.ClosestPlayer == null) return false;
            var flag2 = role.BlessTimer() == 0f;
            if (!flag2) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            if (role.ClosestPlayer == null) return false;

            var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
            if (interact[4] == true)
            {
                role.BlessedPlayer = role.ClosestPlayer;
                role.LastBlessed = DateTime.UtcNow;
                Utils.Rpc(CustomRPC.Fortify, (byte)0, PlayerControl.LocalPlayer.PlayerId, role.BlessedPlayer.PlayerId);
                return false;
            }
            return false;
        }
    }
}
