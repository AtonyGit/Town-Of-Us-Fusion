using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using AmongUs.GameOptions;
using TownOfUsFusion.Roles.Apocalypse;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Extensions;
using Reactor.Utilities;

namespace TownOfUsFusion.NeutralRoles.InquisitorMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class PerformVanquish
{
    public static bool Prefix(KillButton __instance)
    {
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Inquisitor);
        if (!flag) return true;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        if (!PlayerControl.LocalPlayer.CanMove) return false;
        var role = Role.GetRole<Inquisitor>(PlayerControl.LocalPlayer);
        
        var distBetweenPlayers = Utils.GetDistBetweenPlayers(PlayerControl.LocalPlayer, role.ClosestPlayer);
        
        if (__instance == role.InquireButton)
        {
        if (role.InquireTimer() != 0) return false;

        if (role.ClosestPlayer == null) return false;
        var flag6 = distBetweenPlayers <
                    GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        if (!flag6) return false;
        var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
        if (interact[4] == true)
        {
            role.LastInquiredPlayer = role.ClosestPlayer;
            role.LastInquired = DateTime.UtcNow;
        }
        if (interact[0] == true)
        {
            role.LastInquired = DateTime.UtcNow;
            role.LastInquiredPlayer = role.ClosestPlayer;
            return false;
        }
        else if (interact[1] == true)
        {
            role.LastInquired = DateTime.UtcNow;
            role.LastInquired = role.LastInquired.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.InquireCooldown);
            return false;
        }
        else if (interact[3] == true) return false;
        return false;
        }

        if (!role.canVanquish) return false;
        if (!CustomGameOptions.VanquishEnabled) return false;
        if (role.lostVanquish) return false;
        var flag2 = role.VanquishTimer() == 0f;
        if (!flag2) return false;
        if (!__instance.enabled || role.ClosestPlayer == null) return false;
        var flag3 = distBetweenPlayers < GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        if (!flag3) return false;

        var flag4 = role.invalidHeretics ? true : role.ClosestPlayer == role.heretic1 || role.ClosestPlayer == role.heretic2 || role.ClosestPlayer == role.heretic3;
        var interactAlt = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
        if (interactAlt[4] == true) return false;
        else if (interactAlt[0] == true)
        {
            if (!flag4)
            {
                Utils.RpcMurderPlayer(PlayerControl.LocalPlayer, role.ClosestPlayer);
                role.LastVanquished = DateTime.UtcNow;
                role.lostVanquish = true;
            }
            else
            {
                Utils.RpcMurderPlayer(PlayerControl.LocalPlayer, role.ClosestPlayer);
                role.LastVanquished = DateTime.UtcNow;
                if (role.invalidHeretics || role.heretic1.Data.IsDead && role.heretic2.Data.IsDead && role.heretic3.Data.IsDead) {
                    role.didWin = true;
                    role.RegenTask();
                }
            }
            return false;
        }
        else if (interactAlt[1] == true)
        {
            role.LastVanquished = DateTime.UtcNow;
            role.LastVanquished = role.LastVanquished.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.VanquishCooldown);
            return false;
        }
        else if (interactAlt[2] == true)
        {
            role.LastVanquished = DateTime.UtcNow;
            role.LastVanquished = role.LastVanquished.AddSeconds(CustomGameOptions.VestKCReset - CustomGameOptions.VanquishCooldown);
            return false;
        }
        else if (interactAlt[3] == true) return false;
        return false;
    }
}
}