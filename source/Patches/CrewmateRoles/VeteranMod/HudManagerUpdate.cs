using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.VeteranMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudManagerUpdate
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            UpdateAlertButton(__instance);
        }

        public static void UpdateAlertButton(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Veteran)) return;
            var alertButton = __instance.KillButton;

            var role = Role.GetRole<Veteran>(PlayerControl.LocalPlayer);

            alertButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            alertButton.usesRemainingSprite.gameObject.SetActive(true);
            alertButton.usesRemainingText.gameObject.SetActive(true);
            alertButton.usesRemainingText.text = role.UsesLeft.ToString();

            if (role.OnAlert) alertButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.AlertDuration);
            else if (role.ButtonUsable) alertButton.SetCoolDown(role.AlertTimer(), CustomGameOptions.AlertCd);
            else alertButton.SetCoolDown(0f, CustomGameOptions.AlertCd);

            var renderer = alertButton.graphic;
            if (role.OnAlert || (!alertButton.isCoolingDown && role.ButtonUsable && PlayerControl.LocalPlayer.moveable))
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                alertButton.buttonLabelText.color = Palette.EnabledColor;
                alertButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                alertButton.usesRemainingSprite.color = Palette.EnabledColor;
                alertButton.usesRemainingSprite.material.SetFloat("_Desat", 0f);
                alertButton.usesRemainingText.color = Palette.EnabledColor;
                alertButton.usesRemainingText.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
                alertButton.buttonLabelText.color = Palette.DisabledClear;
                alertButton.buttonLabelText.material.SetFloat("_Desat", 1f);
                alertButton.usesRemainingSprite.color = Palette.DisabledClear;
                alertButton.usesRemainingSprite.material.SetFloat("_Desat", 1f);
                alertButton.usesRemainingText.color = Palette.DisabledClear;
                alertButton.usesRemainingText.material.SetFloat("_Desat", 1f);
            }
        }
    }
}