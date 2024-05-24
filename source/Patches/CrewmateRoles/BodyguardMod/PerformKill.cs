using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.BodyguardMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class Guard
{
    public static bool Prefix(KillButton __instance)
    {
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard);
        if (!flag) return true;
        if (!PlayerControl.LocalPlayer.CanMove) return false;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        var role = Role.GetRole<Bodyguard>(PlayerControl.LocalPlayer);
        if (!role.ButtonUsable) return false;
        var guardButton = DestroyableSingleton<HudManager>.Instance.KillButton;
        if (__instance == guardButton)
        {
            if (__instance.isCoolingDown) return false;
            if (!__instance.isActiveAndEnabled) return false;
            if (role.GuardTimer() != 0) return false;

            if (role.GuardedPlayer == null) {
                role.GuardedPlayer = role.ClosestPlayer;
                return false;
            }
            if (role.GuardedPlayer.Data.IsDead) return false;
            role.TimeRemaining = CustomGameOptions.GuardDuration;
            role.UsesLeft--;
            role.Guard();
            Utils.Rpc(CustomRPC.BGGuard, PlayerControl.LocalPlayer.PlayerId);
            return false;
        }

        return true;
    }
}
}