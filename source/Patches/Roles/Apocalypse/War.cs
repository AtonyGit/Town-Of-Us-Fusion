using System;
using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles.Apocalypse
{
    public class War : Role
{
    public War(PlayerControl owner) : base(owner)
    {
        Name = "War";
        Color = Patches.Colors.TrueApoc;
        LastKill = DateTime.UtcNow;
        RoleType = RoleEnum.War;
        AddToRoleHistory(RoleType);
        ImpostorText = () => "";
        TaskText = () => "Eliminate those who remain!\nFake Tasks:";
        Faction = Faction.NeutralApocalypse;
    }

    public PlayerControl ClosestPlayer;
    public DateTime LastKill { get; set; }
    public bool WarWins { get; set; }
    public int JuggKills { get; set; } = 0;


    internal override bool NeutralWin(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return true;

        if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte)|| x.Is(Faction.NeutralKilling)|| x.Is(Faction.NeutralApocalypse))) == 1)
        {
            Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
            ApocWin();
            Utils.EndGame();
            return false;
        }
        else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte)|| x.Is(Faction.NeutralKilling)) && !x.Is(Faction.NeutralApocalypse)) == 0)
        {
            var apocAlives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.NeutralApocalypse)).ToList();
            if (apocAlives.Count == 1) return false;
            Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
            ApocWin();
            Utils.EndGame();
            return false;
        }
        else
        {
            var apocAlives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.NeutralApocalypse)).ToList();
            if (apocAlives.Count == 1 || apocAlives.Count == 2) return false;
            var alives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
            var killersAlive = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(Faction.NeutralApocalypse) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling))).ToList();
            if (killersAlive.Count > 0) return false;
            if (alives.Count <= 6)
            {
            Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
            ApocWin();
                Utils.EndGame();
                return false;
            }
            return false;
        }
    }

    public void Wins()
    {
        WarWins = true;
    }

    public float KillTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastKill;
        var num = 0f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }

    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var apocTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        apocTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = apocTeam;
    }
}
}