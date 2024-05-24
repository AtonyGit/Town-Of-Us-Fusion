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
public class PerformNecro
{
    public static bool Prefix(KillButton __instance)
    {
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.NeoNecromancer);
        if (!flag) return true;
        if (!PlayerControl.LocalPlayer.CanMove) return false;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        var role = Role.GetRole<NeoNecromancer>(PlayerControl.LocalPlayer);

        if (__instance == role.ResurrectButton)
        {
            if (__instance.isCoolingDown) return false;
            if (!__instance.isActiveAndEnabled) return false;
            if (role.ResurrectTimer() != 0) return false;

            var flag2 = role.ResurrectButton.isCoolingDown;
            if (flag2) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (role == null)
                return false;
            if (role.CurrentTarget == null)
                return false;
            if (Vector2.Distance(role.CurrentTarget.TruePosition,
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            var playerId = role.CurrentTarget.ParentId;
            var player = Utils.PlayerById(playerId);

            
            if (player.Is(AllianceEnum.Lover) || player.Is(AllianceEnum.Crewpocalypse) || player.Is(AllianceEnum.Crewpostor) || player.Is(AllianceEnum.Recruit)
            || player.Is(Faction.Impostors) || player.Is(Faction.NeutralNeophyte) || player.Is(Faction.NeutralApocalypse) || player.Is(Faction.NeutralChaos)
            || player.Is(RoleEnum.Detective) || player.Is(RoleEnum.Seer) || player.Is(RoleEnum.Investigator) || player.Is(RoleEnum.Tracker) || player.Is(RoleEnum.Snitch) || player.Is(RoleEnum.Spy) || player.Is(RoleEnum.Trapper)
            || player.Is(RoleEnum.Mayor) || player.Is(RoleEnum.Prosecutor) || player.Is(RoleEnum.Swapper) || player.Is(RoleEnum.Amnesiac) || player.Is(RoleEnum.Survivor)) return false;
            if (PlayerControl.LocalPlayer.killTimer > GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown - 0.5f) return false;

            role.ResurrectCount += 1;
            role.LastResurrected = DateTime.UtcNow;

            Utils.Rpc(CustomRPC.Resurrect, PlayerControl.LocalPlayer.PlayerId, playerId);

            Resurrect(role.CurrentTarget, role);
            return false;
        }
        return false;
    }

    public static void Resurrect(DeadBody target, NeoNecromancer role)
    {
        var parentId = target.ParentId;
        var position = target.TruePosition;
        var player = Utils.PlayerById(parentId);

        var Resurrected = new List<PlayerControl>();

        if (target != null)
        {
            foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
            {
                if (deadBody.ParentId == target.ParentId) deadBody.gameObject.Destroy();
            }
        }

        player.Revive();
        Murder.KilledPlayers.Remove(
            Murder.KilledPlayers.FirstOrDefault(x => x.PlayerId == player.PlayerId));
        Resurrected.Add(player);
        player.NetTransform.SnapTo(new Vector2(position.x, position.y + 0.3636f));

        if (Patches.SubmergedCompatibility.isSubmerged() && PlayerControl.LocalPlayer.PlayerId == player.PlayerId)
        {
            Patches.SubmergedCompatibility.ChangeFloor(player.transform.position.y > -7);
        }
        if (target != null) Object.Destroy(target.gameObject);

        if (Resurrected.Any(x => x.AmOwner))
            try
            {
                Minigame.Instance.Close();
                Minigame.Instance.Close();
            }
            catch
            {
            }
        Utils.NeoConvert(player);
        return;
    }
}
}