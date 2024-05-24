using System;
using TMPro;

namespace TownOfUsFusion.Roles
{
    public class Trickster : Role
{

    public int UsesLeft;
    public TextMeshPro UsesText;
    public bool AddedTricks;

    public bool ButtonUsable => UsesLeft != 0;
    //public bool ButtonUsable => UsesLeft != 0;
    public Trickster(PlayerControl player) : base(player)
    {
        Name = "Trickster";
        ImpostorText = () => "Trick Killers Into A Kamikaze";
        TaskText = () => "Aim for the killers, not the crew";
        AlignmentText = () => "Crew Killing";
        Color = Patches.Colors.Trickster;
        LastKilled = DateTime.UtcNow;
        RoleType = RoleEnum.Trickster;
        AddToRoleHistory(RoleType);

        UsesLeft = 0;
        AddedTricks = false;
    }

    public PlayerControl ClosestPlayer;
    public DateTime LastKilled { get; set; }

    public float TricksterKillTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastKilled;
        var num = CustomGameOptions.TrickCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
}
}