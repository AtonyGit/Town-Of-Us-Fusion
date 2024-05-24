using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.TricksterMod
{
    [HarmonyPatch(typeof(HudManager))]
public class HudTrick
{
    [HarmonyPatch(nameof(HudManager.Update))]
    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Trickster)) return;
        var data = PlayerControl.LocalPlayer.Data;
        var isDead = data.IsDead;
        var trickButton = __instance.KillButton;

        var role = Role.GetRole<Trickster>(PlayerControl.LocalPlayer);

        if (role.UsesText == null && role.UsesLeft >= 0)
        {
            role.UsesText = Object.Instantiate(trickButton.cooldownTimerText, trickButton.transform);
            role.UsesText.gameObject.SetActive(false);
            role.UsesText.transform.localPosition = new Vector3(
                role.UsesText.transform.localPosition.x + 0.26f,
                role.UsesText.transform.localPosition.y + 0.29f,
                role.UsesText.transform.localPosition.z);
            role.UsesText.transform.localScale = role.UsesText.transform.localScale * 0.65f;
            role.UsesText.alignment = TMPro.TextAlignmentOptions.Right;
            role.UsesText.fontStyle = TMPro.FontStyles.Bold;
        }
        if (role.UsesText != null)
        {
            role.UsesText.text = role.UsesLeft + "";
        }
        trickButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        role.UsesText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        if (role.ButtonUsable) trickButton.SetCoolDown(role.TricksterKillTimer(), CustomGameOptions.TrickCd);
        else trickButton.SetCoolDown(0f, CustomGameOptions.TrickCd);
        if (role.UsesLeft == 0) return;

        Utils.SetTarget(ref role.ClosestPlayer, trickButton, float.NaN);

        var renderer = trickButton.graphic;
        if (role.ClosestPlayer != null && role.ButtonUsable)
        {
            renderer.color = Palette.EnabledColor;
            renderer.material.SetFloat("_Desat", 0f);
            role.UsesText.color = Palette.EnabledColor;
            role.UsesText.material.SetFloat("_Desat", 0f);
        }
        else
        {
            renderer.color = Palette.DisabledClear;
            renderer.material.SetFloat("_Desat", 1f);
            role.UsesText.color = Palette.DisabledClear;
            role.UsesText.material.SetFloat("_Desat", 1f);
        }
    }
}
}