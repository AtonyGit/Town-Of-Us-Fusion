using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{

[HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class ShowGuarded
{
    public static Color GuardedColor = new Color(1f, 0.85f, 0f, 1f);
    public static Color ActiveColor = Color.cyan;

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
                bg.GuardedPlayer = null;
                continue;
            }

            var player = bg.GuardedPlayer;
            if (player == null) continue;

            if ((player.Data.IsDead || bg.Player.Data.IsDead || bg.Player.Data.Disconnected) && !player.IsShielded())
            {
                player.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
                player.myRend().material.SetFloat("_Outline", 0f);
                continue;
            }
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard))
            {
                        player.myRend().material.SetColor("_VisorColor", GuardedColor);
                        player.myRend().material.SetFloat("_Outline", 1f);
                        player.myRend().material.SetColor("_OutlineColor", GuardedColor);
                if (bg.Guarding) {
                        player.myRend().material.SetColor("_VisorColor", ActiveColor);
                        player.myRend().material.SetFloat("_Outline", 1f);
                        player.myRend().material.SetColor("_OutlineColor", ActiveColor);
                }
                
                var bgrole = Role.GetRole<Bodyguard>(PlayerControl.LocalPlayer);
                if (!bgrole.GuardedPlayer.Data.IsDead && !bgrole.GuardedPlayer.Data.Disconnected) return;
/*
                Utils.Rpc(CustomRPC.GuardReset, PlayerControl.LocalPlayer.PlayerId);

                GuardReset(PlayerControl.LocalPlayer);*/
            }
    }
    }
    public static void GuardReset(PlayerControl player)
    {
            var bg = Role.GetRole<Bodyguard>(player);
                System.Console.WriteLine(bg.GuardedPlayer.name + " is ex-Guarded and unvisored");
                bg.GuardedPlayer.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
                bg.GuardedPlayer.myRend().material.SetFloat("_Outline", 0f);
                bg.exGuarded = null;
                bg.GuardedPlayer = null;
                //bgNew.RegenTask();
    }
}
}