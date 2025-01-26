using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.AstruistMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    [HarmonyPriority(Priority.Last)]
    public class AltruistingToggle
    {
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Altruist))
            {
                var Altruist = (Altruist) role;
                if (Altruist.Reviving)
                    Altruist.Altruisting();
                else if (Altruist.Enabled) Altruist.Unaltruisting();
            }
        }
    }
}