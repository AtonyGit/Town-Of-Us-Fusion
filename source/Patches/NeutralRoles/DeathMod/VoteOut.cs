using HarmonyLib;
using Reactor.Utilities;
using System.Linq;
using TownOfUsFusion.Patches.NeutralRoles;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.NeutralRoles.DeathMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CmdReportDeadBody))]
    public class SendDeathAlert
    {
        [HarmonyPostfix]
        public static void Postfix([HarmonyArgument(0)] NetworkedPlayerInfo target)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Death))
            {
                if (!((Death)role).HasSentAlert) {
                    ((Death)role).HasSentAlert = true;
                    string alert = $"The Soul Collector has transformed into Death, Horseman of the Apocalypse.\nIf they are not voted out, Apocalypse will win.";
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, alert);
                        Utils.Rpc(CustomRPC.SendChat, alert);
                }
            }
            foreach (var role in Role.GetRoles(RoleEnum.Armageddon))
            {
                if (!((Armageddon)role).HasSentAlert) {
                    ((Armageddon)role).HasSentAlert = true;
                    string alert = $"The Juggernaut has transformed into Armageddon, Horseman of the Apocalypse.\nThey can now destroy people in groups.";
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, alert);
                        Utils.Rpc(CustomRPC.SendChat, alert);
                }
            }
            foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
            {
                if (!((Pestilence)role).HasSentAlert) {
                    ((Pestilence)role).HasSentAlert = true;
                    string alert = $"The Plaguebearer has transformed into Pestilence, Horseman of the Apocalypse.\nThey can now spread pestilence to anyone they wish.";
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, alert);
                        Utils.Rpc(CustomRPC.SendChat, alert);
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

            foreach (var role in Role.GetRoles(RoleEnum.Death))
                if (player.PlayerId != ((Death)role).Player.PlayerId)
                {
                    ((Death)role).Wins();
                }
        }
    }
}