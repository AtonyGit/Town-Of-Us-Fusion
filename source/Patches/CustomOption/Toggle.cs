<<<<<<< Updated upstream
namespace TownOfUsFusion.CustomOption
=======
using System;

namespace TownOfUsFusion.CustomOption
>>>>>>> Stashed changes
{
    public class CustomToggleOption : CustomOption
    {
        protected internal CustomToggleOption(int id, MultiMenu menu, string name, bool value = true) : base(id, menu, name,
            CustomOptionType.Toggle,
            value)
        {
<<<<<<< Updated upstream
            Format = val => (bool) val ? "On" : "Off";
=======
            Format = val => (bool)val ? "On" : "Off";
>>>>>>> Stashed changes
        }

        protected internal bool Get()
        {
<<<<<<< Updated upstream
            return (bool) Value;
=======
            return (bool)Value;
>>>>>>> Stashed changes
        }

        protected internal void Toggle()
        {
            Set(!Get());
        }

        public override void OptionCreated()
        {
            base.OptionCreated();
<<<<<<< Updated upstream
            Setting.Cast<ToggleOption>().TitleText.text = Name;
            Setting.Cast<ToggleOption>().CheckMark.enabled = Get();
=======
            var tgl = Setting.Cast<ToggleOption>();
            tgl.CheckMark.enabled = Get();
>>>>>>> Stashed changes
        }
    }
}