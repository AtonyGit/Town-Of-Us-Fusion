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
        TaskText = () => "You and " + OtherRecruit.Player.GetDefaultOutfit().PlayerName + " are recruited together";
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
        List<PlayerControl> passives = new List<PlayerControl>();
        List<PlayerControl> teams = new List<PlayerControl>();
        List<PlayerControl> jackal = new List<PlayerControl>();

        foreach (var player in canHaveAlliances)
        {
                allPlayers.Add(player);
            if (player.Is(Faction.Crewmates) || player.Is(Faction.NeutralBenign) || player.Is(Faction.NeutralEvil))
                passives.Add(player);
            else if (player.Is(Faction.NeutralKilling) || player.Is(Faction.NeutralApocalypse) || player.Is(Faction.Impostors))
                teams.Add(player);
            else if (player.Is(RoleEnum.Jackal))
                jackal.Add(player);

            allPlayers.Shuffle();
            teams.Shuffle();
            teams.Shuffle();
        }
        if (passives.Count + teams.Count < 5) return;

        var num = Random.RandomRangeInt(0, passives.Count);
        var firstRecruit = passives[num];
        canHaveAlliances.Remove(firstRecruit);

        var lovingEvil = Random.RandomRangeInt(0, 100);

        PlayerControl secondRecruit;
        if (60 < lovingEvil)
        {
            var num3 = Random.RandomRangeInt(0, teams.Count);
            secondRecruit = teams[num3];
        }
        else if (passives.Count < 2)
        {
            var num3 = Random.RandomRangeInt(0, teams.Count);
            secondRecruit = teams[num3];
        }
        else
        {
            var num3 = Random.RandomRangeInt(0, passives.Count);
            while (num3 == num)
            {
                num3 = Random.RandomRangeInt(0, passives.Count);
            }
            secondRecruit = passives[num3];
        }
/*
        if ((passives.Count + NKs.Count + NAs.Count + imps.Count < 6 && allPlayers.Count < 5) || jackal.Count < 1)
        {
            AllianceDictionary.Remove(PlayerControl.LocalPlayer.PlayerId);
            PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("NO RECRUITS WILL SPAWN");
                return;
        }
        PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SETTING RECRUITS");
        var num = Random.RandomRangeInt(0, passives.Count);
        var firstRecruit = passives[num];
        canHaveAlliances.Remove(firstRecruit);
        allPlayers.Remove(firstRecruit);
        passives.Remove(firstRecruit);
        teams.Remove(firstRecruit);
        allPlayers.Remove(jackal[num]);

        var listChance = Random.RandomRangeInt(0, 100);

        PlayerControl secondRecruit;
        if (listChance <= 24 && !firstRecruit.Is(Faction.Crewmates) && !firstRecruit.Is(Faction.NeutralBenign)
        && !firstRecruit.Is(Faction.NeutralEvil))
        {
            var num3 = Random.RandomRangeInt(0, passives.Count);
            secondRecruit = passives[num3];
            allPlayers.RemoveAll(player => player.Is(Faction.Crewmates));
            allPlayers.RemoveAll(player => player.Is(Faction.NeutralBenign));
            allPlayers.RemoveAll(player => player.Is(Faction.NeutralEvil));
            PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SETTING RECRUIT: PASSIVE ROLE");
        }
        else if (listChance <= 70 && !firstRecruit.Is(Faction.NeutralApocalypse))
        {
            teams.RemoveAll(player => player.Is(Faction.NeutralApocalypse));
            var num3 = Random.RandomRangeInt(0, teams.Count);
            secondRecruit = teams[num3];
            allPlayers.RemoveAll(player => player.Is(Faction.NeutralApocalypse));
            PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SETTING RECRUIT: APOCALYPSE");
        }
        else if (listChance <= 70 && !firstRecruit.Is(Faction.Impostors))
        {
            teams.RemoveAll(player => player.Is(Faction.Impostors));
            var num3 = Random.RandomRangeInt(0, teams.Count);
            secondRecruit = teams[num3];
            allPlayers.RemoveAll(player => player.Is(Faction.Impostors));
            PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SETTING RECRUIT: IMPOSTOR");
        }
        else if (listChance <= 70)
        {
            var num3 = Random.RandomRangeInt(0, teams.Count);
            secondRecruit = teams[num3];
            PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SETTING RECRUIT: NEUTRAL KILLING");
        }
        else
        {
            var num3 = Random.RandomRangeInt(0, allPlayers.Count);
            while (num3 == num)
            {
                num3 = Random.RandomRangeInt(0, allPlayers.Count);
            }
            secondRecruit = allPlayers[num3];
            PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SETTING RECRUIT: WILDCARD");
        }*/
        canHaveAlliances.Remove(secondRecruit);

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