using System;

namespace TownOfUsFusion.Roles
{
    public class Sheriff : Role
{
    public bool CanShoot;
    //public bool ButtonUsable => UsesLeft != 0;
    public Sheriff(PlayerControl player) : base(player)
    {
        Name = "Sheriff";
        ImpostorText = () => "Shoot The <color=#FF0000FF>Impostor</color>";
        TaskText = () => "Kill off the impostor but don't kill crewmates";
        AlignmentText = () => "Crew Killing";
        Color = Patches.Colors.Sheriff;
        LastKilled = DateTime.UtcNow;
        RoleType = RoleEnum.Sheriff;
        AddToRoleHistory(RoleType);
        CanShoot = false;
    }

    public PlayerControl ClosestPlayer;
    public DateTime LastKilled { get; set; }

    public float SheriffKillTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastKilled;
        var num = CustomGameOptions.SheriffKillCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
}
}