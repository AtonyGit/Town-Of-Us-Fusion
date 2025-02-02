﻿using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.ImpostorRoles.MorphlingMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static Sprite SampleSprite => TownOfUsFusion.SampleSprite;
        public static Sprite MorphSprite => TownOfUsFusion.MorphSprite;


        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Morphling)) return;
            var role = Role.GetRole<Morphling>(PlayerControl.LocalPlayer);
            if (role.MorphButton == null)
            {
                role.MorphButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.MorphButton.graphic.enabled = true;
                role.MorphButton.graphic.sprite = SampleSprite;
                role.MorphButton.gameObject.SetActive(false);
            }

            if (role.MorphButton.graphic.sprite != SampleSprite && role.MorphButton.graphic.sprite != MorphSprite)
                role.MorphButton.graphic.sprite = SampleSprite;

            if (PlayerControl.LocalPlayer.Data.IsDead) role.MorphButton.SetTarget(null);

            role.MorphButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            if (role.MorphButton.graphic.sprite == SampleSprite)
            {
                role.MorphButton.buttonLabelText.text = "Sample";
                role.MorphButton.SetCoolDown(0f, 1f);
                if (PlayerControl.LocalPlayer.moveable) Utils.SetTarget(ref role.ClosestPlayer, role.MorphButton);
                else role.MorphButton.SetTarget(null);
            }
            else
            {
                if (role.Morphed)
                {
                    role.MorphButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.MorphlingDuration) ;
                    role.MorphButton.graphic.color = Palette.EnabledColor;
                    role.MorphButton.graphic.material.SetFloat("_Desat", 0f);
                    role.MorphButton.buttonLabelText.color = Palette.EnabledColor;
                    role.MorphButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                }
                else if (PlayerControl.LocalPlayer.moveable && role.MorphTimer() == 0f)
                {
                    role.MorphButton.SetCoolDown(role.MorphTimer(), CustomGameOptions.MorphlingCd);
                    role.MorphButton.graphic.color = Palette.EnabledColor;
                    role.MorphButton.graphic.material.SetFloat("_Desat", 0f);
                    role.MorphButton.buttonLabelText.color = Palette.EnabledColor;
                    role.MorphButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                }
                else
                {
                    role.MorphButton.SetCoolDown(role.MorphTimer(), CustomGameOptions.MorphlingCd);
                    role.MorphButton.graphic.color = Palette.DisabledClear;
                    role.MorphButton.graphic.material.SetFloat("_Desat", 1f);
                    role.MorphButton.buttonLabelText.color = Palette.DisabledClear;
                    role.MorphButton.buttonLabelText.material.SetFloat("_Desat", 1f);
                }
            }
        }
    }
}
