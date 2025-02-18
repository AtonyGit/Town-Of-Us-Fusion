using System;
using AmongUs.GameOptions;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.AdmirerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class Admire
    {
        public static bool Prefix(KillButton __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Admirer);
            if (!flag) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Admirer>(PlayerControl.LocalPlayer);
                if (__instance.isCoolingDown) return false;
                if (!__instance.isActiveAndEnabled) return false;
            var protectButton = DestroyableSingleton<HudManager>.Instance.KillButton;
            if (!role.ButtonUsable) return false;

            if (role.AdmiredPlayer == null)
            {
                if (!__instance.enabled) return false;
                var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
                if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                    PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
                if (role.ClosestPlayer == null) return false;

                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
                if (interact[6] == true)
                {
                    role.AdmiredPlayer = role.ClosestPlayer;
                    role.ButtonUsable = false;
                    Utils.Rpc(CustomRPC.Admire, PlayerControl.LocalPlayer.PlayerId, role.ClosestPlayer.PlayerId);
                    return false;
                }
                return false;
            }

            return false;
        }
    }
}