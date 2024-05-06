using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.HuskMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))] // BBFDNCCEJHI
public static class HuskVotingComplete
{
    public static void Postfix(MeetingHud __instance)
    {
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Husk))
        {
            var retributionist = Role.GetRole<Husk>(PlayerControl.LocalPlayer);
            ShowHideButtonsHusk.HideButtonsHusk(retributionist);
        }
    }
}
}