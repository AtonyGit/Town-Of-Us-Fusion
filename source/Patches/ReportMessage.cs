using HarmonyLib;
using Hazel;

namespace TownOfUsFusion
{
    public class Reportmessage
    {
        public static string location;
        
        [HarmonyPatch(typeof(RoomTracker), nameof(RoomTracker.FixedUpdate))]
        public class RecordLocation
        {
            [HarmonyPostfix]
            public static void Postfix(RoomTracker __instance)
            {
                if (__instance.text.transform.localPosition.y != -3.25f)
                {
                    location = __instance.text.text;
                }
                else
                {
                    string name = PlayerControl.LocalPlayer.name;
                    location = $"Body was not reported in a room";
                }
            }
        }
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CmdReportDeadBody))]
        public class Sendchat
        {
            [HarmonyPostfix]
            public static void Postfix([HarmonyArgument(0)] NetworkedPlayerInfo target)
            {
                string report = $"Body reported in: {location}";
                if (target != null && CustomGameOptions.LocationReports)
                {
                    DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, report);
                    Utils.Rpc(CustomRPC.SendChat, report);
                }
                
            }
        }
    }
}
