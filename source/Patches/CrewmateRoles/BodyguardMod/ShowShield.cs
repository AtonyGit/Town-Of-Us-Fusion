using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{
    public enum GuardOptions
{
    Self = 0,
    Bodyguard = 1,
    SelfAndBodyguard = 2,
    Everyone = 3
}

[HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class ShowShield
{
    public static Color ProtectedColor = Color.cyan;

    public static void Postfix(HudManager __instance)
    {
        foreach (var role in Role.GetRoles(RoleEnum.Bodyguard))
        {
            var bg = (Bodyguard)role;

            var exPlayer = bg.exGuarded;
            if (exPlayer != null)
            {
                System.Console.WriteLine(exPlayer.name + " is ex-Guarded and unvisored");
                exPlayer.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
                exPlayer.myRend().material.SetFloat("_Outline", 0f);
                bg.exGuarded = null;
                continue;
            }

            var player = bg.GuardedPlayer;
            if (player == null) continue;

            if (player.Data.IsDead || bg.Player.Data.IsDead || bg.Player.Data.Disconnected)
            {
                Switcheroo.AttackTrigger(bg.Player.PlayerId, player.PlayerId, true);
                continue;
            }

            var showGuarded = CustomGameOptions.ShowGuarded;
            if (showGuarded == GuardOptions.Everyone)
            {
                player.myRend().material.SetColor("_VisorColor", ProtectedColor);
                player.myRend().material.SetFloat("_Outline", 1f);
                player.myRend().material.SetColor("_OutlineColor", ProtectedColor);
            }
            else if (PlayerControl.LocalPlayer.PlayerId == player.PlayerId && (showGuarded == GuardOptions.Self ||
                showGuarded == GuardOptions.SelfAndBodyguard))
            {
                player.myRend().material.SetColor("_VisorColor", ProtectedColor);
                player.myRend().material.SetFloat("_Outline", 1f);
                player.myRend().material.SetColor("_OutlineColor", ProtectedColor);
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard) &&
                     (showGuarded == GuardOptions.Bodyguard || showGuarded == GuardOptions.SelfAndBodyguard))
            {
                player.myRend().material.SetColor("_VisorColor", ProtectedColor);
                player.myRend().material.SetFloat("_Outline", 1f);
                player.myRend().material.SetColor("_OutlineColor", ProtectedColor);
            }
        }
    }
}
}