using System;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Roles.Apocalypse
{
    public class Plaguebearer : Role
{
    public PlayerControl ClosestPlayer;
    public List<byte> InfectedPlayers = new List<byte>();
    public DateTime LastInfected;
    public bool PlaguebearerWins { get; set; }

    public int InfectedAlive => InfectedPlayers.Count(x => Utils.PlayerById(x) != null && Utils.PlayerById(x).Data != null && !Utils.PlayerById(x).Data.IsDead && !Utils.PlayerById(x).Data.Disconnected && !Utils.PlayerById(x).Is(Faction.NeutralApocalypse));
    public bool CanTransform => PlayerControl.AllPlayerControls.ToArray().Count(x => x != null && !x.Data.IsDead && !x.Data.Disconnected) <= InfectedAlive;

    public Plaguebearer(PlayerControl player) : base(player)
    {
        Name = "Plaguebearer";
        ImpostorText = () => "Infect Everyone To Become Pestilence";
        TaskText = () => "Infect everyone to become Pestilence\nFake Tasks:";
        Color = Patches.Colors.RegularApoc;
        RoleType = RoleEnum.Plaguebearer;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralApocalypse;
        InfectedPlayers.Add(player.PlayerId);
    }

    internal override bool NeutralWin(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return true;

        if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralKilling)|| x.Is(Faction.NeutralApocalypse))) == 1)
        {
            Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
            ApocWin();
            Utils.EndGame();
            return false;
        }
        else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralKilling)) && !x.Is(Faction.NeutralApocalypse)) == 0)
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
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(Faction.NeutralApocalypse) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralKilling))).ToList();
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
        PlaguebearerWins = true;
    }

    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var apocTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        //apocTeam.Add(PlayerControl.LocalPlayer);
        foreach (var role in GetRoles(RoleEnum.Plaguebearer))
        {
            var apocRole = (Plaguebearer)role;
            apocTeam.Add(apocRole.Player);
        }
        foreach (var role in GetRoles(RoleEnum.Baker))
        {
            var apocRole = (Baker)role;
            apocTeam.Add(apocRole.Player);
        }
        foreach (var role in GetRoles(RoleEnum.Berserker))
        {
            var apocRole = (Berserker)role;
            apocTeam.Add(apocRole.Player);
        }
        foreach (var role in GetRoles(RoleEnum.SoulCollector))
        {
            var apocRole = (SoulCollector)role;
            apocTeam.Add(apocRole.Player);
        }
        __instance.teamToShow = apocTeam;
    }

    public float InfectTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastInfected;
        var num = CustomGameOptions.InfectCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }

    public void RpcSpreadInfection(PlayerControl source, PlayerControl target)
    {
        new WaitForSeconds(1f);
        SpreadInfection(source, target);
        Utils.Rpc(CustomRPC.Infect, Player.PlayerId, source.PlayerId, target.PlayerId);
    }

    public void SpreadInfection(PlayerControl source, PlayerControl target)
    {
        if (InfectedPlayers.Contains(source.PlayerId) && !InfectedPlayers.Contains(target.PlayerId)) InfectedPlayers.Add(target.PlayerId);
        else if (InfectedPlayers.Contains(target.PlayerId) && !InfectedPlayers.Contains(source.PlayerId)) InfectedPlayers.Add(source.PlayerId);
    }

    public void TurnPestilence()
    {
        var oldRole = GetRole(Player);
        var killsList = (oldRole.CorrectAssassinKills, oldRole.IncorrectAssassinKills);
        RoleDictionary.Remove(Player.PlayerId);
        var role = new Pestilence(Player);
        role.CorrectAssassinKills = killsList.CorrectAssassinKills;
        role.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
        if (Player == PlayerControl.LocalPlayer)
        {
            Coroutines.Start(Utils.FlashCoroutine(Patches.Colors.TrueApoc));
            role.RegenTask();
        }
    }
}
}