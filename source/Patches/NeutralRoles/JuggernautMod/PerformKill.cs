using System;
using HarmonyLib;
<<<<<<< Updated upstream
using TownOfUsFusion.Roles;
using AmongUs.GameOptions;

namespace TownOfUsFusion.NeutralRoles.JuggernautMod
=======
using TownOfUsFusion.Roles;
using AmongUs.GameOptions;

namespace TownOfUsFusion.NeutralRoles.JuggernautMod
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
            if (!__instance.isActiveAndEnabled || __instance.isCoolingDown) return false;
>>>>>>> Stashed changes
            var role = Role.GetRole<Juggernaut>(PlayerControl.LocalPlayer);
            if (role.Player.inVent) return false;
            if (role.KillTimer() != 0) return false;

            if (role.ClosestPlayer == null) return false;
            var distBetweenPlayers = Utils.GetDistBetweenPlayers(PlayerControl.LocalPlayer, role.ClosestPlayer);
            var flag3 = distBetweenPlayers <
                        GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (!flag3) return false;
            var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
            if (interact[4] == true) return false;
            else if (interact[0] == true)
            {
                role.LastKill = DateTime.UtcNow;
                return false;
            }
            else if (interact[1] == true)
            {
                role.LastKill = DateTime.UtcNow;
                role.LastKill = role.LastKill.AddSeconds(-(CustomGameOptions.JuggKCd - CustomGameOptions.ReducedKCdPerKill * role.JuggKills) + CustomGameOptions.ProtectKCReset);
                return false;
            }
            else if (interact[2] == true)
            {
                role.LastKill = DateTime.UtcNow;
                role.LastKill = role.LastKill.AddSeconds(-(CustomGameOptions.JuggKCd - CustomGameOptions.ReducedKCdPerKill * role.JuggKills) + CustomGameOptions.VestKCReset);
                return false;
            }
            else if (interact[3] == true) return false;
            return false;
        }
    }
}