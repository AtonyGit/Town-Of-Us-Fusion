using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.Extensions;

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
        List<PlayerControl> NKs = new List<PlayerControl>();
        List<PlayerControl> NAs = new List<PlayerControl>();
        List<PlayerControl> imps = new List<PlayerControl>();

        foreach (var player in canHaveAlliances)
        {
                allPlayers.Add(player);
            if (player.Is(Faction.Crewmates) || player.Is(Faction.NeutralBenign) || player.Is(Faction.NeutralEvil) || player.Is(Faction.NeutralChaos))
                passives.Add(player);
            else if (player.Is(Faction.NeutralKilling))
                NKs.Add(player);
            else if (player.Is(Faction.NeutralApocalypse))
                NAs.Add(player);
            else if (player.Is(Faction.Impostors))
                imps.Add(player);

            allPlayers.Shuffle();
            NKs.Shuffle();
            NAs.Shuffle();
            imps.Shuffle();
        }

        if (passives.Count < 2 || NKs.Count < 1) return;

        var num = Random.RandomRangeInt(0, passives.Count);
        var firstRecruit = passives[num];
        canHaveAlliances.Remove(firstRecruit);

        var listChance = Random.RandomRangeInt(0, 100);

        PlayerControl secondRecruit;
        if (listChance <= 25)
        {
            var num3 = Random.RandomRangeInt(0, passives.Count);
            secondRecruit = passives[num3];
            allPlayers.RemoveAll(player => player.Is(Faction.Crewmates));
            allPlayers.RemoveAll(player => player.Is(Faction.NeutralBenign));
            allPlayers.RemoveAll(player => player.Is(Faction.NeutralEvil));
            allPlayers.RemoveAll(player => player.Is(Faction.NeutralChaos));
        }
        else if (listChance <= 50)
        {
            var num3 = Random.RandomRangeInt(0, NKs.Count);
            secondRecruit = NKs[num3];
            allPlayers.RemoveAll(player => player.Is(Faction.NeutralKilling));
        }
        else if (listChance <= 75)
        {
            var num3 = Random.RandomRangeInt(0, NAs.Count);
            secondRecruit = NAs[num3];
            allPlayers.RemoveAll(player => player.Is(Faction.NeutralApocalypse));
        }
        else if (listChance <= 100)
        {
            var num3 = Random.RandomRangeInt(0, imps.Count);
            secondRecruit = imps[num3];
            allPlayers.RemoveAll(player => player.Is(Faction.Impostors));
        }
        else
        {
            var num3 = Random.RandomRangeInt(0, allPlayers.Count);
            while (num3 == num)
            {
                num3 = Random.RandomRangeInt(0, allPlayers.Count);
            }
            secondRecruit = allPlayers[num3];
        }
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