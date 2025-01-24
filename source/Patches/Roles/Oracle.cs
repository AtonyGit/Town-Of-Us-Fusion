using System;

namespace TownOfUsFusion.Roles
{
    public class Oracle : Role
    {
        public PlayerControl ClosestPlayer;
        public PlayerControl BlessedPlayer;
        public float Accuracy;
        public bool FirstMeetingDead;
        public Faction RevealedFaction;
        public bool SavedBlessed;
        public DateTime LastBlessed { get; set; }

        public Oracle(PlayerControl player) : base(player)
        {
            Name = "Oracle";
            ImpostorText = () => "Get Other Player's To Confess Their Sins";
            TaskText = () => "Get another player to confess on your passing";
            Color = Patches.Colors.Oracle;
            LastBlessed = DateTime.UtcNow;
            Accuracy = CustomGameOptions.RevealAccuracy;
            FirstMeetingDead = true;
            FirstMeetingDead = false;
            RoleType = RoleEnum.Oracle;
            AddToRoleHistory(RoleType);
        }
        public float BlessTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastBlessed;
            var num = CustomGameOptions.BlessCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
        public float StartTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - StartingCooldown;
            var num = 10000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}