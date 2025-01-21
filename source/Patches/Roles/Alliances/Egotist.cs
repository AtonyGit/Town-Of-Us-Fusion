using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles.Alliances
{
    public class Egotist : Alliance
{
    public Egotist(PlayerControl player) : base(player)
    {
        Name = "Egotist";
        SymbolName = "$";
        TaskText = () => "Betray the crew with the killers";
        Color = Colors.Egotist;
        AllianceType = AllianceEnum.Egotist;
    }

}
}