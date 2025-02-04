using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Prosecutor : Role
    {
        public Prosecutor(PlayerControl player) : base(player)
        {
            Name = "Prosecutor";
            ImpostorText = () => "Exile One Person Of Your Choosing";
            //TaskText = () => "Choose to exile anyone you want";
            TaskText = () => $"Choose to exile {(ProsecutesLeft == 1 ? "one last time!" : $"{ProsecutesLeft} people in total!")}";
            Color = Patches.Colors.Prosecutor;
            RoleType = RoleEnum.Prosecutor;
            AddToRoleHistory(RoleType);
            StartProsecute = false;
            Prosecuted = false;
            ProsecuteThisMeeting = false;
            ProsecutesLeft = CustomGameOptions.MaxProsecutes;
        }
        public int ProsecutesLeft;
        public bool ProsecuteThisMeeting { get; set; }
        public bool Prosecuted { get; set; }
        public bool HasProsecuted => ProsecutesLeft != 0;
        public bool StartProsecute { get; set; }
        public PlayerVoteArea Prosecute { get; set; }

        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected || !CustomGameOptions.CrewKillersContinue) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected && x.Data.IsImpostor()) > 0 && !HasProsecuted) return false;

            return true;
        }
    }
}
