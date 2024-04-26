using System;
using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Husk : Role
{


    public Husk(PlayerControl player) : base(player)
    {
        Name = "Husk";
        ImpostorText = () => "Resurrect The Dead To Do Your Dirty Work";
        TaskText = () => "Help your Necromancer in any way possible\nFake Tasks:";
        AlignmentText = () => "Necromancer Team";
        Color = Patches.Colors.NeoNecromancer;
        RoleType = RoleEnum.Husk;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralNeophyte;
    }

    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var necroTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        necroTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = necroTeam;
    }
}
}