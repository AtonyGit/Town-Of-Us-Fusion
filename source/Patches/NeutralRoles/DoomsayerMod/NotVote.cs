using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.DoomsayerMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))] // BBFDNCCEJHI
public static class VotingComplete
{
    public static void Postfix(MeetingHud __instance)
    {
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer))
        {
            var doomsayer = Role.GetRole<Doomsayer>(PlayerControl.LocalPlayer);
            ShowHideButtonsDoom.HideButtonsDoom(doomsayer);
        }
    }
}
}