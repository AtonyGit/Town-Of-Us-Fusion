using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Roles.Modifiers
{
    public class Dwarf : Modifier, IVisualAlteration
{

    public Dwarf(PlayerControl player) : base(player)
    {
        Name = "Dwarf";
        TaskText = () => "Size isn't what truly matters!";
        Color = Patches.Colors.Dwarf;
        ModifierType = ModifierEnum.Dwarf;
    }

    public bool TryGetModifiedAppearance(out VisualAppearance appearance)
    {
        appearance = Player.GetDefaultAppearance();
        appearance.SpeedFactor = CustomGameOptions.DwarfSpeed;
        appearance.SizeFactor = new Vector3(0.55f, 0.55f, 1.0f);
        return true;
    }
}
}