using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{
    public enum GuardOptions
    {
        Self = 0,
        BG = 1,
        SelfAndBG = 2,
        Everyone = 3
    }

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class ShowProtect
    {
        public static Color ProtectedColor = new Color(1f, 0.85f, 0f, 1f);
        public static Color ShieldedColor = Color.cyan;

        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Bodyguard))
            {
                var ga = (Bodyguard) role;

                var player = ga.guardedPlayer;
                if (player == null) continue;

                if ((player.Data.IsDead || ga.Player.Data.IsDead || ga.Player.Data.Disconnected) && !player.IsShielded())
                {
                    player.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
                    player.myRend().material.SetFloat("_Outline", 0f);
                    continue;
                }

                if (ga.Protecting)
                {
                    var showProtected = CustomGameOptions.ShowGuarding;
                    if (showProtected == GuardOptions.Everyone)
                    {
                        player.myRend().material.SetColor("_VisorColor", ProtectedColor);
                        player.myRend().material.SetFloat("_Outline", 1f);
                        player.myRend().material.SetColor("_OutlineColor", ProtectedColor);
                    }
                    else if (PlayerControl.LocalPlayer.PlayerId == player.PlayerId && (showProtected == GuardOptions.Self ||
                        showProtected == GuardOptions.SelfAndBG))
                    {
                        player.myRend().material.SetColor("_VisorColor", ProtectedColor);
                        player.myRend().material.SetFloat("_Outline", 1f);
                        player.myRend().material.SetColor("_OutlineColor", ProtectedColor);
                    }
                    else if (PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard) &&
                             (showProtected == GuardOptions.BG || showProtected == GuardOptions.SelfAndBG))
                    {
                        player.myRend().material.SetColor("_VisorColor", ProtectedColor);
                        player.myRend().material.SetFloat("_Outline", 1f);
                        player.myRend().material.SetColor("_OutlineColor", ProtectedColor);
                    }
                }
            }
        }
    }
}