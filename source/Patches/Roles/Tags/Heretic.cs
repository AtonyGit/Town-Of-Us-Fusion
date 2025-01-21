using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.Extensions;
using Reactor.Utilities;

namespace TownOfUsFusion.Roles.Tags
{
    public class Heretic : Tag
{
    public Heretic(PlayerControl player) : base(player)
    {
        Name = "Heretic";
        Hidden = true;
        TaskText = () => "You are a Heretic";
        Color = Colors.Crewmate;
        TagType = TagEnum.Heretic;
    }

    public Heretic OtherHeretic { get; set; }
    public Heretic OtherHeretic2 { get; set; }
    public int Num { get; set; }

    public static void Gen(List<PlayerControl> canHaveTags)
    {
        List<PlayerControl> playerList = new List<PlayerControl>();
        List<PlayerControl> fullPlayerList = new List<PlayerControl>();
        List<PlayerControl> chaosPlayerList = new List<PlayerControl>();

        foreach (var player in canHaveTags)
        {
            if (!player.Is(RoleEnum.Inquisitor)) fullPlayerList.Add(player);
            if (!player.Is(RoleEnum.Inquisitor)) playerList.Add(player);
            if (player.Is(Faction.NeutralChaos) && !player.Is(RoleEnum.Inquisitor) && !player.Is(RoleEnum.Sentinel)) chaosPlayerList.Add(player);
        }
        if (fullPlayerList.Count - chaosPlayerList.Count > 2 && chaosPlayerList.Count != 0)
        foreach (var player in fullPlayerList)
        {
            // THIS IS BASICALLY SINCE NEUTRAL CHAOS SHOULDN'T GO AGAINST EACH OTHER BUT THEY MAY HAVE TO IN CERTAIN SCENARIOS
            if (player.Is(Faction.NeutralChaos)) 
            {
                playerList.Remove(player);
                if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"REMOVED FROM HERETIC LIST: {player.Data.PlayerName}");
            }
        }
        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"POSSIBLE HERETICS COUNT: {playerList.Count}");

        var num = Random.RandomRangeInt(0, playerList.Count);
        var firstHeretic = playerList[num];
        canHaveTags.Remove(firstHeretic);
        playerList.Remove(firstHeretic);

        PlayerControl secondHeretic;
            var num2 = Random.RandomRangeInt(0, playerList.Count);
            secondHeretic = playerList[num2];
        canHaveTags.Remove(secondHeretic);
        playerList.Remove(secondHeretic);

        PlayerControl thirdHeretic;
            var num3 = Random.RandomRangeInt(0, playerList.Count);
            thirdHeretic = playerList[num3];
        canHaveTags.Remove(thirdHeretic);
        playerList.Remove(thirdHeretic);

            RoleEnum heretic1role;
            RoleEnum heretic2role;
            RoleEnum heretic3role;
        var h1_role = Role.GetRole(firstHeretic);
            heretic1role = h1_role.RoleType;
        var h2_role = Role.GetRole(secondHeretic);
            heretic2role = h2_role.RoleType;
        var h3_role = Role.GetRole(thirdHeretic);
            heretic3role = h3_role.RoleType;

        Utils.Rpc(CustomRPC.SetHeretics, firstHeretic.PlayerId, secondHeretic.PlayerId, thirdHeretic.PlayerId);
        var heretic1 = new Heretic(firstHeretic);
        var heretic2 = new Heretic(secondHeretic);
        var heretic3 = new Heretic(thirdHeretic);

        heretic1.OtherHeretic = heretic2;
        heretic1.OtherHeretic2 = heretic3;
        
        heretic2.OtherHeretic2 = heretic1;
        heretic2.OtherHeretic = heretic3;

