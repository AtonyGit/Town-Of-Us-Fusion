using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;

namespace TownOfUsFusion.CrewmateRoles.TricksterMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class PerformTrick
{
    public static bool Prefix(KillButton __instance)
    {
        if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Trickster)) return true;
        var role = Role.GetRole<Trickster>(PlayerControl.LocalPlayer);
        if (!PlayerControl.LocalPlayer.CanMove || role.ClosestPlayer == null) return false;
        var flag2 = role.TricksterKillTimer() == 0f;
        if (!flag2) return false;
        if (!__instance.enabled) return false;
        var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
            PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
        if (role.ClosestPlayer == null) return false;
        if (!role.ButtonUsable) return false;

        if (!role.ClosestPlayer.Is(Faction.NeutralNeophyte) && !role.ClosestPlayer.Is(Faction.Impostors)
        && !role.ClosestPlayer.Is(RoleEnum.Sheriff) && !role.ClosestPlayer.Is(RoleEnum.Hunter) && !role.ClosestPlayer.Is(RoleEnum.Trickster)
        && !role.ClosestPlayer.Is(RoleEnum.Inquisitor) && !role.ClosestPlayer.Is(RoleEnum.Glitch) && !role.ClosestPlayer.Is(RoleEnum.Werewolf)
        && !role.ClosestPlayer.Is(RoleEnum.NeoNecromancer) && !role.ClosestPlayer.Is(RoleEnum.Scourge) && !role.ClosestPlayer.Is(RoleEnum.Berserker))
        {
            var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
            if (interact[0] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                role.UsesLeft--;
                if (role.UsesLeft == 0 && role.CorrectKills == 0 && CustomGameOptions.SelfKillAfterFinalTrick)
                    Utils.RpcMurderPlayer(PlayerControl.LocalPlayer, PlayerControl.LocalPlayer);

                return false;
            }
            else if (interact[1] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                role.LastKilled = role.LastKilled.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.TrickCd);
                return false;
            }
            else if (interact[3] == true) return false;
            return false;
        }
        else
        {
            var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
            if (interact[4] == true) return false;
            else if (interact[0] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                    Utils.RpcMurderPlayer(role.ClosestPlayer, PlayerControl.LocalPlayer);
                return false;
            }
            else if (interact[1] == true)
            {
                role.LastKilled = DateTime.UtcNow;
                role.LastKilled = role.LastKilled.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.TrickCd);
                return false;
            }
            else if (interact[3] == true) return false;
            return false;
        }
    }
}
}
