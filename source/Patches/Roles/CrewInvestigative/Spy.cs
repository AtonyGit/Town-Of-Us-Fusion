using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class Operative : Role
    {
        public List<ArrowBehaviour> ImpArrows = new List<ArrowBehaviour>();

        public Dictionary<byte, ArrowBehaviour> OperativeArrows = new Dictionary<byte, ArrowBehaviour>();

        public System.DateTime LastAdmin { get; set; }
        public float AdminTimer()
        {
            var utcNow = System.DateTime.UtcNow;
            var timeSpan = utcNow - LastAdmin;
            var num = CustomGameOptions.AdminCooldown * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

        public Operative(PlayerControl player) : base(player)
        {
            Name = "Operative";
            ImpostorText = () => "Snoop Around And Find Stuff Out";
            TaskText = () =>
                TasksDone
                    ? "Find the arrows pointing to the Impostors!"
                    : "Gain extra info on the Admin Table, & reveal the Impostors by completing tasks!";
            Color = Patches.Colors.Operative;
            RoleType = RoleEnum.Operative;
            AddToRoleHistory(RoleType);
        }
        public bool Revealed => TasksLeft <= CustomGameOptions.OperativeTasksRemaining;
        public bool TasksDone => TasksLeft <= 0;

        internal override bool Criteria()
        {
            return Revealed && PlayerControl.LocalPlayer.Data.IsImpostor() && !Player.Data.IsDead ||
                   base.Criteria();
        }

        internal override bool RoleCriteria()
        {
            var localPlayer = PlayerControl.LocalPlayer;
            if (localPlayer.Data.IsImpostor() && !Player.Data.IsDead)
            {
                return Revealed || base.RoleCriteria();
            }
            else if ((GetRole(localPlayer).Faction == Faction.NeutralKilling || GetRole(localPlayer).Faction == Faction.NeutralNeophyte || GetRole(localPlayer).Faction == Faction.NeutralApocalypse) && !Player.Data.IsDead)
            {
                return Revealed && CustomGameOptions.OperativeSeesNeutrals || base.RoleCriteria();
            }
            return false || base.RoleCriteria();
        }

        public void DestroyArrow(byte targetPlayerId)
        {
            var arrow = OperativeArrows.FirstOrDefault(x => x.Key == targetPlayerId);
            if (arrow.Value != null)
                Object.Destroy(arrow.Value);
            if (arrow.Value.gameObject != null)
                Object.Destroy(arrow.Value.gameObject);
            OperativeArrows.Remove(arrow.Key);
        }
    }
}