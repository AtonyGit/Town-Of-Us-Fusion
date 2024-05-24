using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
[HarmonyPriority(Priority.Last)]
public class GuardUnguard
{
    [HarmonyPriority(Priority.Last)]
    public static void Postfix(HudManager __instance)
    {
        foreach (var role in Role.GetRoles(RoleEnum.Bodyguard))
        {
            var bg = (Bodyguard)role;
            if (bg.Guarding)
                bg.Guard();
            else if (bg.Enabled) bg.UnGuard();
        }
    }
}
}