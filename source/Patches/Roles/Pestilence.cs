using System;
using System.Linq;
<<<<<<< Updated upstream
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
=======
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
>>>>>>> Stashed changes
{
    public class Pestilence : Role
    {
        public Pestilence(PlayerControl owner) : base(owner)
        {
            Name = "Pestilence";
            Color = Patches.Colors.Pestilence;
            LastKill = DateTime.UtcNow;
            RoleType = RoleEnum.Pestilence;
            AddToRoleHistory(RoleType);
            ImpostorText = () => "";
            TaskText = () => "Kill everyone with your unstoppable abilities!\nFake Tasks:";
            Faction = Faction.NeutralKilling;
        }

        public PlayerControl ClosestPlayer;
        public DateTime LastKill { get; set; }
        public bool PestilenceWins { get; set; }

<<<<<<< Updated upstream
        internal override bool NeutralWin(LogicGameFlowNormal __instance)
=======
        internal override bool GameEnd(LogicGameFlowNormal __instance)
>>>>>>> Stashed changes
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
<<<<<<< Updated upstream
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralKilling))) == 1)
=======
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralKilling) || x.IsCrewKiller())) == 1)
>>>>>>> Stashed changes
            {
                Utils.Rpc(CustomRPC.PestilenceWin, Player.PlayerId);
                Wins();
                Utils.EndGame();
                return false;
            }

            return false;
        }

        public void Wins()
        {
            PestilenceWins = true;
        }

        public float KillTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastKill;
            var num = CustomGameOptions.PestKillCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}