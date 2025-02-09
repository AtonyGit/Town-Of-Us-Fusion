using HarmonyLib;
using System.Linq;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.MirrorMasterMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HUDMercShield
    {

        private static Sprite AbsorbSprite => TownOfUsFusion.MirrorAbsorbSprite;

        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            UpdateButtons(__instance);
        }

        public static void UpdateButtons(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.MirrorMaster)) return;

            var role = Role.GetRole<MirrorMaster>(PlayerControl.LocalPlayer);
            var unleashButton = __instance.KillButton;
            if (role.AbsorbButton == null)
            {
                role.AbsorbButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.AbsorbButton.graphic.enabled = true;
                role.AbsorbButton.graphic.sprite = AbsorbSprite;
                role.AbsorbButton.gameObject.SetActive(false);
            }
            role.AbsorbButton.transform.localPosition = new Vector3(-2f, 0f, 0f);
            role.AbsorbButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.AbsorbButton.buttonLabelText.text = "Absorb";
            role.AbsorbButton.buttonLabelText.SetOutlineColor(role.Color);

            if (role.DummyAbsorbButton == null)
            {
                role.DummyAbsorbButton = Object.Instantiate(__instance.AbilityButton, __instance.AbilityButton.transform.parent);
                role.DummyAbsorbButton.graphic.enabled = false;
                role.DummyAbsorbButton.buttonLabelText.enabled = false;
                role.DummyAbsorbButton.cooldownTimerText.enabled = false;
                role.DummyAbsorbButton.gameObject.SetActive(false);
            }
                role.DummyAbsorbButton.canInteract = false;
            role.DummyAbsorbButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
                role.DummyAbsorbButton.transform.localPosition = role.AbsorbButton.transform.localPosition;

                role.DummyAbsorbButton.SetUsesRemaining(role.AbsorbUsesLeft);
                role.DummyAbsorbButton.usesRemainingText.text = role.AbsorbUsesLeft.ToString();
                
            if (role.DummyUnleashButton == null)
            {
                role.DummyUnleashButton = Object.Instantiate(__instance.AbilityButton, __instance.AbilityButton.transform.parent);
                role.DummyUnleashButton.graphic.enabled = false;
                role.DummyUnleashButton.buttonLabelText.enabled = false;
                role.DummyUnleashButton.cooldownTimerText.enabled = false;
                role.DummyUnleashButton.gameObject.SetActive(false);
            }
                role.DummyUnleashButton.canInteract = false;
            role.DummyUnleashButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
                role.DummyUnleashButton.transform.localPosition = unleashButton.transform.localPosition;

                role.DummyUnleashButton.SetUsesRemaining(role.UnleashUsesLeft);
                role.DummyUnleashButton.usesRemainingText.text = role.UnleashUsesLeft.ToString();


            var renderer = role.AbsorbButton.graphic;
            if (role.Absorbed || (!role.AbsorbButton.isCoolingDown && role.AbsorbUsesLeft > 0))
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                role.AbsorbButton.buttonLabelText.color = Palette.EnabledColor;
                role.AbsorbButton.buttonLabelText.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
                role.AbsorbButton.buttonLabelText.color = Palette.DisabledClear;
                role.AbsorbButton.buttonLabelText.material.SetFloat("_Desat", 1f);
            }
            var renderer2 = unleashButton.graphic;
            if (!unleashButton.isCoolingDown && role.UnleashUsesLeft > 0)
            {
                renderer2.color = Palette.EnabledColor;
                renderer2.material.SetFloat("_Desat", 0f);
                unleashButton.buttonLabelText.color = Palette.EnabledColor;
                unleashButton.buttonLabelText.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer2.color = Palette.DisabledClear;
                renderer2.material.SetFloat("_Desat", 1f);
                unleashButton.buttonLabelText.color = Palette.DisabledClear;
                unleashButton.buttonLabelText.material.SetFloat("_Desat", 1f);
            }

            unleashButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            unleashButton.SetCoolDown(role.UnleashTimer(), CustomGameOptions.MirrorUnleashCd);
            if(role.UnleashUsesLeft != 0) Utils.SetTarget(ref role.ClosestPlayer, unleashButton);
            
            if (role.Absorbed) return;
            var notShielded = PlayerControl.AllPlayerControls.ToArray().Where(
                player => role.ShieldedPlayer != player
            ).ToList();
            if(role.AbsorbUsesLeft != 0) Utils.SetTarget(ref role.ClosestPlayer, role.AbsorbButton, float.NaN, notShielded);

        }
    }
}
