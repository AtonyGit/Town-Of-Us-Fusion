using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.OracleMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudConfess
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Oracle)) return;
            var blessButton = __instance.KillButton;

            var role = Role.GetRole<Oracle>(PlayerControl.LocalPlayer);

            blessButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            blessButton.SetCoolDown(role.BlessTimer(), CustomGameOptions.BlessCd);

            var notBlessed = PlayerControl.AllPlayerControls
                .ToArray()
                .Where(x => x != role.BlessedPlayer)
                .ToList();

            Utils.SetTarget(ref role.ClosestPlayer, blessButton, float.NaN, notBlessed);
        }
    }
}
