using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.TrackerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class AddPrints
    {
        private static float _time;

        public static bool GameStarted = false;
        private static float Interval => CustomGameOptions.FootprintInterval;
        private static bool Vent => CustomGameOptions.VentFootprintVisible;

        private static Vector2 Position(PlayerControl player)
        {
            return player.GetTruePosition() + new Vector2(0, 0.366667f);
        }


        public static void Postfix(HudManager __instance)
        {
            if ((GameManager.Instance && !GameManager.Instance.GameHasStarted) || !PlayerControl.LocalPlayer.Is(RoleEnum.Tracker)) return;
            if (MeetingHud.Instance) return;
            // New Footprint
            var tracker = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);

            if (PlayerControl.LocalPlayer.Data.IsDead)
            {
                Footprint.DestroyAll(tracker);
                return;
            }

            _time += Time.deltaTime;
            if (_time >= Interval)
            {
                _time -= Interval;
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (player == null || player.Data.IsDead ||
                        player.PlayerId == PlayerControl.LocalPlayer.PlayerId) continue;
                    if (CustomGameOptions.SeeOnlyTrackedPrints && !tracker.IsTracking(player)) continue;
                    if ((player.Is(RoleEnum.Swooper) && Role.GetRole<Swooper>(player).IsSwooped) || PlayerControl.LocalPlayer.IsHypnotised()) continue;
                    var canPlace = !tracker.AllPrints.Any(print =>
                        Vector3.Distance(print.Position, Position(player)) < 0.5f &&
                        print.Color.a > 0.5 &&
                        print.Player.PlayerId == player.PlayerId);

                    if (Vent && ShipStatus.Instance != null)
                        if (ShipStatus.Instance.AllVents.Any(vent =>
                            Vector2.Distance(vent.gameObject.transform.position, Position(player)) < 1f))
                            canPlace = false;

                    if (canPlace) new Footprint(player, tracker);
                }

                for (var i = 0; i < tracker.AllPrints.Count; i++)
                {
                    try
                    {
                        var footprint = tracker.AllPrints[i];
                        if (footprint.Update()) i--;
                    } catch
                    {
                        //assume footprint value is null and allow the loop to continue
                        continue;
                    }
                    
                }
            }
        }
    }
}