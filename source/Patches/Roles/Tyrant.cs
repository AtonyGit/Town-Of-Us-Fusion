using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class Tyrant : Role
{
    public Tyrant(PlayerControl player) : base(player)
    {
        Name = "Tyrant";
        ImpostorText = () => "Backstab the crew and regain political power";
        TaskText = () => "Reveal your true colors to the murderers";
        Color = Patches.Colors.Tyrant;
        RoleType = RoleEnum.Tyrant;
        AddToRoleHistory(RoleType);
        Revealed = false;
    }
    public bool Revealed { get; set; }

    public GameObject RevealButton = new GameObject();

    internal override bool Criteria()
    {
        return Revealed && !Player.Data.IsDead || base.Criteria();
    }

    internal override bool RoleCriteria()
    {
        if (!Player.Data.IsDead) return Revealed || base.RoleCriteria();
        return false || base.RoleCriteria();
    }
    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__36 __instance)
    {
        var evilTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        evilTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = evilTeam;
    }
}
}