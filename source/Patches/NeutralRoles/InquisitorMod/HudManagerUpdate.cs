using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.InquisitorMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public static class HudManagerUpdate
{
    public static Sprite InquireSprite => TownOfUsFusion.ObserveSprite;
    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Inquisitor)) return;
        var role = Role.GetRole<Inquisitor>(PlayerControl.LocalPlayer);

            var killButton = __instance.KillButton;
        killButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead && CustomGameOptions.VanquishEnabled
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            killButton.SetCoolDown(role.VanquishTimer(), CustomGameOptions.VanquishCooldown);
            Utils.SetTarget(ref role.ClosestPlayer, killButton, float.NaN);

        if (role.InquireButton == null)
        {
            role.InquireButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
            role.InquireButton.graphic.enabled = true;
            role.InquireButton.gameObject.SetActive(false);
        }
        
            var position = __instance.KillButton.transform.localPosition;
            role.InquireButton.transform.localPosition = new Vector3(position.x - 1f, position.y, position.z);

        role.InquireButton.graphic.sprite = InquireSprite;

        role.InquireButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

        role.InquireButton.SetCoolDown(role.InquireTimer(), CustomGameOptions.InquireCooldown);
        Utils.SetTarget(ref role.ClosestPlayer, role.InquireButton);
    }
}
}