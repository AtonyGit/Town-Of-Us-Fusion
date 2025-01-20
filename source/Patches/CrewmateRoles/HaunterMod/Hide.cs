using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.HaunterMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    [HarmonyPriority(Priority.Last)]
    public class Hide
    {
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Haunter))
            {
                var haunter = (Haunter) role;
                if (role.Player.Data != null && role.Player.Data.Disconnected) return;
                var caught = haunter.Caught;
                if (!caught)
                {
                    haunter.Fade();
                }
                else if (haunter.Faded)
                {
                    Utils.Unmorph(haunter.Player);
                    haunter.Player.myRend().color = Color.white;
                    haunter.Player.gameObject.layer = LayerMask.NameToLayer("Ghost");
                    haunter.Faded = false;
                    haunter.Player.MyPhysics.ResetMoveState();
                }
            }
        }
    }
}