﻿using System;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.DetectiveMod
{
    public class BodyReport
{
    public PlayerControl Killer { get; set; }
    public PlayerControl Reporter { get; set; }
    public PlayerControl Body { get; set; }
    public float KillAge { get; set; }

    public static string ParseBodyReport(BodyReport br)
    {
        if (br.KillAge > CustomGameOptions.DetectiveFactionDuration * 1000)
            return
                $"Body Report: The corpse is too old to gain information from. (Killed {Math.Round(br.KillAge / 1000)}s ago)";

        if (br.Killer.PlayerId == br.Body.PlayerId)
            return
                $"Body Report: The kill appears to have been a suicide! (Killed {Math.Round(br.KillAge / 1000)}s ago)";

        var role = Role.GetRole(br.Killer);

        if (br.KillAge < CustomGameOptions.DetectiveRoleDuration * 1000)
            return
                $"Body Report: The killer appears to be a {role.Name}! (Killed {Math.Round(br.KillAge / 1000)}s ago)";

        if (br.Killer.Is(Faction.Crewmates)|| br.Killer.Is(Faction.CrewSentinel))
            return
                $"Body Report: The killer appears to be a Crewmate! (Killed {Math.Round(br.KillAge / 1000)}s ago)";

        else if (br.Killer.Is(Faction.NeutralKilling) || br.Killer.Is(Faction.NeutralBenign) || br.Killer.Is(Faction.NeutralNeophyte) || br.Killer.Is(Faction.NeutralNecro) || br.Killer.Is(Faction.NeutralApocalypse)
        || br.Killer.Is(Faction.NeutralSentinel) || br.Killer.Is(Faction.ChaosSentinel))
            return
                $"Body Report: The killer appears to be a Neutral Role! (Killed {Math.Round(br.KillAge / 1000)}s ago)";

        else
            return
                $"Body Report: The killer appears to be an Impostor! (Killed {Math.Round(br.KillAge / 1000)}s ago)";
    }
}
}
