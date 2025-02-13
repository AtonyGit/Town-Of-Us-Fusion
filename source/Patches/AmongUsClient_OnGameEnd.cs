using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles.Alliances;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    public class AmongUsClientGameEnd
    {
        public static void Postfix()
        {
            List<int> losers = new List<int>();
            bool canWinWithNeutrals = false;
            
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
            foreach (var role in Role.GetRoles(RoleEnum.Doomsayer))
            {
                var doom = (Doomsayer)role;
                losers.Add(doom.Player.GetDefaultOutfit().ColorId);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Cannibal))
            {
                var can = (Cannibal)role;
                losers.Add(can.Player.GetDefaultOutfit().ColorId);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
            {
                var ty = (Tyrant)role;
                losers.Add(ty.Player.GetDefaultOutfit().ColorId);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Executioner))
            {
                var exe = (Executioner)role;
                losers.Add(exe.Player.GetDefaultOutfit().ColorId);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Lawyer))
            {
                var lwyr = (Lawyer)role;
                losers.Add(lwyr.Player.GetDefaultOutfit().ColorId);
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
            foreach (var role in Role.GetRoles(RoleEnum.SoulCollector))
            {
                var sc = (SoulCollector)role;
                losers.Add(sc.Player.GetDefaultOutfit().ColorId);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Arsonist))
            {
                var arso = (Arsonist)role;
                losers.Add(arso.Player.GetDefaultOutfit().ColorId);
            }

            foreach (var role in Role.GetRoles(RoleEnum.Juggernaut))
            {
                var jugg = (Juggernaut)role;
                losers.Add(jugg.Player.GetDefaultOutfit().ColorId);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
            {
                var pest = (Pestilence)role;
                losers.Add(pest.Player.GetDefaultOutfit().ColorId);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Plaguebearer))
            {
                var pb = (Plaguebearer)role;
                losers.Add(pb.Player.GetDefaultOutfit().ColorId);
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
            foreach (var role in Role.GetRoles(RoleEnum.SerialKiller))
            {
                var sk = (SerialKiller)role;
                losers.Add(sk.Player.GetDefaultOutfit().ColorId);
            }

            var toRemoveWinners = EndGameResult.CachedWinners.ToArray().Where(o => losers.Contains(o.ColorId)).ToArray();
            for (int i = 0; i < toRemoveWinners.Count(); i++) EndGameResult.CachedWinners.Remove(toRemoveWinners[i]);

            if (Role.NobodyWins)
            {
                EndGameResult.CachedWinners = new List<CachedPlayerData>();
                return;
            }
            if (Role.SurvOnlyWins)
            {
                EndGameResult.CachedWinners = new List<CachedPlayerData>();
                foreach (var role in Role.GetRoles(RoleEnum.Survivor))
                {
                    var surv = (Survivor)role;
                    if (!surv.Player.Data.IsDead && !surv.Player.Data.Disconnected)
                    {
                        var survData = new CachedPlayerData(surv.Player.Data);
                        if (PlayerControl.LocalPlayer != surv.Player) survData.IsYou = false;
                        EndGameResult.CachedWinners.Add(survData);
                    }
                }

                return;
            }

            if (CustomGameOptions.NeutralEvilWinEndsGame)
            {
                foreach (var role in Role.AllRoles)
                {
                    var type = role.RoleType;

                    if (type == RoleEnum.Jester)
                    {
                        var jester = (Jester)role;
                        if (jester.VotedOut)
                        {
                            canWinWithNeutrals = true;
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var jestData = new CachedPlayerData(jester.Player.Data);
                            jestData.IsDead = false;
                            if (PlayerControl.LocalPlayer != jester.Player) jestData.IsYou = false;
                            EndGameResult.CachedWinners.Add(jestData);

                            foreach (var role2 in Role.GetRoles(RoleEnum.Tyrant))
                            {
                                var tyran = (Tyrant)role2;
                                if (!tyran.Player.Data.IsDead && !tyran.Player.Data.Disconnected)
                                {
                                    var isImp = EndGameResult.CachedWinners.Count != 0 && EndGameResult.CachedWinners[0].IsImpostor;

                                    if (!isImp && canWinWithNeutrals) {
                                    var tyranWinData = new CachedPlayerData(tyran.Player.Data);
                                    if (PlayerControl.LocalPlayer != tyran.Player) tyranWinData.IsYou = false;
                                    EndGameResult.CachedWinners.Add(tyranWinData);
                                    }
                                }
                            }
                            return;
                        }
                    }
                    else if (type == RoleEnum.Executioner)
                    {
                        var executioner = (Executioner)role;
                        if (executioner.TargetVotedOut)
                        {
                            canWinWithNeutrals = true;
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var exeData = new CachedPlayerData(executioner.Player.Data);
                            if (PlayerControl.LocalPlayer != executioner.Player) exeData.IsYou = false;
                            EndGameResult.CachedWinners.Add(exeData);

                            foreach (var role2 in Role.GetRoles(RoleEnum.Tyrant))
                            {
                                var tyran = (Tyrant)role2;
                                if (!tyran.Player.Data.IsDead && !tyran.Player.Data.Disconnected)
                                {
                                    var isImp = EndGameResult.CachedWinners.Count != 0 && EndGameResult.CachedWinners[0].IsImpostor;

                                    if (!isImp && canWinWithNeutrals) {
                                    var tyranWinData = new CachedPlayerData(tyran.Player.Data);
                                    if (PlayerControl.LocalPlayer != tyran.Player) tyranWinData.IsYou = false;
                                    EndGameResult.CachedWinners.Add(tyranWinData);
                                    }
                                }
                            }
                            return;
                        }
                    }
                    else if (type == RoleEnum.Doomsayer)
                    {
                        var doom = (Doomsayer)role;
                        if (doom.WonByGuessing)
                        {
                            canWinWithNeutrals = true;
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var doomData = new CachedPlayerData(doom.Player.Data);
                            if (PlayerControl.LocalPlayer != doom.Player) doomData.IsYou = false;
                            EndGameResult.CachedWinners.Add(doomData);

                            foreach (var role2 in Role.GetRoles(RoleEnum.Tyrant))
                            {
                                var tyran = (Tyrant)role2;
                                if (!tyran.Player.Data.IsDead && !tyran.Player.Data.Disconnected)
                                {
                                    var isImp = EndGameResult.CachedWinners.Count != 0 && EndGameResult.CachedWinners[0].IsImpostor;

                                    if (!isImp && canWinWithNeutrals) {
                                    var tyranWinData = new CachedPlayerData(tyran.Player.Data);
                                    if (PlayerControl.LocalPlayer != tyran.Player) tyranWinData.IsYou = false;
                                    EndGameResult.CachedWinners.Add(tyranWinData);
                                    }
                                }
                            }
                            return;
                        }
                    }
                    else if (type == RoleEnum.Phantom)
                    {
                        var phantom = (Phantom)role;
                        if (phantom.CompletedTasks)
                        {
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var phantomData = new CachedPlayerData(phantom.Player.Data);
                            if (PlayerControl.LocalPlayer != phantom.Player) phantomData.IsYou = false;
                            EndGameResult.CachedWinners.Add(phantomData);
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
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var loverOneData = new CachedPlayerData(lover.Player.Data);
                        var loverTwoData = new CachedPlayerData(otherLover.Player.Data);
                        if (PlayerControl.LocalPlayer != lover.Player) loverOneData.IsYou = false;
                        if (PlayerControl.LocalPlayer != otherLover.Player) loverTwoData.IsYou = false;
                        EndGameResult.CachedWinners.Add(loverOneData);
                        EndGameResult.CachedWinners.Add(loverTwoData);
                        return;
                    }
                }
            }

            if (Role.VampireWins)
            {
                EndGameResult.CachedWinners = new List<CachedPlayerData>();
                canWinWithNeutrals = true;
                foreach (var role in Role.GetRoles(RoleEnum.Vampire))
                {
                    var vamp = (Vampire)role;
                    var vampData = new CachedPlayerData(vamp.Player.Data);
                    if (PlayerControl.LocalPlayer != vamp.Player) vampData.IsYou = false;
                    EndGameResult.CachedWinners.Add(vampData);
                }
            }

            if (Role.ApocWins)
            {
                EndGameResult.CachedWinners = new List<CachedPlayerData>();
                canWinWithNeutrals = true;
                foreach (var role in Role.GetRoles(RoleEnum.Juggernaut))
                {
                    var apoc = (Juggernaut)role;
                    var apocData = new CachedPlayerData(apoc.Player.Data);
                    if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                    EndGameResult.CachedWinners.Add(apocData);
                }
                foreach (var role in Role.GetRoles(RoleEnum.Plaguebearer))
                {
                    var apoc = (Plaguebearer)role;
                    var apocData = new CachedPlayerData(apoc.Player.Data);
                    if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                    EndGameResult.CachedWinners.Add(apocData);
                }
                foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
                {
                    var apoc = (Pestilence)role;
                    var apocData = new CachedPlayerData(apoc.Player.Data);
                    if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                    EndGameResult.CachedWinners.Add(apocData);
                }
                foreach (var role in Role.GetRoles(RoleEnum.SoulCollector))
                {
                    var apoc = (SoulCollector)role;
                    var apocData = new CachedPlayerData(apoc.Player.Data);
                    if (PlayerControl.LocalPlayer != apoc.Player) apocData.IsYou = false;
                    EndGameResult.CachedWinners.Add(apocData);
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
                        canWinWithNeutrals = true;
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var canData = new CachedPlayerData(can.Player.Data);
                        canData.IsDead = false;
                        if (PlayerControl.LocalPlayer != can.Player) canData.IsYou = false;
                        EndGameResult.CachedWinners.Add(canData);

                        foreach (var role2 in Role.GetRoles(RoleEnum.Tyrant))
                        {
                            var tyran = (Tyrant)role2;
                            if (!tyran.Player.Data.IsDead && !tyran.Player.Data.Disconnected)
                            {
                                var isImp = EndGameResult.CachedWinners.Count != 0 && EndGameResult.CachedWinners[0].IsImpostor;

                                if (!isImp && canWinWithNeutrals) {
                                var tyranWinData = new CachedPlayerData(tyran.Player.Data);
                                if (PlayerControl.LocalPlayer != tyran.Player) tyranWinData.IsYou = false;
                                EndGameResult.CachedWinners.Add(tyranWinData);
                                }
                            }
                        }
                        return;
                    }
                }
                else if (type == RoleEnum.Glitch)
                {
                    var glitch = (Glitch)role;
                    if (glitch.GlitchWins)
                    {
                        canWinWithNeutrals = true;
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var glitchData = new CachedPlayerData(glitch.Player.Data);
                        if (PlayerControl.LocalPlayer != glitch.Player) glitchData.IsYou = false;
                        EndGameResult.CachedWinners.Add(glitchData);
                    }
                }
                else if (type == RoleEnum.Arsonist)
                {
                    var arsonist = (Arsonist)role;
                    if (arsonist.ArsonistWins)
                    {
                        canWinWithNeutrals = true;
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var arsonistData = new CachedPlayerData(arsonist.Player.Data);
                        if (PlayerControl.LocalPlayer != arsonist.Player) arsonistData.IsYou = false;
                        EndGameResult.CachedWinners.Add(arsonistData);
                    }
                }
                else if (type == RoleEnum.Werewolf)
                {
                    var werewolf = (Werewolf)role;
                    if (werewolf.WerewolfWins)
                    {
                        canWinWithNeutrals = true;
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var werewolfData = new CachedPlayerData(werewolf.Player.Data);
                        if (PlayerControl.LocalPlayer != werewolf.Player) werewolfData.IsYou = false;
                        EndGameResult.CachedWinners.Add(werewolfData);
                    }
                }
                else if (type == RoleEnum.SerialKiller)
                {
                    var sk = (SerialKiller)role;
                    if (sk.SkWins)
                    {
                        canWinWithNeutrals = true;
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var skData = new CachedPlayerData(sk.Player.Data);
                        if (PlayerControl.LocalPlayer != sk.Player) skData.IsYou = false;
                        EndGameResult.CachedWinners.Add(skData);
                    }
                }
            }


            foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
            {
                var tyran = (Tyrant)role;
                if (!tyran.Player.Data.IsDead && !tyran.Player.Data.Disconnected)
                {
                    var isImp = EndGameResult.CachedWinners.Count != 0 && EndGameResult.CachedWinners[0].IsImpostor;

                    if (!isImp && canWinWithNeutrals) {
                    var tyranWinData = new CachedPlayerData(tyran.Player.Data);
                    if (PlayerControl.LocalPlayer != tyran.Player) tyranWinData.IsYou = false;
                    EndGameResult.CachedWinners.Add(tyranWinData);
                    }
                }
            }
                        
            foreach (var role in Role.GetRoles(RoleEnum.Lawyer))
            {
                var lwyr = (Lawyer)role;
                if (!lwyr.TargetVotedOut && !lwyr.Player.Data.IsDead)
                {
                    var isImp = EndGameResult.CachedWinners[0].IsImpostor;
                    var lwyrWinData = new CachedPlayerData(lwyr.Player.Data);
                    if (isImp) lwyrWinData.IsImpostor = true;
                    if (PlayerControl.LocalPlayer != lwyr.Player) lwyrWinData.IsYou = false;
                    EndGameResult.CachedWinners.Add(lwyrWinData);
                }
            }

            foreach (var role in Role.GetRoles(RoleEnum.Survivor))
            {
                var surv = (Survivor)role;
                if (!surv.Player.Data.IsDead && !surv.Player.Data.Disconnected)
                {
                    var isImp = EndGameResult.CachedWinners.Count != 0 && EndGameResult.CachedWinners[0].IsImpostor;
                    var survWinData = new CachedPlayerData(surv.Player.Data);
                    if (isImp) survWinData.IsImpostor = true;
                    if (PlayerControl.LocalPlayer != surv.Player) survWinData.IsYou = false;
                    EndGameResult.CachedWinners.Add(survWinData);
                }
            }
            foreach (var role in Role.GetRoles(RoleEnum.GuardianAngel))
            {
                var ga = (GuardianAngel)role;
                var gaTargetData = new CachedPlayerData(ga.target.Data);
                foreach (CachedPlayerData winner in EndGameResult.CachedWinners.ToArray())
                {
                    if (gaTargetData.ColorId == winner.ColorId)
                    {
                        var isImp = EndGameResult.CachedWinners[0].IsImpostor;
                        var gaWinData = new CachedPlayerData(ga.Player.Data);
                        if (isImp) gaWinData.IsImpostor = true;
                        if (PlayerControl.LocalPlayer != ga.Player) gaWinData.IsYou = false;
                        EndGameResult.CachedWinners.Add(gaWinData);
                    }
                }
            }
        }
    }
}
