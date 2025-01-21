using HarmonyLib;
using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.InquisitorMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
public class MeetingStart
{
    public static void Postfix(MeetingHud __instance)
    {
        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Inquisitor)) return;
        var inquisRole = Role.GetRole<Inquisitor>(PlayerControl.LocalPlayer);
        if (inquisRole.LastInquiredPlayer != null)
        {
            var playerResults = PlayerReportFeedback(inquisRole.LastInquiredPlayer);
            var roleResults = RoleReportFeedback(inquisRole.LastInquiredPlayer);

            if (!string.IsNullOrWhiteSpace(playerResults)) DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, playerResults);
            if (!string.IsNullOrWhiteSpace(roleResults)) DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, roleResults);
        }
    }

    public static string PlayerReportFeedback(PlayerControl player)
    {
        var inquisRole = Role.GetRole<Inquisitor>(PlayerControl.LocalPlayer);
        if (player == inquisRole.heretic1 || player == inquisRole.heretic2 || player == inquisRole.heretic3)
            return $"Your Inquiry about {player.GetDefaultOutfit().PlayerName} informs you that they are a Heretic!";
        else if (player == PlayerControl.LocalPlayer)
            return $"Your Inquiry reveals that you know enough about yourself.";
        else
            return $"Your Inquiry about {player.GetDefaultOutfit().PlayerName} informs you that they are not a Heretic.";
    }

    public static string RoleReportFeedback(PlayerControl player)
    {
        var inquisRole = Role.GetRole<Inquisitor>(PlayerControl.LocalPlayer);
        if (player == inquisRole.heretic1 || player == inquisRole.heretic2 || player == inquisRole.heretic3)
            return $"They are one of the following: {inquisRole.displayRole1}, {inquisRole.displayRole2}, {inquisRole.displayRole3}";
        else return "";
    }
}
}