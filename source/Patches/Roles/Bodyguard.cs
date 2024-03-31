using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Bodyguard : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Bodyguard(PlayerControl player) : base(player)
    {
        Name = "Bodyguard";
        ImpostorText = () => "Bodyguard A Crewmate With Your Life!";
        TaskText = () => "Protect a crewmate, physically!";
        Color = Patches.Colors.Bodyguard;
        StartingCooldown = DateTime.UtcNow;
        RoleType = RoleEnum.Bodyguard;
        AddToRoleHistory(RoleType);
        GuardedPlayer = null;
    }
    public float StartTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - StartingCooldown;
        var num = 10000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }

    public PlayerControl ClosestPlayer;
    public bool UsedAbility { get; set; } = false;
    public PlayerControl GuardedPlayer { get; set; }
    public PlayerControl exGuarded { get; set; }
}
}