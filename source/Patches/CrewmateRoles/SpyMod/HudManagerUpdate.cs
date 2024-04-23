using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.SpyMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class HudManagerUpdate
{
    public static void Postfix(HudManager __instance)
    {
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Spy)) return;
        //    __instance.KillButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
            __instance.KillButton.graphic.enabled = true;
            __instance.KillButton.gameObject.SetActive(false);
        UpdatePortAdmin(__instance);
    }

    public static void UpdatePortAdmin(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Spy)) return;
        var PortAdmin = __instance.KillButton;

    //    var role = Role.GetRole<Spy>(PlayerControl.LocalPlayer);

        /*if (role.UsesText == null && role.UsesLeft > 0)
        {
            role.UsesText = Object.Instantiate(PortAdmin.cooldownTimerText, PortAdmin.transform);
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
        }*/

        PortAdmin.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        /*role.UsesText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);*/
        //if (role.Vesting) PortAdmin.SetCoolDown(role.TimeRemaining, CustomGameOptions.VestDuration);
        //else if (role.ButtonUsable) PortAdmin.SetCoolDown(role.VestTimer(), CustomGameOptions.VestCd);
        //else PortAdmin.SetCoolDown(0f, CustomGameOptions.VestCd);

        var renderer = PortAdmin.graphic;
            renderer.color = Palette.EnabledColor;
            renderer.material.SetFloat("_Desat", 0f);
    }
}
}