using System;
using System.Linq;
using TMPro;
using UnityEngine;
using TownOfUsFusion.Extensions;
using System.Collections.Generic;
using Reactor.Utilities;
using TownOfUsFusion.Roles.Alliances;
using TownOfUsFusion.NeutralRoles.SentinelMod;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Patches;

namespace TownOfUsFusion.Roles
{
    public class Sentinel : Role
{
    public string team = "";
    public Sentinel(PlayerControl player) : base(player)
    {
        Name = "The Sentinel";
        ImpostorText = () => "Neutralize Your Targets";

        if (team == "Crew") TaskText = () => "Destroy the neutrals and impostors";
        else if (team == "Neutral") TaskText = () => "Destroy the impostors and pacifists\nFake Tasks:";
        else if (team == "Imp") TaskText = () => "Destroy the neutrals and pacifists\nFake Tasks:";
        else TaskText = () => "Destroy everyone in your path\nFake Tasks:";
        Color = Patches.Colors.Sentinel;
        RoleType = RoleEnum.Sentinel;
        AddToRoleHistory(RoleType);
        ChargeUsesLeft = CustomGameOptions.MaxChargeUses;
        PlaceUsesLeft = CustomGameOptions.MaxPlaceUses;
        StunUsesLeft = CustomGameOptions.MaxStunUses;
        if (team == "Crew") Faction = Faction.CrewSentinel;
        else if (team == "Neutral") Faction = Faction.ChaosSentinel;
        else if (team == "Imp") Faction = Faction.ImpSentinel;
        else Faction = Faction.NeutralSentinel;
        ChargedPlayer = null;
    }

    public PlayerControl ClosestPlayer;
    public bool SentinelWins;
    public int ChargeUsesLeft;
    public TextMeshPro ChargeText;
    public DateTime LastCharged;
    private KillButton _chargeButton;
    public bool ChargeUsable => ChargeUsesLeft != 0;
    public PlayerControl ChargedPlayer;
    public float ChargeTimeRemaining;
    public bool ChargeSetOff = true;
    public bool ChargeEnabled = false;
    public Charge Charge = new Charge();

    public bool Charged => ChargeTimeRemaining > 0f;
        public void ChargeAttack()
        {
        ChargeEnabled = true;
        ChargeTimeRemaining -= Time.deltaTime;
        if (MeetingHud.Instance) ChargeSetOff = true;
        if (ChargeTimeRemaining <= 0 && !ChargeSetOff)
        {
            var bomber = GetRole<Sentinel>(PlayerControl.LocalPlayer);
            bomber.Charge.ClearCharge();
            ChargeKill();
        }
        }
    public void ChargeKill()
    {
        ChargeSetOff = true;
        var playersToDie = Utils.GetClosestPlayers(ChargedPlayer.transform.position, CustomGameOptions.ChargeRadius, false);
        playersToDie = Shuffle(playersToDie);
        while (playersToDie.Count > CustomGameOptions.MaxKillsInCharge) playersToDie.Remove(playersToDie[playersToDie.Count - 1]);
        foreach (var player in playersToDie)
        {
            if (!player.Is(RoleEnum.Pestilence) && !player.IsShielded() && !player.IsProtected() && player != ShowRoundOneShield.FirstRoundShielded)
            {
                Utils.RpcMultiMurderPlayer(Player, player);
            }
            else if (player.IsShielded())
            {
                var medic = player.GetMedic().Player.PlayerId;
                Utils.Rpc(CustomRPC.AttemptSound, medic, player.PlayerId);
                StopKill.BreakShield(medic, player.PlayerId, CustomGameOptions.ShieldBreaks);
            }
        }
        ChargedPlayer = null;
    }

