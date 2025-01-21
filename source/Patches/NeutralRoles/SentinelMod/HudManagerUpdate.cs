using System.Collections.Generic;
using System.Linq;
using AmongUs.GameOptions;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.SentinelMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public static class HudManagerUpdate
{
    public static Sprite ChargeSprite => TownOfUsFusion.RampageSprite;
    public static Sprite ChargingSprite => TownOfUsFusion.PoisonedSprite;
    public static Sprite StunSprite => TownOfUsFusion.HackSprite;
    public static Sprite DynamiteSprite => TownOfUsFusion.PlantSprite;
    public static Sprite AwaitSprite => TownOfUsFusion.DetonateSprite;

    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Sentinel)) return;
        var role = Role.GetRole<Sentinel>(PlayerControl.LocalPlayer);
        var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        var truePosition = PlayerControl.LocalPlayer.GetTruePosition();

            var killButton = __instance.KillButton;
        killButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            killButton.SetCoolDown(role.KillTimer(), CustomGameOptions.SentinelKillCd);
            Utils.SetTarget(ref role.ClosestPlayer, killButton, float.NaN);
            var position = killButton.transform.localPosition;

        if (role.ChargeButton == null)
        {
            role.ChargeButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
            role.ChargeButton.graphic.enabled = true;
            role.ChargeButton.gameObject.SetActive(false);
        }
        role.ChargeButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.ChargeButton.transform.localPosition = new Vector3(position.x - 1f, position.y, position.z);
            role.ChargeButton.graphic.sprite = ChargeSprite;
            
        if (role.Charged)
        {
            role.ChargeButton.graphic.sprite = ChargingSprite;
            role.ChargeAttack();
            role.ChargeButton.SetCoolDown(role.ChargeTimeRemaining, CustomGameOptions.ChargeDelay);
            role.Charge.UpdateChargeLocation(role.ChargedPlayer);
        }
        else
        {
            role.ChargeButton.graphic.sprite = ChargeSprite;
            if (!role.ChargeSetOff) role.ChargeKill();
            role.ChargeButton.SetCoolDown(role.ChargeTimer(), CustomGameOptions.SentinelChargeCd);
            Utils.SetTarget(ref role.ClosestPlayer, role.ChargeButton, float.NaN);
            if (PlayerControl.LocalPlayer.killTimer > 0 || role.ClosestPlayer == null)
            {
                role.ChargeButton.graphic.color = Palette.DisabledClear;
                role.ChargeButton.graphic.material.SetFloat("_Desat", 1f);
            }
            else
            {
                role.ChargeButton.graphic.color = Palette.EnabledColor;
                role.ChargeButton.graphic.material.SetFloat("_Desat", 0f);
            }
        }

        role.ChargeButton.graphic.color = Palette.EnabledColor;
        role.ChargeButton.graphic.material.SetFloat("_Desat", 0f);
        if (role.ChargeButton.graphic.sprite == ChargeSprite) role.ChargeButton.SetCoolDown(role.ChargeTimer(), CustomGameOptions.SentinelChargeCd);
        else role.ChargeButton.SetCoolDown(role.ChargeTimeRemaining, CustomGameOptions.ChargeDelay);


        if (role.ChargeText == null && role.ChargeUsesLeft >= 0)
        {
            role.ChargeText = Object.Instantiate(role.ChargeButton.cooldownTimerText, role.ChargeButton.transform);
            role.ChargeText.gameObject.SetActive(false);
            role.ChargeText.transform.localPosition = new Vector3(
                role.ChargeText.transform.localPosition.x + 0.26f,
                role.ChargeText.transform.localPosition.y + 0.29f,
                role.ChargeText.transform.localPosition.z);
            role.ChargeText.transform.localScale = role.ChargeText.transform.localScale * 0.65f;
            role.ChargeText.alignment = TMPro.TextAlignmentOptions.Right;
            role.ChargeText.fontStyle = TMPro.FontStyles.Bold;
        }
        if (role.ChargeText != null)
        {
            role.ChargeText.text = role.ChargeUsesLeft + "";
        }

        role.ChargeText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        if (CustomGameOptions.SentinelVent) {
            if (role.PlaceButton == null)
            {
                role.PlaceButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.PlaceButton.graphic.enabled = true;
                role.PlaceButton.gameObject.SetActive(false);
            }
            role.PlaceButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            if (role.PlaceUsesLeft >= 0) {
                if (role.DynamiteUsed)
                {
                    role.PlaceButton.graphic.sprite = AwaitSprite;
                    //role.DetonateTimer();
                    role.PlaceButton.SetCoolDown(role.PlaceTimer(), CustomGameOptions.SentinelPlaceCd);
                }
                else
                {
                    role.PlaceButton.graphic.sprite = DynamiteSprite;
                    if (PlayerControl.LocalPlayer.killTimer > 0 || !PlayerControl.LocalPlayer.inVent)
                    {
                        role.PlaceButton.graphic.color = Palette.DisabledClear;
                        role.PlaceButton.graphic.material.SetFloat("_Desat", 1f);
                    }
                    else
                    {
                        role.PlaceButton.graphic.color = Palette.EnabledColor;
                        role.PlaceButton.graphic.material.SetFloat("_Desat", 0f);
                    }
                    role.PlaceButton.SetCoolDown(role.PlaceTimer(), CustomGameOptions.SentinelPlaceCd);
                }
            }
            if (role.PlaceText == null && role.PlaceUsesLeft >= 0)
            {
                role.PlaceText = Object.Instantiate(role.PlaceButton.cooldownTimerText, role.PlaceButton.transform);
                role.PlaceText.gameObject.SetActive(false);
                role.PlaceText.transform.localPosition = new Vector3(
                    role.PlaceText.transform.localPosition.x + 0.26f,
                    role.PlaceText.transform.localPosition.y + 0.29f,
                    role.PlaceText.transform.localPosition.z);
                role.PlaceText.transform.localScale = role.PlaceText.transform.localScale * 0.65f;
                role.PlaceText.alignment = TMPro.TextAlignmentOptions.Right;
                role.PlaceText.fontStyle = TMPro.FontStyles.Bold;
            }
            if (role.PlaceText != null)
            {
                role.PlaceText.text = role.PlaceUsesLeft + "";
            }
            role.PlaceText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead && CustomGameOptions.SentinelVent
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

                if (__instance.UseButton != null)
                {
                    role.PlaceButton.transform.position = new Vector3(
                        Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x + 0.75f,
                        __instance.UseButton.transform.position.y, __instance.UseButton.transform.position.z);
                }
                else
                {
                    role.PlaceButton.transform.position = new Vector3(
                        Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x + 0.75f,
                        __instance.PetButton.transform.position.y, __instance.PetButton.transform.position.z);
                }
            role.PlaceButton.graphic.sprite = DynamiteSprite;
        }

        if (role.StunButton == null)
        {
            role.StunButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
            role.StunButton.graphic.enabled = true;
            role.StunButton.gameObject.SetActive(false);
        }
            if (__instance.UseButton != null)
            {
                role.StunButton.transform.position = new Vector3(
                    Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x + 0.75f,
                    __instance.UseButton.transform.position.y + 1f, __instance.UseButton.transform.position.z);
            }
            else
            {
                role.StunButton.transform.position = new Vector3(
                    Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x + 0.75f,
                    __instance.PetButton.transform.position.y + 1f, __instance.PetButton.transform.position.z);
            }

        role.StunButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.StunButton.graphic.sprite = StunSprite;

            role.StunButton.SetCoolDown(role.StunTimer(), CustomGameOptions.SentinelStunCd);

            role.StunButton.SetTarget(null);
            //role.StunnedPlayer = null;

            if (role.StunButton.isActiveAndEnabled)
            {
                Utils.SetTarget(ref role.ClosestPlayer, role.StunButton, float.NaN);
                //role.StunnedPlayer = role.ClosestPlayer;
            }

            if ( role.ClosestPlayer != null)
            {
                        role.StunButton.graphic.color = Palette.EnabledColor;
                        role.StunButton.graphic.material.SetFloat("_Desat", 0f);
                //if (Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ToU hack")) role.StunButton.DoClick();
            }

        if (role.StunText == null && role.StunUsesLeft >= 0)
        {
            role.StunText = Object.Instantiate(role.StunButton.cooldownTimerText, role.StunButton.transform);
            role.StunText.gameObject.SetActive(false);
            role.StunText.transform.localPosition = new Vector3(
                role.StunText.transform.localPosition.x + 0.26f,
                role.StunText.transform.localPosition.y + 0.29f,
                role.StunText.transform.localPosition.z);
            role.StunText.transform.localScale = role.StunText.transform.localScale * 0.65f;
            role.StunText.alignment = TMPro.TextAlignmentOptions.Right;
            role.StunText.fontStyle = TMPro.FontStyles.Bold;
        }
        if (role.StunText != null)
        {
            role.StunText.text = role.StunUsesLeft + "";
        }
        role.StunText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);


    }
}
}
