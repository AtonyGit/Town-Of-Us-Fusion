using System;
using System.Collections.Generic;
using TownOfUsFusion.CrewmateRoles.InvestigatorMod;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class Investigator : Role
    {
        private KillButton _examineButton;
        public PlayerControl ClosestPlayer;
        public DateTime LastExamined { get; set; }
        public CrimeScene CurrentTarget;
        public CrimeScene InvestigatingScene;
        public List<byte> InvestigatedPlayers = new List<byte>();
        public List<GameObject> CrimeScenes = new List<GameObject>();

        public Investigator(PlayerControl player) : base(player)
        {
            Name = "Investigator";
            ImpostorText = () => "Inspect Crime Scenes To Catch The Killer";
            TaskText = () => "Inspect crime scenes, then examine players for clues";
            Color = Patches.Colors.Investigator;
            LastExamined = DateTime.UtcNow;
            RoleType = RoleEnum.Investigator;
            AddToRoleHistory(RoleType);
        }

        public KillButton ExamineButton
        {
            get => _examineButton;
            set
            {
                _examineButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public float ExamineTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastExamined;
            var num = CustomGameOptions.ExamineCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}