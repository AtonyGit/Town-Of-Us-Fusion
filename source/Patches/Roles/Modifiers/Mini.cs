using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Roles.Modifiers
{
    public class Mini : Modifier, IVisualAlteration
    {
        public Mini(PlayerControl player) : base(player)
        {
            var slowText = CustomGameOptions.MiniSpeed >= 1.50 ? " and fast!" : "!";
            Name = "Mini";
            TaskText = () => "You are tiny" + slowText;
            Color = Patches.Colors.Mini;
            ModifierType = ModifierEnum.Mini;
        }

        public bool TryGetModifiedAppearance(out VisualAppearance appearance)
        {
            appearance = Player.GetDefaultAppearance();
            appearance.SpeedFactor = CustomGameOptions.MiniSpeed;
            appearance.SizeFactor = new Vector3(0.40f, 0.40f, 1f);
            return true;
        }
    }
}
