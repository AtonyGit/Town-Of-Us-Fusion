using HarmonyLib;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Cultist;

namespace TownOfUsFusion.CultistRoles.ChameleonMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
[HarmonyPriority(Priority.Last)]
public class SwoopUnSwoop
{
    [HarmonyPriority(Priority.Last)]
    public static void Postfix(HudManager __instance)
    {
        foreach (var role in Role.GetRoles(RoleEnum.Chameleon))
        {
            var chameleon = (Chameleon)role;
            if (chameleon.IsSwooped)
                chameleon.Swoop();
            else if (chameleon.Enabled) chameleon.UnSwoop();
        }
    }
}
}