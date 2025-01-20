<<<<<<< Updated upstream
namespace TownOfUs.CustomOption
=======
using System;

namespace TownOfUsFusion.CustomOption
>>>>>>> Stashed changes
{
    public class CustomStringOption : CustomOption
    {
        protected internal CustomStringOption(int id, MultiMenu menu, string name, string[] values) : base(id, menu, name,
            CustomOptionType.String,
            0)
        {
            Values = values;
<<<<<<< Updated upstream
            Format = value => Values[(int) value];
=======
            Format = value => Values[(int)value];
>>>>>>> Stashed changes
        }

        protected string[] Values { get; set; }

        protected internal int Get()
        {
<<<<<<< Updated upstream
            return (int) Value;
=======
            return (int)Value;
>>>>>>> Stashed changes
        }

        protected internal void Increase()
        {
<<<<<<< Updated upstream
            if (Get() >= Values.Length)
=======
            if (Get() >= Values.Length - 1)
>>>>>>> Stashed changes
                Set(0);
            else
                Set(Get() + 1);
        }

        protected internal void Decrease()
        {
            if (Get() <= 0)
                Set(Values.Length - 1);
            else
                Set(Get() - 1);
        }

        public override void OptionCreated()
        {
<<<<<<< Updated upstream
            var str = Setting.Cast<StringOption>();

            str.TitleText.text = Name;
=======
            base.OptionCreated();
            var str = Setting.Cast<StringOption>();
>>>>>>> Stashed changes
            str.Value = str.oldValue = Get();
            str.ValueText.text = ToString();
        }
    }
}