﻿using HarmonyLib;
using System.Linq;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Patches
{
    [HarmonyPatch]
    public static class SizePatch
    {
        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        [HarmonyPostfix]
        public static void Postfix(HudManager __instance)
        {
            foreach (var player in PlayerControl.AllPlayerControls.ToArray())
            {
<<<<<<< Updated upstream
                if (!(player.Data.IsDead || player.Data.Disconnected))
=======
                if (player.Data != null && !(player.Data.IsDead || player.Data.Disconnected))
>>>>>>> Stashed changes
                    player.transform.localScale = player.GetAppearance().SizeFactor;
                else
                    player.transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            }

            var playerBindings = PlayerControl.AllPlayerControls.ToArray().ToDictionary(player => player.PlayerId);
            var bodies = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            foreach (var body in bodies)
            {
                try {
                    body.transform.localScale = playerBindings[body.ParentId].GetAppearance().SizeFactor;
                } catch {
                }
            }
        }
    }
}