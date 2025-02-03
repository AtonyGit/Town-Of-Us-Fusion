using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.SpyMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudAdim
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Spy)) return;
            
            var adButton = __instance.AdminButton;
            adButton.transform.localPosition = new Vector3(0f, 1f, 0f);

            var role = Role.GetRole<Spy>(PlayerControl.LocalPlayer);

            adButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

                adButton.SetCoolDown(role.AdminTimer(), CustomGameOptions.MediateCooldown);

                var renderer = adButton.graphic;
                if (!adButton.isCoolingDown && PlayerControl.LocalPlayer.moveable)
                {
                    renderer.color = Palette.EnabledColor;
                    renderer.material.SetFloat("_Desat", 0f);
                    adButton.buttonLabelText.color = Palette.EnabledColor;
                    adButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                    return;
                }

                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
                adButton.buttonLabelText.color = Palette.DisabledClear;
                adButton.buttonLabelText.material.SetFloat("_Desat", 1f);
        }
    }
}
