using TMPro;
using System;
using UnityEngine;
using System.Linq;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Patches;

namespace TownOfUsFusion.Roles
{
    public class SerialKiller : Role
    {
        public DateTime LastKill;
        public SerialKiller(PlayerControl player) : base(player)
        {
            Name = "Serial Killer";
            ImpostorText = () => "Hunt Down Your Targets";
            TaskText = () => 
                Target == null ? "Kill your given targets for a reduced kill cooldown" : "Hunt Down " + Target.GetDefaultOutfit().PlayerName;
            Color = Patches.Colors.SerialKiller;
            RoleType = RoleEnum.SerialKiller;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralKilling;
            LastKill = DateTime.UtcNow;
            BloodlustEnd = DateTime.UtcNow;
        }
        public PlayerControl ClosestPlayer;

        public TextMeshPro BloodlustCooldown;
        public DateTime BloodlustEnd;
        public PlayerControl Target = null;
        public bool Scavenging = false;
        public bool GameStarted = false;
        public ArrowBehaviour PreyArrow;

        public bool SkWins;
        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralKilling) || x.IsCrewKiller())) == 1)
            {
                Utils.Rpc(CustomRPC.SkWin, Player.PlayerId);
                Wins();
                Utils.EndGame();
                return false;
            }

            return false;
        }

        public void Wins()
        {
            SkWins = true;
        }

        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
            var skTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            skTeam.Add(PlayerControl.LocalPlayer);
            __instance.teamToShow = skTeam;
        }
        public float KillTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastKill;
            var num = CustomGameOptions.SkKillCooldown * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public float BloodlustTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = BloodlustEnd - utcNow;
            var flag = (float)timeSpan.TotalMilliseconds < 0f;
            if (flag) return 0;
            return ((float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public PlayerControl GetClosestPlayer(PlayerControl toRemove = null)
        {
            var targets = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && x != toRemove && x != ShowRoundOneShield.FirstRoundShielded).ToList();
            if (Player.IsLover()) targets = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(AllianceEnum.Lover) && x != toRemove && x != ShowRoundOneShield.FirstRoundShielded).ToList();
            if (targets.Count == 0) targets = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && x != PlayerControl.LocalPlayer && !x.Is(AllianceEnum.Lover) && x != toRemove).ToList();
            if (targets.Count == 0) targets = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && x != toRemove).ToList();

            var num = double.MaxValue;
            var refPosition = Player.GetTruePosition();
            PlayerControl result = null;
            foreach (var player in targets)
            {
                if (player.Data.IsDead || player.Data.Disconnected || player.PlayerId == Player.PlayerId) continue;
                var playerPosition = player.GetTruePosition();
                var distBetweenPlayers = Vector2.Distance(refPosition, playerPosition);
                var isClosest = distBetweenPlayers < num;
                if (!isClosest) continue;
                var vector = playerPosition - refPosition;
                num = distBetweenPlayers;
                result = player;
            }

            return result;
        }

        public void StopBloodlust()
        {
            Scavenging = false;
            Target = null;
            if (PreyArrow != null) UnityEngine.Object.Destroy(PreyArrow);
            if (PreyArrow.gameObject != null) UnityEngine.Object.Destroy(PreyArrow.gameObject);
            PreyArrow = null;
            RegenTask();
        }
    }
}