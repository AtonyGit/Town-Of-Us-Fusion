using HarmonyLib;
using TownOfUsFusion.Roles;
using Reactor.Utilities.Extensions;

namespace TownOfUsFusion.CrewmateRoles.MayorMod
{
    public class ShowHideButtonsMayor
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
    public static class Confirm
    {
        public static bool Prefix(MeetingHud __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Mayor)) return true;
            var mayor = Role.GetRole<Mayor>(PlayerControl.LocalPlayer);
            if (!mayor.Revealed) mayor.RevealButton.Destroy();
            return true;
        }
    }
}
}