using System;
using System.Collections.Generic;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace TownOfUsFusion.Patches
{
    [HarmonyPatch]

    public class AprilFoolsPatches
    {
        public static int CurrentMode = 0;

        public static Dictionary<int, string> Modes = new()
        {
            {0, "Off"},
            {1, "Horse"},
            {2, "Long"},
            {3, "Both"}
        };

        [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
        [HarmonyPrefix]

        public static void Prefix(MainMenuManager __instance)
        {
            if (__instance.newsButton != null)
            {

                var aprilfoolstoggle = UnityEngine.Object.Instantiate(__instance.newsButton, null);
                aprilfoolstoggle.name = "aprilfoolstoggle";

                aprilfoolstoggle.transform.localScale = new Vector3(0.44f, 0.84f, 1f);

                PassiveButton passive = aprilfoolstoggle.GetComponent<PassiveButton>();
                passive.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();

                aprilfoolstoggle.gameObject.transform.SetParent(GameObject.Find("RightPanel").transform);
                var pos = aprilfoolstoggle.gameObject.AddComponent<AspectPosition>();
                pos.Alignment = AspectPosition.EdgeAlignments.LeftBottom;
                pos.DistanceFromEdge = new Vector3(2.1f, 2f, 0f);

                passive.OnClick.AddListener((Action)(() =>
                {
                    int num = CurrentMode + 1;
                    CurrentMode = num > 3 ? 0 : num;
                    var text = aprilfoolstoggle.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
                    text.text = $"April fools mode: {Modes[CurrentMode]}";
                }));

                var text = aprilfoolstoggle.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
                __instance.StartCoroutine(Effects.Lerp(0.1f, new System.Action<float>((p) =>
                {
                    text.text = $"April fools mode: {Modes[CurrentMode]}";
                    pos.AdjustPosition();
                })));

                aprilfoolstoggle.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            }
        }

        [HarmonyPatch(typeof(AprilFoolsMode), nameof(AprilFoolsMode.ShouldLongAround))]
        [HarmonyPrefix]

        public static bool Prefix(ref bool __result)
        {
            __result = CurrentMode == 3;
            return false;
        }

        [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.SetBodyType))]
        [HarmonyPrefix]

        public static void Prefix(ref PlayerBodyTypes bodyType)
        {
            switch (CurrentMode)
            {
                case 1:
                    bodyType = PlayerBodyTypes.Horse;
                    break;
                case 3:
                    bodyType = PlayerBodyTypes.LongSeeker;
                    break;
            }
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.BodyType), MethodType.Getter)]
        [HarmonyPrefix]

        public static bool Prefix2(ref PlayerBodyTypes __result)
        {
            switch (CurrentMode)
            {
                case 1:
                    __result = PlayerBodyTypes.Horse;
                    return false;
                case 2:
                    __result = PlayerBodyTypes.Long;
                    return false;
                case 3:
                    __result = PlayerBodyTypes.LongSeeker;
                    return false;
                default:
                    return true;
            }
        }
    }
}