using System;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles.Alliances;

namespace TownOfUsFusion.Roles
{
    public class Jackal : Role
{
    public Jackal(PlayerControl owner) : base(owner)
    {
        Name = Utils.GradientColorText("B7B9BA", "5E576B", "Jackal");
        Color = Patches.Colors.Recruit;
        LastKill = DateTime.UtcNow;
        RoleType = RoleEnum.Jackal;
        AddToRoleHistory(RoleType);
        ImpostorText = () => Utils.GradientColorText("B7B9BA", "5E576B", "Lead Your Recruits To Victory");
        //TaskText = () => "Your recruits are this and that\nFake Tasks:";
        TaskText = () => Recruit1 != null && Recruit2 != null ? "Your recruits are " + Recruit1.Player.GetDefaultOutfit().PlayerName + " and " + Recruit2.Player.GetDefaultOutfit().PlayerName + "\nFake Tasks:" : "Odd, your recruits don't appear to exist\nFake Tasks:";
        Faction = Faction.NeutralNeophyte;
        CanKill = CustomGameOptions.JackalCanAlwaysKill;
    }
    public bool CanKill = CustomGameOptions.JackalCanAlwaysKill;

    public Recruit Recruit1 { get; set; }
    public Recruit Recruit2 { get; set; }
    public PlayerControl ClosestPlayer;
    public DateTime LastKill { get; set; }
    
    internal override bool NeutralWin(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return true;

        if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte)|| x.Is(Faction.NeutralKilling)|| x.Is(Faction.NeutralApocalypse))) == 1)
        {
            JackalWin();
            Utils.EndGame();
            return false;
        }
        else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte)) && !x.Is(RoleEnum.Jackal) && !x.Is(AllianceEnum.Recruit)) == 0)
        {
            var jackalsAlive = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && (x.Is(RoleEnum.Jackal) || x.Is(AllianceEnum.Recruit))).ToList();
            if (jackalsAlive.Count == 1) return false;
            JackalWin();
            Utils.EndGame();
            return false;
        }
        else
        {
            var jackalsAlive = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && (x.Is(RoleEnum.Jackal) || x.Is(AllianceEnum.Recruit))).ToList();
            if (jackalsAlive.Count == 1 || jackalsAlive.Count == 2) return false;
            var alives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
            var killersAlive = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(RoleEnum.Jackal) && !x.Is(AllianceEnum.Recruit) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralNeophyte))).ToList();
            if (killersAlive.Count > 0) return false;
            if (alives.Count <= 6)
            {
                JackalWin();
                Utils.EndGame();
                return false;
            }
            return false;
        }
    }

    public float JackalKillTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastKill;
        var num = CustomGameOptions.JackalKillCooldown * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }

    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var jackalTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        jackalTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = jackalTeam;
    }
}
}