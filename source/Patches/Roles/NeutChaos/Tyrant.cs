using System.Collections.Generic;

namespace TownOfUsFusion.Roles
{
    public class Tyrant : Role
    {
        public List<byte> ExtraVotes = new List<byte>();

        public Tyrant(PlayerControl player) : base(player)
        {
            Name = "Tyrant";
            ImpostorText = () => "Betray The Crew To Regain Political Power";
            TaskText = () => "Reveal your true colors to the neutrals.\nFake Tasks:";
            Color = Patches.Colors.Tyrant;
            RoleType = RoleEnum.Tyrant;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralChaos;
            VoteBank = CustomGameOptions.TyrantVoteBank;
        }

        public int VoteBank { get; set; }
        public bool SelfVote { get; set; }

        public bool VotedOnce { get; set; }
        public bool PlacedVote { get; set; }

        public PlayerVoteArea Abstain { get; set; }

        public bool CanVote => VoteBank > 0 && !SelfVote;
    }
}