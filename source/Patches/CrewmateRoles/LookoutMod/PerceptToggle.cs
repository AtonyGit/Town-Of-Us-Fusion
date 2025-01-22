using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.LookoutMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    [HarmonyPriority(Priority.Last)]
    public class PerceptToggle
    {
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Lookout))
            {
                var lookout = (Lookout) role;
                if (lookout.Percepting)
                    lookout.Percept();
                else if (lookout.Enabled) lookout.Unpercept();
            }
        }
    }
}