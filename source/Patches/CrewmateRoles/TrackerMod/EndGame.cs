using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.TrackerMod
{
    public class EndGame
    {
        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.ExitGame))]
        [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]

        public static class EndGamePatch
        {
            public static void Prefix()
            {
                foreach (var role in Role.GetRoles(RoleEnum.Tracker)) ((Tracker)role).AllPrints.Clear();
            }
        }
    }
}