    public KillButton ChargeButton
    {
        get => _chargeButton;
        set
        {
            _chargeButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }
    public int PlaceUsesLeft;
    public TextMeshPro PlaceText;
    public DateTime LastPlaced;
    private KillButton _placeButton;
    public bool PlaceUsable => PlaceUsesLeft != 0;
    public Vector3 DynamitePoint;
    public Dynamite Dynamite = new Dynamite();
    public bool DynamiteUsed = false;
    public static Material dynamiteMaterial = TownOfUsFusion.bundledAssets.Get<Material>("bomb");
    public bool DynamiteTriggered = false;
    public void DetonateTimer()
    {
        DynamiteUsed = true;
        if (DynamiteTriggered)
        {
            var bomber = GetRole<Sentinel>(PlayerControl.LocalPlayer);
            bomber.Dynamite.ClearDynamite();
            DetonateKillStart();
        }
    }
    public void DetonateKillStart()
    {
        DynamiteTriggered = false;
        DynamiteUsed = false;
        var playersToDie = Utils.GetClosestPlayers(DynamitePoint, CustomGameOptions.PlaceRadius, false);
        playersToDie = Shuffle(playersToDie);
        while (playersToDie.Count > CustomGameOptions.MaxKillsInPlaced) playersToDie.Remove(playersToDie[playersToDie.Count - 1]);
        foreach (var player in playersToDie)
        {
            if (!player.Is(RoleEnum.Pestilence) && !player.IsShielded() && !player.IsProtected() && player != ShowRoundOneShield.FirstRoundShielded)
            {
                Utils.RpcMultiMurderPlayer(Player, player);
            }
            else if (player.IsShielded())
            {
                var medic = player.GetMedic().Player.PlayerId;
                Utils.Rpc(CustomRPC.AttemptSound, medic, player.PlayerId);
                StopKill.BreakShield(medic, player.PlayerId, CustomGameOptions.ShieldBreaks);
            }
        }
    }
    public static Il2CppSystem.Collections.Generic.List<PlayerControl> Shuffle(Il2CppSystem.Collections.Generic.List<PlayerControl> playersToDie)
    {
        var count = playersToDie.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = playersToDie[i];
            playersToDie[i] = playersToDie[r];
            playersToDie[r] = tmp;
        }
        return playersToDie;
    }
    
