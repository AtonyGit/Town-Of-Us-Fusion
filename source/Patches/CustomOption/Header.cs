using System.Drawing;

namespace TownOfUsFusion.CustomOption
{
    public class CustomHeaderOption : CustomOption
    {
        protected internal CustomHeaderOption(int id, MultiMenu menu, string name) : base(id, menu, name, CustomOptionType.Header, 0)
        {
        }

        public override void OptionCreated()
        {
            base.OptionCreated();
            Setting.Cast<ToggleOption>().TitleText.text = Name;
            Setting.Cast<ToggleOption>().TitleText.outlineColor = new UnityEngine.Color(0f, 0f, 0f, 1f);
            Setting.Cast<ToggleOption>().TitleText.outlineWidth = 2;
        }
    }
}