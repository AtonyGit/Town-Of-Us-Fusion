using HarmonyLib;
using UnityEngine;

namespace TownOfUsFusion.Patches
{
    [HarmonyPatch(typeof(VitalsMinigame), nameof(VitalsMinigame.Begin))]
    public class NoVitals
    {
        public static bool Prefix(VitalsMinigame __instance)
        {
            if (PlayerControl.LocalPlayer.Data.IsDead) return true;
<<<<<<< Updated upstream
            if (CustomGameOptions.GameMode == GameMode.Cultist ||
                (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter) && !CustomGameOptions.TransporterVitals))
=======
            if ((PlayerControl.LocalPlayer.Is(RoleEnum.Transporter) && !CustomGameOptions.TransporterVitals))
>>>>>>> Stashed changes
            {
                Object.Destroy(__instance.gameObject);
                return false;
            }

            return true;
        }
    }
}