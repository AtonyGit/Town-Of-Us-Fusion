using System;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class CursedSoul : Role
    {
        public CursedSoul(PlayerControl player) : base(player)
        {
            Name = Utils.GradientColorText("79FFB3", "B579FF", "Cursed Soul");
            ImpostorText = () => Utils.GradientColorText("79FFB3", "B579FF", "Pass Your Curse To Others");
            TaskText = () => Utils.GradientColorText("79FFB3", "B579FF", "Steal other people's roles.\nFake Tasks:");
            Color = Patches.Colors.CursedSoul;
            RoleType = RoleEnum.CursedSoul;
            Faction = Faction.NeutralCursed;
        }

        public PlayerControl ClosestPlayer;
        public PlayerControl CursedPlayer;
        public DateTime LastSoulSwapped { get; set; }


        public float SoulSwapTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastSoulSwapped;
            var num = CustomGameOptions.SoulSwapCooldown * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}