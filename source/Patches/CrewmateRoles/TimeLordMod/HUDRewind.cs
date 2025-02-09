using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.TimeLordMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class HudManagerUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.TimeLord)) return;
            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var rewindButton = __instance.KillButton;

            var role = Role.GetRole<TimeLord>(PlayerControl.LocalPlayer);


            rewindButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

                rewindButton.SetCoolDown(role.TimeLordRewindTimer(), role.GetCooldown());

            var renderer = rewindButton.graphic;
            if (!rewindButton.isCoolingDown & !RecordRewind.rewinding & rewindButton.enabled)
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                rewindButton.buttonLabelText.color = Palette.EnabledColor;
                rewindButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                return;
            }

            renderer.color = Palette.DisabledClear;
            renderer.material.SetFloat("_Desat", 1f);
            rewindButton.buttonLabelText.color = Palette.DisabledClear;
            rewindButton.buttonLabelText.material.SetFloat("_Desat", 1f);
        }
    }
}