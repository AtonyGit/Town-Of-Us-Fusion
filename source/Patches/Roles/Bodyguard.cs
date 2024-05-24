using System;
using UnityEngine;
using TMPro;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Bodyguard : Role
{
    public bool Enabled;
    public DateTime LastGuarded;
    public float TimeRemaining;

    public int UsesLeft;
    public TextMeshPro UsesText;

    public bool ButtonUsable => UsesLeft != 0;

    public PlayerControl ClosestPlayer;
    public PlayerControl GuardedPlayer { get; set; }
    public PlayerControl exGuarded { get; set; }

    public Bodyguard(PlayerControl player) : base(player)
    {
        Name = "Bodyguard";
        ImpostorText = () => "Get Down Mr President!";
        TaskText = () => "Guard the Crewmates!";
        Color = Patches.Colors.Bodyguard;
        LastGuarded = DateTime.UtcNow;
        RoleType = RoleEnum.Bodyguard;
        AddToRoleHistory(RoleType);
        //Scale = 1.4f;

        UsesLeft = CustomGameOptions.MaxGuards;
    }

    public bool Guarding => TimeRemaining > 0f;

    public float GuardTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastGuarded;
        var num = CustomGameOptions.GuardCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }

    public void Guard()
    {
        Enabled = true;
        TimeRemaining -= Time.deltaTime;
    }


    public void UnGuard()
    {
        var bg = GetRole<Bodyguard>(Player);
        if (!bg.GuardedPlayer.IsGuarded())
        {
            bg.GuardedPlayer.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
            bg.GuardedPlayer.myRend().material.SetFloat("_Outline", 0f);
        }
        Enabled = false;
        LastGuarded = DateTime.UtcNow;
    }

    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var bgTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        bgTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = bgTeam;
    }
}
}