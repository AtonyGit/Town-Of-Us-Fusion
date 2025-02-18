using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.AdmirerMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudManagerUpdate
    {
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Admirer)) return;
            var admireButton = __instance.KillButton;

            var role = Role.GetRole<Admirer>(PlayerControl.LocalPlayer);


            /*admireButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);*/
            admireButton.SetCoolDown(0.01f, 0.01f);
            if (role.AdmiredPlayer == null) {
                Utils.SetTarget(ref role.ClosestPlayer, admireButton);
            }

                var renderer = admireButton.graphic;
                if (role.ButtonUsable && PlayerControl.LocalPlayer.moveable && role.ClosestPlayer != null)
                {
                    renderer.color = Palette.EnabledColor;
                    renderer.material.SetFloat("_Desat", 0f);
                    admireButton.buttonLabelText.color = Palette.EnabledColor;
                    admireButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                }
                else
                {
                    renderer.color = Palette.DisabledClear;
                    renderer.material.SetFloat("_Desat", 1f);
                    admireButton.buttonLabelText.color = Palette.DisabledClear;
                    admireButton.buttonLabelText.material.SetFloat("_Desat", 1f);
                }
            }
        }
    }