using System.Linq;
using HarmonyLib;
using UnityEngine;
using Reactor.Utilities;
using System;
using Object = UnityEngine.Object;
using TownOfUsFusion.Patches;
using System.Collections.Generic;

namespace TownOfUsFusion.BetterMaps
{
    [HarmonyPatch(typeof(ShipStatus))]

public static class MiraShipStatusPatch
{
    public static readonly Vector3 CommsPos = new(14.5f, 3.1f, 2f);

    public static bool IsAdjustmentsDone;
    public static bool IsVentsFetched;
    public static bool IsRoomsFetched;
    public static bool IsVentModified;

    public static Vent SpawnVent;
    public static Vent ReactorVent;
    public static Vent DeconVent;
    public static Vent LockerVent;
    public static Vent LabVent;
    public static Vent LightsVent;
    public static Vent AdminVent;
    public static Vent YRightVent;
    public static Vent O2Vent;
    public static Vent BalcVent;
    public static Vent MedicVent;
    public static Vent CommsVent;

    public static GameObject Comms;

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

        public static void Postfix(ShipStatus __instance)
        {
            if (!CustomGameOptions.BetterMiraEnabled)
                return;

        var ventsList = Object.FindObjectsOfType<Vent>().ToList();
            if (!IsVentModified && __instance.Type == ShipStatus.MapType.Hq)
            {
                CommsVent.Id = GetAvailableId();
                IsVentModified = true;
                var vents = ventsList.ToList();
                vents.Add(CommsVent);
                //ventsList = AllVents.ToArray();
            }
        }
    }

    public static void ApplyChanges(ShipStatus __instance)
    {
        if (!CustomGameOptions.BetterMiraEnabled)
            return;

        if (__instance.Type == ShipStatus.MapType.Hq)
        {
            FindRooms();
            FindVents();
            AdjustMira();
        }
    }

    public static void AdjustMira()
    {
        if (IsVentsFetched && CustomGameOptions.BMVentImprovements && IsRoomsFetched)
            AdjustVents();

        IsAdjustmentsDone = true;
    }

    public static void FindVents()
    {
        var ventsList = Object.FindObjectsOfType<Vent>().ToList();
        if (SpawnVent == null)
            SpawnVent = ventsList.Find(vent => vent.gameObject.name == "LaunchVent");

        if (BalcVent == null)
            BalcVent = ventsList.Find(vent => vent.gameObject.name == "BalconyVent");

        if (ReactorVent == null)
            ReactorVent = ventsList.Find(vent => vent.gameObject.name == "ReactorVent");

        if (LabVent == null)
            LabVent = ventsList.Find(vent => vent.gameObject.name == "LabVent");

        if (LockerVent == null)
            LockerVent = ventsList.Find(vent => vent.gameObject.name == "LockerVent");

        if (AdminVent == null)
            AdminVent = ventsList.Find(vent => vent.gameObject.name == "AdminVent");

        if (LightsVent == null)
            LightsVent = ventsList.Find(vent => vent.gameObject.name == "OfficeVent");

        if (O2Vent == null)
            O2Vent = ventsList.Find(vent => vent.gameObject.name == "AgriVent");

        if (DeconVent == null)
            DeconVent = ventsList.Find(vent => vent.gameObject.name == "DeconVent");

        if (MedicVent == null)
            MedicVent = ventsList.Find(vent => vent.gameObject.name == "MedVent");

        if (YRightVent == null)
            YRightVent = ventsList.Find(vent => vent.gameObject.name == "YHallRightVent");

        if (CommsVent == null)
        {
            CommsVent = Object.Instantiate(YRightVent, Comms.transform);
            CommsVent.Right = null;
            CommsVent.Left = null;
            CommsVent.Center = null;
            CommsVent.name = "CommsVent";
        }

        IsVentsFetched = SpawnVent && BalcVent && ReactorVent && LabVent && LockerVent && AdminVent && O2Vent && LightsVent && DeconVent && MedicVent && YRightVent && CommsVent;
    }

    public static void FindRooms()
    {
        if (Comms == null)
            Comms = Object.FindObjectsOfType<GameObject>().ToList().Find(o => o.name == "Comms");

        IsRoomsFetched = Comms;
    }

    public static void AdjustVents()
    {
        if (IsVentsFetched && IsRoomsFetched)
        {
            MoveCommsVent();
            ReconnectVents();
        }
    }

    public static void ReconnectVents()
    {
        O2Vent.Right = BalcVent;
        O2Vent.Left = MedicVent;
        O2Vent.Center = null;
        MedicVent.Center = O2Vent;
        MedicVent.Right = BalcVent;
        MedicVent.Left = null;
        BalcVent.Left = MedicVent;
        BalcVent.Center = O2Vent;
        BalcVent.Right = null;

        AdminVent.Center = YRightVent;
        AdminVent.Left = null;
        AdminVent.Right = null;
        YRightVent.Center = AdminVent;
        YRightVent.Left = null;
        YRightVent.Right = null;

        LabVent.Right = LightsVent;
        LabVent.Left = null;
        LabVent.Center = null;
        LightsVent.Left = LabVent;
        LightsVent.Right = null;
        LightsVent.Center = null;

        SpawnVent.Center = ReactorVent;
        SpawnVent.Right = null;
        SpawnVent.Left = null;
        ReactorVent.Left = SpawnVent;
        ReactorVent.Right = null;
        ReactorVent.Center = null;

        CommsVent.Left = LockerVent;
        CommsVent.Center = DeconVent;
        LockerVent.Right = CommsVent;
        LockerVent.Center = DeconVent;
        LockerVent.Left = null;
        DeconVent.Left = LockerVent;
        DeconVent.Right = CommsVent;
        DeconVent.Center = null;
    }

    public static void MoveCommsVent()
    {
        if (CommsVent.transform.position != CommsPos)
            CommsVent.transform.position = CommsPos;
    }
    public static int GetAvailableId()
    {
        var ventsList = Object.FindObjectsOfType<Vent>().ToList();
        var id = 0;

        while (true)
        {
            if (ShipStatus.Instance.AllVents.All(v => v.Id != id)) return id;
            id++;
        }
    }
}
}