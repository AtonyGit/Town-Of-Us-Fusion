using HarmonyLib;
using System.Linq;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.CrewmateRoles.PsychicMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class MeetingStartPsychic
    {
        public static void Postfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Psychic)) return;
            var psychicRole = Role.GetRole<Psychic>(PlayerControl.LocalPlayer);
            psychicRole.IsSeerMode = !psychicRole.IsSeerMode;
            psychicRole.RegenTask();
            if (psychicRole.Confessor != null)
            {
                var playerResults = PlayerReportFeedback(psychicRole.Confessor);

                if (!string.IsNullOrWhiteSpace(playerResults)) DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, playerResults);
            }
        }

        public static string PlayerReportFeedback(PlayerControl player)
        {
            if (player.Data.IsDead || player.Data.Disconnected) return "Your vision failed as your target perished";
            var allPlayers = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && x != PlayerControl.LocalPlayer && x != player).ToList();
            if (allPlayers.Count < 2) return "Too few people alive to receive a vision";
            var evilPlayers = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected &&
            (x.Is(Faction.Impostors) || (x.Is(Faction.NeutralKilling) && CustomGameOptions.NeutralKillingShowsEvil) ||
            (x.Is(Faction.NeutralEvil) && CustomGameOptions.NeutralEvilShowsEvil) || (x.Is(Faction.NeutralBenign) && CustomGameOptions.NeutralBenignShowsEvil))).ToList();
            //if (evilPlayers.Count == 0) return $"{player.GetDefaultOutfit().PlayerName} confesses to knowing that there are no more evil players!"; 
            if (evilPlayers.Count == 0) return $"Your vision with {player.GetDefaultOutfit().PlayerName} reveals that there are no more evil players!";
            allPlayers.Shuffle();
            evilPlayers.Shuffle();
            var secondPlayer = allPlayers[0];
            var firstTwoEvil = false;
            foreach (var evilPlayer in evilPlayers)
            {
                if (evilPlayer == player || evilPlayer == secondPlayer) firstTwoEvil = true;
            }
            if (firstTwoEvil)
            {
                var thirdPlayer = allPlayers[1];
                return $"Your vision revealed that {player.GetDefaultOutfit().PlayerName}, {secondPlayer.GetDefaultOutfit().PlayerName} and/or {thirdPlayer.GetDefaultOutfit().PlayerName} is evil!";
            }
            else
            {
                var thirdPlayer = evilPlayers[0];
                return $"Your vision revealed that {player.GetDefaultOutfit().PlayerName}, {secondPlayer.GetDefaultOutfit().PlayerName} and/or {thirdPlayer.GetDefaultOutfit().PlayerName} is evil!";
            }
        }
    }
}