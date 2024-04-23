using System;
using System.Collections.Generic;
using System.Linq;
using AmongUs.Data;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.Patches.CustomCosmetics
{
    [HarmonyPatch(typeof(VisorsTab))]
    public static class VisorsTabPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(VisorsTab.Update))]
        public static void UpdatePostfix(VisorsTab __instance)
        {
                var Eventbutton = GameObject.Find("Map_Lover");

                var ITN = GameObject.Find("Main Camera/LobbyPlayerCustomizationMenu(Clone)/PlayerGroup");
                if (ITN != null)
                {
                    if (ITN.activeSelf == true)
                    {
                        Eventbutton.transform.localScale = new Vector3(0f, 0f, 0f);
                    }
                    else
                    {
                        Eventbutton.transform.localScale = new Vector3(2f, 2f, 1f);
                    }
                }
                var Sprite = Eventbutton.transform.FindChild("Hat Button").transform.FindChild("Icon");
                Sprite.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.VisorColorButtonSprite;
        }
    }
}
