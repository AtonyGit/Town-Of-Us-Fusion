using HarmonyLib;
using Reactor.Utilities;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;
using TownOfUsFusion.Roles.Apocalypse;

namespace TownOfUsFusion.NeutralRoles.CannibalMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class PerformKillButton

{
    public static bool Prefix(KillButton __instance)
    {
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal);
        if (!flag) return true;
        if (!PlayerControl.LocalPlayer.CanMove) return false;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        var role = Role.GetRole<Cannibal>(PlayerControl.LocalPlayer);

        if (__instance == role.EatButton)
        {
            var flag2 = __instance.isCoolingDown;
            if (flag2) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (Vector2.Distance(role.CurrentTarget.TruePosition,
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            var playerId = role.CurrentTarget.ParentId;
            var player = Utils.PlayerById(playerId);
            if (player.IsInfected() || role.Player.IsInfected())
            {
                foreach (var pb in Role.GetRoles(RoleEnum.Plaguebearer)) ((Plaguebearer)pb).RpcSpreadInfection(player, role.Player);
            }

            Utils.Rpc(CustomRPC.CannibalEat, PlayerControl.LocalPlayer.PlayerId, playerId);

            Coroutines.Start(EatCoroutine.CannibalCoroutine(role.CurrentTarget, role));
            return false;
        }

        return true;
    }
}
}