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

                
            watchButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

                watchButton.usesRemainingSprite.gameObject.SetActive(true);
                watchButton.usesRemainingText.gameObject.SetActive(true);
                watchButton.usesRemainingText.text = role.UsesLeft.ToString();
                
                role.PerceptButton.usesRemainingSprite.gameObject.SetActive(true);
                role.PerceptButton.usesRemainingText.gameObject.SetActive(true);
                role.PerceptButton.usesRemainingText.text = role.PerceptUsesLeft.ToString();

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
                    role.PerceptButton.buttonLabelText.color = Palette.EnabledColor;
                    role.PerceptButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                    role.PerceptButton.usesRemainingSprite.color = Palette.EnabledColor;
                    role.PerceptButton.usesRemainingSprite.material.SetFloat("_Desat", 0f);
                    role.PerceptButton.usesRemainingText.color = Palette.EnabledColor;
                    role.PerceptButton.usesRemainingText.material.SetFloat("_Desat", 0f);
                }
                else
                {
                    role.PerceptButton.graphic.color = Palette.EnabledColor;
                    role.PerceptButton.graphic.material.SetFloat("_Desat", 0f);
                    role.PerceptButton.buttonLabelText.color = Palette.DisabledClear;
                    role.PerceptButton.buttonLabelText.material.SetFloat("_Desat", 1f);
                    role.PerceptButton.usesRemainingSprite.color = Palette.DisabledClear;
                    role.PerceptButton.usesRemainingSprite.material.SetFloat("_Desat", 1f);
                    role.PerceptButton.usesRemainingText.color = Palette.DisabledClear;
                    role.PerceptButton.usesRemainingText.material.SetFloat("_Desat", 1f);
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
                    watchButton.buttonLabelText.color = Palette.EnabledColor;
                    watchButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                    watchButton.usesRemainingSprite.color = Palette.EnabledColor;
                    watchButton.usesRemainingSprite.material.SetFloat("_Desat", 0f);
                    watchButton.usesRemainingText.color = Palette.EnabledColor;
                    watchButton.usesRemainingText.material.SetFloat("_Desat", 0f);
                }
                else
                {
                    renderer.color = Palette.DisabledClear;
                    renderer.material.SetFloat("_Desat", 1f);
                    watchButton.buttonLabelText.color = Palette.DisabledClear;
                    watchButton.buttonLabelText.material.SetFloat("_Desat", 1f);
                    watchButton.usesRemainingSprite.color = Palette.DisabledClear;
                    watchButton.usesRemainingSprite.material.SetFloat("_Desat", 1f);
                    watchButton.usesRemainingText.color = Palette.DisabledClear;
                    watchButton.usesRemainingText.material.SetFloat("_Desat", 1f);
                }
            }
        }
    }
}