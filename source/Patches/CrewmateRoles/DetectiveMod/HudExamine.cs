using AmongUs.GameOptions;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.DetectiveMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudExamine
    {
<<<<<<< Updated upstream
        public static Sprite ExamineSprite => TownOfUsFusion.ExamineSprite;
=======
        public static Sprite ExamineSprite => TownOfUsFusion.ExamineSprite;
>>>>>>> Stashed changes

        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
<<<<<<< Updated upstream
            UpdateExamineButton(__instance);
        }

        public static void UpdateExamineButton(HudManager __instance)
        {
=======
>>>>>>> Stashed changes
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Detective)) return;

            var role = Role.GetRole<Detective>(PlayerControl.LocalPlayer);

            if (role.ExamineButton == null)
            {
                role.ExamineButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.ExamineButton.graphic.enabled = true;
                role.ExamineButton.gameObject.SetActive(false);
            }

            role.ExamineButton.graphic.sprite = ExamineSprite;
            role.ExamineButton.transform.localPosition = new Vector3(-2f, 0f, 0f);

<<<<<<< Updated upstream
=======
            if (PlayerControl.LocalPlayer.Data.IsDead) role.ExamineButton.SetTarget(null);

>>>>>>> Stashed changes
            __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            role.ExamineButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            role.ExamineButton.SetCoolDown(role.ExamineTimer(), CustomGameOptions.ExamineCd);
<<<<<<< Updated upstream
            Utils.SetTarget(ref role.ClosestPlayer, role.ExamineButton, float.NaN);

            var renderer = role.ExamineButton.graphic;
            if (role.ClosestPlayer != null)
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
=======

            if (role.InvestigatedPlayers.Count > 0)
            {
                Utils.SetTarget(ref role.ClosestPlayer, role.ExamineButton, float.NaN);

                var renderer = role.ExamineButton.graphic;
                if (role.ClosestPlayer != null && role.InvestigatingScene != null)
                {
                    renderer.color = Palette.EnabledColor;
                    renderer.material.SetFloat("_Desat", 0f);
                }
                else
                {
                    renderer.color = Palette.DisabledClear;
                    renderer.material.SetFloat("_Desat", 1f);
                }
>>>>>>> Stashed changes
            }

            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            var flag = (GameOptionsManager.Instance.currentNormalGameOptions.GhostsDoTasks || !data.IsDead) &&
                       (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) &&
                       PlayerControl.LocalPlayer.CanMove;
            var allocs = Physics2D.OverlapCircleAll(truePosition, maxDistance,
                LayerMask.GetMask(new[] { "Players", "Ghost" }));

            var killButton = __instance.KillButton;
<<<<<<< Updated upstream
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

            KillButtonTarget.SetTarget(killButton, closestBody, role);
=======
            CrimeScene closestScene = null;
            var closestDistance = float.MaxValue;
            foreach (var collider2D in allocs)
            {
                if (!flag || isDead || collider2D.gameObject.name != "CrimeScene") continue;
                var component = collider2D.GetComponent<CrimeScene>();
                if (component == null) continue;
                if (role.InvestigatingScene == component) continue;

                if (!(Vector2.Distance(truePosition, component.gameObject.transform.position) <=
                      maxDistance)) continue;

                var distance = Vector2.Distance(truePosition, component.gameObject.transform.position);
                if (!(distance < closestDistance)) continue;
                closestScene = component;
                closestDistance = distance;
            }

            KillButtonTarget.SetTarget(killButton, closestScene, role);
>>>>>>> Stashed changes
            killButton.SetCoolDown(0f, 1f);
        }
    }
}