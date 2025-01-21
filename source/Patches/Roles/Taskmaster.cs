using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class Taskmaster : Role
{

    public Dictionary<byte, ArrowBehaviour> TaskmasterArrows = new Dictionary<byte, ArrowBehaviour>();
    public List<ArrowBehaviour> ImpArrows = new List<ArrowBehaviour>();
    public bool hasExtraTasks;

    public Taskmaster(PlayerControl player) : base(player)
    {
        Name = "Taskmaster";
        ImpostorText = () => "Finish All The Tasks";
        TaskText = () =>
            hasExtraTasks
                ? "Complete the master tasks to win for the Crew!"
                : "Complete your current tasks to get master tasks!";
        AlignmentText = () => "Crew Support";
        Color = Patches.Colors.Taskmaster;
        RoleType = RoleEnum.Taskmaster;
        AddToRoleHistory(RoleType);
    }
    public bool Revealed => TasksLeft <= CustomGameOptions.TMTasksRemaining && hasExtraTasks;
    public bool TasksDone => TasksLeft <= 0 && hasExtraTasks;

    internal override bool Criteria()
    {
        return Revealed && PlayerControl.LocalPlayer.Data.IsImpostor() && !Player.Data.IsDead ||
               base.Criteria();
    }

    internal override bool RoleCriteria()
    {
        var localPlayer = PlayerControl.LocalPlayer;
        if ((localPlayer.Data.IsImpostor() || GetRole(localPlayer).Faction == Faction.ImpSentinel) && !Player.Data.IsDead)
        {
            return Revealed || base.RoleCriteria();
        }
        else if ((GetRole(localPlayer).Faction == Faction.NeutralKilling || GetRole(localPlayer).Faction == Faction.NeutralApocalypse || GetRole(localPlayer).Faction == Faction.NeutralNeophyte || GetRole(localPlayer).Faction == Faction.NeutralNecro
        || GetRole(localPlayer).Faction == Faction.NeutralSentinel || GetRole(localPlayer).Faction == Faction.ChaosSentinel) && !Player.Data.IsDead)
        {
            return Revealed && CustomGameOptions.SnitchSeesNeutrals || base.RoleCriteria();
        }
        return false || base.RoleCriteria();
    }

    public void DestroyArrow(byte targetPlayerId)
    {
        var arrow = TaskmasterArrows.FirstOrDefault(x => x.Key == targetPlayerId);
        if (arrow.Value != null)
            Object.Destroy(arrow.Value);
        if (arrow.Value.gameObject != null)
            Object.Destroy(arrow.Value.gameObject);
        TaskmasterArrows.Remove(arrow.Key);
    }
}
}