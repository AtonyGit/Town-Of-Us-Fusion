using HarmonyLib;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.MayorMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))] // BBFDNCCEJHI
    public static class VotingComplete
    {
        public static void Postfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Mayor))
            {
                var mayor = Role.GetRole<Mayor>(PlayerControl.LocalPlayer);
                if (!mayor.Revealed) mayor.RevealButton.Destroy();
            }
        }
    }
}