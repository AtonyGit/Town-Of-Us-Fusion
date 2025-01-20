using HarmonyLib;
using UnityEngine;

namespace TownOfUsFusion
{
    [HarmonyPriority(Priority.VeryHigh)] // to show this message first, or be overrided if any plugins do
    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShowerUpdate
    {
        public static void Postfix(VersionShower __instance)
        {
            var text = __instance.text;
<<<<<<< Updated upstream
            text.text += " - <color=#00FF00FF>TownOfUsFusion v" + TownOfUsFusion.VersionString + "</color>" + TownOfUsFusion.VersionTag;
=======
        text.text += $" - <color=#FF6A51FF>TOUR v{TownOfUsFusion.TouVersionString}</color>" +
        $"<color=#8E5BF3FF> | Fusion v{TownOfUsFusion.VersionString}</color>" + (TownOfUsFusion.isDevBuild ? $"<color=#DA4291FF> Dev {TownOfUsFusion.DevBuildVersion}</color>" : "");
>>>>>>> Stashed changes
            text.transform.localPosition += new Vector3(-0.8f, -0.16f, 0f);

            if (GameObject.Find("RightPanel"))
            {
                text.transform.SetParent(GameObject.Find("RightPanel").transform);

                var aspect = text.gameObject.AddComponent<AspectPosition>();
                aspect.Alignment = AspectPosition.EdgeAlignments.Top;
                aspect.DistanceFromEdge = new Vector3(-0.2f, 2.5f, 8f);

                aspect.StartCoroutine(Effects.Lerp(0.1f, new System.Action<float>((p) =>
                {
                    aspect.AdjustPosition();
                })));

                return;
            }
        }
    }
}
