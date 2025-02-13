using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.SeerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class Update
{
    private static void UpdateMeeting(MeetingHud __instance, Seer seer)
    {
        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (!seer.Investigated.Contains(player.PlayerId)) continue;
            foreach (var state in __instance.playerStates)
            {
                if (player.PlayerId != state.TargetPlayerId) continue;
                var roleType = Utils.GetRole(player);
                switch (roleType)
                {
                    default:
                        if ((player.Is(Faction.CrewSentinel) || player.Is(Faction.Crewmates) && !(player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante) || player.Is(RoleEnum.VampireHunter) || player.Is(RoleEnum.Hunter))) ||
                            ((player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante) || player.Is(RoleEnum.VampireHunter) || player.Is(RoleEnum.Hunter)) && !CustomGameOptions.CrewKillingRed) ||
                            (player.Is(Faction.NeutralBenign) && !CustomGameOptions.NeutBenignRed) ||
                            (player.Is(Faction.NeutralEvil) && !CustomGameOptions.NeutEvilRed) ||
                            ((player.Is(Faction.NeutralChaos) || player.Is(Faction.ChaosSentinel)) && !CustomGameOptions.NeutChaosRed) ||
                            ((player.Is(Faction.NeutralNeophyte) || player.Is(Faction.NeutralNecro)) && !CustomGameOptions.NeutNeophyteRed) ||
                            (player.Is(Faction.NeutralApocalypse) && !CustomGameOptions.NeutApocalypseRed) ||
                            ((player.Is(Faction.NeutralKilling) || player.Is(Faction.NeutralSentinel)) && !CustomGameOptions.NeutKillingRed))
                        {
                            state.NameText.color = Color.green;
                        }
                        else if (player.Is(RoleEnum.Traitor) && CustomGameOptions.TraitorColourSwap)
                        {
                            foreach (var role in Role.GetRoles(RoleEnum.Traitor))
                            {
                                var traitor = (Traitor)role;
                                if ((traitor.formerRole == RoleEnum.Sheriff || traitor.formerRole == RoleEnum.Vigilante ||
                                    traitor.formerRole == RoleEnum.Veteran ||
                                    traitor.formerRole == RoleEnum.Hunter || traitor.formerRole == RoleEnum.VampireHunter)
                                && CustomGameOptions.CrewKillingRed) state.NameText.color = Color.red;
                                else state.NameText.color = Color.green;
                            }
                        }
                        else
                        {
                            state.NameText.color = Color.red;
                        }
                        break;
                }
            }
        }
    }

    [HarmonyPriority(Priority.Last)]
    private static void Postfix(HudManager __instance)

    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (PlayerControl.LocalPlayer.Data.IsDead) return;

        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Seer)) return;
        var seer = Role.GetRole<Seer>(PlayerControl.LocalPlayer);
        if (MeetingHud.Instance != null) UpdateMeeting(MeetingHud.Instance, seer);

        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (!seer.Investigated.Contains(player.PlayerId)) continue;
            var roleType = Utils.GetRole(player);
            switch (roleType)
            {
                default:
                    if ((player.Is(Faction.CrewSentinel) || player.Is(Faction.Crewmates) && !(player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante) || player.Is(RoleEnum.VampireHunter) || player.Is(RoleEnum.Hunter))) ||
                        ((player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante) || player.Is(RoleEnum.VampireHunter) || player.Is(RoleEnum.Hunter)) && !CustomGameOptions.CrewKillingRed) ||
                        (player.Is(Faction.NeutralBenign) && !CustomGameOptions.NeutBenignRed) ||
                        (player.Is(Faction.NeutralEvil) && !CustomGameOptions.NeutEvilRed) ||
                        ((player.Is(Faction.NeutralChaos) || player.Is(Faction.ChaosSentinel)) && !CustomGameOptions.NeutChaosRed) ||
                        ((player.Is(Faction.NeutralNeophyte) || player.Is(Faction.NeutralNecro)) && !CustomGameOptions.NeutNeophyteRed) ||
                        (player.Is(Faction.NeutralApocalypse) && !CustomGameOptions.NeutApocalypseRed) ||
                        ((player.Is(Faction.NeutralKilling) || player.Is(Faction.NeutralSentinel)) && !CustomGameOptions.NeutKillingRed))
                    {
                        player.nameText().color = Color.green;
                    }
                    else if (player.Is(RoleEnum.Traitor) && CustomGameOptions.TraitorColourSwap)
                    {
                        foreach (var role in Role.GetRoles(RoleEnum.Traitor))
                        {
                            var traitor = (Traitor)role;
                            if ((traitor.formerRole == RoleEnum.Sheriff || traitor.formerRole == RoleEnum.Vigilante ||
                                    traitor.formerRole == RoleEnum.Hunter || traitor.formerRole == RoleEnum.VampireHunter)
                                && CustomGameOptions.CrewKillingRed) player.nameText().color = Color.red;
                            else player.nameText().color = Color.green;
                        }
                    }
                    else
                    {
                        player.nameText().color = Color.red;
                    }
                    break;
            }
        }
    }
}
}