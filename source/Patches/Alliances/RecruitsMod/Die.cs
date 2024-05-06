using HarmonyLib;
using TownOfUsFusion.CrewmateRoles.AltruistMod;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Alliances;

namespace TownOfUsFusion.Alliances.RecruitsMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Die))]
public class Die
{
    public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] DeathReason reason)
    {
        __instance.Data.IsDead = true;

        var flag3 = __instance.IsRecruit() && CustomGameOptions.DoJackalRecruitsDie;
        if (!flag3) return true;
        var otherRecruit = Alliance.GetAlliance<Recruit>(__instance).OtherRecruit.Player;
        if (otherRecruit.Data.IsDead) return true;

        if (reason == DeathReason.Exile)
        {
            KillButtonTarget.DontRevive = __instance.PlayerId;
            if (!otherRecruit.Is(RoleEnum.Pestilence)) otherRecruit.Exiled();
        }
        else if (AmongUsClient.Instance.AmHost && !otherRecruit.Is(RoleEnum.Pestilence)) Utils.RpcMurderPlayer(otherRecruit, otherRecruit);

        return true;
    }
}
}