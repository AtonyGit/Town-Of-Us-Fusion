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
    public static Sprite BittenSprite => TownOfUsFusion.BittenSprite;
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

        Utils.SetTarget(ref role.ClosestPlayer, biteButton, float.NaN, notVampire);

        var renderer = biteButton.graphic;

        if (role.ClosestPlayer != null)
        {
            renderer.color = Palette.EnabledColor;
            renderer.material.SetFloat("_Desat", 0f);
        }
        else
        {
            renderer.color = Palette.DisabledClear;
            renderer.material.SetFloat("_Desat", 1f);
        }
        
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
                    }
                    else
                    {
                        biteButton.graphic.color = Palette.DisabledClear;
                        biteButton.graphic.material.SetFloat("_Desat", 1f);
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
