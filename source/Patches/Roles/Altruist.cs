using System;
using TMPro;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class Altruist : Role
    {
        public DeadBody CurrentTarget;
        public DateTime LastRevived { get; set; }
        public bool Enabled;

        public int RevivesLeft;
        public float TimeRemaining;
        
        public Altruist(PlayerControl player) : base(player)
        {
            Name = "Altruist";
            ImpostorText = () => "Sacrifice Yourself To Save Another";
            TaskText = () => "Revive a dead body at the cost of your own life";
            Color = Patches.Colors.Altruist;
            RoleType = RoleEnum.Altruist;
            AddToRoleHistory(RoleType);
            RevivesLeft = CustomGameOptions.AltruistMaxRevives;
        }
        public float ReviveTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastRevived;
            var num = CustomGameOptions.ReviveCooldown * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

        public bool Reviving => TimeRemaining > 0f;
        public void Altruisting()
        {
            Enabled = true;
            TimeRemaining -= Time.deltaTime;
            if (Player.Data.IsDead)
            {
                TimeRemaining = 0f;
            }
        }
        private AbilityButton _dummyButton;
        public AbilityButton DummyButton
        {
            get => _dummyButton;
            set
            {
                _dummyButton = value;
            }
        }

        public void Unaltruisting()
        {
            Enabled = false;
            LastRevived = DateTime.UtcNow;
        }
    }
}