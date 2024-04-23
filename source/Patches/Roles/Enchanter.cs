using System;
using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Enchanter : Role
{
    public DeadBody CurrentTarget;
    public KillButton _ResurrectButton;
    public DateTime LastResurrected;
    public int ResurrectCount;


    public Enchanter(PlayerControl player) : base(player)
    {
        Name = "Enchanter";
        ImpostorText = () => "Resurrect The Dead To Do Your Dirty Work";
        TaskText = () => "Use your old powers to sense useful utilities\nFake Tasks:";
        AlignmentText = () => "Necromancer Team";
        Color = Patches.Colors.NeoNecromancer;
        LastResurrected = DateTime.UtcNow;
        RoleType = RoleEnum.Enchanter;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralNeophyte;
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


    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__36 __instance)
    {
        var necroTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        necroTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = necroTeam;
    }

    public float ResurrectTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastResurrected;
        var num = (CustomGameOptions.NecroResurrectCooldown + CustomGameOptions.NecroIncreasedCooldownPerResurrect * ResurrectCount) * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
}
}