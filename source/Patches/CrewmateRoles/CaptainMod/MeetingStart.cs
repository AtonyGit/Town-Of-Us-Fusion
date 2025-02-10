using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.CaptainMod
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.StartMeeting))]
    class StartMeetingPatch
    {
        public static void Prefix(PlayerControl __instance, [HarmonyArgument(0)] NetworkedPlayerInfo meetingTarget)
        {
            if (__instance == null)
            {
                return;
            }
            foreach (var pros in Role.GetRoles(RoleEnum.Captain))
            {
                var prosRole = (Captain)pros;
                prosRole.StartTribunal = false;
            }
            return;
        }
    }
}
