using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.OperativeMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class UpdateArrows
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;

            foreach (var role in Role.AllRoles.Where(x => x.RoleType == RoleEnum.Operative))
            {
                var operative = (Operative)role;
                if (PlayerControl.LocalPlayer.Data.IsDead || operative.Player.Data.IsDead)
                {
                    operative.OperativeArrows.Values.DestroyAll();
                    operative.OperativeArrows.Clear();
                    operative.ImpArrows.DestroyAll();
                    operative.ImpArrows.Clear();
                }

                foreach (var arrow in operative.ImpArrows) arrow.target = operative.Player.transform.position;

                foreach (var arrow in operative.OperativeArrows)
                {
                    var player = Utils.PlayerById(arrow.Key);
                    if (player == null || player.Data == null || player.Data.IsDead || player.Data.Disconnected)
                    {
                        operative.DestroyArrow(arrow.Key);
                        continue;
                    }
                    arrow.Value.target = player.transform.position;
                }
            }
        }
    }
}