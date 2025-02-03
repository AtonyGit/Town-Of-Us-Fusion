using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.TrackerMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudTrack
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            UpdateTrackButton(__instance);
        }

        public static void UpdateTrackButton(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Tracker)) return;
            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var trackButton = __instance.KillButton;

            var role = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);


            trackButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            /*trackButton.usesRemainingSprite.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            trackButton.usesRemainingText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);*/
            if (role.DummyButton == null)
            {
                role.DummyButton = Object.Instantiate(__instance.AbilityButton, __instance.AbilityButton.transform.parent);
                role.DummyButton.graphic.enabled = false;
                role.DummyButton.buttonLabelText.enabled = false;
                role.DummyButton.cooldownTimerText.enabled = false;
                role.DummyButton.gameObject.SetActive(false);
            }
            role.DummyButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.DummyButton.transform.localPosition = trackButton.transform.localPosition;
            role.DummyButton.SetUsesRemaining(role.UsesLeft);
            role.DummyButton.usesRemainingText.text = role.UsesLeft.ToString();

            if (role.ButtonUsable) trackButton.SetCoolDown(role.TrackerTimer(), CustomGameOptions.TrackCd);
            else trackButton.SetCoolDown(0f, CustomGameOptions.TrackCd);
            if (role.UsesLeft == 0) return;

            var notTracked = PlayerControl.AllPlayerControls
                .ToArray()
                .Where(x => !role.IsTracking(x))
                .ToList();

            Utils.SetTarget(ref role.ClosestPlayer, trackButton, float.NaN, notTracked);

            var renderer = trackButton.graphic;
            if (role.ClosestPlayer != null && role.ButtonUsable && PlayerControl.LocalPlayer.moveable)
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                trackButton.buttonLabelText.color = Palette.EnabledColor;
                trackButton.buttonLabelText.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
                trackButton.buttonLabelText.color = Palette.DisabledClear;
                trackButton.buttonLabelText.material.SetFloat("_Desat", 1f);
            }
        }
    }
}