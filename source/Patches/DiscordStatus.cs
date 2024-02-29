using Discord;
using HarmonyLib;
namespace TownOfUsFusion.Patches
{
    internal class DiscordStatus
{
    [HarmonyPatch(typeof(ActivityManager), nameof(ActivityManager.UpdateActivity))]
    [HarmonyPrefix]
    public static void Prefix([HarmonyArgument(0)] Activity activity)
    {
        activity.Details += $" Town of Us Fusion v{TownOfUsFusion.VersionString}";
    }
}
}
