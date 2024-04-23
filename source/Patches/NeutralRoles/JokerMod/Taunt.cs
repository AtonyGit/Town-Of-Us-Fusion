using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.JokerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class Taunt
{
    public static bool Prefix(KillButton __instance)
    {
        if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
        var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Joker);
        if (!flag) return true;
        var role = Role.GetRole<Joker>(PlayerControl.LocalPlayer);
        if (!PlayerControl.LocalPlayer.CanMove) return false;
        if (PlayerControl.LocalPlayer.Data.IsDead) return false;
        if (role.UsedAbility || role.ClosestPlayer == null) return false;
        if (role.StartTimer() > 0) return false;

        var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
        if (interact[4] == true)
        {
            Utils.Rpc(CustomRPC.SetJkTarget, PlayerControl.LocalPlayer.PlayerId, role.ClosestPlayer.PlayerId);

            //var joker = new Joker(PlayerControl.LocalPlayer);
            //role.ShieldedPlayer = role.ClosestPlayer;
            role.UsedAbility = true;
            role.TaskText = () => "Get {target.name} lynched to start your master plan.\nFake Tasks:";
            role.target.nameText().color = Color.black;
            return false;
        }
        return false;
    }
}
}
