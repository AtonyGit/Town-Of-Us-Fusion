using System;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Roles;
using TownOfUsFusion.CrewmateRoles.AltruistMod;

namespace TownOfUsFusion.CrewmateRoles.CaptainMod
{
    [HarmonyPatch(typeof(AirshipExileController), nameof(AirshipExileController.WrapUpAndSpawn))]
    public static class AirshipExileController_WrapUpAndSpawn
    {
        public static void Postfix(AirshipExileController __instance) => ExilePros.ExileControllerPostfix(__instance);
    }

    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public class ExilePros
    {
        public static void ExileControllerPostfix(ExileController __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Captain))
            {
                var cap = (Captain)role;
                if (cap.TribunalThisMeeting)
                {
                        cap.EjectionsPerTribunal -= 1;
                        if (cap.EjectionsPerTribunal == 0) 
                        {
                            cap.TribunalThisMeeting = false;
                            cap.TribunalsLeft -= 1;
                            cap.EjectionsPerTribunal = CustomGameOptions.MaxTribunalEjects;
                        }

                    if (cap.TribunalThisMeeting)
                    {
                        Utils.Rpc(CustomRPC.CallTribunalMeeting, PlayerControl.LocalPlayer.PlayerId);

                        if (AmongUsClient.Instance.AmHost)
                        {
                            MeetingRoomManager.Instance.reporter = PlayerControl.LocalPlayer;
                            MeetingRoomManager.Instance.target = null;
                            AmongUsClient.Instance.DisconnectHandlers.AddUnique(
                                MeetingRoomManager.Instance.Cast<IDisconnectHandler>());
                            //if (GameManager.Instance.CheckTaskCompletion());
                            DestroyableSingleton<HudManager>.Instance.OpenMeetingRoom(PlayerControl.LocalPlayer);
                            PlayerControl.LocalPlayer.RpcStartMeeting(null);
                        }
                    }
                }
            }
        }

        public static void Postfix(ExileController __instance) => ExileControllerPostfix(__instance);

        [HarmonyPatch(typeof(Object), nameof(Object.Destroy), new Type[] { typeof(GameObject) })]
        public static void Prefix(GameObject obj)
        {
            if (!SubmergedCompatibility.Loaded || GameOptionsManager.Instance?.currentNormalGameOptions?.MapId != 6) return;
            if (obj.name?.Contains("ExileCutscene") == true) ExileControllerPostfix(ExileControllerPatch.lastExiled);
        }
    }
}