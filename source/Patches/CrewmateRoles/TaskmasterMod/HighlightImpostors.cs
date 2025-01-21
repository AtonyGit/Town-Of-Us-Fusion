using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.TaskmasterMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class HighlightImpostors
{
    private static void UpdateMeeting(MeetingHud __instance)
    {
        foreach (var player in PlayerControl.AllPlayerControls)
        {
            foreach (var state in __instance.playerStates)
            {
                if (player.PlayerId != state.TargetPlayerId) continue;
                var role = Role.GetRole(player);
                if (player.Is(Faction.Impostors) || player.Is(Faction.ImpSentinel))
                    state.NameText.color = Palette.ImpostorRed;
                if ((player.Is(Faction.NeutralKilling) || player.Is(Faction.NeutralNeophyte) || player.Is(Faction.NeutralNecro) || player.Is(Faction.NeutralApocalypse)
                || player.Is(Faction.ChaosSentinel) || player.Is(Faction.NeutralSentinel)) && CustomGameOptions.TMSeesNeutrals)
                    state.NameText.color = role.Color;
            }
        }
    }

    public static void Postfix(HudManager __instance)
    {
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Taskmaster)) return;
        var role = Role.GetRole<Taskmaster>(PlayerControl.LocalPlayer);
        if (!role.TasksDone) return;

        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (player.Data.IsImpostor() || player.Is(Faction.ImpSentinel)) player.nameText().color = Palette.ImpostorRed;
            var playerRole = Role.GetRole(player);
            if ((playerRole.Faction == Faction.NeutralKilling || playerRole.Faction == Faction.NeutralNeophyte || playerRole.Faction == Faction.NeutralNecro || playerRole.Faction == Faction.NeutralApocalypse
            || playerRole.Faction == Faction.NeutralSentinel || playerRole.Faction == Faction.ChaosSentinel) && CustomGameOptions.TMSeesNeutrals)
                player.nameText().color = playerRole.Color;
        }
    }
}
}