using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.PsychicMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudInvestigate
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            UpdateInvButton(__instance);
        }

        public static void UpdateInvButton(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Psychic)) return;
            var investigateButton = __instance.KillButton;

            var role = Role.GetRole<Psychic>(PlayerControl.LocalPlayer);

            investigateButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            investigateButton.SetCoolDown(role.PsychicTimer(), CustomGameOptions.PsychicCd);
            if (role.IsSeerMode) {
                var notInvestigated = PlayerControl.AllPlayerControls
                    .ToArray()
                    .Where(x => !role.Investigated.Contains(x.PlayerId))
                    .ToList();

                Utils.SetTarget(ref role.ClosestPlayer, investigateButton, float.NaN, notInvestigated);
            }
            else {
                var notConfessing = PlayerControl.AllPlayerControls
                    .ToArray()
                    .Where(x => x != role.Confessor)
                    .ToList();

                Utils.SetTarget(ref role.ClosestPlayer, investigateButton, float.NaN, notConfessing);
            }
        }
    }
}
