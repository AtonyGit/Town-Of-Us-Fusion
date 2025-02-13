using System;
using System.Linq;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Patches;

namespace TownOfUsFusion.Roles
{
    public class Armaggeddon : Role
    {
        public Armaggeddon(PlayerControl owner) : base(owner)
        {
            Name = "Armaggeddon";
            Color = Patches.Colors.Apocalypse;
            LastKilled = DateTime.UtcNow;
            RoleType = RoleEnum.Armaggeddon;
            AddToRoleHistory(RoleType);
            ImpostorText = () => "";
            TaskText = () => "Wreak havoc and destroy the weak!\nFake Tasks:";
            Faction = Faction.NeutralApocalypse;
            Invincible = true;
            Transformed = true;
        }

        public PlayerControl ClosestPlayer;
        public void TriggerDestroy()
        {
            var playersToDie = Utils.GetClosestPlayers(Player.GetTruePosition(), CustomGameOptions.DestroyRadius, false);
            playersToDie = Shuffle(playersToDie);
            while (playersToDie.Count > CustomGameOptions.MaxKillsInDestruction) playersToDie.Remove(playersToDie[playersToDie.Count - 1]);
            foreach (var player in playersToDie)
            {
                if (!player.IsInvincible() && !player.Is(Faction.NeutralApocalypse) && !player.IsShielded() && !player.IsProtected() && player != ShowRoundOneShield.FirstRoundShielded)
                {
                    Utils.RpcMultiMurderPlayer(Player, player);
                }
                else if (player.IsShielded())
                {
                    var medic = player.GetMedic().Player.PlayerId;
                    Utils.Rpc(CustomRPC.AttemptSound, medic, player.PlayerId);
                    StopKill.BreakShield(medic, player.PlayerId, CustomGameOptions.ShieldBreaks);
                }
            }
        }
        public static Il2CppSystem.Collections.Generic.List<PlayerControl> Shuffle(Il2CppSystem.Collections.Generic.List<PlayerControl> playersToDie)
        {
            var count = playersToDie.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = playersToDie[i];
                playersToDie[i] = playersToDie[r];
                playersToDie[r] = tmp;
            }
            return playersToDie;
        }
        public DateTime LastKilled { get; set; }
        public bool ArmaggeddonWins { get; set; }

        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralApocalypse))) == 1)
            {
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                Utils.EndGame();
                return false;
            }
            else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling)) && !x.Is(Faction.NeutralApocalypse)) == 0)
            {
                var apocAlives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.NeutralApocalypse)).ToList();
                if (apocAlives.Count == 1) return false;
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                Utils.EndGame();
                return false;
            }
            else
            {
                var apocAlives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.NeutralApocalypse)).ToList();
                if (apocAlives.Count == 1 || apocAlives.Count == 2) return false;
                var alives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
                var killersAlive = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(Faction.NeutralApocalypse) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling))).ToList();
                if (killersAlive.Count > 0) return false;
                if (alives.Count <= 6)
                {
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                    Utils.EndGame();
                    return false;
                }
                return false;
            }
        }

        public void Wins()
        {
            ArmaggeddonWins = true;
        }

        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
            var apocTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            __instance.teamToShow = apocTeam;
        }

        public float KillTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastKilled;
            var num = (CustomGameOptions.ArmKillCd) * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}