using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.InvestigatorMod
{
    public class EndGame
    {
<<<<<<< Updated upstream
        public static void Reset()
        {
            foreach (var role in Role.GetRoles(RoleEnum.Investigator)) ((Investigator) role).AllPrints.Clear();
        }

        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.ExitGame))]
        public static class EndGamePatch
        {
            public static void Prefix(AmongUsClient __instance)
            {
                Reset();
            }
        }

        [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
        public static class EndGameManagerPatch
        {
            public static bool Prefix(EndGameManager __instance)
            {
                Reset();

                return true;
=======
        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.ExitGame))]
        [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]

        public static class EndGamePatch
        {
            public static void Prefix()
            {
                foreach (var role in Role.GetRoles(RoleEnum.Investigator)) ((Investigator)role).AllPrints.Clear();
>>>>>>> Stashed changes
            }
        }
    }
}