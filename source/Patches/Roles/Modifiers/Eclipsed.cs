namespace TownOfUsFusion.Roles.Modifiers
{
    public class Eclipsed : Modifier
{
    public Eclipsed(PlayerControl player) : base(player)
    {
        Name = "Eclipsed";
        TaskText = () => "You can only see in the dark, and are blind in the light";
        Color = Patches.Colors.Eclipsed;
        ModifierType = ModifierEnum.Eclipsed;
    }
}
}