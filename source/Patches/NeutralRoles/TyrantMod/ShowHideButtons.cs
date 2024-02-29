using HarmonyLib;
using TownOfUsFusion.Roles;
using Reactor.Utilities.Extensions;

namespace TownOfUsFusion.NeutralRoles.TyrantMod
{
    public class ShowHideButtonsTyrant
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
    public static class Confirm
    {
        public static bool Prefix(MeetingHud __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Tyrant)) return true;
            var tyrant = Role.GetRole<Tyrant>(PlayerControl.LocalPlayer);
            if (!tyrant.Revealed) tyrant.RevealButton.Destroy();
            return true;
        }
    }
}
}