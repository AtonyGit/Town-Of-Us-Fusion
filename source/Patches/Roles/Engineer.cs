using TMPro;

namespace TownOfUsFusion.Roles
{
    public class Engineer : Role
{
    public Engineer(PlayerControl player) : base(player)
    {
        Name = "Engineer";
        ImpostorText = () => "Maintain Important Systems On The Ship";
        TaskText = () => CustomGameOptions.GameMode == GameMode.Cultist ? "Vent around" : "Vent around and fix sabotages";
        AlignmentText = () => "Crew Support";
        Color = Patches.Colors.Engineer;
        RoleType = RoleEnum.Engineer;
        AddToRoleHistory(RoleType);
        UsesLeft = CustomGameOptions.MaxFixes;
    }

    public int UsesLeft;
    public TextMeshPro UsesText;

    public bool ButtonUsable => UsesLeft != 0;
}
}