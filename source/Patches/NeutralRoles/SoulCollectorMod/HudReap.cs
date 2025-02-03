using AmongUs.GameOptions;
using HarmonyLib;
using System.Linq;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.SoulCollectorMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HudReap
    {
        public static Sprite ReapSprite => TownOfUsFusion.ReapSprite;

        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.SoulCollector)) return;

            var role = Role.GetRole<SoulCollector>(PlayerControl.LocalPlayer);

            if (!PlayerControl.LocalPlayer.IsHypnotised())
            {
                foreach (var playerId in role.ReapedPlayers)
                {
                    var player = Utils.PlayerById(playerId);
                    var playerData = player?.Data;
                    if (playerData == null || playerData.Disconnected || playerData.IsDead || PlayerControl.LocalPlayer.Data.IsDead || player.Is(Faction.NeutralApocalypse))
                        continue;

                    var colour = Color.green;
                    if (player.Is(ModifierEnum.Shy)) colour.a = Modifier.GetModifier<Shy>(player).Opacity;
                    player.nameText().color = colour;
                }
            }

            if (role.ReapButton == null)
            {
                role.ReapButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.ReapButton.graphic.enabled = true;
                role.ReapButton.gameObject.SetActive(false);
            }

            role.ReapButton.graphic.sprite = ReapSprite;
            role.ReapButton.buttonLabelText.text = "Reap";
            role.ReapButton.buttonLabelText.SetOutlineColor(role.Color);
            role.ReapButton.transform.localPosition = new Vector3(-2f, 0f, 0f);
/*
            if (role.CollectedText == null)
            {
                role.CollectedText = Object.Instantiate(__instance.KillButton.cooldownTimerText, __instance.KillButton.transform);
                role.CollectedText.gameObject.SetActive(false);
                role.CollectedText.transform.localPosition = new Vector3(
                    role.CollectedText.transform.localPosition.x + 0.26f,
                    role.CollectedText.transform.localPosition.y + 0.29f,
                    role.CollectedText.transform.localPosition.z);
                role.CollectedText.transform.localScale = role.CollectedText.transform.localScale * 0.65f;
                role.CollectedText.alignment = TMPro.TextAlignmentOptions.Right;
                role.CollectedText.fontStyle = TMPro.FontStyles.Bold;
                role.CollectedText.enableWordWrapping = false;
            }
            if (role.CollectedText != null)
            {
                role.CollectedText.text = role.SoulsCollected + "/" + CustomGameOptions.SoulsToWin + "";
            }*/

            if (PlayerControl.LocalPlayer.Data.IsDead) role.ReapButton.SetTarget(null);

            __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.ReapButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            if (role.DummyButton == null)
            {
                role.DummyButton = Object.Instantiate(__instance.AbilityButton, __instance.AbilityButton.transform.parent);
                role.DummyButton.graphic.enabled = false;
                role.DummyButton.buttonLabelText.enabled = false;
                role.DummyButton.cooldownTimerText.enabled = false;
                role.DummyButton.gameObject.SetActive(false);
                role.DummyButton.canInteract = false;
            }
            role.DummyButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            role.DummyButton.transform.localPosition = role.ReapButton.transform.localPosition;
            role.DummyButton.SetUsesRemaining(role.SoulsCollected);
            role.DummyButton.usesRemainingText.text = role.SoulsCollected + "/" + CustomGameOptions.SoulsToWin + "";

            role.ReapButton.SetCoolDown(role.ReapTimer(), CustomGameOptions.ReapCd);

            var notReaped = PlayerControl.AllPlayerControls.ToArray().Where(
                player => !role.ReapedPlayers.Contains(player.PlayerId) && !player.Is(Faction.NeutralApocalypse)
            ).ToList();

            Utils.SetTarget(ref role.ClosestPlayer, role.ReapButton, float.NaN, notReaped);

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
            Soul closestSoul = null;
            var closestDistance = float.MaxValue;
            foreach (var collider2D in allocs)
            {
                if (!flag || isDead || collider2D.gameObject.name != "Soul") continue;
                var component = collider2D.GetComponent<Soul>();
                if (component == null) continue;

                if (!(Vector2.Distance(truePosition, component.gameObject.transform.position) <=
                      maxDistance)) continue;

                var distance = Vector2.Distance(truePosition, component.gameObject.transform.position);
                if (!(distance < closestDistance)) continue;
                closestSoul = component;
                closestDistance = distance;
            }

            KillButtonTarget.SetTarget(killButton, closestSoul, role);
            killButton.SetCoolDown(0f, 1f);
        }
    }
}