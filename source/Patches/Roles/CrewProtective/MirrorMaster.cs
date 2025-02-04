using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace TownOfUsFusion.Roles
{
    public class MirrorMaster : Role
    {
        public KillButton _absorbButton;
        public readonly List<GameObject> Buttons = new List<GameObject>();
        public DateTime StartingCooldown { get; set; }
        public int Brilders { get; set; }
        public bool Enabled { get; set; }
        public DateTime LastUnleashed { get; set; }
        public DateTime LastAbsorbed { get; set; }
        public float TimeRemaining { get; set; }

        public int AbsorbUsesLeft;
        public bool AbsorbUsable => AbsorbUsesLeft != 0;
        public int UnleashUsesLeft;
        public bool UnleashUsable => UnleashUsesLeft != 0;
        public bool Absorbed = false;
        public bool Absorbing => ShieldedPlayer != null;

        private AbilityButton _dummyAbsorbButton;
        public AbilityButton DummyAbsorbButton
        {
            get => _dummyAbsorbButton;
            set
            {
                _dummyAbsorbButton = value;
            }
        }
        private AbilityButton _dummyUnleashButton;
        public AbilityButton DummyUnleashButton
        {
            get => _dummyUnleashButton;
            set
            {
                _dummyUnleashButton = value;
            }
        }
        public MirrorMaster(PlayerControl player) : base(player)
        {
            Name = "Mirror Master";
            ImpostorText = () => "Redirect Others' Attacks";
            TaskText = () => "Save crewmates from direct attacks, then unleash them onto others!";
            Color = Patches.Colors.MirrorMaster;
            StartingCooldown = DateTime.UtcNow;
            LastUnleashed = DateTime.UtcNow;
            RoleType = RoleEnum.MirrorMaster;
            Faction = Faction.Crewmates;
            AddToRoleHistory(RoleType);
            ShieldedPlayer = null;
            AbsorbUsesLeft = CustomGameOptions.MaxMirrors;
        }

        public KillButton AbsorbButton
        {
            get => _absorbButton;
            set
            {
                _absorbButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public float UnleashTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastUnleashed;
            var num = CustomGameOptions.MirrorUnleashCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
        public float AbsorbTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastAbsorbed;
            var num = CustomGameOptions.MirrorAbsorbCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public PlayerControl ClosestPlayer;
        public bool UsedAbility { get; set; } = false;
        public PlayerControl ShieldedPlayer { get; set; }
        public PlayerControl exShielded { get; set; }
    }
}