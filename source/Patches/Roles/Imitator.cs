using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Imitator : Role
    {
        public readonly List<GameObject> Buttons = new List<GameObject>();

        public readonly List<bool> ListOfActives = new List<bool>();
        public PlayerControl ImitatePlayer = null;
<<<<<<< Updated upstream
=======
        public List<RoleEnum> ImitatableRoles = [RoleEnum.Detective, RoleEnum.Investigator, RoleEnum.Mystic, RoleEnum.Seer, RoleEnum.Spy, RoleEnum.Tracker, RoleEnum.Sheriff,
                            RoleEnum.Veteran, RoleEnum.Altruist, RoleEnum.Engineer, RoleEnum.Medium, RoleEnum.Transporter, RoleEnum.Trapper, RoleEnum.Medic, RoleEnum.Aurial,
                            RoleEnum.Oracle, RoleEnum.Hunter, RoleEnum.Warden];
>>>>>>> Stashed changes

        public List<RoleEnum> trappedPlayers = null;
        public PlayerControl confessingPlayer = null;


        public Imitator(PlayerControl player) : base(player)
        {
            Name = "Imitator";
            ImpostorText = () => "Use The True-Hearted Dead To Benefit The Crew";
            TaskText = () => "Use dead roles to benefit the crew";
            Color = Patches.Colors.Imitator;
            RoleType = RoleEnum.Imitator;
            AddToRoleHistory(RoleType);
        }
<<<<<<< Updated upstream
=======

        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected || !CustomGameOptions.CrewKillersContinue) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected && x.Data.IsImpostor()) > 0 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => x.Data.IsDead && !x.Data.Disconnected &&
                (x.Is(RoleEnum.Hunter) || x.Is(RoleEnum.Sheriff) || x.Is(RoleEnum.Veteran))) > 0) return false;

            return true;
        }
>>>>>>> Stashed changes
    }
}