namespace TownOfUsFusion.Modifiers;

public class HighPriorityModifier : GameModifier
{
    public override string ModifierName => "High Priority";
    public override int GetAssignmentChance()
    {
        return 100;
    }

    public override int GetAmountPerGame()
    {
        return 1;
    }

    public override int Priority()
    {
        return 100;
    }
}
