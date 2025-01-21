using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.TaskmasterMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class UpdateArrows
{
    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;

        foreach (var role in Role.AllRoles.Where(x => x.RoleType == RoleEnum.Taskmaster))
        {
            var tm = (Taskmaster)role;
            if (PlayerControl.LocalPlayer.Data.IsDead || tm.Player.Data.IsDead)
            {
                tm.TaskmasterArrows.Values.DestroyAll();
                tm.TaskmasterArrows.Clear();
                tm.ImpArrows.DestroyAll();
                tm.ImpArrows.Clear();
            }

            foreach (var arrow in tm.ImpArrows) arrow.target = tm.Player.transform.position;

            foreach (var arrow in tm.TaskmasterArrows)
            {
                var player = Utils.PlayerById(arrow.Key);
                if (player == null || player.Data == null || player.Data.IsDead || player.Data.Disconnected)
                {
                    tm.DestroyArrow(arrow.Key);
                    continue;
                }
                arrow.Value.target = player.transform.position;
            }
        }
    }
}
}