using System;
using System.Collections.Generic;

namespace TownOfUsFusion.Roles
{
    public class Psychic : Role
    {
        public List<byte> Investigated = new List<byte>();
        public PlayerControl Confessor;
        public float Accuracy;
        public bool FirstMeetingDead;
        public Faction RevealedFaction;

        public Psychic(PlayerControl player) : base(player)
        {
            Name = "Psychic";
            ImpostorText = () => "Reveal The Alliance Of Other Players";
            TaskText = () => "Reveal alliances of other players to find Evils";
            Color = Patches.Colors.Psychic;
            LastInvestigated = DateTime.UtcNow;
            Accuracy = CustomGameOptions.RevealAccuracy;
            FirstMeetingDead = true;
            FirstMeetingDead = false;
            RoleType = RoleEnum.Psychic;
            AddToRoleHistory(RoleType);
        }

        public PlayerControl ClosestPlayer;
        public DateTime LastInvestigated { get; set; }

        public float PsychicTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastInvestigated;
            var num = CustomGameOptions.PsychicCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}