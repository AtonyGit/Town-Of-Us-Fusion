using HarmonyLib;
using Reactor.Utilities;
using System.Linq;
using TownOfUs.Patches.NeutralRoles;
using TownOfUs.Roles;

namespace TownOfUs.NeutralRoles.JesterMod
{
<<<<<<< Updated upstream
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
=======
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.BeginForGameplay))]
>>>>>>> Stashed changes
    internal class MeetingExiledEnd
    {
        private static void Postfix(ExileController __instance)
        {
<<<<<<< Updated upstream
            var exiled = __instance.exiled;
=======
            var exiled = __instance.initData.networkedPlayer;
>>>>>>> Stashed changes
            if (exiled == null) return;
            var player = exiled.Object;

            var role = Role.GetRole(player);
            if (role == null) return;
            if (role.RoleType == RoleEnum.Jester)
            {
                ((Jester)role).Wins();
                

                if (CustomGameOptions.NeutralEvilWinEndsGame || !CustomGameOptions.JesterHaunt) return;
                if (PlayerControl.LocalPlayer != player) return;
                role.PauseEndCrit = true;

                byte[] toKill = MeetingHud.Instance.playerStates.Where(x => !Utils.PlayerById(x.TargetPlayerId).Is(RoleEnum.Pestilence) && x.VotedFor == player.PlayerId).Select(x => x.TargetPlayerId).ToArray();
<<<<<<< Updated upstream
                var pk = new PunishmentKill((x) =>
=======
                var pk = new PlayerMenu((x) =>
>>>>>>> Stashed changes
                {
                    Utils.RpcMultiMurderPlayer(player, x);
                    role.PauseEndCrit = false;
                }, (y) =>
                {
                    return toKill.Contains(y.PlayerId);
                });
                Coroutines.Start(pk.Open(3f));
            }
        }
    }
}