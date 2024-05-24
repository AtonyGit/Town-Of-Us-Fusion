using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.InquisitorMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.SetTarget))]
public class BodyButtonTarget
{
    public static bool Prefix(KillButton __instance)
    {
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Inquisitor)) return true;
        return __instance == DestroyableSingleton<HudManager>.Instance.KillButton;
    }

    public static void SetTarget(KillButton __instance, DeadBody target, Inquisitor role)
    {
        if (role.CurrentBodyTarget && role.CurrentBodyTarget != target)
        {
            foreach (var body in role.CurrentBodyTarget.bodyRenderers) body.material.SetFloat("_Outline", 0f);
        }

        role.CurrentBodyTarget = target;
        if (role.CurrentBodyTarget && __instance.enabled)
        {
            SpriteRenderer component = null;
            foreach (var body in role.CurrentBodyTarget.bodyRenderers) component = body;
            component.material.SetFloat("_Outline", 1f);
            component.material.SetColor("_OutlineColor", Color.yellow);
            __instance.graphic.color = Palette.EnabledColor;
            __instance.graphic.material.SetFloat("_Desat", 0f);
            return;
        }

        __instance.graphic.color = Palette.DisabledClear;
        __instance.graphic.material.SetFloat("_Desat", 1f);
    }
}
}