using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Apocalypse;

namespace TownOfUsFusion.NeutralRoles.WarMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public static class HudManagerUpdate
{
    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.War)) return;
        var role = Role.GetRole<War>(PlayerControl.LocalPlayer);

        __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

        __instance.KillButton.SetCoolDown(role.KillTimer(), 0f);

        var notApocTeam = PlayerControl.AllPlayerControls.ToArray()
            .Where(x => !x.Is(Faction.NeutralApocalypse)).ToList();

        Utils.SetTarget(ref role.ClosestPlayer, __instance.KillButton, float.NaN, notApocTeam);
    }
}
}