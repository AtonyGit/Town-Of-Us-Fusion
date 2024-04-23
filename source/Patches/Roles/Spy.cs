using System.Collections.Generic;
using TMPro;

namespace TownOfUsFusion.Roles
{
    public class Spy : Role
{
    public KillButton _padminButton;
    public Dictionary<byte, TMP_Text> PlayerNumbers = new Dictionary<byte, TMP_Text>();
    public bool ButtonUsable = true;
    public Spy(PlayerControl player) : base(player)
    {
        Name = "Spy";
        ImpostorText = () => "Snoop Around And Find Stuff Out";
        TaskText = () => "Gain extra information on the Admin Table";
        AlignmentText = () => "Crew Investigative";
        Color = Patches.Colors.Spy;
        RoleType = RoleEnum.Spy;
        AddToRoleHistory(RoleType);
    }
    public KillButton portableAdminButton
    {
        get => _padminButton;
        set
        {
            _padminButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }

    }
}