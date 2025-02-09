using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.TransporterMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HUDTransport
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Transporter)) return;
            var data = PlayerControl.LocalPlayer.Data;
            var transportButton = __instance.KillButton;

            var role = Role.GetRole<Transporter>(PlayerControl.LocalPlayer);


            transportButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            /*transportButton.usesRemainingSprite.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            transportButton.usesRemainingText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);*/
            if (role.DummyButton == null)
            {
                role.DummyButton = Object.Instantiate(__instance.AbilityButton, __instance.AbilityButton.transform.parent);
                role.DummyButton.graphic.enabled = false;
                role.DummyButton.glyph.enabled = false;
                role.DummyButton.buttonLabelText.enabled = false;
                role.DummyButton.cooldownTimerText.enabled = false;
                role.DummyButton.gameObject.SetActive(false);
            }
                role.DummyButton.canInteract = false;
            role.DummyButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.DummyButton.transform.localPosition = transportButton.transform.localPosition + new Vector3(5f, 0f, 0f);
            role.DummyButton.usesRemainingText.transform.localPosition -= new Vector3(5f, 0f, 0f);
            role.DummyButton.usesRemainingSprite.transform.localPosition -= new Vector3(5f, 0f, 0f);
            role.DummyButton.SetUsesRemaining(role.UsesLeft);
            role.DummyButton.usesRemainingText.text = role.UsesLeft.ToString();

            if (data.IsDead) return;

            if (role.ButtonUsable) transportButton.SetCoolDown(role.TransportTimer(), CustomGameOptions.TransportCooldown);
            else transportButton.SetCoolDown(0f, CustomGameOptions.TransportCooldown);

            var renderer = transportButton.graphic;
            if (!transportButton.isCoolingDown && role.ButtonUsable && PlayerControl.LocalPlayer.moveable)
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                transportButton.buttonLabelText.color = Palette.EnabledColor;
                transportButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                return;
            }

            renderer.color = Palette.DisabledClear;
            renderer.material.SetFloat("_Desat", 1f);
            transportButton.buttonLabelText.color = Palette.DisabledClear;
            transportButton.buttonLabelText.material.SetFloat("_Desat", 1f);
        }
    }
}