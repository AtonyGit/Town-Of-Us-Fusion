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
    public bool allHereticsDead => (heretic1.Data.IsDead || heretic1 == null) && (heretic2.Data.IsDead || heretic2 == null)&& (heretic3.Data.IsDead || heretic3 == null);
    public bool didWin = false;
    public readonly List<GameObject> Buttons = new List<GameObject>();
    private KillButton _InquireButton;
    public DateTime LastInquired;
    public DateTime LastVanquished;
    public PlayerControl ClosestPlayer;
    public PlayerControl LastInquiredPlayer;
    public bool canVanquish = CustomGameOptions.VanquishEnabled ? CustomGameOptions.VanquishRoundOne : false;
    public bool lostVanquish = false;
    public DeadBody CurrentBodyTarget;
    public Inquisitor(PlayerControl player) : base(player)
    {
        Name = "Inquisitor";
        ImpostorText = () => "Vanquish The Heretics";
        TaskText = () => $"The Heretics are: {displayRole1}, {displayRole2}, and {displayRole3}";
        Color = Patches.Colors.Inquisitor;
        RoleType = RoleEnum.Inquisitor;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralChaos;
        canVanquish = CustomGameOptions.VanquishEnabled ? CustomGameOptions.VanquishRoundOne : false;
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
    public static void Gen(PlayerControl inquisPlayerThingy, List<PlayerControl> players)
    {
        List<PlayerControl> allPlayers = new List<PlayerControl>();
        PlayerControl inquisPlayer = inquisPlayerThingy;
        foreach (var player in players)
        {
            allPlayers.Add(player);
            allPlayers.Shuffle();
            //if (player.Is(RoleEnum.Inquisitor)) inquisPlayer = player; 
            if (player.Is(Faction.NeutralChaos)) allPlayers.Remove(player);
        }
        while (allPlayers.Count > 3)
        {
        var numButPeak = UnityEngine.Random.RandomRangeInt(0, allPlayers.Count);
            PlayerControl die = allPlayers[numButPeak];
            allPlayers.Remove(die);
        }
            PlayerControl heretic1;
            PlayerControl heretic2;
            PlayerControl heretic3;
        var num = UnityEngine.Random.RandomRangeInt(0, allPlayers.Count);
            heretic1 = allPlayers[num];
            allPlayers.Remove(heretic1);

        var num2 = UnityEngine.Random.RandomRangeInt(0, allPlayers.Count);
            heretic2 = allPlayers[num2];
            allPlayers.Remove(heretic2);

        var num3 = UnityEngine.Random.RandomRangeInt(0, allPlayers.Count);
            heretic3 = allPlayers[num3];
            allPlayers.Remove(heretic3);

            RoleEnum heretic1role;
            RoleEnum heretic2role;
            RoleEnum heretic3role;
        var h1_role = Role.GetRole(heretic1);
            heretic1role = h1_role.RoleType;
        var h2_role = Role.GetRole(heretic2);
            heretic2role = h2_role.RoleType;
        var h3_role = Role.GetRole(heretic3);
            heretic3role = h3_role.RoleType;


        if (inquisPlayer != null) Utils.Rpc(CustomRPC.SetHeretics, heretic1.PlayerId, heretic2.PlayerId, heretic3.PlayerId, inquisPlayer.PlayerId);
            foreach (var role in Role.GetRoles(RoleEnum.Inquisitor))
            {
            var inquisButReal = (Inquisitor)role;
  

            PlayerControl heretic21;
            PlayerControl heretic22;
            PlayerControl heretic23;
                heretic21 = heretic1;
                heretic22 = heretic2;
                heretic23 = heretic3;
            string displayRole1;
            string displayRole2;
            string displayRole3;
                displayRole1 = $"{heretic1role}";
                displayRole2 = $"{heretic2role}";
                displayRole3 = $"{heretic3role}";
        foreach (var player in allPlayers)
        {          
            if (player.Is(heretic1role)) {
                if(heretic1role == RoleEnum.Glitch) displayRole1 = "The Glitch";
                else if(heretic1role == RoleEnum.GuardianAngel) displayRole1 = "Guardian Angel";
                else if(heretic1role == RoleEnum.NeoNecromancer) displayRole1 = "Necromancer";
                else if(heretic1role == RoleEnum.VampireHunter) displayRole1 = "Vampire Hunter";
                else if(heretic1role == RoleEnum.SoulCollector) displayRole1 = "Soul Collector";
                else displayRole1 = $"{heretic1role}";
                heretic21 = player;
                }
            if (player.Is(heretic2role)) {
                if(heretic2role == RoleEnum.Glitch) displayRole2 = "The Glitch";
                else if(heretic2role == RoleEnum.GuardianAngel) displayRole2 = "Guardian Angel";
                else if(heretic2role == RoleEnum.NeoNecromancer) displayRole2 = "Necromancer";
                else if(heretic2role == RoleEnum.VampireHunter) displayRole2 = "Vampire Hunter";
                else if(heretic2role == RoleEnum.SoulCollector) displayRole2 = "Soul Collector";
                else displayRole2 = $"{heretic2role}";
                heretic22 = player;
                }
            if (player.Is(heretic3role)) {
                if(heretic3role == RoleEnum.Glitch) displayRole3 = "The Glitch";
                else if(heretic3role == RoleEnum.GuardianAngel) displayRole3 = "Guardian Angel";
                else if(heretic3role == RoleEnum.NeoNecromancer) displayRole3 = "Necromancer";
                else if(heretic3role == RoleEnum.VampireHunter) displayRole3 = "Vampire Hunter";
                else if(heretic3role == RoleEnum.SoulCollector) displayRole3 = "Soul Collector";
                else displayRole3 = $"{heretic3role}";
                heretic23 = player;
                }
        }
            inquisButReal.hereticRole1 = heretic1role;
            inquisButReal.hereticRole2 = heretic2role;
            inquisButReal.hereticRole3 = heretic3role;
            inquisButReal.heretic1 = heretic21;
            inquisButReal.heretic2 = heretic22;
            inquisButReal.heretic3 = heretic23;
                if(heretic1role == RoleEnum.Glitch) displayRole1 = "The Glitch";
                else if(heretic1role == RoleEnum.GuardianAngel) displayRole1 = "Guardian Angel";
                else if(heretic1role == RoleEnum.NeoNecromancer) displayRole1 = "Necromancer";
                else if(heretic1role == RoleEnum.VampireHunter) displayRole1 = "Vampire Hunter";
                else if(heretic1role == RoleEnum.SoulCollector) displayRole1 = "Soul Collector";
                else displayRole1 = $"{heretic1role}";
                if(heretic2role == RoleEnum.Glitch) displayRole2 = "The Glitch";
                else if(heretic2role == RoleEnum.GuardianAngel) displayRole2 = "Guardian Angel";
                else if(heretic2role == RoleEnum.NeoNecromancer) displayRole2 = "Necromancer";
                else if(heretic2role == RoleEnum.VampireHunter) displayRole2 = "Vampire Hunter";
                else if(heretic2role == RoleEnum.SoulCollector) displayRole2 = "Soul Collector";
                else displayRole2 = $"{heretic2role}";
                if(heretic3role == RoleEnum.Glitch) displayRole3 = "The Glitch";
                else if(heretic3role == RoleEnum.GuardianAngel) displayRole3 = "Guardian Angel";
                else if(heretic3role == RoleEnum.NeoNecromancer) displayRole3 = "Necromancer";
                else if(heretic3role == RoleEnum.VampireHunter) displayRole3 = "Vampire Hunter";
                else if(heretic3role == RoleEnum.SoulCollector) displayRole3 = "Soul Collector";
                else displayRole3 = $"{heretic3role}";
            inquisButReal.displayRole1 = displayRole1;
            inquisButReal.displayRole2 = displayRole2;
            inquisButReal.displayRole3 = displayRole3;
            //PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"{heretic21}: {heretic1role}");
            //PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"{heretic22}: {heretic2role}");
            //PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"{heretic23}: {heretic3role}");
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