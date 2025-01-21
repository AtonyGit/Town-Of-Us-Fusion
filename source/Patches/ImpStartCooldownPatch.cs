using HarmonyLib;
using System;
using UnityEngine;
using TownOfUsFusion.Extensions;
using AmongUs.GameOptions;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetKillTimer))]
public static class PatchKillTimer
{
    public static bool GameStarted = false;
    [HarmonyPriority(Priority.First)]
    public static void Prefix(PlayerControl __instance, ref float time)
    {
        bool isCursedSoul = (PlayerControl.LocalPlayer.Is(Faction.NeutralCursed));
        bool isCursedImp = (PlayerControl.LocalPlayer.Is(RoleEnum.Impostor) || PlayerControl.LocalPlayer.Is(RoleEnum.Blackmailer)
        || PlayerControl.LocalPlayer.Is(RoleEnum.Bomber) || PlayerControl.LocalPlayer.Is(RoleEnum.Escapist)
        || PlayerControl.LocalPlayer.Is(RoleEnum.Grenadier) || PlayerControl.LocalPlayer.Is(RoleEnum.Janitor)
        || PlayerControl.LocalPlayer.Is(RoleEnum.Miner) || PlayerControl.LocalPlayer.Is(RoleEnum.Morphling)
        || PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner) || PlayerControl.LocalPlayer.Is(RoleEnum.Swooper)
        || PlayerControl.LocalPlayer.Is(RoleEnum.Traitor) || PlayerControl.LocalPlayer.Is(RoleEnum.Undertaker)
        || PlayerControl.LocalPlayer.Is(RoleEnum.Venerer) || PlayerControl.LocalPlayer.Is(RoleEnum.Warlock));

        if ((__instance.Data.IsImpostor() || (isCursedSoul && isCursedImp)) && time <= 11f
            && Math.Abs(__instance.killTimer - time) > 2 * Time.deltaTime
            && GameStarted == false)
        {
            if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek)
                time = GameOptionsManager.Instance.currentHideNSeekGameOptions.KillCooldown - 0.25f;
            else time = CustomGameOptions.InitialCooldowns - 0.25f;
            GameStarted = true;
        }
    }
}
}