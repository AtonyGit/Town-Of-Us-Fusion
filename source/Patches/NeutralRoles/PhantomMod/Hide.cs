using HarmonyLib;
using TownOfUs.Extensions;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.NeutralRoles.PhantomMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    [HarmonyPriority(Priority.Last)]
    public class Hide
    {
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Phantom))
            {
                var phantom = (Phantom)role;
<<<<<<< Updated upstream
                if (role.Player.Data.Disconnected) return;
=======
                if (role.Player.Data != null && role.Player.Data.Disconnected) return;
>>>>>>> Stashed changes
                var caught = phantom.Caught;
                if (!caught)
                {
                    phantom.Fade();
                }
                else if (phantom.Faded)
                {
                    Utils.Unmorph(phantom.Player);
                    phantom.Player.myRend().color = Color.white;
                    phantom.Player.gameObject.layer = LayerMask.NameToLayer("Ghost");
                    phantom.Faded = false;
                    phantom.Player.MyPhysics.ResetMoveState();
                }
            }
        }
    }
}