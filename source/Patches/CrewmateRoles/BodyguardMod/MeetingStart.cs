using HarmonyLib;
using System.Linq;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class MeetingStartBodyguard
    {
        public static void Postfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard)) return;
            var psychicRole = Role.GetRole<Bodyguard>(PlayerControl.LocalPlayer);
            psychicRole.guardedPlayer = null;
        }

    }
}