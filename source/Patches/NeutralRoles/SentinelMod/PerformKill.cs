using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using AmongUs.GameOptions;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.SentinelMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class PerformKill
{
    public static Sprite ChargeSprite => TownOfUsFusion.RampageSprite;
    public static Sprite ChargingSprite => TownOfUsFusion.PoisonedSprite;
    public static Sprite DynamiteSprite => TownOfUsFusion.PlantSprite;
    public static Sprite AwaitSprite => TownOfUsFusion.DetonateSprite;

    public static bool Prefix(KillButton __instance)
    {
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Sentinel);
        if (!flag) return true;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        var role = Role.GetRole<Sentinel>(PlayerControl.LocalPlayer);

        if (__instance == role.ChargeButton)
        {
            var flag2 = __instance.isCoolingDown;
            if (flag2) return false;
            if (role.Player.inVent) return false;
            if (role.ClosestPlayer == null) return false;
            if (!__instance.isActiveAndEnabled) return false;
            if (role.ChargeButton.graphic.sprite == ChargeSprite)
            {

                role.ChargeSetOff = false;
                role.ChargedPlayer = role.ClosestPlayer;
                role.LastKilled = DateTime.UtcNow;
                role.LastCharged = DateTime.UtcNow;
                role.LastKilled = role.LastKilled.AddSeconds(CustomGameOptions.ChargeDelay);
                role.LastCharged = role.LastKilled.AddSeconds(CustomGameOptions.ChargeDelay);
                var pos = role.ChargedPlayer.transform.position;
                pos.z += 0.001f;
                role.ChargeButton.graphic.sprite = ChargingSprite;
                role.ChargeTimeRemaining = CustomGameOptions.ChargeDelay;
                role.ChargeButton.SetCoolDown(role.ChargeTimeRemaining, CustomGameOptions.ChargeDelay);
                PlayerControl.LocalPlayer.SetKillTimer(role.KillTimer() + CustomGameOptions.ChargeDelay);
                DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);
                role.ChargeUsesLeft--;
                role.Charge = ChargeExtentions.CreateCharge(role.ChargedPlayer);
                return false;
            }
            else return false;
        }
        if (__instance == role.PlaceButton)
        {
            var flag2 = __instance.isCoolingDown;
            if (flag2) return false;
            if (!role.Player.inVent) return false;
            if (role.DynamiteUsed) return false;
            if (!__instance.isActiveAndEnabled) return false;
                role.DynamiteTriggered = false;
                role.DynamiteUsed = true;
                role.DynamitePoint = new Vector2(PlayerControl.LocalPlayer.transform.position.x, PlayerControl.LocalPlayer.transform.position.y + 0.3636f);
                var pos = role.DynamitePoint;
                pos.z += 0.001f;
                role.DynamitePoint = pos;
                role.PlaceButton.graphic.sprite = AwaitSprite;

                DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);
                role.PlaceUsesLeft--;
                role.Dynamite = DynamiteExtentions.CreateDynamite(pos);
                return false;
        }
        if (__instance == role.StunButton)
        {
            var flag2 = __instance.isCoolingDown;
            if (flag2) return false;
            if (role.ClosestPlayer == null) return false;
            var interact3 = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, false);
                if (interact3[4])
                {
                    role.StunnedPlayer = role.ClosestPlayer;
                    role.StunUsesLeft--;
                    role.SetStunned(role.StunnedPlayer);
                    //role.LastStunned = DateTime.UtcNow;
                    //role.LastStunned.AddSeconds(CustomGameOptions.StunDuration);
                }
                if (interact3[0])
                {
                    //role.LastStunned = DateTime.UtcNow;
                    role.StunnedPlayer = role.ClosestPlayer;
                    role.StunUsesLeft--;
                    role.SetStunned(role.StunnedPlayer);
                    return false;
                }
                else if (interact3[1])
                {
                    role.LastStunned = DateTime.UtcNow;
                    role.LastStunned.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.SentinelStunCd);
                    return false;
                }
                else if (interact3[3])
                {
                    return false;
                }
                return false;
        }

        if (!PlayerControl.LocalPlayer.CanMove) return false;
        if (role.Player.inVent) return false;
        if (role.KillTimer() != 0) return false;
        if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
        if (!__instance.isActiveAndEnabled || __instance.isCoolingDown) return false;
        if (role.ClosestPlayer == null) return false;
        var distBetweenPlayers = Utils.GetDistBetweenPlayers(PlayerControl.LocalPlayer, role.ClosestPlayer);
        var flag3 = distBetweenPlayers <
                    GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        if (!flag3) return false;

        var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
        if (interact[4] == true) return false;
        else if (interact[0] == true)
        {
            role.LastKilled = DateTime.UtcNow;
            role.LastCharged = DateTime.UtcNow;

            return false;
        }
        else if (interact[1] == true)
        {
            role.LastKilled = DateTime.UtcNow;
            role.LastCharged = DateTime.UtcNow;
            role.LastKilled = role.LastKilled.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.SentinelKillCd);
            role.LastCharged = role.LastKilled.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.SentinelChargeCd);
            return false;
        }
        else if (interact[2] == true)
        {
            role.LastKilled = DateTime.UtcNow;
            role.LastCharged = DateTime.UtcNow;
            role.LastKilled = role.LastKilled.AddSeconds(CustomGameOptions.VestKCReset - CustomGameOptions.SentinelKillCd);
            role.LastCharged = role.LastKilled.AddSeconds(CustomGameOptions.VestKCReset - CustomGameOptions.SentinelChargeCd);
            return false;
        }
        else if (interact[3] == true) return false;

        return false;
    }
}
}
