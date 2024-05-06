using HarmonyLib;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Cultist;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;
using System;
using AmongUs.GameOptions;

namespace TownOfUsFusion.NeutralRoles.ApparitionistMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class PerformResurrect2
{
    public static bool Prefix(KillButton __instance)
    {
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Apparitionist);
        if (!flag) return true;
        if (!PlayerControl.LocalPlayer.CanMove) return false;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        var role = Role.GetRole<Apparitionist>(PlayerControl.LocalPlayer);
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

            if (player.Is(AllianceEnum.Lover) || !player.Is(AllianceEnum.Crewpocalypse) || !player.Is(AllianceEnum.Crewpostor) || !player.Is(AllianceEnum.Recruit) || player.Is(Faction.Impostors) || player.Is(RoleEnum.Vampire) || player.Is(Faction.NeutralApocalypse) || player.Is(Faction.NeutralChaos)
             || player.Is(RoleEnum.Detective) || player.Is(RoleEnum.Seer) || player.Is(RoleEnum.Investigator) || player.Is(RoleEnum.Tracker) || player.Is(RoleEnum.Snitch)
            || player.Is(RoleEnum.Spy) || player.Is(RoleEnum.Trapper) || player.Is(RoleEnum.VampireHunter) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante)
             || player.Is(RoleEnum.Mayor) || player.Is(RoleEnum.Prosecutor) || player.Is(RoleEnum.Amnesiac) || player.Is(RoleEnum.GuardianAngel)) return false;
            if (PlayerControl.LocalPlayer.killTimer > GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown - 0.5f) return false;

            role.ResurrectCount += 1;
            role.LastResurrected = DateTime.UtcNow;

            Utils.Rpc(CustomRPC.Resurrect2, PlayerControl.LocalPlayer.PlayerId, playerId);

            Resurrect2(role.CurrentTarget, role);
            return false;
        }

        return true;
    }

    public static void Resurrect2(DeadBody target, Apparitionist role)
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