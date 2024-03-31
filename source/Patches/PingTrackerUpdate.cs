using HarmonyLib;
using UnityEngine;

namespace TownOfUsFusion
{
    //[HarmonyPriority(Priority.VeryHigh)] // to show this message first, or be overrided if any plugins do
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
public static class PingTracker_Update
{

    [HarmonyPostfix]
    public static void Postfix(PingTracker __instance)
    {
        var position = __instance.GetComponent<AspectPosition>();
        position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
        position.AdjustPosition();
        var host = GameData.Instance.GetHost();

        __instance.text.text =
            "<size=2><color=#FF6A51FF>TownOfUs v" + TownOfUsFusion.TouVersionString + "</color>" +
            "<size=2><color=#8E5BF3FF> | Fusion v" + TownOfUsFusion.VersionString + "</color>\n" +
            $"Ping: {AmongUsClient.Instance.Ping}ms\n" +
            /*(!MeetingHud.Instance
                ?  "<size=1.7><color=#8E5BF3FF>Fork By: Atony</color>\n" +
                "<size=1.7><color=#5BB6F3FF>TOU-R By: Donners & MyDragonBreath</color>\n" : "") +*/
            (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started
                ? /*"<size=2><color=#A364CAFF>Formerly: Slushiegoose & Polus.gg</color>\n" +
                 */$"<size=3>Host: {host.PlayerName}" : "") +
                "</size>";
    }
}
}
