using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.GuardianAngelMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudManagerUpdate
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            UpdateProtectButton(__instance);
        }

        public static void UpdateProtectButton(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel)) return;
            var protectButton = __instance.KillButton;

            var role = Role.GetRole<GuardianAngel>(PlayerControl.LocalPlayer);

            protectButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            /*protectButton.usesRemainingSprite.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            protectButton.usesRemainingText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);*/
            if (role.DummyButton == null)
            {
                role.DummyButton = Object.Instantiate(__instance.AbilityButton, __instance.AbilityButton.transform.parent);
                role.DummyButton.graphic.enabled = false;
                role.DummyButton.buttonLabelText.enabled = false;
                role.DummyButton.cooldownTimerText.enabled = false;
                role.DummyButton.gameObject.SetActive(false);
                role.DummyButton.canInteract = false;
            }
            role.DummyButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.DummyButton.transform.localPosition = protectButton.transform.localPosition;
            role.DummyButton.SetUsesRemaining(role.UsesLeft);
            role.DummyButton.usesRemainingText.text = role.UsesLeft.ToString();

            if (role.Protecting) protectButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.ProtectDuration);
            else if (role.ButtonUsable) protectButton.SetCoolDown(role.ProtectTimer(), CustomGameOptions.ProtectCd);
            else protectButton.SetCoolDown(0f, CustomGameOptions.ProtectCd);

            var renderer = protectButton.graphic;
            if (role.Protecting || (!protectButton.isCoolingDown && role.ButtonUsable && PlayerControl.LocalPlayer.moveable))
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                protectButton.buttonLabelText.color = Palette.EnabledColor;
                protectButton.buttonLabelText.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
                protectButton.buttonLabelText.color = Palette.DisabledClear;
                protectButton.buttonLabelText.material.SetFloat("_Desat", 1f);
            }
        }
    }
}