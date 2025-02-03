using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.SurvivorMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            UpdateVestButton(__instance);
        }

        public static void UpdateVestButton(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Survivor)) return;
            var vestButton = __instance.KillButton;

            var role = Role.GetRole<Survivor>(PlayerControl.LocalPlayer);

            vestButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            /*vestButton.usesRemainingSprite.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            vestButton.usesRemainingText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);*/
            vestButton.SetUsesRemaining(role.UsesLeft);
            vestButton.usesRemainingText.text = role.UsesLeft.ToString();

            if (role.Vesting) vestButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.VestDuration);
            else if (role.ButtonUsable) vestButton.SetCoolDown(role.VestTimer(), CustomGameOptions.VestCd);
            else vestButton.SetCoolDown(0f, CustomGameOptions.VestCd);

            var renderer = vestButton.graphic;
            if (role.Vesting || (!vestButton.isCoolingDown && role.ButtonUsable && PlayerControl.LocalPlayer.moveable))
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                vestButton.buttonLabelText.color = Palette.EnabledColor;
                vestButton.buttonLabelText.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
                vestButton.buttonLabelText.color = Palette.DisabledClear;
                vestButton.buttonLabelText.material.SetFloat("_Desat", 1f);
            }
        }
    }
}