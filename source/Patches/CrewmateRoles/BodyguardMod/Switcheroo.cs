using HarmonyLib;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class Switcheroo
{
    public static void AttackTrigger(byte bgId, byte playerId, bool flag)
    {
        if (!flag)
            return;

        var player = Utils.PlayerById(playerId);
        foreach (var role in Role.GetRoles(RoleEnum.Bodyguard))
            if (((Bodyguard)role).GuardedPlayer.PlayerId == playerId)
            {
                ((Bodyguard)role).GuardedPlayer = null;
                ((Bodyguard)role).exGuarded = player;
                System.Console.WriteLine(player.name + " Is Ex-Guarded");
            }

        player.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
        player.myRend().material.SetFloat("_Outline", 0f);
    }
}
}