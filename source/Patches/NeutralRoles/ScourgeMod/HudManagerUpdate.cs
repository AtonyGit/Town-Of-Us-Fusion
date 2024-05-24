using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.ScourgeMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public static class HudManagerUpdate
{
    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Scourge)) return;
        var role = Role.GetRole<Scourge>(PlayerControl.LocalPlayer);

        __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

        __instance.KillButton.SetCoolDown(role.ScourgeKillTimer(), CustomGameOptions.ScourgeKillCooldown);

        var notNecroTeam = PlayerControl.AllPlayerControls.ToArray()
            .Where(x => !x.Is(Faction.NeutralNecro)).ToList();
            
            if (role.ClosestPlayer != null)
            {
                role.ClosestPlayer.myRend().material.SetColor("_OutlineColor", Colors.NeoNecromancer);
            }
        Utils.SetTarget(ref role.ClosestPlayer, __instance.KillButton, float.NaN, notNecroTeam);
    }
}
}