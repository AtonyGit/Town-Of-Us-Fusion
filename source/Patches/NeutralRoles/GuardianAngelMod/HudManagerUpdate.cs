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

            protectButton.usesRemainingSprite.gameObject.SetActive(true);
            protectButton.usesRemainingText.gameObject.SetActive(true);
            protectButton.usesRemainingText.text = role.UsesLeft.ToString();
            
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
                protectButton.usesRemainingSprite.color = Palette.EnabledColor;
                protectButton.usesRemainingSprite.material.SetFloat("_Desat", 0f);
                protectButton.usesRemainingText.color = Palette.EnabledColor;
                protectButton.usesRemainingText.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
                protectButton.buttonLabelText.color = Palette.DisabledClear;
                protectButton.buttonLabelText.material.SetFloat("_Desat", 1f);
                protectButton.usesRemainingSprite.color = Palette.DisabledClear;
                protectButton.usesRemainingSprite.material.SetFloat("_Desat", 1f);
                protectButton.usesRemainingText.color = Palette.DisabledClear;
                protectButton.usesRemainingText.material.SetFloat("_Desat", 1f);
            }
        }
    }
}