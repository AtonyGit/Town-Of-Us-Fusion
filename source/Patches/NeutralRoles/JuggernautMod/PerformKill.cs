using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using AmongUs.GameOptions;

namespace TownOfUsFusion.NeutralRoles.JuggernautMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKill
    {
        public static bool Prefix(KillButton __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Juggernaut);
            if (!flag) return true;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (!__instance.isActiveAndEnabled || __instance.isCoolingDown) return false;
            var role = Role.GetRole<Juggernaut>(PlayerControl.LocalPlayer);
            if (role.Player.inVent) return false;
            if (role.KillTimer() != 0) return false;

            if (role.ClosestPlayer == null) return false;
            var distBetweenPlayers = Utils.GetDistBetweenPlayers(PlayerControl.LocalPlayer, role.ClosestPlayer);
            var flag3 = distBetweenPlayers <
                        GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (!flag3) return false;
            var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
            if (interact[6] == true) return false;
            else if (interact[0] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                return false;
            }
            else if (interact[5] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                return false;
            }
            else if (interact[4] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                return false;
            }
            else if (interact[1] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                role.LastKilled = role.LastKilled.AddSeconds(-(CustomGameOptions.JuggKCd - CustomGameOptions.ReducedKCdPerKill * role.JuggKills) + CustomGameOptions.ProtectKCReset);
                return false;
            }
            else if (interact[2] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                role.LastKilled = role.LastKilled.AddSeconds(-(CustomGameOptions.JuggKCd - CustomGameOptions.ReducedKCdPerKill * role.JuggKills) + CustomGameOptions.VestKCReset);
                return false;
            }
            else if (interact[3] == true) return false;
            return false;
        }
    }
}