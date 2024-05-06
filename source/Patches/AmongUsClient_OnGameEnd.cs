using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Alliances;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles.Apocalypse;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
public class EndGameManager_SetEverythingUp
{
    public static void Prefix()
    {
        List<int> losers = new List<int>();
        foreach (var role in Role.GetRoles(RoleEnum.Amnesiac))
        {
            var amne = (Amnesiac)role;
            losers.Add(amne.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.GuardianAngel))
        {
            var ga = (GuardianAngel)role;
            losers.Add(ga.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Survivor))
        {
            var surv = (Survivor)role;
            losers.Add(surv.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
        {
            var ty = (Tyrant)role;
            losers.Add(ty.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Cannibal))
        {
            var can = (Cannibal)role;
            losers.Add(can.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Doomsayer))
        {
            var doom = (Doomsayer)role;
            losers.Add(doom.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Joker))
        {
            var jk = (Joker)role;
            losers.Add(jk.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Executioner))
        {
            var exe = (Executioner)role;
            losers.Add(exe.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Jester))
        {
            var jest = (Jester)role;
            losers.Add(jest.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Phantom))
        {
            var phan = (Phantom)role;
            losers.Add(phan.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Arsonist))
        {
            var arso = (Arsonist)role;
            losers.Add(arso.Player.GetDefaultOutfit().ColorId);
        }

        foreach (var role in Role.GetRoles(RoleEnum.Jackal))
        {
            var jackal = (Jackal)role;
            losers.Add(jackal.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var alliance in Alliance.GetAlliances(AllianceEnum.Recruit))
        {
            var jackal = (Recruit)alliance;
            losers.Add(jackal.Player.GetDefaultOutfit().ColorId);
        }

        // APOCALYPSE
        foreach (var role in Role.GetRoles(RoleEnum.Baker))
        {
            var bak = (Baker)role;
            losers.Add(bak.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Berserker))
        {
            var jugg = (Berserker)role;
            losers.Add(jugg.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Plaguebearer))
        {
            var pb = (Plaguebearer)role;
            losers.Add(pb.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.SoulCollector))
        {
            var sc = (SoulCollector)role;
            losers.Add(sc.Player.GetDefaultOutfit().ColorId);
        }

        foreach (var role in Role.GetRoles(RoleEnum.Famine))
        {
            var bak = (Famine)role;
            losers.Add(bak.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.War))
        {
            var jugg = (War)role;
            losers.Add(jugg.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
        {
            var pest = (Pestilence)role;
            losers.Add(pest.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Death))
        {
            var sc = (Death)role;
            losers.Add(sc.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var alliance in Alliance.GetAlliances(AllianceEnum.Crewpocalypse))
        {
            var crewpoc = (Crewpocalypse)alliance;
            losers.Add(crewpoc.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var alliance in Alliance.GetAlliances(AllianceEnum.Crewpostor))
        {
            var crewpost = (Crewpostor)alliance;
            losers.Add(crewpost.Player.GetDefaultOutfit().ColorId);
        }


        foreach (var role in Role.GetRoles(RoleEnum.Glitch))
        {
            var glitch = (Glitch)role;
            losers.Add(glitch.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Vampire))
        {
            var vamp = (Vampire)role;
            losers.Add(vamp.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Werewolf))
        {
            var ww = (Werewolf)role;
            losers.Add(ww.Player.GetDefaultOutfit().ColorId);
        }

        foreach (var role in Role.GetRoles(RoleEnum.NeoNecromancer))
        {
            var necro = (NeoNecromancer)role;
            losers.Add(necro.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Scourge))
        {
            var scourge = (Scourge)role;
            losers.Add(scourge.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Enchanter))
        {
            var enchanter = (Enchanter)role;
            losers.Add(enchanter.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Apparitionist))
        {
            var apparitionist = (Apparitionist)role;
            losers.Add(apparitionist.Player.GetDefaultOutfit().ColorId);
        }
        foreach (var role in Role.GetRoles(RoleEnum.Husk))
        {
            var husk = (Husk)role;
            losers.Add(husk.Player.GetDefaultOutfit().ColorId);
        }

        var toRemoveWinners = TempData.winners.ToArray().Where(o => losers.Contains(o.ColorId)).ToArray();
        for (int i = 0; i < toRemoveWinners.Count(); i++) TempData.winners.Remove(toRemoveWinners[i]);

        if (Role.NecroWins)
        {
            TempData.winners = new List<WinningPlayerData>();
            foreach (var role in Role.GetRoles(RoleEnum.NeoNecromancer))
            {
                var necro = (NeoNecromancer)role;
                var necroData = new WinningPlayerData(necro.Player.Data);
                if (PlayerControl.LocalPlayer != necro.Player) necroData.IsYou = false;
                TempData.winners.Add(necroData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Scourge))
            {
                var scourge = (Scourge)role;
                var scourgeData = new WinningPlayerData(scourge.Player.Data);
                if (PlayerControl.LocalPlayer != scourge.Player) scourgeData.IsYou = false;
                TempData.winners.Add(scourgeData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Enchanter))
            {
                var enchanter = (Enchanter)role;
                var enchanterData = new WinningPlayerData(enchanter.Player.Data);
                if (PlayerControl.LocalPlayer != enchanter.Player) enchanterData.IsYou = false;
                TempData.winners.Add(enchanterData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Apparitionist))
            {
                var apparitionist = (Apparitionist)role;
                var apparitionistData = new WinningPlayerData(apparitionist.Player.Data);
                if (PlayerControl.LocalPlayer != apparitionist.Player) apparitionistData.IsYou = false;
                TempData.winners.Add(apparitionistData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Husk))
            {
                var husk = (Husk)role;
                var huskData = new WinningPlayerData(husk.Player.Data);
                if (PlayerControl.LocalPlayer != husk.Player) huskData.IsYou = false;
                TempData.winners.Add(huskData);
            }
            return;
        }

        if (Role.NobodyWins)
        {
            TempData.winners = new List<WinningPlayerData>();
            return;
        }
        if (Role.SurvOnlyWins)
        {
            TempData.winners = new List<WinningPlayerData>();
            foreach (var role in Role.GetRoles(RoleEnum.Survivor))
            {
                var surv = (Survivor)role;
                if (!surv.Player.Data.IsDead && !surv.Player.Data.Disconnected)
                {
                    var survData = new WinningPlayerData(surv.Player.Data);
                    if (PlayerControl.LocalPlayer != surv.Player) survData.IsYou = false;
                    TempData.winners.Add(new WinningPlayerData(surv.Player.Data));
                }
            }

            return;
        }

        if (CustomGameOptions.NeutralEvilWinEndsGame)
        {
            var canTyrantWin = false;
            foreach (var role in Role.AllRoles)
            {
                var type = role.RoleType;
                if (type == RoleEnum.Jester)
                {
                    var jester = (Jester)role;
                    if (jester.VotedOut)
                    {
                        TempData.winners = new List<WinningPlayerData>();
                        var jestData = new WinningPlayerData(jester.Player.Data);
                        jestData.IsDead = false;
                        if (PlayerControl.LocalPlayer != jester.Player) jestData.IsYou = false;
                        canTyrantWin = true;
                        TempData.winners.Add(jestData);
                        return;
                    }
                }
                else if (type == RoleEnum.Executioner)
                {
                    var executioner = (Executioner)role;
                    if (executioner.TargetVotedOut)
                    {
                        TempData.winners = new List<WinningPlayerData>();
                        var exeData = new WinningPlayerData(executioner.Player.Data);
                        if (PlayerControl.LocalPlayer != executioner.Player) exeData.IsYou = false;
                        canTyrantWin = true;
                        TempData.winners.Add(exeData);
                        return;
                    }
                }
                else if (type == RoleEnum.Doomsayer)
                {
                    var doom = (Doomsayer)role;
                    if (doom.WonByGuessing)
                    {
                        TempData.winners = new List<WinningPlayerData>();
                        var doomData = new WinningPlayerData(doom.Player.Data);
                        if (PlayerControl.LocalPlayer != doom.Player) doomData.IsYou = false;
                        canTyrantWin = true;
                        TempData.winners.Add(doomData);
                        return;
                    }
                }
                else if (type == RoleEnum.Phantom)
                {
                    var phantom = (Phantom)role;
                    if (phantom.CompletedTasks)
                    {
                        TempData.winners = new List<WinningPlayerData>();
                        var phantomData = new WinningPlayerData(phantom.Player.Data);
                        if (PlayerControl.LocalPlayer != phantom.Player) phantomData.IsYou = false;
                        TempData.winners.Add(phantomData);
                        return;
                    }
                }

                if (type == RoleEnum.Tyrant)
                {
                    var tyrant = (Tyrant)role;
                    if (canTyrantWin && tyrant.Revealed)
                    {
                        TempData.winners = new List<WinningPlayerData>();
                        var tyData = new WinningPlayerData(tyrant.Player.Data);
                        tyData.IsDead = false;
                        if (PlayerControl.LocalPlayer != tyrant.Player) tyData.IsYou = false;
                        TempData.winners.Add(tyData);
                        return;
                    }
                }
            }
        }

        foreach (var alliance in Alliance.AllAlliances)
        {
            var type = alliance.AllianceType;

            if (type == AllianceEnum.Lover)
            {
                var lover = (Lover)alliance;
                if (lover.LoveCoupleWins)
                {
                    var otherLover = lover.OtherLover;
                    TempData.winners = new List<WinningPlayerData>();
                    var loverOneData = new WinningPlayerData(lover.Player.Data);
                    var loverTwoData = new WinningPlayerData(otherLover.Player.Data);
                    if (PlayerControl.LocalPlayer != lover.Player) loverOneData.IsYou = false;
                    if (PlayerControl.LocalPlayer != otherLover.Player) loverTwoData.IsYou = false;
                    TempData.winners.Add(loverOneData);
                    TempData.winners.Add(loverTwoData);
                    return;
                }
            } else
            if (type == AllianceEnum.Crewpostor)
            {
                var crewpostor = (Crewpostor)alliance;
                var isImp = TempData.winners.Count != 0 && TempData.winners[0].IsImpostor;
                var crewpostorWinData = new WinningPlayerData(crewpostor.Player.Data);
                if (isImp) crewpostorWinData.IsImpostor = true;
                if (PlayerControl.LocalPlayer != crewpostor.Player) crewpostorWinData.IsYou = false;
                if (!isImp) return;
                TempData.winners.Add(crewpostorWinData);
                return;
            }
        }
        

        if (Role.ApocWins)
        {
            TempData.winners = new List<WinningPlayerData>();
            foreach (var role in Role.GetRoles(RoleEnum.Baker))
            {
                var apoc = (Baker)role;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Berserker))
            {
                var apoc = (Berserker)role;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Plaguebearer))
            {
                var apoc = (Plaguebearer)role;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.SoulCollector))
            {
                var apoc = (SoulCollector)role;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }
            
            foreach (var role in Role.GetRoles(RoleEnum.Famine))
            {
                var apoc = (Famine)role;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.War))
            {
                var apoc = (War)role;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
            {
                var apoc = (Pestilence)role;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Death))
            {
                var apoc = (Death)role;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }
            foreach (var alliance in Alliance.GetAlliances(AllianceEnum.Crewpocalypse))
            {
                var apoc = (Crewpocalypse)alliance;
                var apocData = new WinningPlayerData(apoc.Player.Data);
                if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                TempData.winners.Add(apocData);
            }/*
            foreach (var alliance in Alliance.AllAlliances)
            {
                if (alliance.AllianceType == AllianceEnum.Crewpocalypse)
                {
                    var apoc = (Crewpostor)alliance;
                    var isApoc = TempData.winners.Count != 0 && TempData.winners[0].IsImpostor;
                    var apocData = new WinningPlayerData(apoc.Player.Data);
                if (isApoc) apocData.IsImpostor = true;
                    if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                    TempData.winners.Add(apocData);
                }
            }*/
        }

        if (Role.JackalWins)
        {
            TempData.winners = new List<WinningPlayerData>();
            foreach (var role in Role.GetRoles(RoleEnum.Jackal))
            {
                var jackal = (Jackal)role;
                var jackalData = new WinningPlayerData(jackal.Player.Data);
                if (PlayerControl.LocalPlayer != jackal.Player) jackalData.IsYou = false;
                TempData.winners.Add(jackalData);
            }
            foreach (var alliance in Alliance.GetAlliances(AllianceEnum.Recruit))
            {
                var recruit = (Recruit)alliance;
                var recruitData = new WinningPlayerData(recruit.Player.Data);
                if (PlayerControl.LocalPlayer != recruit.Player) recruitData.IsYou = false;
                TempData.winners.Add(recruitData);
            }
        }

        if (Role.VampireWins)
        {
            TempData.winners = new List<WinningPlayerData>();
            foreach (var role in Role.GetRoles(RoleEnum.Vampire))
            {
                var vamp = (Vampire)role;
                var vampData = new WinningPlayerData(vamp.Player.Data);
                if (PlayerControl.LocalPlayer != vamp.Player) vampData.IsYou = false;
                TempData.winners.Add(vampData);
            }
        }

        foreach (var role in Role.AllRoles)
        {
            var type = role.RoleType;

            if (type == RoleEnum.Cannibal)
            {
                var can = (Cannibal)role;
                if (can.EatWin)
                {
                    TempData.winners = new List<WinningPlayerData>();
                    var canData = new WinningPlayerData(can.Player.Data);
                    canData.IsDead = false;
                    if (PlayerControl.LocalPlayer != can.Player) canData.IsYou = false;
                    TempData.winners.Add(canData);
                    return;
                }
            }
            else if (type == RoleEnum.Glitch)
            {
                var glitch = (Glitch)role;
                if (glitch.GlitchWins)
                {
                    TempData.winners = new List<WinningPlayerData>();
                    var glitchData = new WinningPlayerData(glitch.Player.Data);
                    if (PlayerControl.LocalPlayer != glitch.Player) glitchData.IsYou = false;
                    TempData.winners.Add(glitchData);
                }
            }
            else if (type == RoleEnum.Arsonist)
            {
                var arsonist = (Arsonist)role;
                if (arsonist.ArsonistWins)
                {
                    TempData.winners = new List<WinningPlayerData>();
                    var arsonistData = new WinningPlayerData(arsonist.Player.Data);
                    if (PlayerControl.LocalPlayer != arsonist.Player) arsonistData.IsYou = false;
                    TempData.winners.Add(arsonistData);
                }
            }
            else if (type == RoleEnum.Werewolf)
            {
                var werewolf = (Werewolf)role;
                if (werewolf.WerewolfWins)
                {
                    TempData.winners = new List<WinningPlayerData>();
                    var werewolfData = new WinningPlayerData(werewolf.Player.Data);
                    if (PlayerControl.LocalPlayer != werewolf.Player) werewolfData.IsYou = false;
                    TempData.winners.Add(werewolfData);
                }
            }
        }

        foreach (var role in Role.GetRoles(RoleEnum.Survivor))
        {
            var surv = (Survivor)role;
            if (!surv.Player.Data.IsDead && !surv.Player.Data.Disconnected)
            {
                var isImp = TempData.winners.Count != 0 && TempData.winners[0].IsImpostor;
                var survWinData = new WinningPlayerData(surv.Player.Data);
                if (isImp) survWinData.IsImpostor = true;
                if (PlayerControl.LocalPlayer != surv.Player) survWinData.IsYou = false;
                TempData.winners.Add(survWinData);
            }
        }
        foreach (var role in Role.GetRoles(RoleEnum.GuardianAngel))
        {
            var ga = (GuardianAngel)role;
            var gaTargetData = new WinningPlayerData(ga.target.Data);
            foreach (WinningPlayerData winner in TempData.winners.ToArray())
            {
                if (gaTargetData.ColorId == winner.ColorId)
                {
                    var isImp = TempData.winners[0].IsImpostor;
                    var gaWinData = new WinningPlayerData(ga.Player.Data);
                    if (isImp) gaWinData.IsImpostor = true;
                    if (PlayerControl.LocalPlayer != ga.Player) gaWinData.IsYou = false;
                    TempData.winners.Add(gaWinData);
                }
            }
        }
    }
}
}
