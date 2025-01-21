using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.NeutralRoles.SentinelMod
{
    public class Dynamite
{
    public Transform transform;
}

[HarmonyPatch]
public static class DynamiteExtentions
{
    public static void ClearDynamite(this Dynamite b)
    {
        Object.Destroy(b.transform.gameObject);
        b = null;
    }

    public static Dynamite CreateDynamite(this Vector3 location)
    {
        var DynamitePref = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        DynamitePref.name = "Dynamite";
        DynamitePref.transform.localScale = new Vector3(CustomGameOptions.PlaceRadius * ShipStatus.Instance.MaxLightRadius * 2f,
            CustomGameOptions.PlaceRadius * ShipStatus.Instance.MaxLightRadius * 2f, CustomGameOptions.PlaceRadius * ShipStatus.Instance.MaxLightRadius * 2f);
        GameObject.Destroy(DynamitePref.GetComponent<SphereCollider>());
        DynamitePref.GetComponent<MeshRenderer>().material = Roles.Sentinel.dynamiteMaterial;
        DynamitePref.transform.position = location;
        var DynamiteScript = new Dynamite();
        DynamiteScript.transform = DynamitePref.transform;
        return DynamiteScript;
    }
}
    public class Charge
{
    public Transform transform;
}

[HarmonyPatch]
public static class ChargeExtentions
{
    public static void ClearCharge(this Charge b)
    {
        Object.Destroy(b.transform.gameObject);
        b = null;
    }

    public static void UpdateChargeLocation(this Charge b, PlayerControl followedPlayer)
    {
        b.transform.position = followedPlayer.transform.position;
    }

    public static Charge CreateCharge(this PlayerControl location)
    {
        var ChargePref = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ChargePref.name = "Charge";
        ChargePref.transform.localScale = new Vector3(CustomGameOptions.ChargeRadius * ShipStatus.Instance.MaxLightRadius * 2f,
            CustomGameOptions.ChargeRadius * ShipStatus.Instance.MaxLightRadius * 2f, CustomGameOptions.ChargeRadius * ShipStatus.Instance.MaxLightRadius * 2f);
        GameObject.Destroy(ChargePref.GetComponent<SphereCollider>());
        ChargePref.GetComponent<MeshRenderer>().material = Roles.Sentinel.dynamiteMaterial;
        ChargePref.transform.position = location.transform.position;
        var ChargeScript = new Charge();
        ChargeScript.transform = ChargePref.transform;
        return ChargeScript;
    }
}
}