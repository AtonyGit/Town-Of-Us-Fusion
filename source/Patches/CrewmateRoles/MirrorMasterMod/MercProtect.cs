using AmongUs.GameOptions;
using HarmonyLib;
using Hazel;
using Il2CppSystem;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.MirrorMasterMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class MirrorProtect
    {
        public static bool Prefix(KillButton __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.MirrorMaster);
            if (!flag) return true;
            var role = Role.GetRole<MirrorMaster>(PlayerControl.LocalPlayer);
            if (!PlayerControl.LocalPlayer.CanMove) return false;

            var distBetweenPlayers = Utils.GetDistBetweenPlayers(PlayerControl.LocalPlayer, role.ClosestPlayer);
            var flag3 = distBetweenPlayers <
                        GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (!flag3) return false;
            if (role.ClosestPlayer == null) return false;
            
            if (__instance == role.AbsorbButton)
            {
                if (!__instance.isActiveAndEnabled) return false;
                if (__instance.isCoolingDown) return false;
                if (role.AbsorbTimer() != 0) return false;
                if (role.AbsorbUsesLeft <= 0) return false;

                var interact2 = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, false);
                if (interact2[6] == true && role.ShieldedPlayer == null)
                {
                    role.ShieldedPlayer = role.ClosestPlayer;
                    role.ShieldedPlayer.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
                    role.ShieldedPlayer.myRend().material.SetFloat("_Outline", 0f);
                    role.LastUnleashed = System.DateTime.UtcNow;
                    role.LastAbsorbed = System.DateTime.UtcNow;
                }
                return false;
            }

            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            if (!__instance.isActiveAndEnabled) return false;
            if (__instance.isCoolingDown) return false;
            if (role.UnleashTimer() != 0) return false;
            if (role.UnleashUsesLeft <= 0) return false;
            var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
            if (interact[6] == true)
            {
                role.UnleashUsesLeft -= 1;
                role.ShieldedPlayer = null;
                role.LastUnleashed = System.DateTime.UtcNow;
                role.LastAbsorbed = System.DateTime.UtcNow;
            }
            if (interact[0] == true)
            {
                role.LastUnleashed = System.DateTime.UtcNow;
                role.LastAbsorbed = System.DateTime.UtcNow;
            }
            else if (interact[4] == true)
            {
                role.LastUnleashed = System.DateTime.UtcNow;
                role.LastAbsorbed = System.DateTime.UtcNow;
            }
            else if (interact[1] == true)
            {
                role.LastUnleashed = System.DateTime.UtcNow;
                role.LastAbsorbed = System.DateTime.UtcNow;
                role.LastUnleashed = role.LastUnleashed.AddSeconds(-CustomGameOptions.MirrorUnleashCd + CustomGameOptions.ProtectKCReset);
                role.LastAbsorbed = role.LastAbsorbed.AddSeconds(-CustomGameOptions.MirrorAbsorbCd + CustomGameOptions.ProtectKCReset);
            }
            else if (interact[2] == true)
            {
                role.LastUnleashed = System.DateTime.UtcNow;
                role.LastAbsorbed = System.DateTime.UtcNow;
                role.LastUnleashed = role.LastUnleashed.AddSeconds(-CustomGameOptions.MirrorUnleashCd + CustomGameOptions.VestKCReset);
                role.LastAbsorbed = role.LastAbsorbed.AddSeconds(-CustomGameOptions.MirrorAbsorbCd + CustomGameOptions.VestKCReset);
            }
            return false;
        }
    }
}
