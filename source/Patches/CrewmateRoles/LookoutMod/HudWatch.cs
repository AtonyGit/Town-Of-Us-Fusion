using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.LookoutMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudTrack
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Lookout)) return;
            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var watchButton = __instance.KillButton;

            var role = Role.GetRole<Lookout>(PlayerControl.LocalPlayer);

            if (role.UsesText == null && role.UsesLeft > 0)
            {
                role.UsesText = Object.Instantiate(watchButton.cooldownTimerText, watchButton.transform);
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

            if (role.PerceptButton == null)
            {
                role.PerceptButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.PerceptButton.graphic.enabled = true;
                role.PerceptButton.gameObject.SetActive(false);
            }

            role.PerceptButton.graphic.sprite = TownOfUsFusion.PerceptSprite;
            role.PerceptButton.buttonLabelText.text = "Eagle Eye";
            role.PerceptButton.buttonLabelText.SetOutlineColor(role.Color);
            role.PerceptButton.transform.localPosition = new Vector3(-2f, 0f, 0f);

            role.PerceptButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            if (role.PerceptUsesText == null && role.PerceptUsesLeft > 0)
            {
                role.PerceptUsesText = Object.Instantiate(role.PerceptButton.cooldownTimerText, role.PerceptButton.transform);
                role.PerceptUsesText.gameObject.SetActive(false);
                role.PerceptUsesText.transform.localPosition = new Vector3(
                    role.PerceptUsesText.transform.localPosition.x + 0.26f,
                    role.PerceptUsesText.transform.localPosition.y + 0.29f,
                    role.PerceptUsesText.transform.localPosition.z);
                role.PerceptUsesText.transform.localScale = role.PerceptUsesText.transform.localScale * 0.65f;
                role.PerceptUsesText.alignment = TMPro.TextAlignmentOptions.Right;
                role.PerceptUsesText.fontStyle = TMPro.FontStyles.Bold;
            }
            if (role.PerceptUsesText != null)
            {
                role.PerceptUsesText.text = role.PerceptUsesLeft + "";
            }
            role.PerceptUsesText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            watchButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.UsesText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
                    
            if (role.Percepting)
            {
                role.PerceptButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.PerceptDuration);
                role.PerceptButton.graphic.color = Palette.EnabledColor;
                role.PerceptButton.graphic.material.SetFloat("_Desat", 0f);
            }
            else
            {
                role.PerceptButton.SetCoolDown(role.PerceptTimer(), CustomGameOptions.PerceptCd);

                if (role.PerceptTimer() > 0f || !PlayerControl.LocalPlayer.moveable)
                {
                    role.PerceptButton.graphic.color = Palette.DisabledClear;
                    role.PerceptButton.graphic.material.SetFloat("_Desat", 1f);
                }
                else
                {
                    role.PerceptButton.graphic.color = Palette.EnabledColor;
                    role.PerceptButton.graphic.material.SetFloat("_Desat", 0f);
                }
            }

            if (role.ButtonUsable) watchButton.SetCoolDown(role.WatchTimer(), CustomGameOptions.WatchCooldown);
            else watchButton.SetCoolDown(0f, CustomGameOptions.WatchCooldown);
            if (role.UsesLeft != 0) {
                var notWatching = PlayerControl.AllPlayerControls
                    .ToArray()
                    .Where(x => !role.Watching.ContainsKey(x.PlayerId))
                    .ToList();

                Utils.SetTarget(ref role.ClosestPlayer, watchButton, float.NaN, notWatching);

                var renderer = watchButton.graphic;
                if (role.ClosestPlayer != null && role.ButtonUsable && PlayerControl.LocalPlayer.moveable)
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
}