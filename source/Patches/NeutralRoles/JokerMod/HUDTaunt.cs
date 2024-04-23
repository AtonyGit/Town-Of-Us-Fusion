using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.JokerMod
{
    [HarmonyPatch(typeof(HudManager))]
public class HUDTaunt
{
    [HarmonyPatch(nameof(HudManager.Update))]
    public static void Postfix(HudManager __instance)
    {
        UpdateTauntButton(__instance);
    }

    public static void UpdateTauntButton(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Joker)) return;

        var tauntButton = __instance.KillButton;
        var role = Role.GetRole<Joker>(PlayerControl.LocalPlayer);

        tauntButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        tauntButton.SetCoolDown(role.StartTimer(), 10f);
        if (role.UsedAbility) return;
        //Utils.SetJkTarget(ref role.ClosestPlayer, tauntButton);
        Utils.SetTarget(ref role.ClosestPlayer, tauntButton);
    }
}
}
