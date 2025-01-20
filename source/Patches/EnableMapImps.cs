using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace TownOfUsFusion
{
<<<<<<< Updated upstream
    [HarmonyPatch(typeof(GameSettingMenu), nameof(GameSettingMenu.InitializeOptions))]
    public class EnableMapImps
    {
        private static void Prefix(ref GameSettingMenu __instance)
=======
    [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Initialize))]
    public class EnableMapImps
    {
        private static void Prefix(ref GameOptionsMenu __instance)
>>>>>>> Stashed changes
        {
            __instance.HideForOnline = new Il2CppReferenceArray<Transform>(0);
        }
    }
<<<<<<< Updated upstream

    [HarmonyPatch(typeof(ImpostorRole), nameof(ImpostorRole.CanUse))]
    public class ImpTasks
    {
        private static bool Prefix(ImpostorRole __instance, ref IUsable usable, ref bool __result)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.CultistSnitch)) return true;
            __result = true;
            return false;
        }
    }
=======
>>>>>>> Stashed changes
}