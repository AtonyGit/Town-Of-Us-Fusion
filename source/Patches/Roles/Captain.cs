using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Captain : Role
    {
        public Captain(PlayerControl player) : base(player)
        {
            Name = "Captain";
            ImpostorText = () => "Look At Me, I'm The Captain Now";
            TaskText = () => "Choose to exile two players in a single meeting";
            Color = Patches.Colors.Captain;
            RoleType = RoleEnum.Captain;
            AddToRoleHistory(RoleType);
            StartTribunal = false;
            Tribunaled = false;
            TribunalThisMeeting = false;
            TribunalsLeft = CustomGameOptions.MaxTribunals;
        }
        public int TribunalsLeft;
        public bool TribunalThisMeeting { get; set; }
        public bool Tribunaled { get; set; }
        public bool HasTribunaled => TribunalsLeft != 0;
        public bool StartTribunal { get; set; }
        public PlayerVoteArea Tribunal { get; set; }

        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected || !CustomGameOptions.CrewKillersContinue) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected && x.Data.IsImpostor()) > 0 && !HasTribunaled) return false;

            return true;
        }
    }
}
