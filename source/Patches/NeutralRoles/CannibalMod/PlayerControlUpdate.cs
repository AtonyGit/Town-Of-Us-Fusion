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
        if (role.EatButton == null)
        {
            role.EatButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
            role.EatButton.graphic.enabled = true;
            role.EatButton.gameObject.SetActive(false);
        }

        role.EatButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        role.EatButton.graphic.sprite = TownOfUsFusion.ConsumeSprite;


        var data = PlayerControl.LocalPlayer.Data;
        var isDead = data.IsDead;
        var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
        var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        var flag = (GameOptionsManager.Instance.currentNormalGameOptions.GhostsDoTasks || !data.IsDead) &&
                   (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) &&
                   PlayerControl.LocalPlayer.CanMove;
        var allocs = Physics2D.OverlapCircleAll(truePosition, maxDistance,
            LayerMask.GetMask(new[] { "Players", "Ghost" }));
        var killButton = role.EatButton;
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

        KillButtonTarget.SetTarget(killButton, closestBody, role);
        role.EatButton.SetCoolDown(PlayerControl.LocalPlayer.killTimer, GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);
    }
}
}