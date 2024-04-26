using UnityEngine;
using Il2CppSystem.Collections.Generic;
using System;

namespace TownOfUsFusion.Roles
{
    public class Joker : Role
{
    public PlayerControl target;
    public PlayerControl ClosestPlayer;
    public bool UsedAbility { get; set; } = false;
    public DateTime StartingCooldown { get; set; }
    public bool TargetVotedOut;
    public bool HasTarget => target != null;
//    public KillButton _tauntButton;
    public Joker(PlayerControl player) : base(player)
    {
        Name = "Joker";
        ImpostorText = () => "Taunt A Target To Torture Others";
        TaskText = () => HasTarget ? "Get {target.name} lynched to start your master plan.\nFake Tasks:" : "Find a target to taunt\nFake Tasks:";
        Color = Patches.Colors.Joker;
        RoleType = RoleEnum.Joker;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralChaos;
    }

/*    public KillButton TauntButton
    {
        get => _tauntButton;
        set
        {
            _tauntButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }*/
    public float StartTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - StartingCooldown;
        var num = 10000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var jokerTeam = new List<PlayerControl>();
        jokerTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = jokerTeam;
    }

    public void Wins()
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return;
        TargetVotedOut = true;
    }
}
}