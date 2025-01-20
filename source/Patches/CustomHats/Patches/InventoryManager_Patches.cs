using HarmonyLib;

namespace TownOfUsFusion.Patches.CustomHats.Patches
{
    [HarmonyPatch(typeof(InventoryManager), nameof(InventoryManager.CheckUnlockedItems))]
    public class InventoryManager_Patches
    {
        public static void Prefix()
        {
            HatLoader.LoadHatsRoutine();
        }
    }
}   