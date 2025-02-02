using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class Lookout : Role
    {
        public PlayerControl ClosestPlayer;
        public DateTime LastWatched { get; set; }
        public DateTime LastPercepted { get; set; }

        private KillButton _perceptButton;
        public bool Enabled;
        public float PerceptTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastPercepted;
            var num = CustomGameOptions.PerceptCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }
        public int PerceptUsesLeft;
        public bool PerceptButtonUsable => PerceptUsesLeft != 0;
        public float TimeRemaining;

        public int UsesLeft;

        public bool ButtonUsable => UsesLeft != 0;
        public Dictionary<byte, List<RoleEnum>> Watching { get; set; } = new();

        public Lookout(PlayerControl player) : base(player)
        {
            Name = "Lookout";
            ImpostorText = () => "Keep Your Eyes Wide Open";
            TaskText = () => "Watch other crewmates";
            Color = Patches.Colors.Lookout;
            LastWatched = DateTime.UtcNow;
            RoleType = RoleEnum.Lookout;
            AddToRoleHistory(RoleType);

            UsesLeft = CustomGameOptions.MaxWatches;
            PerceptUsesLeft = CustomGameOptions.MaxPercepts;
        }

        public float WatchTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastWatched;
            var num = CustomGameOptions.WatchCooldown * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

        public bool Percepting => TimeRemaining > 0f;
        public void Percept()
        {
            Enabled = true;
            TimeRemaining -= Time.deltaTime;
            if (Player.Data.IsDead)
            {
                TimeRemaining = 0f;
            }
        }

        public void Unpercept()
        {
            Enabled = false;
            LastPercepted = DateTime.UtcNow;
        }
        public KillButton PerceptButton
        {
            get => _perceptButton;
            set
            {
                _perceptButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }
    }
}