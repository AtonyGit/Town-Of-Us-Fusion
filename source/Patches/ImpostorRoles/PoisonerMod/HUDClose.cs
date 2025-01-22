using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.ImpostorRoles.PoisonerMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner))
            {
                var role = Role.GetRole<Poisoner>(PlayerControl.LocalPlayer);
                role.PoisonButton.graphic.sprite = TownOfUsFusion.PoisonSprite;
                role.PoisonButton.buttonLabelText.text = "Poison";
                role.LastPoisoned = DateTime.UtcNow;
            }
        }
    }
}