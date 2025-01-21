using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;

namespace TownOfUsFusion.NeutralRoles.CannibalMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class PlayerControlUpdate
{
    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal)) return;

        var role = Role.GetRole<Cannibal>(PlayerControl.LocalPlayer);

        var data = PlayerControl.LocalPlayer.Data;
        var isDead = data.IsDead;
        var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
        var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        var flag = (GameOptionsManager.Instance.currentNormalGameOptions.GhostsDoTasks || !data.IsDead) &&
                   (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) &&
                   PlayerControl.LocalPlayer.CanMove;
        var allocs = Physics2D.OverlapCircleAll(truePosition, maxDistance,
            LayerMask.GetMask(new[] { "Players", "Ghost" }));
        DeadBody closestBody = null;
        var closestDistance = float.MaxValue;

        foreach (var collider2D in allocs)
        {
            if (!flag || isDead || collider2D.tag != "DeadBody") continue;
            var component = collider2D.GetComponent<DeadBody>();
            if (!(Vector2.Distance(truePosition, component.TruePosition) <=
                  maxDistance)) continue;

            var distance = Vector2.Distance(truePosition, component.TruePosition);
            if (!(distance < closestDistance)) continue;
            closestBody = component;
            closestDistance = distance;
        }

        KillButtonTarget.SetTarget(__instance.KillButton, closestBody, role);
        __instance.KillButton.SetCoolDown(PlayerControl.LocalPlayer.killTimer, GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);
    }
}
}