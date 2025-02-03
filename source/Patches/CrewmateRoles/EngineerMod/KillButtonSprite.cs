using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.EngineerMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class KillButtonSprite
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Engineer)) return;

            var role = Role.GetRole<Engineer>(PlayerControl.LocalPlayer);


            __instance.KillButton.SetCoolDown(0f, 10f);
            __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            /*__instance.KillButton.usesRemainingSprite.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            __instance.KillButton.usesRemainingText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
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
            role.DummyButton.transform.localPosition = __instance.KillButton.transform.localPosition;
                role.DummyButton.SetUsesRemaining(role.UsesLeft);
                role.DummyButton.usesRemainingText.text = role.UsesLeft.ToString();

            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (!ShipStatus.Instance) return;
            var system = ShipStatus.Instance.Systems[SystemTypes.Sabotage].Cast<SabotageSystemType>();
            if (system == null) return;
            var sabActive = system.AnyActive;
            var renderer = __instance.KillButton.graphic;
            if (sabActive & role.ButtonUsable & __instance.KillButton.enabled && PlayerControl.LocalPlayer.moveable)
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                __instance.KillButton.buttonLabelText.color = Palette.EnabledColor;
                __instance.KillButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                return;
            }

            renderer.color = Palette.DisabledClear;
            renderer.material.SetFloat("_Desat", 1f);
            __instance.KillButton.buttonLabelText.color = Palette.DisabledClear;
            __instance.KillButton.buttonLabelText.material.SetFloat("_Desat", 1f);
        }
    }
}