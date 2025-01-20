using System;
using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;
<<<<<<< Updated upstream
using TownOfUs.CrewmateRoles.MedicMod;
using Reactor.Utilities;
using AmongUs.GameOptions;
=======
using Reactor.Utilities;
using AmongUs.GameOptions;
using TownOfUsFusion.Extensions;
>>>>>>> Stashed changes

namespace TownOfUs.CrewmateRoles.DetectiveMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKill
    {
        public static bool Prefix(KillButton __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Detective)) return true;
            var role = Role.GetRole<Detective>(PlayerControl.LocalPlayer);
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];

            if (__instance == role.ExamineButton)
            {
                var flag2 = role.ExamineTimer() == 0f;
                if (!flag2) return false;
<<<<<<< Updated upstream
                if (role.ClosestPlayer == null) return false;
                if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                    PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
                if (role.ClosestPlayer == null) return false;
                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
                if (interact[4] == true)
                {
                    if (role.DetectedKillers.Contains(role.ClosestPlayer.PlayerId) || (CustomGameOptions.CanDetectLastKiller && role.LastKiller == role.ClosestPlayer)) Coroutines.Start(Utils.FlashCoroutine(Color.red));
=======
                if (role.InvestigatingScene == null) return false;
                if (role.ClosestPlayer == null) return false;
                if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                    PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
                if (interact[4] == true)
                {
                    if (role.InvestigatedPlayers.Contains(role.ClosestPlayer.PlayerId))
                    {
                        Coroutines.Start(Utils.FlashCoroutine(Color.red));
                        var deadPlayer = role.InvestigatingScene.DeadPlayer;
                        if (DestroyableSingleton<HudManager>.Instance)
                            DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, $"{role.ClosestPlayer.GetDefaultOutfit().PlayerName} was at the scene of {deadPlayer.GetDefaultOutfit().PlayerName}'s death!");
                    }
>>>>>>> Stashed changes
                    else Coroutines.Start(Utils.FlashCoroutine(Color.green));
                }
                if (interact[0] == true)
                {
                    role.LastExamined = DateTime.UtcNow;
                    return false;
                }
                else if (interact[1] == true)
                {
                    role.LastExamined = DateTime.UtcNow;
                    role.LastExamined = role.LastExamined.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.ExamineCd);
                    return false;
                }
                else if (interact[3] == true) return false;
                return false;
            }
            else
            {
                if (role.CurrentTarget == null)
                    return false;
<<<<<<< Updated upstream
                if (Vector2.Distance(role.CurrentTarget.TruePosition,
                    PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
                var playerId = role.CurrentTarget.ParentId;
                var player = Utils.PlayerById(playerId);
=======
                if (Vector2.Distance(role.CurrentTarget.gameObject.transform.position,
                    PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
                var player = role.CurrentTarget.DeadPlayer;
                var abilityUsed = Utils.AbilityUsed(PlayerControl.LocalPlayer);
                if (!abilityUsed) return false;
>>>>>>> Stashed changes
                if (player.IsInfected() || role.Player.IsInfected())
                {
                    foreach (var pb in Role.GetRoles(RoleEnum.Plaguebearer)) ((Plaguebearer)pb).RpcSpreadInfection(player, role.Player);
                }
<<<<<<< Updated upstream
                foreach (var deadPlayer in Murder.KilledPlayers)
                {
                    if (deadPlayer.PlayerId == playerId) role.DetectedKillers.Add(deadPlayer.KillerId);
                }
=======
                role.InvestigatingScene = role.CurrentTarget;
                role.InvestigatedPlayers.AddRange(role.CurrentTarget.ScenePlayers);
>>>>>>> Stashed changes
                return false;
            }
        }
    }
}
