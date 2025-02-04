using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.MirrorMasterMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class MirrorMeetingStart
    {
        public static void Postfix(MeetingHud __instance)
        {
            foreach(var role in Role.GetRoles(RoleEnum.MirrorMaster))
            {
                var merc = (MirrorMaster)role;
                if (merc.ShieldedPlayer != null && !merc.ShieldedPlayer.Data.Disconnected)
                {
                    merc.ShieldedPlayer.myRend().material.SetColor("_VisorColor", Palette.VisorColor);
                    merc.ShieldedPlayer.myRend().material.SetFloat("_Outline", 0f);
                }
                merc.ShieldedPlayer = null;
            }
        }
    }
}