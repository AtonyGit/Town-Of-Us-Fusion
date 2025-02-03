﻿using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.TrapperMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudTrap
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            UpdateTrapButton(__instance);
        }

        public static void UpdateTrapButton(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Trapper)) return;
            var trapButton = __instance.KillButton;

            var role = Role.GetRole<Trapper>(PlayerControl.LocalPlayer);

            trapButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            if (role.ButtonUsable) trapButton.SetCoolDown(role.TrapTimer(), CustomGameOptions.TrapCooldown);
            else trapButton.SetCoolDown(0f, CustomGameOptions.TrapCooldown);

            trapButton.usesRemainingSprite.gameObject.SetActive(true);
            trapButton.usesRemainingText.gameObject.SetActive(true);
            trapButton.usesRemainingText.text = role.UsesLeft.ToString();

            var renderer = trapButton.graphic;
            if (!trapButton.isCoolingDown && role.ButtonUsable && PlayerControl.LocalPlayer.moveable)
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                trapButton.buttonLabelText.color = Palette.EnabledColor;
                trapButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                trapButton.usesRemainingSprite.color = Palette.EnabledColor;
                trapButton.usesRemainingSprite.material.SetFloat("_Desat", 0f);
                trapButton.usesRemainingText.color = Palette.EnabledColor;
                trapButton.usesRemainingText.material.SetFloat("_Desat", 0f);
                return;
            }

            renderer.color = Palette.DisabledClear;
            renderer.material.SetFloat("_Desat", 1f);
            trapButton.buttonLabelText.color = Palette.DisabledClear;
            trapButton.buttonLabelText.material.SetFloat("_Desat", 1f);
            trapButton.usesRemainingSprite.color = Palette.DisabledClear;
            trapButton.usesRemainingSprite.material.SetFloat("_Desat", 1f);
            trapButton.usesRemainingText.color = Palette.DisabledClear;
            trapButton.usesRemainingText.material.SetFloat("_Desat", 1f);
        }
    }
}
