using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.SpyMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class UpdateArrows
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;

            foreach (var role in Role.AllRoles.Where(x => x.RoleType == RoleEnum.Spy))
            {
                var spy = (Spy)role;
                if (PlayerControl.LocalPlayer.Data.IsDead || spy.Player.Data.IsDead)
                {
                    spy.SpyArrows.Values.DestroyAll();
                    spy.SpyArrows.Clear();
                    spy.ImpArrows.DestroyAll();
                    spy.ImpArrows.Clear();
                }

                foreach (var arrow in spy.ImpArrows) arrow.target = spy.Player.transform.position;

                foreach (var arrow in spy.SpyArrows)
                {
                    var player = Utils.PlayerById(arrow.Key);
                    if (player == null || player.Data == null || player.Data.IsDead || player.Data.Disconnected)
                    {
                        spy.DestroyArrow(arrow.Key);
                        continue;
                    }
                    arrow.Value.target = player.transform.position;
                }
            }
        }
    }
}