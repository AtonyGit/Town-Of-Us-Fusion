namespace TownOfUsFusion.Roles
{
    public class Altruist : Role
{
    public bool CurrentlyReviving;
    public DeadBody CurrentTarget;

    public bool ReviveUsed;

    public Altruist(PlayerControl player) : base(player)
    {
        Name = "Altruist";
        ImpostorText = () => "Sacrifice Yourself To Save Another";
        TaskText = () => "Revive a dead body at the cost of your own life";
        AlignmentText = () => "Crew Protective";
        Color = Patches.Colors.Altruist;
        RoleType = RoleEnum.Altruist;
        AddToRoleHistory(RoleType);
    }
}
}