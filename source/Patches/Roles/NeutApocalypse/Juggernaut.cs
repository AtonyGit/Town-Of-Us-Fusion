﻿using System;
using System.Linq;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Juggernaut : Role
    {
        public Juggernaut(PlayerControl owner) : base(owner)
        {
            Name = "Juggernaut";
            Color = Patches.Colors.Apocalypse;
            LastKilled = DateTime.UtcNow;
            RoleType = RoleEnum.Juggernaut;
            AddToRoleHistory(RoleType);
            ImpostorText = () => "Your Power Grows With Every Kill";
            TaskText = () => "With each kill your kill cooldown decreases\nFake Tasks:";
            Faction = Faction.NeutralApocalypse;
        }

        public PlayerControl ClosestPlayer;
        public DateTime LastKilled { get; set; }
        public bool JuggernautWins { get; set; }
        public int JuggKills { get; set; } = 0;
        public bool CanTransform => JuggKills >= CustomGameOptions.KillsNeeded;


        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralApocalypse))) == 1)
            {
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                Utils.EndGame();
                return false;
            }
            else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling)) && !x.Is(Faction.NeutralApocalypse)) == 0)
            {
                var apocAlives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.NeutralApocalypse)).ToList();
                if (apocAlives.Count == 1) return false;
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                Utils.EndGame();
                return false;
            }
            else
            {
                var apocAlives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.NeutralApocalypse)).ToList();
                if (apocAlives.Count == 1 || apocAlives.Count == 2) return false;
                var alives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
                var killersAlive = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(Faction.NeutralApocalypse) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling))).ToList();
                if (killersAlive.Count > 0) return false;
                if (alives.Count <= 6)
                {
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                    Utils.EndGame();
                    return false;
                }
                return false;
            }
        }

        public void Wins()
        {
            JuggernautWins = true;
        }

        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
            var apocTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            foreach (var role in GetRoles(RoleEnum.Juggernaut))
            {
                var apocRole = (Juggernaut)role;
                apocTeam.Add(apocRole.Player);
            }
            foreach (var role in GetRoles(RoleEnum.SoulCollector))
            {
                var apocRole = (SoulCollector)role;
                apocTeam.Add(apocRole.Player);
            }
            foreach (var role in GetRoles(RoleEnum.Plaguebearer))
            {
                var apocRole = (Plaguebearer)role;
                apocTeam.Add(apocRole.Player);
            }
            /*foreach (var role in GetRoles(RoleEnum.Baker))
            {
                var apocRole = (Baker)role;
                apocTeam.Add(apocRole.Player);
            }*/
            __instance.teamToShow = apocTeam;
        }
        public void TurnArmaggeddon()
        {
            var oldRole = GetRole(Player);
            var killsList = (oldRole.CorrectAssassinKills, oldRole.IncorrectAssassinKills);
            RoleDictionary.Remove(Player.PlayerId);
            var role = new Armaggeddon(Player);
            role.Invincible = true;
            role.Transformed = true;
            role.CorrectAssassinKills = killsList.CorrectAssassinKills;
            role.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
            if (Player == PlayerControl.LocalPlayer)
            {
                Coroutines.Start(Utils.FlashCoroutine(Patches.Colors.Apocalypse));
                role.RegenTask();
            }
        }

        public float KillTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastKilled;
            var num = (CustomGameOptions.JuggKCd - CustomGameOptions.ReducedKCdPerKill * JuggKills) * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}