using System;

namespace TownOfUsFusion.Roles
{
    public class Oracle : Role
    {
        public PlayerControl ClosestPlayer;
        public PlayerControl BlessedPlayer;
        public Faction RevealedFaction;
        public bool SavedBlessed;
        public DateTime LastBlessed { get; set; }

        public Oracle(PlayerControl player) : base(player)
        {
            Name = "Oracle";
            ImpostorText = () => "Bless Thy Fellow Crewmates";
            TaskText = () => "Bless another player to protect them from interactions and ejections.";
            Color = Patches.Colors.Oracle;
            LastBlessed = DateTime.UtcNow;
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
    }
}