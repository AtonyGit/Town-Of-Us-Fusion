using HarmonyLib;
using System;
using System.Linq;
using TownOfUsFusion.Roles;
using UnityEngine;
using TownOfUsFusion.Modifiers.UnderdogMod;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles.Modifiers;

namespace TownOfUsFusion.NeutralRoles.SerialKillerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static Sprite Sprite => TownOfUsFusion.Arrow;
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.SerialKiller)) return;
            var role = Role.GetRole<SerialKiller>(PlayerControl.LocalPlayer);

            __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            __instance.KillButton.SetCoolDown(role.KillTimer(), CustomGameOptions.SkKillCooldown);

            /*__instance.KillButton.usesRemainingSprite.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started && role.Scavenging);
            __instance.KillButton.usesRemainingText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started && role.Scavenging);*/
            if (role.DummyButton == null)
            {
                role.DummyButton = UnityEngine.Object.Instantiate(__instance.AbilityButton, __instance.AbilityButton.transform.parent);
                role.DummyButton.graphic.enabled = false;
                role.DummyButton.buttonLabelText.enabled = false;
                role.DummyButton.cooldownTimerText.enabled = false;
                role.DummyButton.gameObject.SetActive(false);
            }
                role.DummyButton.canInteract = false;
            role.DummyButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started && role.Scavenging);
            role.DummyButton.transform.localPosition = __instance.KillButton.transform.localPosition;
            role.DummyButton.SetUsesRemaining(Convert.ToInt32(Math.Round(role.BloodlustTimer())));
            role.DummyButton.usesRemainingText.text = Convert.ToInt32(Math.Round(role.BloodlustTimer())).ToString();
/*
            if (role.BloodlustCooldown == null)
            {
                role.BloodlustCooldown = UnityEngine.Object.Instantiate(__instance.KillButton.cooldownTimerText, __instance.KillButton.transform);
                role.BloodlustCooldown.gameObject.SetActive(false);
                role.BloodlustCooldown.transform.localPosition = new Vector3(
                    role.BloodlustCooldown.transform.localPosition.x + 0.26f,
                    role.BloodlustCooldown.transform.localPosition.y + 0.29f,
                    role.BloodlustCooldown.transform.localPosition.z);
                role.BloodlustCooldown.transform.localScale *= 0.65f;
                role.BloodlustCooldown.alignment = TMPro.TextAlignmentOptions.Right;
                role.BloodlustCooldown.fontStyle = TMPro.FontStyles.Bold;
                role.BloodlustCooldown.enableWordWrapping = false;
            }
            if (role.BloodlustCooldown != null)
            {
                role.BloodlustCooldown.text = Convert.ToInt32(Math.Round(role.BloodlustTimer())).ToString();
            }
            role.BloodlustCooldown.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started && role.Scavenging);*/

                if ((CamouflageUnCamouflage.IsCamoed && CustomGameOptions.CamoCommsKillAnyone) || PlayerControl.LocalPlayer.IsHypnotised()) Utils.SetTarget(ref role.ClosestPlayer, __instance.KillButton);
                else if (role.Player.IsLover()) Utils.SetTarget(ref role.ClosestPlayer, __instance.KillButton, float.NaN, PlayerControl.AllPlayerControls.ToArray().Where(x => !x.IsLover()).ToList());
                else Utils.SetTarget(ref role.ClosestPlayer, __instance.KillButton);
/*
            if (role.Scavenging && PlayerControl.LocalPlayer.moveable && __instance.KillButton.currentTarget != null)
            {
                role.BloodlustCooldown.color = Palette.EnabledColor;
                role.BloodlustCooldown.material.SetFloat("_Desat", 0f);
            }
            else
            {
                role.BloodlustCooldown.color = Palette.DisabledClear;
                role.BloodlustCooldown.material.SetFloat("_Desat", 1f);
            }*/

            if ((role.BloodlustTimer() == 0f || MeetingHud.Instance || PlayerControl.LocalPlayer.Data.IsDead) && role.Scavenging)
            {
                role.StopBloodlust();
                
                __instance.KillButton.SetCoolDown(role.KillTimer(), CustomGameOptions.SkKillCooldown);
            }

            if (!role.GameStarted && PlayerControl.LocalPlayer.killTimer > 0f) role.GameStarted = true;

            if (PlayerControl.LocalPlayer.killTimer == 0f && !role.Scavenging && role.GameStarted && !PlayerControl.LocalPlayer.Data.IsDead)
            {
                role.Scavenging = true;
                role.BloodlustEnd = DateTime.UtcNow.AddSeconds(CustomGameOptions.BloodlustDuration);
                role.Target = role.GetClosestPlayer();
                role.RegenTask();
            }

            if (role.Target != null)
            {
                if (role.PreyArrow == null)
                {
                    var gameObj = new GameObject();
                    var arrow = gameObj.AddComponent<ArrowBehaviour>();
                    gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                    var renderer = gameObj.AddComponent<SpriteRenderer>();
                    renderer.sprite = Sprite;
                    renderer.color = Colors.SerialKiller;
                    arrow.image = renderer;
                    gameObj.layer = 5;
                    arrow.target = role.Target.transform.position;
                    role.PreyArrow = arrow;
                }
                role.PreyArrow.target = role.Target.transform.position;
            }

            if (!PlayerControl.LocalPlayer.IsHypnotised())
            {
                if (role.Target != null && !role.Target.Data.IsDead && !role.Target.Data.Disconnected)
                {
                    if (role.Target.GetCustomOutfitType() != CustomPlayerOutfitType.Camouflage &&
                        role.Target.GetCustomOutfitType() != CustomPlayerOutfitType.Swooper)
                    {
                        var colour = new Color(0.45f, 0f, 0f);
                        if (role.Target.Is(ModifierEnum.Shy)) colour.a = Modifier.GetModifier<Shy>(role.Target).Opacity;
                        role.Target.nameText().color = colour;
                    }
                    else role.Target.nameText().color = Color.clear;
                }
            }
        }
    }
}