using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(CrewmateGhostRole), nameof(CrewmateGhostRole.CanUse))]
    public class CanUseCrew
    {
        public static bool Prefix(CrewmateGhostRole __instance, IUsable console, ref bool __result)
        {
            if ((__instance.Player.Is(RoleEnum.Phantom) && !Role.GetRole<Phantom>(__instance.Player).Caught) || (__instance.Player.Is(RoleEnum.Haunter) && !Role.GetRole<Haunter>(__instance.Player).Caught))
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}