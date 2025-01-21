using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.SnitchMod
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
                if (player.Is(Faction.ImpSentinel) || player.Is(Faction.Impostors) && !player.Is(RoleEnum.Traitor))
                    state.NameText.color = Palette.ImpostorRed;
                else if (player.Is(RoleEnum.Traitor) && CustomGameOptions.SnitchSeesTraitor)
                    state.NameText.color = Palette.ImpostorRed;
                if ((player.Is(Faction.NeutralKilling) || player.Is(Faction.NeutralNeophyte) || player.Is(Faction.NeutralNecro) || player.Is(Faction.NeutralApocalypse)
                || player.Is(Faction.NeutralSentinel) || player.Is(Faction.ChaosSentinel)) && CustomGameOptions.SnitchSeesNeutrals)
                    state.NameText.color = role.Color;
            }
        }
    }

    public static void Postfix(HudManager __instance)
    {
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Snitch)) return;
        var role = Role.GetRole<Snitch>(PlayerControl.LocalPlayer);
        if (!role.TasksDone) return;
        if (MeetingHud.Instance && CustomGameOptions.SnitchSeesImpInMeeting) UpdateMeeting(MeetingHud.Instance);

        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (player.Is(Faction.ImpSentinel) || player.Data.IsImpostor() && !player.Is(RoleEnum.Traitor)) player.nameText().color = Palette.ImpostorRed;
            else if (player.Is(RoleEnum.Traitor) && CustomGameOptions.SnitchSeesTraitor) player.nameText().color = Palette.ImpostorRed;
            var playerRole = Role.GetRole(player);
            if ((playerRole.Faction == Faction.NeutralKilling || playerRole.Faction == Faction.NeutralNeophyte || playerRole.Faction == Faction.NeutralNecro || playerRole.Faction == Faction.NeutralApocalypse
            || player.Is(Faction.NeutralSentinel) || player.Is(Faction.ChaosSentinel)) && CustomGameOptions.SnitchSeesNeutrals)
                player.nameText().color = playerRole.Color;
        }
    }
}
}