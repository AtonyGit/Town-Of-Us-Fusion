using System;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Roles;
using TownOfUsFusion.CrewmateRoles.AltruistMod;

namespace TownOfUsFusion.CrewmateRoles.ProsecutorMod
{
    [HarmonyPatch(typeof(AirshipExileController), nameof(AirshipExileController.WrapUpAndSpawn))]
public static class AirshipExileController_WrapUpAndSpawn
{
    public static void Postfix(AirshipExileController __instance) => ExilePros.ExileControllerPostfix(__instance);
}

[HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
public class ExilePros
{
    public static void ExileControllerPostfix(ExileController __instance)
    {
        foreach (var role in Role.GetRoles(RoleEnum.Prosecutor))
        {
            var pros = (Prosecutor)role;
            if (pros.ProsecuteThisMeeting)
            {
                var exiled = __instance.exiled?.Object;
                if (exiled != null && exiled.Is(Faction.Crewmates) && !exiled.IsRecruit() && !exiled.IsEgotist() && !exiled.IsLover() && CustomGameOptions.ProsDiesOnIncorrectPros 
                && !PlayerControl.LocalPlayer.Is(AllianceEnum.Crewpostor) && !PlayerControl.LocalPlayer.Is(AllianceEnum.Egotist) && !PlayerControl.LocalPlayer.Is(AllianceEnum.Crewpocalypse) && !PlayerControl.LocalPlayer.Is(AllianceEnum.Recruit))
                {
                    KillButtonTarget.DontRevive = pros.Player.PlayerId;
                    pros.Player.Exiled();
                    pros.DeathReason = DeathReasonEnum.Suicide;
                }
                pros.ProsecuteThisMeeting = false;
            }
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