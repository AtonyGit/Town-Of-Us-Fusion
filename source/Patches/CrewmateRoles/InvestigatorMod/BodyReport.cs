using System;
using System.Linq;
using HarmonyLib;
using TownOfUsFusion.CrewmateRoles.MedicMod;

namespace TownOfUsFusion.CrewmateRoles.InvestigatorMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CmdReportDeadBody))]
    internal class BodyReportPatch
    {
        private static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] NetworkedPlayerInfo info)
        {
            if (info == null) return;
            var matches = Murder.KilledPlayers.Where(x => x.PlayerId == info.PlayerId).ToArray();
            DeadPlayer killer = null;

            if (matches.Length > 0)
                killer = matches[0];

            if (killer == null)
                return;

            var isInvestigatorAlive = __instance.Is(RoleEnum.Investigator);
            var areReportsEnabled = CustomGameOptions.InvestigatorReportOn;

            if (!isInvestigatorAlive || !areReportsEnabled)
                return;

            var isUserInvestigator = PlayerControl.LocalPlayer.Is(RoleEnum.Investigator);
            if (!isUserInvestigator)
                return;
            var br = new BodyReport
            {
                Killer = Utils.PlayerById(killer.KillerId),
                Reporter = __instance,
                Body = Utils.PlayerById(killer.PlayerId),
                KillAge = (float) (DateTime.UtcNow - killer.KillTime).TotalMilliseconds
            };

            var reportMsg = BodyReport.ParseBodyReport(br);

            if (string.IsNullOrWhiteSpace(reportMsg))
                return;

            if (DestroyableSingleton<HudManager>.Instance)
                DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, reportMsg);
        }
    }
}