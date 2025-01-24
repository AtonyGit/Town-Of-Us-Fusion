using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles.Alliances
{
    public class Crewpostor : Alliance
{
    public Crewpostor(PlayerControl player) : base(player)
    {
        Name = "Crewpostor";
        SymbolName = "§";
        TaskText = () => "You are a Crewmate working for the Impostors";
        Color = Colors.Impostor;
        AllianceType = AllianceEnum.Crewpostor;
    }

}
}