    public KillButton PlaceButton
    {
        get => _placeButton;
        set
        {
            _placeButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }
    public int StunUsesLeft;
    public TextMeshPro StunText;
    public DateTime LastStunned;
    private KillButton _stunButton;
    public bool StunUsable => StunUsesLeft != 0;
    public PlayerControl StunnedPlayer;
    public float StunTimeRemaining;
    public bool StunActive = false;

    public KillButton StunButton
    {
        get => _stunButton;
        set
        {
            _stunButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }
    
    public void RpcSetStunned(PlayerControl stunned)
    {
        SetStunned(stunned);
    }

    public void SetStunned(PlayerControl stunned)
    {
        Utils.Rpc(CustomRPC.SetStunned, stunned.PlayerId);
        LastStunned = DateTime.UtcNow;
        Coroutines.Start(AbilityCoroutine.Stun(this, stunned));
    }
    public DateTime LastKilled;
    
    public static void Gen(List<PlayerControl> sentList)
    {
        List<PlayerControl> allPlayers = new List<PlayerControl>();
        List<PlayerControl> sentinalTrue = new List<PlayerControl>();

        foreach (var player in sentList)
        {
                if (!player.Is(RoleEnum.Sentinel) && !player.Is(Faction.NeutralChaos) && !player.Is(AllianceEnum.Lover)) 
                {
                    allPlayers.Add(player);
                }
                else if (player.Is(RoleEnum.Sentinel)) sentinalTrue.Add(player);
        }
        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"LOADED {sentList.Count} PLAYERS TO SENTINEL LIST");

        var newTeam = "";

        var soloChance = UnityEngine.Random.RandomRangeInt(0, 100);

        PlayerControl selectedPlayer = null;
        if (CustomGameOptions.SentinelSoloPercent > soloChance)
        {
            newTeam = "";
        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SENTINEL PROTOCOL: TERMINATE");
        }
        else
        {
            var num3 = UnityEngine.Random.RandomRangeInt(0, allPlayers.Count);
            selectedPlayer = allPlayers[num3];
            if ((selectedPlayer.Is(Faction.Crewmates) || selectedPlayer.Is(Faction.NeutralBenign)) && !selectedPlayer.Is(AllianceEnum.Crewpocalypse)
            && !selectedPlayer.Is(AllianceEnum.Crewpostor) && !selectedPlayer.Is(AllianceEnum.Egotist) && !selectedPlayer.Is(AllianceEnum.Recruit)) newTeam = "Crew";
            else if (selectedPlayer.Is(Faction.NeutralEvil) || selectedPlayer.Is(Faction.NeutralKilling) || selectedPlayer.Is(Faction.NeutralNeophyte) || selectedPlayer.Is(AllianceEnum.Crewpocalypse)
            || selectedPlayer.Is(Faction.NeutralNecro) || selectedPlayer.Is(Faction.NeutralApocalypse) || selectedPlayer.Is(AllianceEnum.Egotist) || selectedPlayer.Is(AllianceEnum.Recruit)) newTeam = "Neutral";
            else if (selectedPlayer.Is(Faction.Impostors) || selectedPlayer.Is(AllianceEnum.Crewpostor)) newTeam = "Imp";
        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SENTINEL SCANNING: {selectedPlayer.Data.PlayerName}");
        }
                
            foreach (var role in Role.GetRoles(RoleEnum.Sentinel))
            {
                if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SENTINEL LOADING");
                var newSentinel = Role.GetRole(role.Player);
                newSentinel.RemoveFromRoleHistory(role.RoleType);

                Role.RoleDictionary.Remove(role.Player.PlayerId);
                var sentinelButYeah = new Sentinel(role.Player);
                if (newTeam == "" && TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SENTINEL PROTOCOL: TERMINATE");
                if (newTeam == "Crew") {
                    sentinelButYeah.team = "Crew";
                    sentinelButYeah.Faction = Faction.CrewSentinel;
                    sentinelButYeah.TaskText = () => "Destroy the neutrals and impostors";
                    if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SENTINEL PROTOCOL: PASSIVE | {selectedPlayer.Data.PlayerName}");
                    if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SENTINEL FACTION: {sentinelButYeah.Faction}");
                }
                else if (newTeam == "Neutral") {
                    sentinelButYeah.team = "Neutral";
                    sentinelButYeah.Faction = Faction.ChaosSentinel;
                    sentinelButYeah.TaskText = () => "Destroy the impostors and pacifists\nFake Tasks:";
                    if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SENTINEL PROTOCOL: NEUTRAL | {selectedPlayer.Data.PlayerName}");
                    if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SENTINEL FACTION: {sentinelButYeah.Faction}");
                }
                else if (newTeam == "Imp") {
                    sentinelButYeah.team = "Imp";
                    sentinelButYeah.Faction = Faction.ImpSentinel;
                    sentinelButYeah.TaskText = () => "Destroy the neutrals and pacifists\nFake Tasks:";
                    if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SENTINEL PROTOCOL: PARASITE | {selectedPlayer.Data.PlayerName}");
                    if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"SENTINEL FACTION: {sentinelButYeah.Faction}");
                }
                if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SENTINEL FINALIZED.");
                
                sentinelButYeah.ChargeUsesLeft = CustomGameOptions.MaxChargeUses;
                sentinelButYeah.PlaceUsesLeft = CustomGameOptions.MaxPlaceUses;
                sentinelButYeah.StunUsesLeft = CustomGameOptions.MaxStunUses;
                sentinelButYeah.DynamiteUsed = false;
                sentinelButYeah.DynamiteTriggered = false;

                sentinelButYeah.RegenTask();
            }

    }
    internal override bool NeutralWin(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return true;
        if (team == "") {
            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralSentinel) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralApocalypse))) == 1)
            {
                Utils.Rpc(CustomRPC.SentinelWin, Player.PlayerId);
                Wins();
                Utils.EndGame();

                return false;
            }
        }/*
        else if (team == "Neutral") {
            if ((PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.ChaosSentinel) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralApocalypse))) == 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.ChaosSentinel) || x.Is(Faction.NeutralKilling)|| x.Is(Faction.NeutralApocalypse))) > 1)
                    || PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.ChaosSentinel)) == 1)
            {
                Utils.Rpc(CustomRPC.SentinelWin, Player.PlayerId);
                Wins();
                Utils.EndGame();

                return false;
            }
        }*/ else if (team == "Imp") {
            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.ImpSentinel) || x.Is(Faction.NeutralKilling)|| x.Is(Faction.NeutralApocalypse))) == 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.ImpSentinel))) > 1)
            {
                //Utils.Rpc(CustomRPC.SentinelWin, Player.PlayerId);
                Wins();
                Utils.EndGame();

                return false;
            }
        }
        else if (team == "Crew") {
            if ( PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.CrewSentinel) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralApocalypse))) == 1 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Is(Faction.Crewmates) || x.Is(Faction.CrewSentinel))) == PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected)
                    /*|| PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(Faction.CrewSentinel)) == 1)*/)
            {
                Utils.Rpc(CustomRPC.SentinelCrewWin);
                Wins();
                Utils.EndGameCrew();

                return false;
            }
        }
            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) == 1 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(RoleEnum.Sentinel)) == 1)
            {
                Utils.Rpc(CustomRPC.SentinelWin, Player.PlayerId);
                Wins();
                Utils.EndGame();

                return false;
            }

        return false;
    }

    public void Wins()
    {
        SentinelWins = true;
    }
    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var sentTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        sentTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = sentTeam;
    }

    public float ChargeTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastCharged;
        var num = CustomGameOptions.SentinelChargeCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
    public float PlaceTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastPlaced;
        var num = CustomGameOptions.SentinelPlaceCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
    public float StunTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastStunned;
        var num = CustomGameOptions.SentinelStunCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
    public float KillTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastKilled;
        var num = CustomGameOptions.SentinelKillCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }
}
}