using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.Roles.Apocalypse
{
    public class Baker : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Baker(PlayerControl player) : base(player)
    {
        Name = "Baker";
        ImpostorText = () => "Feed Your People Before The Inevitable";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.RegularApoc;
        RoleType = RoleEnum.Baker;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralApocalypse;
    }
    public bool BakerWins { get; set; }

    internal override bool NeutralWin(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return true;

        if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralSentinel) || x.Is(Faction.ImpSentinel) || x.Is(Faction.NeutralApocalypse))) == 1)
        {
            Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
            ApocWin();
            Utils.EndGame();
            return false;
        }
        else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralSentinel) || x.Is(Faction.ImpSentinel)) && !x.Is(Faction.NeutralApocalypse)) == 0)
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
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(Faction.NeutralApocalypse) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralSentinel) || x.Is(Faction.ImpSentinel))).ToList();
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
        BakerWins = true;
    }
    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var apocTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        //apocTeam.Add(PlayerControl.LocalPlayer);
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
        foreach (var role in GetRoles(RoleEnum.Plaguebearer))
        {
            var apocRole = (Plaguebearer)role;
            apocTeam.Add(apocRole.Player);
        }
        foreach (var role in GetRoles(RoleEnum.SoulCollector))
        {
            var apocRole = (SoulCollector)role;
            apocTeam.Add(apocRole.Player);
        }
        __instance.teamToShow = apocTeam;
    }
}
}