using System.Linq;
using HarmonyLib;
using UnityEngine;
using Reactor.Utilities;
using System;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace TownOfUsFusion.BetterMaps
{
    [HarmonyPatch(typeof(ShipStatus))]

public static class SkeldShipStatusPatch
{
    public static readonly Vector3 ReactorVentNewPos = new(-2.95f, -10.95f, 2f);
    public static readonly Vector3 ShieldsVentNewPos = new(2f, -15f, 2f);
    public static readonly Vector3 BigYVentNewPos = new(5.2f, -4.85f, 2f);
    public static readonly Vector3 NavVentNorthNewPos = new(-11.85f, -11.55f, 2f);
    public static readonly Vector3 CafeVentNewPos = new(-3.9f, 5.5f, 2f);

    public static bool IsAdjustmentsDone;
    public static bool IsVentsFetched;

    public static Vent NavVentSouth;
    public static Vent NavVentNorth;
    public static Vent ShieldsVent;
    public static Vent WeaponsVent;
    public static Vent REngineVent;
    public static Vent UpperReactorVent;
    public static Vent LEngineVent;
    public static Vent ReactorVent;
    public static Vent BigYVent;
    public static Vent CafeVent;

    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Begin))]
    public static class ShipStatusBeginPatch
    {
        public static void Prefix(ShipStatus __instance) => ApplyChanges(__instance);
    }

    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Awake))]
    public static class ShipStatusAwakePatch
    {
        public static void Prefix(ShipStatus __instance) => ApplyChanges(__instance);
    }

    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.FixedUpdate))]
    public static class ShipStatusFixedUpdatePatch
    {
        public static void Prefix(ShipStatus __instance)
        {
            if (!IsAdjustmentsDone || !IsVentsFetched)
                ApplyChanges(__instance);
        }
    }

    public static void ApplyChanges(ShipStatus __instance)
    {
        if (!CustomGameOptions.BetterSkeldEnabled || GameOptionsManager.Instance.CurrentGameOptions.MapId == 3)
            return;

        if (__instance.Type == ShipStatus.MapType.Ship/* && MapPatches.CurrentMap != 3*/)
        {
            FindVents();
            AdjustSkeld();
        }
    }

    public static void AdjustSkeld()
    {
        if (IsVentsFetched && CustomGameOptions.BSVentImprovements)
            AdjustVents();

        IsAdjustmentsDone = true;
    }

    public static void FindVents()
    {
        var ventsList = Object.FindObjectsOfType<Vent>().ToList();

        if (NavVentSouth == null)
            NavVentSouth = ventsList.Find(vent => vent.gameObject.name == "NavVentSouth");

        if (NavVentNorth == null)
            NavVentNorth = ventsList.Find(vent => vent.gameObject.name == "NavVentNorth");

        if (ShieldsVent == null)
            ShieldsVent = ventsList.Find(vent => vent.gameObject.name == "ShieldsVent");

        if (WeaponsVent == null)
            WeaponsVent = ventsList.Find(vent => vent.gameObject.name == "WeaponsVent");

        if (REngineVent == null)
            REngineVent = ventsList.Find(vent => vent.gameObject.name == "REngineVent");

        if (UpperReactorVent == null)
            UpperReactorVent = ventsList.Find(vent => vent.gameObject.name == "UpperReactorVent");

        if (LEngineVent == null)
            LEngineVent = ventsList.Find(vent => vent.gameObject.name == "LEngineVent");

        if (ReactorVent == null)
            ReactorVent = ventsList.Find(vent => vent.gameObject.name == "ReactorVent");

        if (BigYVent == null)
            BigYVent = ventsList.Find(vent => vent.gameObject.name == "BigYVent");

        if (CafeVent == null)
            CafeVent = ventsList.Find(vent => vent.gameObject.name == "CafeVent");

        IsVentsFetched = NavVentSouth && NavVentNorth && ShieldsVent && WeaponsVent && REngineVent && UpperReactorVent && LEngineVent && ReactorVent && BigYVent && CafeVent;
    }

    public static void AdjustVents()
    {
        if (IsVentsFetched)
        {
            MoveVents();
            ReconnectVents();
        }
    }

    public static void MoveVents()
    {
        MoveReactorVent();
        MoveShieldsVent();
        MoveBigYVent();
        MoveNavVentNorth();
        MoveCafeVent();
    }

    public static void ReconnectVents()
    {
        WeaponsVent.Right = NavVentNorth;
        WeaponsVent.Left = NavVentSouth;
        NavVentNorth.Right = ShieldsVent;
        NavVentNorth.Left = WeaponsVent;
        NavVentSouth.Right = ShieldsVent;
        NavVentSouth.Left = WeaponsVent;
        ShieldsVent.Right = NavVentNorth;
        ShieldsVent.Left = NavVentSouth;
        LEngineVent.Right = ReactorVent;
        LEngineVent.Left = UpperReactorVent;
        UpperReactorVent.Right = LEngineVent;
        UpperReactorVent.Left = REngineVent;
        ReactorVent.Right = LEngineVent;
        ReactorVent.Left = REngineVent;
        REngineVent.Right = ReactorVent;
        REngineVent.Left = UpperReactorVent;
    }

    public static void MoveReactorVent()
    {
        if (ReactorVent.transform.position != ReactorVentNewPos)
            ReactorVent.transform.position = ReactorVentNewPos;
    }

    public static void MoveShieldsVent()
    {
        if (ShieldsVent.transform.position != ShieldsVentNewPos)
            ShieldsVent.transform.position = ShieldsVentNewPos;
    }

    public static void MoveBigYVent()
    {
        if (BigYVent.transform.position != BigYVentNewPos)
            BigYVent.transform.position = BigYVentNewPos;
    }

    public static void MoveNavVentNorth()
    {
        if (NavVentNorth.transform.position != NavVentNorthNewPos)
            NavVentNorth.transform.position = NavVentNorthNewPos;
    }

    public static void MoveCafeVent()
    {
        if (CafeVent.transform.position != CafeVentNewPos)
            CafeVent.transform.position = CafeVentNewPos;
    }
}
}