        heretic3.OtherHeretic2 = heretic1;
        heretic3.OtherHeretic = heretic2;
            foreach (var role in Role.GetRoles(RoleEnum.Inquisitor))
            {
            var inquisButReal = (Inquisitor)role;
  

            inquisButReal.heretic1 = heretic1.Player;
            inquisButReal.heretic2 = heretic2.Player;
            inquisButReal.heretic3 = heretic3.Player;
            string displayRole1;
            string displayRole2;
            string displayRole3;
                displayRole1 = $"{heretic1role}";
                displayRole2 = $"{heretic2role}";
                displayRole3 = $"{heretic3role}";
        foreach (var player in playerList)
        {          
            if (player.Is(heretic1role)) {
                if(heretic1role == RoleEnum.Glitch) displayRole1 = "The Glitch";
                else if(heretic1role == RoleEnum.GuardianAngel) displayRole1 = "Guardian Angel";
                else if(heretic3role == RoleEnum.Sentinel) displayRole1 = "The Sentinel";
                else if(heretic1role == RoleEnum.NeoNecromancer) displayRole1 = "Necromancer";
                else if(heretic1role == RoleEnum.VampireHunter) displayRole1 = "Vampire Hunter";
                else if(heretic1role == RoleEnum.SoulCollector) displayRole1 = "Soul Collector";
                else displayRole1 = $"{heretic1role}";
                inquisButReal.heretic1 = player;
                }
            if (player.Is(heretic2role)) {
                if(heretic2role == RoleEnum.Glitch) displayRole2 = "The Glitch";
                else if(heretic2role == RoleEnum.GuardianAngel) displayRole2 = "Guardian Angel";
                else if(heretic3role == RoleEnum.Sentinel) displayRole2 = "The Sentinel";
                else if(heretic2role == RoleEnum.NeoNecromancer) displayRole2 = "Necromancer";
                else if(heretic2role == RoleEnum.VampireHunter) displayRole2 = "Vampire Hunter";
                else if(heretic2role == RoleEnum.SoulCollector) displayRole2 = "Soul Collector";
                else displayRole2 = $"{heretic2role}";
                inquisButReal.heretic2 = player;
                }
            if (player.Is(heretic3role)) {
                if(heretic3role == RoleEnum.Glitch) displayRole3 = "The Glitch";
                else if(heretic3role == RoleEnum.GuardianAngel) displayRole3 = "Guardian Angel";
                else if(heretic3role == RoleEnum.Sentinel) displayRole3 = "The Sentinel";
                else if(heretic3role == RoleEnum.NeoNecromancer) displayRole3 = "Necromancer";
                else if(heretic3role == RoleEnum.VampireHunter) displayRole3 = "Vampire Hunter";
                else if(heretic3role == RoleEnum.SoulCollector) displayRole3 = "Soul Collector";
                else displayRole3 = $"{heretic3role}";
                inquisButReal.heretic3 = player;
                }
        }
            inquisButReal.hereticRole1 = heretic1role;
            inquisButReal.hereticRole2 = heretic2role;
            inquisButReal.hereticRole3 = heretic3role;
                if(heretic1role == RoleEnum.Glitch) displayRole1 = "The Glitch";
                else if(heretic1role == RoleEnum.GuardianAngel) displayRole1 = "Guardian Angel";
                else if(heretic3role == RoleEnum.Sentinel) displayRole1 = "The Sentinel";
                else if(heretic1role == RoleEnum.NeoNecromancer) displayRole1 = "Necromancer";
                else if(heretic1role == RoleEnum.VampireHunter) displayRole1 = "Vampire Hunter";
                else if(heretic1role == RoleEnum.SoulCollector) displayRole1 = "Soul Collector";
                else displayRole1 = $"{heretic1role}";
                if(heretic2role == RoleEnum.Glitch) displayRole2 = "The Glitch";
                else if(heretic2role == RoleEnum.GuardianAngel) displayRole2 = "Guardian Angel";
                else if(heretic3role == RoleEnum.Sentinel) displayRole2 = "The Sentinel";
                else if(heretic2role == RoleEnum.NeoNecromancer) displayRole2 = "Necromancer";
                else if(heretic2role == RoleEnum.VampireHunter) displayRole2 = "Vampire Hunter";
                else if(heretic2role == RoleEnum.SoulCollector) displayRole2 = "Soul Collector";
                else displayRole2 = $"{heretic2role}";
                if(heretic3role == RoleEnum.Glitch) displayRole3 = "The Glitch";
                else if(heretic3role == RoleEnum.GuardianAngel) displayRole3 = "Guardian Angel";
                else if(heretic3role == RoleEnum.Sentinel) displayRole3 = "The Sentinel";
                else if(heretic3role == RoleEnum.NeoNecromancer) displayRole3 = "Necromancer";
                else if(heretic3role == RoleEnum.VampireHunter) displayRole3 = "Vampire Hunter";
                else if(heretic3role == RoleEnum.SoulCollector) displayRole3 = "Soul Collector";
                else displayRole3 = $"{heretic3role}";
            inquisButReal.displayRole1 = displayRole1;
            inquisButReal.displayRole2 = displayRole2;
            inquisButReal.displayRole3 = displayRole3;
            inquisButReal.RegenTask();
            }
    }
}
}