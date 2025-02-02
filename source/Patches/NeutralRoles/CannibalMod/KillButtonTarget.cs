using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.CannibalMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.SetTarget))]
public class KillButtonTarget
{
    public static bool Prefix(KillButton __instance)
    {
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal)) return true;
        return __instance == DestroyableSingleton<HudManager>.Instance.KillButton;
    }

    public static void SetTarget(KillButton __instance, DeadBody target, Cannibal role)
    {
        if (role.CurrentTarget && role.CurrentTarget != target)
        {
            foreach (var body in role.CurrentTarget.bodyRenderers) body.material.SetFloat("_Outline", 0f);
        }

        role.CurrentTarget = target;
        if (role.CurrentTarget && __instance.enabled)
        {
            SpriteRenderer component = null;
            foreach (var body in role.CurrentTarget.bodyRenderers) component = body;
            component.material.SetFloat("_Outline", 1f);
            component.material.SetColor("_OutlineColor", Color.yellow);
            __instance.graphic.color = Palette.EnabledColor;
            __instance.graphic.material.SetFloat("_Desat", 0f);
            __instance.buttonLabelText.color = Palette.EnabledColor;
            __instance.buttonLabelText.material.SetFloat("_Desat", 0f);
            return;
        }

        __instance.graphic.color = Palette.DisabledClear;
        __instance.graphic.material.SetFloat("_Desat", 1f);
        __instance.buttonLabelText.color = Palette.DisabledClear;
        __instance.buttonLabelText.material.SetFloat("_Desat", 1f);
    }
}
}