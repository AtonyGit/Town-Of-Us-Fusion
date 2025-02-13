using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.NeutralRoles.SentinelMod;
using UnityEngine;
using AmongUs.GameOptions;
using TownOfUsFusion.Patches;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(HudManager))]
public static class HudManagerVentPatch
{
    [HarmonyPatch(nameof(HudManager.Update))]
    public static void Postfix(HudManager __instance)
    {
        if (__instance.ImpostorVentButton == null || __instance.ImpostorVentButton.gameObject == null || __instance.ImpostorVentButton.IsNullOrDestroyed())
            return;

        bool active = PlayerControl.LocalPlayer != null && VentPatches.CanVent(PlayerControl.LocalPlayer, PlayerControl.LocalPlayer._cachedData) && !MeetingHud.Instance;
        if (active != __instance.ImpostorVentButton.gameObject.active)
            __instance.ImpostorVentButton.gameObject.SetActive(active);
    }
}

[HarmonyPatch(typeof(Vent), nameof(Vent.CanUse))]
public static class VentPatches
{
    public static bool CanVent(PlayerControl player, GameData.PlayerInfo playerInfo)
    {
        if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek) return false;

        if (player.inVent)
            return true;

        if (playerInfo.IsDead)
            return false;

        if (CustomGameOptions.GameMode == GameMode.Cultist && !player.Is(RoleEnum.Engineer)) return false;
        else if (CustomGameOptions.GameMode == GameMode.Cultist && player.Is(RoleEnum.Engineer)) return true;

        if (player.Is(RoleEnum.Morphling) && !CustomGameOptions.MorphlingVent
            || player.Is(RoleEnum.Swooper) && !CustomGameOptions.SwooperVent
            || player.Is(RoleEnum.Grenadier) && !CustomGameOptions.GrenadierVent
            || player.Is(RoleEnum.Undertaker) && !CustomGameOptions.UndertakerVent
            || player.Is(RoleEnum.Escapist) && !CustomGameOptions.EscapistVent
            || player.Is(RoleEnum.Poisoner) && !CustomGameOptions.PoisonerVent
            || player.Is(RoleEnum.Bomber) && !CustomGameOptions.BomberVent
            || (player.Is(RoleEnum.Undertaker) && Role.GetRole<Undertaker>(player).CurrentlyDragging != null && !CustomGameOptions.UndertakerVentWithBody))
            return false;

        if (player.Is(RoleEnum.Engineer) ||
            (player.Is(RoleEnum.Glitch) && CustomGameOptions.GlitchVent) || (player.Is(RoleEnum.Berserker) && CustomGameOptions.JuggVent) ||
            (player.Is(RoleEnum.Husk) && CustomGameOptions.CanHuskVent) ||
            (player.Is(RoleEnum.Pestilence) && CustomGameOptions.PestVent) || (player.Is(RoleEnum.Jester) && CustomGameOptions.JesterVent) ||
            (player.Is(RoleEnum.Vampire) && CustomGameOptions.VampVent) ||
            (player.Is(RoleEnum.Sentinel) && CustomGameOptions.SentinelVent))
            return true;

        if (player.Is(RoleEnum.Werewolf) && CustomGameOptions.WerewolfVent)
        {
            var role = Role.GetRole<Werewolf>(PlayerControl.LocalPlayer);
            if (role.Rampaged) return true;
        }

        return playerInfo.IsImpostor();
    }

    public static void Postfix(Vent __instance,
        [HarmonyArgument(0)] GameData.PlayerInfo playerInfo,
        [HarmonyArgument(1)] ref bool canUse,
        [HarmonyArgument(2)] ref bool couldUse,
        ref float __result)
    {
        float num = float.MaxValue;
        PlayerControl playerControl = playerInfo.Object;

        if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.Normal) couldUse = CanVent(playerControl, playerInfo) && !playerControl.MustCleanVent(__instance.Id) && (!playerInfo.IsDead || playerControl.inVent) && (playerControl.CanMove || playerControl.inVent);
        else if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek && playerControl.Data.IsImpostor()) couldUse = false;
        else couldUse = canUse;

        var ventitaltionSystem = ShipStatus.Instance.Systems[SystemTypes.Ventilation].Cast<VentilationSystem>();

        if (ventitaltionSystem != null && ventitaltionSystem.IsVentCurrentlyBeingCleaned(__instance.Id))
        {
            couldUse = false;
        }

        canUse = couldUse;

        if (canUse)
        {
            Vector3 center = playerControl.Collider.bounds.center;
            Vector3 position = __instance.transform.position;
            num = Vector2.Distance((Vector2)center, (Vector2)position);

            if (__instance.Id == 14 && SubmergedCompatibility.isSubmerged())
                canUse &= (double)num <= (double)__instance.UsableDistance;
            else
                canUse = ((canUse ? 1 : 0) & ((double)num > (double)__instance.UsableDistance ? 0 : (!PhysicsHelpers.AnythingBetween(playerControl.Collider, (Vector2)center, (Vector2)position, Constants.ShipOnlyMask, false) ? 1 : 0))) != 0;

        }

        __result = num;
    }
}

[HarmonyPatch(typeof(Vent), nameof(Vent.SetButtons))]
public static class JesterEnterVent
{
    public static bool Prefix(Vent __instance)
    {
        foreach (var sent in Role.GetRoles(RoleEnum.Sentinel))
        {
            var sentRole = (Sentinel)sent;
            var sentinel = Role.GetRole<Sentinel>(sent.Player);

            float num = float.MaxValue;
            Vector3 center = sentRole.DynamitePoint;
            Vector3 position = PlayerControl.LocalPlayer.transform.position;
            num = Vector2.Distance((Vector2)center, (Vector2)position);
            if (Utils.GetClosestPlayers(center, CustomGameOptions.PlaceRadius, false).Contains(PlayerControl.LocalPlayer) && !PlayerControl.LocalPlayer.Is(RoleEnum.Sentinel))
            {
                sentRole.DynamiteTriggered = true;
            }
        }
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Jester) && CustomGameOptions.JesterVent)
            return false;
        return true;
    }
}
}