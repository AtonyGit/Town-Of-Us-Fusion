using System;
using HarmonyLib;
using Reactor.Utilities;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.Patches
{
    [HarmonyPatch]
public class MurderPlayer
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    public class MurderPlayerPatch
    {
        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
        {
            Utils.MurderPlayer(__instance, target, true);
            if (__instance.Is(RoleEnum.NeoNecromancer))
            {
            var role = Role.GetRole<NeoNecromancer>(__instance);
            role.LastKilled = DateTime.UtcNow;
            role.CanKill = false;
            PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Necromancer Kill patch was loaded");
            } else
            PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Necromancer Kill patch was NOT loaded");
            
            if (target.Is(RoleEnum.NeoNecromancer))
            {
                foreach (var player2 in PlayerControl.AllPlayerControls)
                {
                    if (/*player2.Is(RoleEnum.NeoNecromancer) || */player2.Is(RoleEnum.Apparitionist) || player2.Is(RoleEnum.Scourge) || player2.Is(RoleEnum.Enchanter) || player2.Is(RoleEnum.Husk)) Utils.MurderPlayer(player2, player2, true);
                }
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    [HarmonyPriority(Priority.Last)]
    public class DoClickPatch
    {
        public static bool Prefix(KillButton __instance, ref bool __runOriginal)
        {
            if (!__runOriginal) return false;
            if (__instance.isActiveAndEnabled && __instance.currentTarget && !__instance.isCoolingDown && !PlayerControl.LocalPlayer.Data.IsDead && PlayerControl.LocalPlayer.CanMove)
            {
                if (AmongUsClient.Instance.AmHost)
                {
                    PlayerControl.LocalPlayer.CheckMurder(__instance.currentTarget);
                }
                else
                {
                    Utils.Rpc(CustomRPC.CheckMurder, PlayerControl.LocalPlayer.PlayerId, __instance.currentTarget.PlayerId);
                }
                __instance.SetTarget(null);
            }
            return false;
        }
    }
}
}