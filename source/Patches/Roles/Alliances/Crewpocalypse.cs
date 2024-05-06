using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles.Alliances
{
    public class Crewpocalypse : Alliance
{
    public Crewpocalypse(PlayerControl player) : base(player)
    {
        Name = "Crewpocalypse";
        SymbolName = "§";
        TaskText = () => "You are a Crewmate working for Apocalypse";
        Color = Colors.RegularApoc;
        AllianceType = AllianceEnum.Crewpocalypse;
    }

}
}