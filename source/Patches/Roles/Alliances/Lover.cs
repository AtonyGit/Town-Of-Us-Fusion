﻿using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles.Alliances
{
    public class Lover : Alliance
{
    public Lover(PlayerControl player) : base(player)
    {
        Name = "Lover";
        SymbolName = "♥";
        TaskText = () => "You are in Love with " + OtherLover.Player.GetDefaultOutfit().PlayerName;
        Color = Colors.Lovers;
        AllianceType = AllianceEnum.Lover;
    }

    public Lover OtherLover { get; set; }
    public bool LoveCoupleWins { get; set; }
    public int Num { get; set; }

    public override List<PlayerControl> GetTeammates()
    {
        var loverTeam = new List<PlayerControl>
            {
                PlayerControl.LocalPlayer,
                OtherLover.Player
            };
        return loverTeam;
    }

    public static void Gen(List<PlayerControl> canHaveAlliances)
    {
        List<PlayerControl> crewmates = new List<PlayerControl>();
        List<PlayerControl> impostors = new List<PlayerControl>();

        foreach (var player in canHaveAlliances)
        {
            if (player.Is(Faction.Impostors) || player.Is(Faction.NeutralApocalypse) || player.Is(Faction.NeutralSentinel) || player.Is(Faction.ImpSentinel) || player.Is(Faction.NeutralApocalypse) && CustomGameOptions.NeutralLovers)
                impostors.Add(player);
            else if (player.Is(Faction.Crewmates) || player.Is(Faction.CrewSentinel) || (player.Is(Faction.NeutralBenign) && CustomGameOptions.NeutralLovers)
                 || (player.Is(Faction.NeutralChaos) && !player.Is(RoleEnum.Inquisitor) && CustomGameOptions.NeutralLovers)
                 || (player.Is(Faction.ChaosSentinel) && CustomGameOptions.NeutralLovers)
                 || (player.Is(Faction.NeutralEvil) && CustomGameOptions.NeutralLovers))
                crewmates.Add(player);
        }

        if (crewmates.Count < 2 || impostors.Count < 1) return;

        var num = Random.RandomRangeInt(0, crewmates.Count);
        var firstLover = crewmates[num];
        canHaveAlliances.Remove(firstLover);

        var lovingimp = Random.RandomRangeInt(0, 100);

        PlayerControl secondLover;
        if (CustomGameOptions.LovingImpPercent > lovingimp)
        {
            var num3 = Random.RandomRangeInt(0, impostors.Count);
            secondLover = impostors[num3];
        }
        else
        {
            var num3 = Random.RandomRangeInt(0, crewmates.Count);
            while (num3 == num)
            {
                num3 = Random.RandomRangeInt(0, crewmates.Count);
            }
            secondLover = crewmates[num3];
        }
        canHaveAlliances.Remove(secondLover);

        Utils.Rpc(CustomRPC.SetCouple, firstLover.PlayerId, secondLover.PlayerId);
        var lover1 = new Lover(firstLover);
        var lover2 = new Lover(secondLover);

        lover1.OtherLover = lover2;
        lover2.OtherLover = lover1;
    }

    internal override bool AllianceWin(LogicGameFlowNormal __instance)
    {
        if (FourPeopleLeft()) return false;

        if (CheckLoversWin())
        {
            Utils.Rpc(CustomRPC.LoveWin, Player.PlayerId);
            Win();
            Utils.EndGame();
            return false;
        }

        return true;
    }

    private bool FourPeopleLeft()
    {
        var players = PlayerControl.AllPlayerControls.ToArray();
        var alives = players.Where(x => !x.Data.IsDead).ToList();
        var lover1 = Player;
        var lover2 = OtherLover.Player;
        {
            return !lover1.Data.IsDead && !lover1.Data.Disconnected && !lover2.Data.IsDead && !lover2.Data.Disconnected &&
                   alives.Count() == 4 && (lover1.Is(Faction.Impostors) || lover2.Is(Faction.Impostors));
        }
    }

    private bool CheckLoversWin()
    {
        //System.Console.WriteLine("CHECKWIN");
        var players = PlayerControl.AllPlayerControls.ToArray();
        var alives = players.Where(x => !x.Data.IsDead).ToList();
        var lover1 = Player;
        var lover2 = OtherLover.Player;

        return !lover1.Data.IsDead && !lover1.Data.Disconnected && !lover2.Data.IsDead && !lover2.Data.Disconnected &&
               (alives.Count == 3) | (alives.Count == 2);
    }

    public void Win()
    {
        if (CustomGameOptions.NeutralEvilWinEndsGame && Role.AllRoles.Where(x => x.RoleType == RoleEnum.Jester).Any(x => ((Jester)x).VotedOut)) return;
        LoveCoupleWins = true;
        OtherLover.LoveCoupleWins = true;
    }
}
}