using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class Captain : Role
    {
        public List<byte> EjectedPlayers = new List<byte>();
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
            EjectionsPerTribunal = CustomGameOptions.MaxTribunalEjects;
        }
        public GameObject TribunalButton = new GameObject();
        public int TribunalsLeft;
        public int EjectionsPerTribunal;
        internal override bool Criteria()
        {
            return HasRevealed && !Player.Data.IsDead || base.Criteria();
        }

        internal override bool RoleCriteria()
        {
            if (!Player.Data.IsDead) return HasRevealed || base.RoleCriteria();
            return false || base.RoleCriteria();
        }
        public bool TribunalThisMeeting { get; set; }
        public bool Tribunaled { get; set; }
        public bool HasTribunaled => TribunalsLeft != 0;
        public bool StartTribunal { get; set; }
        public bool HasRevealed { get; set; }
        public PlayerVoteArea Tribunal { get; set; }

        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected || !CustomGameOptions.CrewKillersContinue) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected && x.Data.IsImpostor()) > 0 && !HasTribunaled) return false;

            return true;
        }
    }
}
