using System.Collections.Generic;
using UnityEngine;
using System;
using TownOfUsFusion.Roles;
using System.Linq;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Roles.Apocalypse;
using Reactor.Utilities;

namespace TownOfUsFusion.Roles
{
    public class Inquisitor : Role
{
    public RoleEnum hereticRole1;
    public RoleEnum hereticRole2;
    public RoleEnum hereticRole3;
    public PlayerControl heretic1;
    public PlayerControl heretic2;
    public PlayerControl heretic3;
    public string displayRole1;
    public string displayRole2;
    public string displayRole3;    
    public bool invalidHeretics => heretic1 == null || heretic2 == null || heretic3 == null;
    public bool allHereticsDead => (heretic1.Data.IsDead && heretic2.Data.IsDead && heretic3.Data.IsDead) || invalidHeretics;
    public bool didWin = false;
    public readonly List<GameObject> Buttons = new List<GameObject>();
    private KillButton _InquireButton;
    public DateTime LastInquired;
    public DateTime LastVanquished;
    public PlayerControl ClosestPlayer;
    public PlayerControl LastInquiredPlayer;
    public bool canVanquish;
    public bool lostVanquish = false;
    public DeadBody CurrentBodyTarget;
    public Inquisitor(PlayerControl player) : base(player)
    {
        Name = "Inquisitor";
        ImpostorText = () => "Vanquish The Heretics";
        TaskText = () => allHereticsDead ? "The Heretics are all Vanquished\nFake Tasks:" : $"The Heretics are: {displayRole1}, {displayRole2}, and {displayRole3}\nFake Tasks:";
        Color = Patches.Colors.Inquisitor;
        RoleType = RoleEnum.Inquisitor;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralChaos;
        canVanquish = false;
    }
    
    public KillButton InquireButton
    {
        get => _InquireButton;
        set
        {
            _InquireButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }
    public float InquireTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastInquired;
        var num = CustomGameOptions.InquireCooldown * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
    public float VanquishTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastVanquished;
        var num = CustomGameOptions.VanquishCooldown * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var vanTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        vanTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = vanTeam;
    }

}
}