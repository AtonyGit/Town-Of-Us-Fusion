using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.NeutralRoles.SoulCollectorMod;
using UnityEngine;

namespace TownOfUsFusion.Roles
{
    public class Death : Role
    {
        public bool CollectedSouls = false;
        public bool HasSentAlert = false;

        public Death(PlayerControl player) : base(player)
        {
            Name = "Death";
            ImpostorText = () => "";
            TaskText = () => "Survive the following meeting to win.\nFake Tasks:";
            Invincible = true;
            Transformed = true;
            Color = Patches.Colors.Apocalypse;
            VentSprite = TownOfUsFusion.ApocVent;
            RoleType = RoleEnum.Death;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralApocalypse;
        }

        public void Wins()
        {
            CollectedSouls = true;
        }
        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;
            
            if (CollectedSouls) {
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                Utils.EndGame();
                return false;
            }

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralApocalypse))) == 1)
            {
                Utils.Rpc(CustomRPC.ApocWin, Player.PlayerId);
                ApocWin();
                Utils.EndGame();
                return false;
            }
            else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralKilling)) && !x.Is(Faction.NeutralApocalypse)) == 0)
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

        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
            var apocTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            __instance.teamToShow = apocTeam;
        }
    }
}