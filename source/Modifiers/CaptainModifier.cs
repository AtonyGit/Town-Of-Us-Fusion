namespace TownOfUsFusion.Modifiers;

public class CaptainModifier : GameModifier
{
    public override string ModifierName => "Captain";

    public override int GetAmountPerGame()
    {
        return 1;
    }

    public override int GetAssignmentChance()
    {
        return 100;
    }
}
