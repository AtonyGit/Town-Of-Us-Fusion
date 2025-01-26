using UnityEngine;

namespace TownOfUsFusion.Roles.Modifiers
{
    public class Oblivious : Modifier
    {
        public Oblivious(PlayerControl player) : base(player)
        {
            Name = "Oblivious";
            TaskText = () => "Your report button does not light up";
            Color = Patches.Colors.Oblivious;
            ModifierType = ModifierEnum.Oblivious;
        }
    }
}