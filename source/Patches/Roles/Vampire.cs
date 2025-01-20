using System;
using System.Linq;
using Il2CppSystem.Collections.Generic;
<<<<<<< Updated upstream
using TownOfUsFusion.Extensions;
=======
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles.Modifiers;
>>>>>>> Stashed changes

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
            Faction = Faction.NeutralKilling;
            AddToRoleHistory(RoleType);
        }

        public PlayerControl ClosestPlayer;
        public DateTime LastBit { get; set; }

        public float BiteTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastBit;
            var num = CustomGameOptions.BiteCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

<<<<<<< Updated upstream
        internal override bool NeutralWin(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralKilling))) == 1)
=======
        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;

            var vampsAlive = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(RoleEnum.Vampire)).ToList();
            var loversAlive = PlayerControl.AllPlayerControls.ToArray()
                .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(ModifierEnum.Lover)).ToList();

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 2 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralKilling) || x.IsCrewKiller())) == 1)
>>>>>>> Stashed changes
            {
                VampWin();
                Utils.EndGame();
                return false;
            }
            else if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) <= 4 &&
                    PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected &&
<<<<<<< Updated upstream
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralKilling)) && !x.Is(RoleEnum.Vampire)) == 0)
            {
                var vampsAlives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(RoleEnum.Vampire)).ToList();
                if (vampsAlives.Count == 1) return false;
=======
                    (x.Data.IsImpostor() || x.Is(Faction.NeutralKilling) || x.IsCrewKiller()) && !x.Is(RoleEnum.Vampire)) == 0)
            {
                if (vampsAlive.Count == 1) return false;
                else if (loversAlive.Count == 2 && PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) == 4) return false;
>>>>>>> Stashed changes
                VampWin();
                Utils.EndGame();
                return false;
            }
            else
            {
<<<<<<< Updated upstream
                var vampsAlives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Is(RoleEnum.Vampire)).ToList();
                if (vampsAlives.Count == 1 || vampsAlives.Count == 2) return false;
                var alives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
                var killersAlive = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(RoleEnum.Vampire) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralKilling))).ToList();
                if (killersAlive.Count > 0) return false;
=======
                if (vampsAlive.Count == 1 || vampsAlive.Count == 2) return false;
                var alives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
                var killersAlive = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected && !x.Is(RoleEnum.Vampire) && (x.Is(Faction.Impostors) || x.Is(Faction.NeutralKilling) || x.IsCrewKiller())).ToList();
                if (killersAlive.Count > 0) return false;
                if (loversAlive.Count == 2) return false;
>>>>>>> Stashed changes
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