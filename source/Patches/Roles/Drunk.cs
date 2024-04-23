using UnityEngine;

namespace TownOfUsFusion.Roles.Modifiers
{
    public class Drunk : Modifier
    {
        public Drunk(PlayerControl player) : base(player)
        {
            Name = "Drunk";
            TaskText = () => /*Inverrrrrted contrrrrols */"slorrrtnoc detrrrrevnI";
            //Color = new Color(0.46f, 0.5f, 0f, 1f);
            Color = Patches.Colors.Drunk;
            ModifierType = ModifierEnum.Drunk;
        }
    }
}