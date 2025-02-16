using System;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities;
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
            TaskText = () => "Collect souls to become Death.\nFake Tasks:";
            Color = Patches.Colors.Apocalypse;
            AbilitySprite = TownOfUsFusion.CollectSprite;
            AbilityText = "Collect";
            SecondAbilityButton = ReapButton;
            SecondAbilitySprite = TownOfUsFusion.ReapSprite;
            SecondAbilityText = "Reap";
            VentSprite = TownOfUsFusion.ApocVent;
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
        public void TurnDeath()
        {
            var oldRole = GetRole(Player);
            var killsList = (oldRole.CorrectAssassinKills, oldRole.IncorrectAssassinKills);
            RoleDictionary.Remove(Player.PlayerId);
            var role = new Death(Player);
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
    }
}