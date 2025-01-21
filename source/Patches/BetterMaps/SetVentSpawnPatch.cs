using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using TownOfUsFusion.Patches;
using TownOfUsFusion.CrewmateRoles.AurialMod;
using TownOfUsFusion.Patches.ScreenEffects;
using System.Linq;
using System.Collections.Generic;

namespace TownOfUsFusion.BetterMaps
{
    [HarmonyPatch(typeof(AirshipExileController), nameof(AirshipExileController.WrapUpAndSpawn))]
public static class AirshipExileController_WrapUpAndSpawn
{
    public static void Postfix(AirshipExileController __instance) => SetVentSpawnPatch.ExileControllerPostfixAlt(__instance);
}
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.OnDestroy))]
    public static class IntroCutsceneOnDestroyPatch
    {
        public static void Prefix() => SetVentSpawnPatch.ExileControllerPostfix();
    }

[HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
public class SetVentSpawnPatch
{
    public static Vector2 StartPosition;

    public static void ExileControllerPostfix()
    {
        var ventChance = Random.RandomRangeInt(0, 100);
        var playerList = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(RoleEnum.Haunter) && !x.Is(RoleEnum.Phantom)).ToList();

        if (CustomGameOptions.RandomVentSpawn < ventChance) return;
        foreach (var ventingPlayer in playerList)
        {
        if (!ventingPlayer.Is(RoleEnum.Phantom) && !ventingPlayer.Is(RoleEnum.Haunter)) ventingPlayer.MyPhysics.ResetMoveState();
        List<Vent> vents = new();
        var CleanVentTasks = ventingPlayer.myTasks.ToArray().Where(x => x.TaskType == TaskTypes.VentCleaning).ToList();
        if (CleanVentTasks != null)
        {
            var ids = CleanVentTasks.Where(x => !x.IsComplete)
                                    .ToList()
                                    .ConvertAll(x => x.FindConsoles()[0].ConsoleId);

            vents = ShipStatus.Instance.AllVents.Where(x => !ids.Contains(x.Id)).ToList();
        }
        else vents = ShipStatus.Instance.AllVents.ToList();

        var startingVent = vents[Random.RandomRangeInt(0, vents.Count)];


        Utils.Rpc(CustomRPC.SetPos, ventingPlayer.PlayerId, startingVent.transform.position.x, startingVent.transform.position.y + 0.3636f);
        var pos = new Vector2(startingVent.transform.position.x, startingVent.transform.position.y + 0.3636f);

        ventingPlayer.transform.position = pos;
        ventingPlayer.NetTransform.SnapTo(pos);
        ventingPlayer.NetTransform.RpcSnapTo(pos);
        }
    }
    
    public static void ExileControllerPostfixAlt(ExileController __instance)
    {
        var ventChance = Random.RandomRangeInt(0, 100);
        var playerList = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(RoleEnum.Haunter) && !x.Is(RoleEnum.Phantom)).ToList();

        if (CustomGameOptions.RandomVentSpawn < ventChance) return;
        foreach (var ventingPlayer in playerList)
        {
        if (!ventingPlayer.Is(RoleEnum.Phantom) && !ventingPlayer.Is(RoleEnum.Haunter)) ventingPlayer.MyPhysics.ResetMoveState();
        List<Vent> vents = new();
        var CleanVentTasks = ventingPlayer.myTasks.ToArray().Where(x => x.TaskType == TaskTypes.VentCleaning).ToList();
        if (CleanVentTasks != null)
        {
            var ids = CleanVentTasks.Where(x => !x.IsComplete)
                                    .ToList()
                                    .ConvertAll(x => x.FindConsoles()[0].ConsoleId);

            vents = ShipStatus.Instance.AllVents.Where(x => !ids.Contains(x.Id)).ToList();
        }
        else vents = ShipStatus.Instance.AllVents.ToList();

        var startingVent = vents[Random.RandomRangeInt(0, vents.Count)];


        Utils.Rpc(CustomRPC.SetPos, ventingPlayer.PlayerId, startingVent.transform.position.x, startingVent.transform.position.y + 0.3636f);
        var pos = new Vector2(startingVent.transform.position.x, startingVent.transform.position.y + 0.3636f);

        ventingPlayer.transform.position = pos;
        ventingPlayer.NetTransform.SnapTo(pos);
        ventingPlayer.NetTransform.RpcSnapTo(pos);
        }
    }
    public static void Postfix(ExileController __instance) => ExileControllerPostfixAlt(__instance);

    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), new Type[] { typeof(GameObject) })]
    public static void Prefix(GameObject obj)
    {
        if (!SubmergedCompatibility.Loaded || GameOptionsManager.Instance?.currentNormalGameOptions?.MapId != 6) return;
        if (obj.name?.Contains("ExileCutscene") == true) ExileControllerPostfixAlt(ExileControllerPatch.lastExiled);
    }
}
}