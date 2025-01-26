using HarmonyLib;

namespace TownOfUsFusion.Modifiers
{
    public class Oblivious
    {
        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        public class HudManagerUpdate
        {
            public static void Postfix(HudManager __instance)
            {
                if (PlayerControl.LocalPlayer.Is(ModifierEnum.Oblivious))
                {
                    try {
                        DestroyableSingleton<HudManager>.Instance.ReportButton.SetActive(false); }
                    catch {}
                }
            }
        }
    }
}