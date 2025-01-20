using System;
using System.Collections.Generic;

namespace TownOfUs.Roles
{
    public class Detective : Role
    {
        private KillButton _examineButton;
        public PlayerControl ClosestPlayer;
        public DateTime LastExamined { get; set; }
<<<<<<< Updated upstream
        public DeadBody CurrentTarget;
        public List<byte> DetectedKillers = new List<byte>();
        public PlayerControl LastKiller;
=======
        public CrimeScene CurrentTarget;
        public CrimeScene InvestigatingScene;
        public List<byte> InvestigatedPlayers = new List<byte>();
        public List<GameObject> CrimeScenes = new List<GameObject>();
>>>>>>> Stashed changes

        public Detective(PlayerControl player) : base(player)
        {
            Name = "Detective";
<<<<<<< Updated upstream
            ImpostorText = () => "Find A Body Then Examine Players To Find Blood";
            TaskText = () => "Examine suspicious players to find evildoers";
=======
            ImpostorText = () => "Inspect Crime Scenes To Catch The Killer";
            TaskText = () => "Inspect crime scenes, then examine players for clues";
>>>>>>> Stashed changes
            Color = Patches.Colors.Detective;
            LastExamined = DateTime.UtcNow;
            RoleType = RoleEnum.Detective;
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