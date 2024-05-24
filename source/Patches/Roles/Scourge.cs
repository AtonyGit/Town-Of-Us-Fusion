using System;
using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Scourge : Role
{

    public PlayerControl ClosestPlayer;

    public Scourge(PlayerControl player) : base(player)
    {
        Name = "Scourge";
        ImpostorText = () => "Resurrect The Dead To Do Your Dirty Work";
        TaskText = () => "Help your Necromancer kill opposers\nFake Tasks:";
        AlignmentText = () => "Necromancer Team";
        Color = Patches.Colors.NeoNecromancer;
        LastKilled = DateTime.UtcNow;
        RoleType = RoleEnum.Scourge;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralNecro;
    }
    public DateTime LastKilled { get; set; }

    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var necroTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        necroTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = necroTeam;
    }


    public float ScourgeKillTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastKilled;
        var num = CustomGameOptions.ScourgeKillCooldown * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
}
}