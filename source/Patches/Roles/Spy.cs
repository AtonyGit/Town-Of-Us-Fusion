using System;
using System.Collections.Generic;
using TMPro;

namespace TownOfUsFusion.Roles
{
    public class Spy : Role
{
    public KillButton _impBugButton;
    public PlayerControl ClosestPlayer;
    public PlayerControl BuggedPlayer;
    public DateTime LastBugged;
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
    public KillButton impBugButton
    {
        get => _impBugButton;
        set
        {
            _impBugButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }
        public float BugTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastBugged;
            var num = GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

    }
}