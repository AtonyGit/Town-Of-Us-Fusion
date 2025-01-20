using HarmonyLib;
<<<<<<< Updated upstream
using TownOfUsFusion.Roles;
=======
using Reactor.Utilities;
using TownOfUsFusion.Patches.NeutralRoles;
using TownOfUsFusion.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;
>>>>>>> Stashed changes

namespace TownOfUsFusion.CrewmateRoles.TransporterMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKillButton
    {
        public static bool Prefix(KillButton __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Transporter)) return true;
            var role = Role.GetRole<Transporter>(PlayerControl.LocalPlayer);
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            if (!__instance.enabled) return false;
            if (role.TransportTimer() != 0f) return false;

<<<<<<< Updated upstream
            if (role.TransportList == null && role.ButtonUsable)
            {
                role.PressedButton = true;
                role.MenuClick = true;
=======
            if (role.ButtonUsable)
            {
                try
                {
                    PlayerMenu.singleton.Menu.ForceClose();
                }
                catch {
                    role.TransportPlayer1 = null;
                    role.TransportPlayer2 = null;
                    List<byte> transportTargets = new List<byte>();
                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        if (!player.Data.Disconnected)
                        {
                            if (!player.Data.IsDead) transportTargets.Add(player.PlayerId);
                            else
                            {
                                foreach (var body in Object.FindObjectsOfType<DeadBody>())
                                {
                                    if (body.ParentId == player.PlayerId) transportTargets.Add(player.PlayerId);
                                }
                            }
                        }
                    }
                    byte[] transporttargetIDs = transportTargets.ToArray();
                    var pk = new PlayerMenu((x) =>
                    {
                        role.TransportPlayer1 = x;
                        role.SwappingMenus = true;
                        Coroutines.Start(role.OpenSecondMenu());
                    }, (y) =>
                    {
                        return transporttargetIDs.Contains(y.PlayerId);
                    });
                    Coroutines.Start(pk.Open(0f, true));
                }
>>>>>>> Stashed changes
            }

            return false;
        }
    }
}