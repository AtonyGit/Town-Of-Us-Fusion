using HarmonyLib;
using System;
using System.Linq;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.AltruistMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class MeetingStart
    {
        public static void Postfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Altruist)) return;
            var alt = Role.GetRole<Altruist>(PlayerControl.LocalPlayer);
            alt.CurrentTarget = null;
            
        }
    }
}
