using HarmonyLib;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.TyrantMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))] // BBFDNCCEJHI
public static class VotingComplete
{
    public static void Postfix(MeetingHud __instance)
    {
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Tyrant))
        {
            var tyrant = Role.GetRole<Tyrant>(PlayerControl.LocalPlayer);
            if (!tyrant.Revealed) tyrant.RevealButton.Destroy();
        }
    }
}
}