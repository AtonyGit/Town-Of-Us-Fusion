using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.ProsecutorMod
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.StartMeeting))]
    class StartMeetingPatch
    {
<<<<<<< Updated upstream
        public static void Prefix(PlayerControl __instance, [HarmonyArgument(0)] GameData.PlayerInfo meetingTarget)
=======
        public static void Prefix(PlayerControl __instance, [HarmonyArgument(0)] NetworkedPlayerInfo meetingTarget)
>>>>>>> Stashed changes
        {
            if (__instance == null)
            {
                return;
            }
            foreach (var pros in Role.GetRoles(RoleEnum.Prosecutor))
            {
                var prosRole = (Prosecutor)pros;
                prosRole.StartProsecute = false;
            }
            return;
        }
    }
}
