using System;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Roles;
using TownOfUsFusion.CrewmateRoles.AltruistMod;

namespace TownOfUsFusion.NeutraleRoles.JokerMod
{
    [HarmonyPatch(typeof(AirshipExileController), nameof(AirshipExileController.WrapUpAndSpawn))]
public static class AirshipExileController_WrapUpAndSpawn
{
    public static void Postfix(AirshipExileController __instance) => ExileJoker.ExileControllerPostfix(__instance);
}

[HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
public class ExileJoker
{
    public static void ExileControllerPostfix(ExileController __instance)
    {
        var exiled = __instance.exiled;
        if (exiled == null) return;
        var player = exiled.Object;

        foreach (var role in Role.GetRoles(RoleEnum.Joker))
            if (player.PlayerId == ((Joker)role).target.PlayerId)
            {
                KillButtonTarget.DontRevive = role.Player.PlayerId;
                role.Player.Exiled();
            }
    }

    public static void Postfix(ExileController __instance) => ExileControllerPostfix(__instance);

    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), new Type[] { typeof(GameObject) })]
    public static void Prefix(GameObject obj)
    {
        if (!SubmergedCompatibility.Loaded || GameOptionsManager.Instance?.currentNormalGameOptions?.MapId != 6) return;
        if (obj.name?.Contains("ExileCutscene") == true) ExileControllerPostfix(ExileControllerPatch.lastExiled);
    }
}
}