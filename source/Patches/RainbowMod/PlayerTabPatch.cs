﻿using HarmonyLib;
using UnityEngine;

namespace TownOfUsFusion.RainbowMod
{
    [HarmonyPatch(typeof(PlayerTab))]
public static class PlayerTabPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(PlayerTab.OnEnable))]
    public static void OnEnablePostfix(PlayerTab __instance)
    {
        for (int i = 0; i < __instance.ColorChips.Count; i++)
        {
            var colorChip = __instance.ColorChips[i];
            colorChip.transform.localScale *= 0.55f;
            var x = __instance.XRange.Lerp((i % 8) / 6.95f);
            var y = __instance.YStart - (i / 8) * 0.35f + 0.05f;
            colorChip.transform.localPosition = new Vector3(x, y, -1f);
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(PlayerTab.Update))]
    public static void UpdatePostfix(PlayerTab __instance)
    {
        for (int i = 0; i < __instance.ColorChips.Count; i++)
        {
            if (RainbowUtils.IsRainbow(i)) __instance.ColorChips[i].Inner.SpriteColor = RainbowUtils.Rainbow;
            if (RainbowUtils.IsGalaxy(i)) __instance.ColorChips[i].Inner.SpriteColor = RainbowUtils.Galaxy;
            if (RainbowUtils.IsFire(i)) __instance.ColorChips[i].Inner.SpriteColor = RainbowUtils.Fire;
            if (RainbowUtils.IsAcid(i)) __instance.ColorChips[i].Inner.SpriteColor = RainbowUtils.Acid;
            if (RainbowUtils.IsMonochrome(i))
            {
                __instance.ColorChips[i].Inner.SpriteColor = RainbowUtils.Monochrome;
                break;
            }
        }

    }
}
}
