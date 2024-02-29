using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.ImpostorRoles.SwooperMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
[HarmonyPriority(Priority.Last)]
public class SwoopUnswoop
{
    [HarmonyPriority(Priority.Last)]
    public static void Postfix(HudManager __instance)
    {
        foreach (var role in Role.GetRoles(RoleEnum.Swooper))
        {
            var swooper = (Swooper)role;
            if (swooper.IsSwooped)
                swooper.Swoop();
            else if (swooper.Enabled) swooper.UnSwoop();
        }
    }
}
}