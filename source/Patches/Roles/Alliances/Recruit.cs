using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.Extensions;
using Reactor.Utilities;
using LibCpp2IL.Elf;

namespace TownOfUsFusion.Roles.Alliances
{
    public class Recruit : Alliance
{
    public Recruit(PlayerControl player) : base(player)
    {
        Name = Utils.GradientColorText("B7B9BA", "5E576B", "Recruit");
        SymbolName = "§";
        TaskText = () => Utils.GradientColorText("B7B9BA", "5E576B", "You and " + OtherRecruit.Player.GetDefaultOutfit().PlayerName + " are recruited together by a Jackal");
        Color = Colors.Recruit;
        AllianceType = AllianceEnum.Recruit;
    }

    public Recruit OtherRecruit { get; set; }
    //public bool JackalWins { get; set; }
    public int Num { get; set; }

    public override List<PlayerControl> GetTeammates()
    {
        var RecruitTeam = new List<PlayerControl>
            {
                PlayerControl.LocalPlayer,
                OtherRecruit.Player
            };
        return RecruitTeam;
    }

    public static void Gen(List<PlayerControl> canHaveAlliances)
    {
        List<PlayerControl> allPlayers = new List<PlayerControl>();

        foreach (var player in canHaveAlliances)
        {
                if (!player.Is(RoleEnum.Jackal) && !player.Is(Faction.NeutralNeophyte) && !player.Is(Faction.NeutralChaos)) 
                {
                    allPlayers.Add(player);
                    if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"ADDING PLAYER TO POSSIBLE RECRUIT LIST: {player.Data.PlayerName} | {Role.GetRole(player).RoleType}");
                }

            //allPlayers.Shuffle();
            if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"ADDED TO RECRUIT LIST: {player.Data.PlayerName}");
        }
        //if (passives.Count + teams.Count < 5) return;

        var num = Random.RandomRangeInt(0, allPlayers.Count);
        var firstRecruit = allPlayers[num];
        if (allPlayers.Count < 5)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Jackal))
            {
            var jackalPrime = (Jackal)role;
            jackalPrime.Recruit1 = null;
            jackalPrime.Recruit2 = null;
            if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"RECRUITS FAILED TO SPAWN, JACKAL IS SOLO");
            jackalPrime.CanKill = true;
            }
            return;
        }
        canHaveAlliances.Remove(firstRecruit);
        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"FIRST RECRUIT: {firstRecruit.Data.PlayerName} | {Role.GetRole(firstRecruit).RoleType}");

        if (firstRecruit.Is(Faction.Crewmates) || firstRecruit.Is(Faction.NeutralBenign) || firstRecruit.Is(Faction.NeutralEvil))
            foreach (var player2 in canHaveAlliances)
            {
                if(player2.Is(Faction.Crewmates) || player2.Is(Faction.NeutralBenign) || player2.Is(Faction.NeutralEvil))
                {
                allPlayers.Remove(player2);
                if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"REMOVED FROM LIST: {player2.Data.PlayerName} | {Role.GetRole(player2).RoleType}");
                }
            }
        else if (firstRecruit.Is(Faction.NeutralApocalypse))
            foreach (var player3 in canHaveAlliances)
            {
                if(player3.Is(Faction.NeutralApocalypse))
                {
                allPlayers.Remove(player3);
                if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"REMOVED FROM LIST: {player3.Data.PlayerName} | {Role.GetRole(player3).RoleType}");
                }
            }
        else if(firstRecruit.Is(Faction.Impostors))
            foreach (var player4 in canHaveAlliances)
            {
                if (player4.Is(Faction.Impostors))
                {
                allPlayers.Remove(player4);
                if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"REMOVED FROM LIST: {player4.Data.PlayerName} | {Role.GetRole(player4).RoleType}");
                }
            }
            //allPlayers.Shuffle();


        PlayerControl secondRecruit;
            var num3 = Random.RandomRangeInt(0, allPlayers.Count);
            secondRecruit = allPlayers[num3];
        canHaveAlliances.Remove(secondRecruit);
        allPlayers.Remove(secondRecruit);
        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SECOND RECRUIT: {secondRecruit.Data.PlayerName} | {Role.GetRole(secondRecruit).RoleType}");

        Utils.Rpc(CustomRPC.SetRecruits, firstRecruit.PlayerId, secondRecruit.PlayerId);
        var Recruit1 = new Recruit(firstRecruit);
        var Recruit2 = new Recruit(secondRecruit);

        Recruit1.OtherRecruit = Recruit2;
        Recruit2.OtherRecruit = Recruit1;
            foreach (var role in Role.GetRoles(RoleEnum.Jackal))
            {
            var jackalPrime = (Jackal)role;
            jackalPrime.Recruit1 = Recruit1.OtherRecruit;
            jackalPrime.Recruit2 = Recruit2.OtherRecruit;
            if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SET RECRUITS: {Recruit1.OtherRecruit.PlayerName}, {Recruit2.OtherRecruit.PlayerName}");
            if(Recruit1 == null || Recruit2 == null) jackalPrime.CanKill = true;
            }
    }

    internal override bool AllianceWin(LogicGameFlowNormal __instance)
    {
        if (FourPeopleLeft()) return false;

        if (CheckRecruitsWin())
        {
            Utils.Rpc(CustomRPC.JackalWin, Player.PlayerId);
            Role.JackalWin();
            Utils.EndGame();
            return false;
        }

        return true;
    }

    private bool FourPeopleLeft()
    {
        var players = PlayerControl.AllPlayerControls.ToArray();
        var alives = players.Where(x => !x.Data.IsDead).ToList();
        var Recruit1 = Player;
        var Recruit2 = OtherRecruit.Player;
        {
            return !Recruit1.Data.IsDead && !Recruit1.Data.Disconnected && !Recruit2.Data.IsDead && !Recruit2.Data.Disconnected &&
                   alives.Count() == 4 && (Recruit1.Is(Faction.Impostors) || Recruit2.Is(Faction.Impostors));
        }
    }

    private bool CheckRecruitsWin()
    {
        //System.Console.WriteLine("CHECKWIN");
        var players = PlayerControl.AllPlayerControls.ToArray();
        var alives = players.Where(x => !x.Data.IsDead).ToList();
        var Recruit1 = Player;
        var Recruit2 = OtherRecruit.Player;

        return !Recruit1.Data.IsDead && !Recruit1.Data.Disconnected && !Recruit2.Data.IsDead && !Recruit2.Data.Disconnected &&
               (alives.Count == 3) | (alives.Count == 2);
    }
/*
    public void Win()
    {
        if (CustomGameOptions.NeutralEvilWinEndsGame && Role.AllRoles.Where(x => x.RoleType == RoleEnum.Jester).Any(x => ((Jester)x).VotedOut)) return;
        JackalWins = true;
        OtherRecruit.JackalWins = true;
    }*/
}
}