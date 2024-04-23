using HarmonyLib;
using Reactor.Utilities;
using System.Linq;
using TownOfUsFusion.Patches.NeutralRoles;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.JokerMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
internal class MeetingExiledEnd
{
    private static void Postfix(ExileController __instance)
    {
        var exiled = __instance.exiled;
        if (exiled == null) return;
        var player = exiled.Object;

        foreach (var role in Role.GetRoles(RoleEnum.Joker))
            if (player.PlayerId == ((Joker)role).target.PlayerId)
            {
                ((Joker)role).Wins();
                if (PlayerControl.LocalPlayer != ((Joker)role).Player) return;
                role.PauseEndCrit = true;

                byte[] toKill = MeetingHud.Instance.playerStates.Where(x => !Utils.PlayerById(x.TargetPlayerId).Is(RoleEnum.Pestilence) && x.VotedFor == ((Joker)role).target.PlayerId).Select(x => x.TargetPlayerId).ToArray();
                var pk = new PunishmentKill((x) =>
                {
                    Utils.RpcMultiMurderPlayer(((Joker)role).Player, x);
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