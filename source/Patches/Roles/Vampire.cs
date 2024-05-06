using System;
using System.Linq;
using Il2CppSystem.Collections.Generic;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.NeutralRoles.VampireMod;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class Vampire : Role
{
    public Vampire(PlayerControl player) : base(player)
    {
        Name = "Vampire";
        ImpostorText = () => "Convert Crewmates And Kill The Rest";
        TaskText = () => "Bite all other players\nFake Tasks:";
        Color = Patches.Colors.Vampire;
        LastBit = DateTime.UtcNow;
        RoleType = RoleEnum.Vampire;
        Faction = Faction.NeutralNeophyte;
        AddToRoleHistory(RoleType);
        BittenPlayer = null;
    }

        public float TimeRemaining;
        public bool Bitten => TimeRemaining > 0f;
        public bool Enabled = false;
    public PlayerControl ClosestPlayer;
    public PlayerControl BittenPlayer;
    public DateTime LastBit { get; set; }

        public void BiteThingy()
        {
            Enabled = true;
            TimeRemaining -= Time.deltaTime;
            if (MeetingHud.Instance)
            {
                TimeRemaining = 0;
            }
            if (TimeRemaining <= 0)
            {
                BiteKill();
            }
        }
        public void BiteKill()
        {
            var aliveVamps = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(RoleEnum.Vampire) && !x.Data.IsDead && !x.Data.Disconnected).ToList();
            var vamps = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(RoleEnum.Vampire)).ToList();

            if (!BittenPlayer.Is(RoleEnum.Pestilence) && (BittenPlayer.Is(Faction.Crewmates) || (BittenPlayer.Is(Faction.NeutralBenign)
            && CustomGameOptions.CanBiteNeutralBenign) || (BittenPlayer.Is(Faction.NeutralChaos)
            && CustomGameOptions.CanBiteNeutralChaos) || (BittenPlayer.Is(Faction.NeutralEvil)
            && CustomGameOptions.CanBiteNeutralEvil)) && !BittenPlayer.Is(AllianceEnum.Lover) &&
            !BittenPlayer.Is(AllianceEnum.Crewpocalypse) && !BittenPlayer.Is(AllianceEnum.Crewpostor) &&
            !BittenPlayer.Is(AllianceEnum.Recruit) &&
            aliveVamps.Count == 1 && vamps.Count < CustomGameOptions.MaxVampiresPerGame)
            {
                Bite.Convert(BittenPlayer);
                Utils.Rpc(CustomRPC.Bite, BittenPlayer.PlayerId);
            } else {
                Utils.RpcMultiMurderPlayer(Player, BittenPlayer);
                if (!BittenPlayer.Data.IsDead) SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.5f);
            }
            BittenPlayer = null;
            Enabled = false;
            LastBit = DateTime.UtcNow;
        }
    public float BiteTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - LastBit;
        var num = CustomGameOptions.BiteCd * 1000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }

    internal override bool NeutralWin(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return true;

        if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte)|| x.Is(Faction.NeutralKilling)|| x.Is(Faction.NeutralApocalypse))) == 1)
        {
            VampWin();
            Utils.EndGame();
            return false;
        }
        else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte)) && !x.Is(RoleEnum.Vampire)) == 0)
        {
            var vampsAlives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(RoleEnum.Vampire)).ToList();
            if (vampsAlives.Count == 1) return false;
            VampWin();
            Utils.EndGame();
            return false;
        }
        else
        {
            var vampsAlives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(RoleEnum.Vampire)).ToList();
            if (vampsAlives.Count == 1 || vampsAlives.Count == 2) return false;
            var alives = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
            var killersAlive = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(RoleEnum.Vampire) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralNeophyte))).ToList();
            if (killersAlive.Count > 0) return false;
            if (alives.Count <= 6)
            {
                VampWin();
                Utils.EndGame();
                return false;
            }
            return false;
        }
    }

    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var vampTeam = new List<PlayerControl>();
        vampTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = vampTeam;
    }
}
}