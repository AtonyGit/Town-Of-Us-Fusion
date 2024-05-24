using System;
using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Apparitionist : Role
{
    public DeadBody CurrentTarget;
    public KillButton _ResurrectButton;
    public DateTime LastResurrected;
    public int ResurrectCount;


    public Apparitionist(PlayerControl player) : base(player)
    {
        Name = "Apparitionist";
        ImpostorText = () => "Resurrect The Dead To Do Your Dirty Work";
        TaskText = () => "Use Black Magic to Resurrect the dead\nFake Tasks:";
        AlignmentText = () => "Necromancer Team";
        Color = Patches.Colors.NeoNecromancer;
        LastResurrected = DateTime.UtcNow;
        RoleType = RoleEnum.Apparitionist;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralNecro;
    }

    public KillButton ResurrectButton
    {
        get => _ResurrectButton;
        set
        {
            _ResurrectButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }

    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var necroTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        necroTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = necroTeam;
    }

    public float ResurrectTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastResurrected;
        var num = (CustomGameOptions.AppaResurrectCooldown + CustomGameOptions.AppaIncreasedCooldownPerResurrect * ResurrectCount) * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
}
}