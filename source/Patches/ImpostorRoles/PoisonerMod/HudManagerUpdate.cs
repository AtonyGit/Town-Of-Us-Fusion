using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using System.Linq;
using Hazel;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.ImpostorRoles.PoisonerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static Sprite PoisonSprite => TownOfUsFusion.PoisonSprite;
        public static Sprite PoisonedSprite => TownOfUsFusion.PoisonedSprite;

        [HarmonyPriority(Priority.Last)]
        public static void Postfix(HudManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner)) return;
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            var role = Role.GetRole<Poisoner>(PlayerControl.LocalPlayer);
            if (role.PoisonButton == null) {
                role.PoisonButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.PoisonButton.graphic.enabled = true;
                role.PoisonButton.graphic.sprite = PoisonSprite;
                role.PoisonButton.gameObject.SetActive(false);
            }

            //role.PoisonButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);

            role.PoisonButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            //__instance.KillButton.Hide();
            
            var position = __instance.KillButton.transform.localPosition;
            __instance.ImpostorVentButton.transform.localPosition = new Vector3(position.x - 2f, position.y, position.z);
            role.PoisonButton.transform.localPosition = new Vector3(position.x - 1f, position.y, position.z);
            var notImp = PlayerControl.AllPlayerControls
                    .ToArray()
                    .Where(x => !x.Is(Faction.Impostors))
                    .ToList();
            Utils.SetTarget(ref role.ClosestPlayer, role.PoisonButton, float.NaN, notImp);

            if (role.ClosestPlayer != null)
            {
                role.ClosestPlayer.myRend().material.SetColor("_OutlineColor", Palette.Purple);
            }

                if (role.Poisoned)
                {
                    role.Player.SetKillTimer(GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown/* + CustomGameOptions.PoisonDuration*/);
                    role.PoisonButton.graphic.sprite = PoisonedSprite;
                    role.Poison();
                    role.PoisonButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.PoisonDuration);
                }
                else
                {
                    role.PoisonButton.graphic.sprite = PoisonSprite;
                    if (role.PoisonedPlayer && role.PoisonedPlayer != PlayerControl.LocalPlayer)
                    {
                        role.PoisonKill();
                    }
                    if (role.ClosestPlayer != null)
                    {
                        role.PoisonButton.graphic.color = Palette.EnabledColor;
                        role.PoisonButton.graphic.material.SetFloat("_Desat", 0f);
                    }
                    else
                    {
                        role.PoisonButton.graphic.color = Palette.DisabledClear;
                        role.PoisonButton.graphic.material.SetFloat("_Desat", 1f);
                    }
                    role.PoisonButton.SetCoolDown(PlayerControl.LocalPlayer.killTimer,
                    GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);

        if (role.PoisonButton.graphic.sprite == PoisonSprite) role.PoisonButton.SetCoolDown(PlayerControl.LocalPlayer.killTimer,
            GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);
        else role.PoisonButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.PoisonDuration);

                    //role.PoisonButton.SetCoolDown(role.PoisonTimer(), CustomGameOptions.PoisonCd);
                    role.PoisonedPlayer = PlayerControl.LocalPlayer; //Only do this to stop repeatedly trying to re-kill poisoned player. null didn't work for some reason
                }
        }
    }
}
