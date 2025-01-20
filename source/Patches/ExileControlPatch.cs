using HarmonyLib;

namespace TownOfUsFusion.Patches
{
<<<<<<< Updated upstream
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
=======
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.BeginForGameplay))]
>>>>>>> Stashed changes
    [HarmonyPriority(Priority.First)]
    class ExileControllerPatch
    {
        public static ExileController lastExiled;
        public static void Prefix(ExileController __instance)
        {
            lastExiled = __instance;
        }
    }
}