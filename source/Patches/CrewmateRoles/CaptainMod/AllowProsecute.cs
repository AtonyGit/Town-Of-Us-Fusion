using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.CaptainMod
{
    [HarmonyPatch(typeof(PlayerVoteArea))]
    public class AllowExtraVotes
    {
        [HarmonyPatch(typeof(PlayerVoteArea), nameof(PlayerVoteArea.VoteForMe))]
        public static class VoteForMe
        {
            public static bool Prefix(PlayerVoteArea __instance)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Captain)) return true;
                var role = Role.GetRole<Captain>(PlayerControl.LocalPlayer);
                if (__instance.Parent.state == MeetingHud.VoteStates.Proceeding ||
                    __instance.Parent.state == MeetingHud.VoteStates.Results)
                    return false;

                if (__instance != role.Tribunal)
                {
                    if (role.StartTribunal)
                    {
                        role.TribunalThisMeeting = true;
                        role.StartTribunal = false;
                        Utils.Rpc(CustomRPC.Tribunal, false, role.Player.PlayerId);
                    }
                    return true;
                }
                else
                {
                    role.StartTribunal = true;
                    MeetingHud.Instance.SkipVoteButton.gameObject.SetActive(false);
                    //AddTribunal.UpdateButton(role, MeetingHud.Instance);
                    if (!AmongUsClient.Instance.AmHost)
                    {
                        Utils.Rpc(CustomRPC.Tribunal, true, role.Player.PlayerId);
                    }
                    return false;
                }
            }
        }
    }

    [HarmonyPatch(typeof(ExileController), nameof(ExileController.BeginForGameplay))]
    internal class MeetingExiledEnd
    {
        private static void Postfix(ExileController __instance)
        {
            var exiled = __instance.initData.networkedPlayer;
            if (exiled == null) return;
            var player = exiled.Object;

            foreach (var role in Role.GetRoles(RoleEnum.Captain))
            {
                var prosRole = (Captain)role;
                prosRole.StartTribunal = false;
            }
        }
    }
}