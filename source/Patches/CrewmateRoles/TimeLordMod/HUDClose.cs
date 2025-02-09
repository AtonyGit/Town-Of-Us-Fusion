using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.CrewmateRoles.TimeLordMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public static class HUDClose
    {
        public static void Postfix()
        {
            foreach (var role in Role.GetRoles(RoleEnum.TimeLord))
            {
                var TimeLord = (TimeLord) role;
                TimeLord.FinishRewind = DateTime.UtcNow;
                TimeLord.StartRewind = DateTime.UtcNow;
                TimeLord.FinishRewind = TimeLord.FinishRewind.AddSeconds(-10.0);
                TimeLord.StartRewind = TimeLord.StartRewind.AddSeconds(-20.0);
            }
        }
    }
}