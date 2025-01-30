using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class BGTargetColor
    {

        private static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard)) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;

            var role = Role.GetRole<Bodyguard>(PlayerControl.LocalPlayer);

            if (!PlayerControl.LocalPlayer.IsHypnotised() && role.guardedPlayer != null && !role.guardedPlayer.Data.IsDead)
            {
                    var colour = new Color(1f, 0.85f, 0f);
                    if (role.guardedPlayer.Is(ModifierEnum.Shy)) colour.a = Modifier.GetModifier<Shy>(role.guardedPlayer).Opacity;
                    role.guardedPlayer.nameText().color = colour;
            }
        }
    }
}