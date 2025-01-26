using System;
using AmongUs.GameOptions;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class Protect
    {
        public static bool Prefix(KillButton __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard);
            if (!flag) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Bodyguard>(PlayerControl.LocalPlayer);
                if (__instance.isCoolingDown) return false;
                if (!__instance.isActiveAndEnabled) return false;
            var protectButton = DestroyableSingleton<HudManager>.Instance.KillButton;
            if (!role.ButtonUsable) return false;

            if (role.guardedPlayer == null)
            {
                var flag2 = role.TargetTimer() == 0f;
                if (!flag2) return false;
                if (!__instance.enabled) return false;
                var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
                if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                    PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
                if (role.ClosestPlayer == null) return false;

                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
                if (interact[4] == true)
                {
                    role.guardedPlayer = role.ClosestPlayer;
                    role.LastProtected = DateTime.UtcNow;
                    //Utils.Rpc(CustomRPC.Protect, PlayerControl.LocalPlayer.PlayerId, role.ClosestPlayer.PlayerId);
                    return false;
                }
                return false;
            }
            else
            {
                if (role.ProtectTimer() != 0) return false;
                var abilityUsed = Utils.AbilityUsed(PlayerControl.LocalPlayer);
                if (!abilityUsed) return false;
                role.TimeRemaining = CustomGameOptions.ProtectDuration;
                role.UsesLeft--;
                role.Protect();
                Utils.Rpc(CustomRPC.BGProtect, PlayerControl.LocalPlayer.PlayerId);
                return false;
            }
        }
    }
}