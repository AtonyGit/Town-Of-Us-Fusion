using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.DoomsayerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class HudManagerUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer)) return;
            var role = Role.GetRole<Doomsayer>(PlayerControl.LocalPlayer);

            __instance.KillButton.SetCoolDown(role.ObserveTimer(), CustomGameOptions.ObserveCooldown);
            Utils.SetTarget(ref role.ClosestPlayer, __instance.KillButton);
        }
    }
}