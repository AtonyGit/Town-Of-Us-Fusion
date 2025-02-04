using System;
using UnityEngine;
using TMPro;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Bodyguard : Role
    {
        public bool Enabled;
        public DateTime LastProtected;
        public float TimeRemaining;

        public int UsesLeft;

        public bool ButtonUsable => UsesLeft != 0;

        public PlayerControl guardedPlayer { get; set; }
        public PlayerControl ClosestPlayer;
        public Bodyguard(PlayerControl player) : base(player)
        {
            Name = "Bodyguard";
            ImpostorText = () => "Get Down Mr President!";
            TaskText = () => "Take Down Killers To Protect Crewmates";
            Color = Patches.Colors.Bodyguard;
            LastProtected = DateTime.UtcNow;
            RoleType = RoleEnum.Bodyguard;
            AddToRoleHistory(RoleType);

            UsesLeft = CustomGameOptions.MaxProtects;
        }

        public bool Protecting => TimeRemaining > 0f;

        public float TargetTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastProtected;
            var num = CustomGameOptions.GuardCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
        public float ProtectTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastProtected;
            var num = CustomGameOptions.GuardCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public void Protect()
        {
            Enabled = true;
            TimeRemaining -= Time.deltaTime;
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
        public void UnProtect()
        {
            var ga = GetRole<Bodyguard>(Player);
            if (!ga.guardedPlayer.IsGuarded())
            {
                ga.guardedPlayer.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
                ga.guardedPlayer.myRend().material.SetFloat("_Outline", 0f);
            }
            Enabled = false;
            LastProtected = DateTime.UtcNow;
        }
    }
}