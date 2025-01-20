using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using System.Linq;
<<<<<<< Updated upstream
using TownOfUs.Roles;
using TownOfUs.Roles.Modifiers;
using TownOfUs.Extensions;
=======
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.Extensions;
>>>>>>> Stashed changes

namespace TownOfUs
{
<<<<<<< Updated upstream
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
    public class EndGameManager_SetEverythingUp
    {
        public static void Prefix()
=======
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    public class AmongUsClientGameEnd
    {
        public static void Postfix()
>>>>>>> Stashed changes
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
            foreach (var role in Role.GetRoles(RoleEnum.Doomsayer))
            {
                var doom = (Doomsayer)role;
                losers.Add(doom.Player.GetDefaultOutfit().ColorId);
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
<<<<<<< Updated upstream
=======
            foreach (var role in Role.GetRoles(RoleEnum.SoulCollector))
            {
                var sc = (SoulCollector)role;
                losers.Add(sc.Player.GetDefaultOutfit().ColorId);
            }
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
            var toRemoveWinners = TempData.winners.ToArray().Where(o => losers.Contains(o.ColorId)).ToArray();
            for (int i = 0; i < toRemoveWinners.Count(); i++) TempData.winners.Remove(toRemoveWinners[i]);

            if (Role.NobodyWins)
            {
                TempData.winners = new List<WinningPlayerData>();
=======
            var toRemoveWinners = EndGameResult.CachedWinners.ToArray().Where(o => losers.Contains(o.ColorId)).ToArray();
            for (int i = 0; i < toRemoveWinners.Count(); i++) EndGameResult.CachedWinners.Remove(toRemoveWinners[i]);

            if (Role.NobodyWins)
            {
                EndGameResult.CachedWinners = new List<CachedPlayerData>();
>>>>>>> Stashed changes
                return;
            }
            if (Role.SurvOnlyWins)
            {
<<<<<<< Updated upstream
                TempData.winners = new List<WinningPlayerData>();
=======
                EndGameResult.CachedWinners = new List<CachedPlayerData>();
>>>>>>> Stashed changes
                foreach (var role in Role.GetRoles(RoleEnum.Survivor))
                {
                    var surv = (Survivor)role;
                    if (!surv.Player.Data.IsDead && !surv.Player.Data.Disconnected)
                    {
<<<<<<< Updated upstream
                        var survData = new WinningPlayerData(surv.Player.Data);
                        if (PlayerControl.LocalPlayer != surv.Player) survData.IsYou = false;
                        TempData.winners.Add(new WinningPlayerData(surv.Player.Data));
=======
                        var survData = new CachedPlayerData(surv.Player.Data);
                        if (PlayerControl.LocalPlayer != surv.Player) survData.IsYou = false;
                        EndGameResult.CachedWinners.Add(survData);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                            TempData.winners = new List<WinningPlayerData>();
                            var jestData = new WinningPlayerData(jester.Player.Data);
                            jestData.IsDead = false;
                            if (PlayerControl.LocalPlayer != jester.Player) jestData.IsYou = false;
                            TempData.winners.Add(jestData);
=======
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var jestData = new CachedPlayerData(jester.Player.Data);
                            jestData.IsDead = false;
                            if (PlayerControl.LocalPlayer != jester.Player) jestData.IsYou = false;
                            EndGameResult.CachedWinners.Add(jestData);
>>>>>>> Stashed changes
                            return;
                        }
                    }
                    else if (type == RoleEnum.Executioner)
                    {
                        var executioner = (Executioner)role;
                        if (executioner.TargetVotedOut)
                        {
<<<<<<< Updated upstream
                            TempData.winners = new List<WinningPlayerData>();
                            var exeData = new WinningPlayerData(executioner.Player.Data);
                            if (PlayerControl.LocalPlayer != executioner.Player) exeData.IsYou = false;
                            TempData.winners.Add(exeData);
=======
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var exeData = new CachedPlayerData(executioner.Player.Data);
                            if (PlayerControl.LocalPlayer != executioner.Player) exeData.IsYou = false;
                            EndGameResult.CachedWinners.Add(exeData);
>>>>>>> Stashed changes
                            return;
                        }
                    }
                    else if (type == RoleEnum.Doomsayer)
                    {
                        var doom = (Doomsayer)role;
                        if (doom.WonByGuessing)
                        {
<<<<<<< Updated upstream
                            TempData.winners = new List<WinningPlayerData>();
                            var doomData = new WinningPlayerData(doom.Player.Data);
                            if (PlayerControl.LocalPlayer != doom.Player) doomData.IsYou = false;
                            TempData.winners.Add(doomData);
=======
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var doomData = new CachedPlayerData(doom.Player.Data);
                            if (PlayerControl.LocalPlayer != doom.Player) doomData.IsYou = false;
                            EndGameResult.CachedWinners.Add(doomData);
                            return;
                        }
                    }
                    else if (type == RoleEnum.SoulCollector)
                    {
                        var sc = (SoulCollector)role;
                        if (sc.CollectedSouls)
                        {
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var scData = new CachedPlayerData(sc.Player.Data);
                            if (PlayerControl.LocalPlayer != sc.Player) scData.IsYou = false;
                            EndGameResult.CachedWinners.Add(scData);
>>>>>>> Stashed changes
                            return;
                        }
                    }
                    else if (type == RoleEnum.Phantom)
                    {
                        var phantom = (Phantom)role;
                        if (phantom.CompletedTasks)
                        {
<<<<<<< Updated upstream
                            TempData.winners = new List<WinningPlayerData>();
                            var phantomData = new WinningPlayerData(phantom.Player.Data);
                            if (PlayerControl.LocalPlayer != phantom.Player) phantomData.IsYou = false;
                            TempData.winners.Add(phantomData);
=======
                            EndGameResult.CachedWinners = new List<CachedPlayerData>();
                            var phantomData = new CachedPlayerData(phantom.Player.Data);
                            if (PlayerControl.LocalPlayer != phantom.Player) phantomData.IsYou = false;
                            EndGameResult.CachedWinners.Add(phantomData);
>>>>>>> Stashed changes
                            return;
                        }
                    }
                }
            }

            foreach (var modifier in Modifier.AllModifiers)
            {
                var type = modifier.ModifierType;

                if (type == ModifierEnum.Lover)
                {
                    var lover = (Lover)modifier;
                    if (lover.LoveCoupleWins)
                    {
                        var otherLover = lover.OtherLover;
<<<<<<< Updated upstream
                        TempData.winners = new List<WinningPlayerData>();
                        var loverOneData = new WinningPlayerData(lover.Player.Data);
                        var loverTwoData = new WinningPlayerData(otherLover.Player.Data);
                        if (PlayerControl.LocalPlayer != lover.Player) loverOneData.IsYou = false;
                        if (PlayerControl.LocalPlayer != otherLover.Player) loverTwoData.IsYou = false;
                        TempData.winners.Add(loverOneData);
                        TempData.winners.Add(loverTwoData);
=======
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var loverOneData = new CachedPlayerData(lover.Player.Data);
                        var loverTwoData = new CachedPlayerData(otherLover.Player.Data);
                        if (PlayerControl.LocalPlayer != lover.Player) loverOneData.IsYou = false;
                        if (PlayerControl.LocalPlayer != otherLover.Player) loverTwoData.IsYou = false;
                        EndGameResult.CachedWinners.Add(loverOneData);
                        EndGameResult.CachedWinners.Add(loverTwoData);
>>>>>>> Stashed changes
                        return;
                    }
                }
            }

            if (Role.VampireWins)
            {
<<<<<<< Updated upstream
                TempData.winners = new List<WinningPlayerData>();
                foreach (var role in Role.GetRoles(RoleEnum.Vampire))
                {
                    var vamp = (Vampire)role;
                    var vampData = new WinningPlayerData(vamp.Player.Data);
                    if (PlayerControl.LocalPlayer != vamp.Player) vampData.IsYou = false;
                    TempData.winners.Add(vampData);
=======
                EndGameResult.CachedWinners = new List<CachedPlayerData>();
                foreach (var role in Role.GetRoles(RoleEnum.Vampire))
                {
                    var vamp = (Vampire)role;
                    var vampData = new CachedPlayerData(vamp.Player.Data);
                    if (PlayerControl.LocalPlayer != vamp.Player) vampData.IsYou = false;
                    EndGameResult.CachedWinners.Add(vampData);
>>>>>>> Stashed changes
                }
            }

            foreach (var role in Role.AllRoles)
            {
                var type = role.RoleType;

                if (type == RoleEnum.Glitch)
                {
                    var glitch = (Glitch)role;
                    if (glitch.GlitchWins)
                    {
<<<<<<< Updated upstream
                        TempData.winners = new List<WinningPlayerData>();
                        var glitchData = new WinningPlayerData(glitch.Player.Data);
                        if (PlayerControl.LocalPlayer != glitch.Player) glitchData.IsYou = false;
                        TempData.winners.Add(glitchData);
=======
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var glitchData = new CachedPlayerData(glitch.Player.Data);
                        if (PlayerControl.LocalPlayer != glitch.Player) glitchData.IsYou = false;
                        EndGameResult.CachedWinners.Add(glitchData);
>>>>>>> Stashed changes
                    }
                }
                else if (type == RoleEnum.Juggernaut)
                {
                    var juggernaut = (Juggernaut)role;
                    if (juggernaut.JuggernautWins)
                    {
<<<<<<< Updated upstream
                        TempData.winners = new List<WinningPlayerData>();
                        var juggData = new WinningPlayerData(juggernaut.Player.Data);
                        if (PlayerControl.LocalPlayer != juggernaut.Player) juggData.IsYou = false;
                        TempData.winners.Add(juggData);
=======
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var juggData = new CachedPlayerData(juggernaut.Player.Data);
                        if (PlayerControl.LocalPlayer != juggernaut.Player) juggData.IsYou = false;
                        EndGameResult.CachedWinners.Add(juggData);
>>>>>>> Stashed changes
                    }
                }
                else if (type == RoleEnum.Arsonist)
                {
                    var arsonist = (Arsonist)role;
                    if (arsonist.ArsonistWins)
                    {
<<<<<<< Updated upstream
                        TempData.winners = new List<WinningPlayerData>();
                        var arsonistData = new WinningPlayerData(arsonist.Player.Data);
                        if (PlayerControl.LocalPlayer != arsonist.Player) arsonistData.IsYou = false;
                        TempData.winners.Add(arsonistData);
=======
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var arsonistData = new CachedPlayerData(arsonist.Player.Data);
                        if (PlayerControl.LocalPlayer != arsonist.Player) arsonistData.IsYou = false;
                        EndGameResult.CachedWinners.Add(arsonistData);
>>>>>>> Stashed changes
                    }
                }
                else if (type == RoleEnum.Plaguebearer)
                {
                    var plaguebearer = (Plaguebearer)role;
                    if (plaguebearer.PlaguebearerWins)
                    {
<<<<<<< Updated upstream
                        TempData.winners = new List<WinningPlayerData>();
                        var pbData = new WinningPlayerData(plaguebearer.Player.Data);
                        if (PlayerControl.LocalPlayer != plaguebearer.Player) pbData.IsYou = false;
                        TempData.winners.Add(pbData);
=======
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var pbData = new CachedPlayerData(plaguebearer.Player.Data);
                        if (PlayerControl.LocalPlayer != plaguebearer.Player) pbData.IsYou = false;
                        EndGameResult.CachedWinners.Add(pbData);
>>>>>>> Stashed changes
                    }
                }
                else if (type == RoleEnum.Pestilence)
                {
                    var pestilence = (Pestilence)role;
                    if (pestilence.PestilenceWins)
                    {
<<<<<<< Updated upstream
                        TempData.winners = new List<WinningPlayerData>();
                        var pestilenceData = new WinningPlayerData(pestilence.Player.Data);
                        if (PlayerControl.LocalPlayer != pestilence.Player) pestilenceData.IsYou = false;
                        TempData.winners.Add(pestilenceData);
=======
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var pestilenceData = new CachedPlayerData(pestilence.Player.Data);
                        if (PlayerControl.LocalPlayer != pestilence.Player) pestilenceData.IsYou = false;
                        EndGameResult.CachedWinners.Add(pestilenceData);
>>>>>>> Stashed changes
                    }
                }
                else if (type == RoleEnum.Werewolf)
                {
                    var werewolf = (Werewolf)role;
                    if (werewolf.WerewolfWins)
                    {
<<<<<<< Updated upstream
                        TempData.winners = new List<WinningPlayerData>();
                        var werewolfData = new WinningPlayerData(werewolf.Player.Data);
                        if (PlayerControl.LocalPlayer != werewolf.Player) werewolfData.IsYou = false;
                        TempData.winners.Add(werewolfData);
=======
                        EndGameResult.CachedWinners = new List<CachedPlayerData>();
                        var werewolfData = new CachedPlayerData(werewolf.Player.Data);
                        if (PlayerControl.LocalPlayer != werewolf.Player) werewolfData.IsYou = false;
                        EndGameResult.CachedWinners.Add(werewolfData);
>>>>>>> Stashed changes
                    }
                }
            }

            foreach (var role in Role.GetRoles(RoleEnum.Survivor))
            {
                var surv = (Survivor)role;
                if (!surv.Player.Data.IsDead && !surv.Player.Data.Disconnected)
                {
<<<<<<< Updated upstream
                    var isImp = TempData.winners.Count != 0 && TempData.winners[0].IsImpostor;
                    var survWinData = new WinningPlayerData(surv.Player.Data);
                    if (isImp) survWinData.IsImpostor = true;
                    if (PlayerControl.LocalPlayer != surv.Player) survWinData.IsYou = false;
                    TempData.winners.Add(survWinData);
=======
                    var isImp = EndGameResult.CachedWinners.Count != 0 && EndGameResult.CachedWinners[0].IsImpostor;
                    var survWinData = new CachedPlayerData(surv.Player.Data);
                    if (isImp) survWinData.IsImpostor = true;
                    if (PlayerControl.LocalPlayer != surv.Player) survWinData.IsYou = false;
                    EndGameResult.CachedWinners.Add(survWinData);
>>>>>>> Stashed changes
                }
            }
            foreach (var role in Role.GetRoles(RoleEnum.GuardianAngel))
            {
                var ga = (GuardianAngel)role;
<<<<<<< Updated upstream
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
=======
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
>>>>>>> Stashed changes
                    }
                }
            }
        }
    }
}
