using HarmonyLib;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.MirrorMasterMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class StopAbility
    {
        public static void BreakShield(byte mercId, byte playerId)
        {
            if (CustomGameOptions.NotificationMirrorShield == NotificationOptions.Everyone)
                Coroutines.Start(Utils.FlashCoroutine(new Color(0f, 0.5f, 0f, 1f)));
            else if (PlayerControl.LocalPlayer.PlayerId == playerId &&
                (CustomGameOptions.NotificationMirrorShield == NotificationOptions.Shielded || CustomGameOptions.NotificationMirrorShield == NotificationOptions.MMAndShielded))
                Coroutines.Start(Utils.FlashCoroutine(new Color(0f, 0.5f, 0f, 1f)));
            else if (PlayerControl.LocalPlayer.PlayerId == mercId &&
                (CustomGameOptions.NotificationMirrorShield == NotificationOptions.MirrorMaster || CustomGameOptions.NotificationMirrorShield == NotificationOptions.MMAndShielded))
                Coroutines.Start(Utils.FlashCoroutine(new Color(0f, 0.5f, 0f, 1f)));

            var player = Utils.PlayerById(playerId);
            foreach (var role in Role.GetRoles(RoleEnum.MirrorMaster))
                if (((MirrorMaster)role).ShieldedPlayer.PlayerId == playerId && ((MirrorMaster)role).Player.PlayerId == mercId)
                {
                    var merc = (MirrorMaster)role;
                    merc.ShieldedPlayer = null;
                    merc.exShielded = player;
                    merc.UnleashUsesLeft += 1;
                    merc.AbsorbUsesLeft -= 1;
                    System.Console.WriteLine(player.name + " Is Ex-Shielded");
                }

            player.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
            player.myRend().material.SetFloat("_Outline", 0f);
        }
    }
}