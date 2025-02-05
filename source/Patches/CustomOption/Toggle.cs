using System;

namespace TownOfUsFusion.CustomOption
{
    public class CustomToggleOption : CustomOption
    {
        protected internal CustomToggleOption(int id, MultiMenu menu, string name, bool value = true) : base(id, menu, name,
            CustomOptionType.Toggle,
            value)
        {
            Format = val => (bool)val ? "On" : "Off";
        }

        protected internal bool Get()
        {
            return (bool)Value;
        }

        protected internal void Toggle()
        {
            Set(!Get());
        }

        public override void OptionCreated()
        {
            base.OptionCreated();
            var tgl = Setting.Cast<ToggleOption>();
            tgl.CheckMark.enabled = Get();
            tgl.TitleText.outlineColor = new UnityEngine.Color(0f, 0f, 0f, 1f);
            tgl.TitleText.outlineWidth = 2;
        }
    }
}