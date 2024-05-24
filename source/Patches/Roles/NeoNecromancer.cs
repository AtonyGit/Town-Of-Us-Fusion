using System;
using System.Linq;
using InnerNet;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles
{
    public class NeoNecromancer : Role
{
    public DeadBody CurrentTarget;
    public PlayerControl ClosestPlayer;
    private KillButton _ResurrectButton;
    public PlayerControl KillTarget { get; set; }
    public DateTime LastKilled { get; set; }
    public DateTime LastResurrected { get; set; }
    public int ResurrectCount;
    public bool CanKill;


    public NeoNecromancer(PlayerControl player) : base(player)
    {
        Name = "Necromancer";
        ImpostorText = () => "Resurrect The Dead To Do Your Dirty Work";
        TaskText = () => "Resurrect the dead to do your bidding\nFake Tasks:";
        AlignmentText = () => "Necromancer Team";
        Color = Patches.Colors.NeoNecromancer;
        LastKilled = DateTime.UtcNow;
        LastResurrected = DateTime.UtcNow;
        RoleType = RoleEnum.NeoNecromancer;
        Faction = Faction.NeutralNecro;
        AddToRoleHistory(RoleType);
        CanKill = true;
    }

    public KillButton ResurrectButton
    {
        get => _ResurrectButton;
        set
        {
            _ResurrectButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }

    internal override bool NeutralWin(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return true;

        if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralKilling)|| x.Is(Faction.NeutralApocalypse))) == 1)
        {
            NecroWin();
            Utils.EndGame();
            return false;
        }
        else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralKilling)|| x.Is(Faction.NeutralApocalypse)) && !x.Is(Faction.NeutralNecro)) == 0)
        {
            var necrosAlives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.NeutralNecro)).ToList();
            if (necrosAlives.Count == 1) return false;
            NecroWin();
            Utils.EndGame();
            return false;
        }
        else
        {
            var necrosAlives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.NeutralNecro)).ToList();
            if (necrosAlives.Count == 1 || necrosAlives.Count == 2) return false;
            var alives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
            var killersAlive = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(Faction.NeutralNecro) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling))).ToList();
            if (killersAlive.Count > 0) return false;
            if (alives.Count <= 6)
            {
                NecroWin();
                Utils.EndGame();
                return false;
            }
            return false;
        }
    }



    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var necroTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        necroTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = necroTeam;
    }

    public float NecroKillTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastKilled;
        var num = CustomGameOptions.NecroKillCooldown * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
    public float ResurrectTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastResurrected;
        var num = (CustomGameOptions.NecroResurrectCooldown + CustomGameOptions.NecroIncreasedCooldownPerResurrect * ResurrectCount) * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
}
}