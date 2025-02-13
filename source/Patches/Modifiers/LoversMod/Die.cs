using HarmonyLib;
using TownOfUsFusion.CrewmateRoles.AltruistMod;
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Alliances;

namespace TownOfUsFusion.Modifiers.LoversMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Die))]
    public class Die
    {
        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] DeathReason reason)
        {
            __instance.Data.IsDead = true;

            var flag3 = __instance.IsLover() && CustomGameOptions.BothLoversDie;
            if (!flag3) return true;
            var otherLover = Alliance.GetAlliance<Lover>(__instance).OtherLover.Player;
            if (otherLover.Data.IsDead) return true;

            if (reason == DeathReason.Exile)
            {
                KillButtonTarget.DontRevive = __instance.PlayerId;
                if (!otherLover.IsInvincible()) otherLover.Exiled();
            }
            else if (AmongUsClient.Instance.AmHost && !otherLover.IsInvincible()) Utils.RpcMurderPlayer(otherLover, otherLover);
            if (otherLover.Is(RoleEnum.Sheriff))
            {
                var sheriff = Role.GetRole<Sheriff>(otherLover);
                sheriff.IncorrectKills -= 1;
            }

            return true;
        }
    }
}