using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{
    [HarmonyPatch(typeof(HudManager))]
public class HUDProtect
{
    [HarmonyPatch(nameof(HudManager.Update))]
    public static void Postfix(HudManager __instance)
    {
        UpdateGuardButton(__instance);
    }

    public static void UpdateGuardButton(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard)) return;

        var guardButton = __instance.KillButton;
        var role = Role.GetRole<Bodyguard>(PlayerControl.LocalPlayer);

        guardButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        guardButton.SetCoolDown(role.StartTimer(), 10f);
        if (role.UsedAbility) return;
        Utils.SetTarget(ref role.ClosestPlayer, guardButton);
    }
}
}
