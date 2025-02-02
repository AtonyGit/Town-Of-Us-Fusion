using HarmonyLib;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.ImpostorRoles.PoisonerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.SetTarget))]
    public class SetTarget
    {
        public static void Postfix(KillButton __instance, [HarmonyArgument(0)] PlayerControl target)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner)) return;
            var role = Role.GetRole<Poisoner>(PlayerControl.LocalPlayer);
            if (target != null && __instance == DestroyableSingleton<HudManager>.Instance.KillButton)
                if (target.Data.IsImpostor())
                {
                    __instance.graphic.color = Palette.DisabledClear;
                    __instance.graphic.material.SetFloat("_Desat", 1f);
                    __instance.buttonLabelText.color = Palette.DisabledClear;
                    __instance.buttonLabelText.material.SetFloat("_Desat", 1f);
                }
        }
    }
}