using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;

namespace TownOfUsFusion.CrewmateRoles.AltruistMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Altruist)) return;

            var role = Role.GetRole<Altruist>(PlayerControl.LocalPlayer);

            var killButton = __instance.KillButton;

            killButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            killButton.usesRemainingSprite.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            killButton.usesRemainingText.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            killButton.usesRemainingText.text = role.RevivesLeft.ToString();

            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            var flag = (GameOptionsManager.Instance.currentNormalGameOptions.GhostsDoTasks || !data.IsDead) &&
                       (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) &&
                       PlayerControl.LocalPlayer.CanMove;
            var allocs = Physics2D.OverlapCircleAll(truePosition, maxDistance,
                LayerMask.GetMask(new[] { "Players", "Ghost" }));

            DeadBody closestBody = null;
            var closestDistance = float.MaxValue;

            foreach (var collider2D in allocs)
            {
                if (!flag || isDead || collider2D.tag != "DeadBody") continue;
                var component = collider2D.GetComponent<DeadBody>();
                if (!(Vector2.Distance(truePosition, component.TruePosition) <=
                      maxDistance)) continue;

                var distance = Vector2.Distance(truePosition, component.TruePosition);
                if (!(distance < closestDistance)) continue;
                closestBody = component;
                closestDistance = distance;
            }

            if (role.Reviving)
            {
                killButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.ReviveDuration);
                killButton.graphic.color = Palette.EnabledColor;
                killButton.buttonLabelText.color = Palette.EnabledColor;
                killButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                killButton.graphic.material.SetFloat("_Desat", 0f);
            }
            else
            {
                killButton.SetCoolDown(role.ReviveTimer(), CustomGameOptions.ReviveCooldown);

                if (role.ReviveTimer() > 0f || !PlayerControl.LocalPlayer.moveable)
                {
                    killButton.graphic.color = Palette.DisabledClear;
                    killButton.graphic.material.SetFloat("_Desat", 1f);
                    killButton.buttonLabelText.color = Palette.DisabledClear;
                    killButton.buttonLabelText.material.SetFloat("_Desat", 1f);
                }
                else
                {
                    killButton.graphic.color = Palette.EnabledColor;
                    killButton.graphic.material.SetFloat("_Desat", 0f);
                    killButton.buttonLabelText.color = Palette.EnabledColor;
                    killButton.buttonLabelText.material.SetFloat("_Desat", 0f);
                }
            }

            KillButtonTarget.SetTarget(killButton, closestBody, role);
            __instance.KillButton.SetCoolDown(0f, 1f);
        }
    }
}