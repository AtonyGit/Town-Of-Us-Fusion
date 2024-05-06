using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Alliances;
using TownOfUsFusion.Roles.Apocalypse;

namespace TownOfUsFusion.NeutralRoles.JackalMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public static class HudManagerUpdate
{
    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Jackal)) return;
        var isDead = PlayerControl.LocalPlayer.Data.IsDead;
        var role = Role.GetRole<Jackal>(PlayerControl.LocalPlayer);

        __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

        __instance.KillButton.SetCoolDown(role.JackalKillTimer(), CustomGameOptions.JackalKillCooldown);

        var notRecruits = PlayerControl.AllPlayerControls.ToArray()
            .Where(x => !x.Is(AllianceEnum.Recruit)).ToList();

        Utils.SetTarget(ref role.ClosestPlayer, __instance.KillButton, float.NaN, notRecruits);
    }
}
}