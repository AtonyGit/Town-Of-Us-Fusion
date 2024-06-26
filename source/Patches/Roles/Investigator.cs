using System.Collections.Generic;
using TownOfUsFusion.CrewmateRoles.InvestigatorMod;

namespace TownOfUsFusion.Roles
{
    public class Investigator : Role
{
    public readonly List<Footprint> AllPrints = new List<Footprint>();


    public Investigator(PlayerControl player) : base(player)
    {
        Name = "Investigator";
        ImpostorText = () => "Find All Impostors By Examining Footprints";
        TaskText = () => "You can see everyone's footprints";
        AlignmentText = () => "Crew Investigative";
        Color = Patches.Colors.Investigator;
        RoleType = RoleEnum.Investigator;
        AddToRoleHistory(RoleType);
        Scale = 1.4f;
    }
}
}