using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.VampireMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudBite
    {
    public static Sprite BiteSprite => TownOfUsFusion.BiteSprite;
    public static Sprite BittenSprite => TownOfUsFusion.BitSprite;
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Vampire)) return;
            var biteButton = __instance.KillButton;

            var role = Role.GetRole<Vampire>(PlayerControl.LocalPlayer);

            biteButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            biteButton.SetCoolDown(role.BiteTimer(), CustomGameOptions.BiteCd);

            var notVampire = PlayerControl.AllPlayerControls
                .ToArray()
                .Where(x => !x.Is(RoleEnum.Vampire))
                .ToList();

            var notVampireOrLover = PlayerControl.AllPlayerControls
                .ToArray()
                .Where(x => !x.Is(RoleEnum.Vampire) && !x.IsLover())
                .ToList();

            if ((CamouflageUnCamouflage.IsCamoed && CustomGameOptions.CamoCommsKillAnyone) || PlayerControl.LocalPlayer.IsHypnotised()) Utils.SetTarget(ref role.ClosestPlayer, biteButton);
            else if (PlayerControl.LocalPlayer.IsLover() && CustomGameOptions.ImpLoverKillTeammate) Utils.SetTarget(ref role.ClosestPlayer, biteButton, float.NaN, PlayerControl.AllPlayerControls.ToArray().Where(x => !x.IsLover()).ToList());
            else if (PlayerControl.LocalPlayer.IsLover()) Utils.SetTarget(ref role.ClosestPlayer, biteButton, float.NaN, notVampireOrLover);
            else Utils.SetTarget(ref role.ClosestPlayer, biteButton, float.NaN, notVampire);

            try
            {
                if (role.Bitten)
                {
                //    role.Player.SetKillTimer(GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown/* + CustomGameOptions.PoisonDuration*/);
                    biteButton.graphic.sprite = BittenSprite;
                    role.BiteThingy();
                    biteButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.BiteDuration);
                }
                else
                {
                    biteButton.graphic.sprite = BiteSprite;
                    if (role.BittenPlayer && role.BittenPlayer != PlayerControl.LocalPlayer)
                    {
                        role.BiteKill();
                    }
                    if (role.ClosestPlayer != null)
                    {
                        biteButton.graphic.color = Palette.EnabledColor;
                        biteButton.graphic.material.SetFloat("_Desat", 0f);
                        biteButton.buttonLabelText.color = Palette.EnabledColor;
                        biteButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                    }
                    else
                    {
                        biteButton.graphic.color = Palette.DisabledClear;
                        biteButton.graphic.material.SetFloat("_Desat", 1f);
                        biteButton.buttonLabelText.color = Palette.DisabledClear;
                        biteButton.buttonLabelText.material.SetFloat("_Desat", 1f);
                    }
                    biteButton.SetCoolDown(role.BiteTimer(), CustomGameOptions.BiteCd);
                    role.BittenPlayer = PlayerControl.LocalPlayer; //Only do this to stop repeatedly trying to re-kill Bitten player. null didn't work for some reason
                }
            }
            catch
            {

            }
        }
    }
}
