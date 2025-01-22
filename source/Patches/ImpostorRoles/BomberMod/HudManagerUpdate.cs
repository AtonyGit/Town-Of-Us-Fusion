using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.ImpostorRoles.BomberMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static Sprite PlantSprite => TownOfUsFusion.PlantSprite;
        public static Sprite DetonateSprite => TownOfUsFusion.DetonateSprite;

        [HarmonyPriority(Priority.Last)]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Bomber)) return;
            var role = Role.GetRole<Bomber>(PlayerControl.LocalPlayer);
            if (role.PlantButton == null)
            {
                role.PlantButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.PlantButton.graphic.enabled = true;
                role.PlantButton.graphic.sprite = PlantSprite;
                role.PlantButton.gameObject.SetActive(false);
            }

            role.PlantButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            if (role.Detonating)
            {
                role.PlantButton.graphic.sprite = DetonateSprite;
                role.PlantButton.buttonLabelText.text = "Detonating";
                role.PlantButton.graphic.color = Palette.EnabledColor;
                role.PlantButton.graphic.material.SetFloat("_Desat", 0f);
                role.DetonateTimer();
                role.PlantButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.DetonateDelay);
            }
            else
            {
                role.PlantButton.graphic.sprite = PlantSprite;
                role.PlantButton.buttonLabelText.text = "Plant";
                if (!role.Detonated) role.DetonateKillStart();
                if (PlayerControl.LocalPlayer.killTimer > 0 || !PlayerControl.LocalPlayer.moveable)
                {
                    role.PlantButton.graphic.color = Palette.DisabledClear;
                    role.PlantButton.graphic.material.SetFloat("_Desat", 1f);
                }
                else
                {
                    role.PlantButton.graphic.color = Palette.EnabledColor;
                    role.PlantButton.graphic.material.SetFloat("_Desat", 0f);
                }
                role.PlantButton.SetCoolDown(PlayerControl.LocalPlayer.killTimer,
                    GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);
            }

            if (role.PlantButton.graphic.sprite == PlantSprite) role.PlantButton.SetCoolDown(PlayerControl.LocalPlayer.killTimer, 
                GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);
            else role.PlantButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.DetonateDelay);
        }
    }
}
