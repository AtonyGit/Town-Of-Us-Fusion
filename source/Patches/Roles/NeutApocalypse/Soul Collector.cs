using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.NeutralRoles.SoulCollectorMod;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class SoulCollector : Role
    {
        private KillButton _reapButton;
        public PlayerControl ClosestPlayer;
        public DateTime LastReaped { get; set; }
        public Soul CurrentTarget;
        public List<GameObject> Souls = new List<GameObject>();
        public bool CollectedSouls = false;
        public int SoulsCollected = 0;
        public List<byte> ReapedPlayers = new List<byte>();
        //public TextMeshPro CollectedText { get; set; }
        private AbilityButton _dummyButton;
        public AbilityButton DummyButton
        {
            get => _dummyButton;
            set
            {
                _dummyButton = value;
            }
        }

        public SoulCollector(PlayerControl player) : base(player)
        {
            Name = "Soul Collector";
            ImpostorText = () => "Collect Souls";
            TaskText = () => "Collect souls to win the game";
            Color = Patches.Colors.Apocalypse;
            LastReaped = DateTime.UtcNow;
            RoleType = RoleEnum.SoulCollector;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralApocalypse;
        }

        public KillButton ReapButton
        {
            get => _reapButton;
            set
            {
                _reapButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public float ReapTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastReaped;
            var num = CustomGameOptions.ReapCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;
            
            
            if (CollectedSouls) {
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                Utils.EndGame();
                return false;
            }

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

        /*public void Wins()
        {
            SoulCollectorWins = true;
        }*/

        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
            var apocTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
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
            foreach (var role in GetRoles(RoleEnum.Juggernaut))
            {
                var apocRole = (Juggernaut)role;
                apocTeam.Add(apocRole.Player);
            }
            /*foreach (var role in GetRoles(RoleEnum.Baker))
            {
                var apocRole = (Baker)role;
                apocTeam.Add(apocRole.Player);
            }*/
            __instance.teamToShow = apocTeam;
        }
    }
}