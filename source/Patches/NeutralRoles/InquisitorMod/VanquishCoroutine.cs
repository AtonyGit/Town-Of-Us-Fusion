using System.Collections;
using TownOfUsFusion.Roles;
using UnityEngine;
using System.Linq;
using TownOfUsFusion.CrewmateRoles.DetectiveMod;

namespace TownOfUsFusion.NeutralRoles.InquisitorMod
{
    public class VanquishCoroutine
{
    private static readonly int BodyColor = Shader.PropertyToID("_BodyColor");
    private static readonly int BackColor = Shader.PropertyToID("_BackColor");

    public static IEnumerator InquisitorCoroutine(DeadBody body, Inquisitor role)
    {
        BodyButtonTarget.SetTarget(DestroyableSingleton<HudManager>.Instance.KillButton, null, role);
        var scene = Object.FindObjectsOfType<CrimeScene>().Where(cs => cs.DeadPlayer.PlayerId == body.ParentId).FirstOrDefault();
        SpriteRenderer renderer = null;
        SpriteRenderer crimeSceneRenderer = null;
        if (scene != null) crimeSceneRenderer = scene.gameObject.GetComponent<SpriteRenderer>();
        foreach (var body2 in body.bodyRenderers) renderer = body2;
        var backColor = renderer.material.GetColor(BackColor);
        var bodyColor = renderer.material.GetColor(BodyColor);
        var newColor = new Color(1f, 1f, 1f, 0f);
        for (var i = 0; i < 15; i++)
        {
            if (body == null) yield break;
            renderer.color = Color.Lerp(backColor, newColor, i / 15f);
            renderer.color = Color.Lerp(bodyColor, newColor, i / 15f);
            if (crimeSceneRenderer != null)
                {
                    crimeSceneRenderer.color = Color.Lerp(crimeSceneRenderer.color, newColor, i / 15f);
                }
            yield return null;
        }

            if(scene != null)
            {
                foreach (var detRole in Role.GetRoles(RoleEnum.Detective))
                {
                    var detective = (Detective)detRole;
                    if (detective.CrimeScenes.Contains(scene.gameObject))
                        detective.CrimeScenes.Remove(scene.gameObject);
                    if (detective.InvestigatingScene == scene)
                    {
                        detective.InvestigatingScene = null;
                        detective.InvestigatedPlayers.Clear();
                    }
                }
                Object.Destroy(scene.gameObject);
            }
        Object.Destroy(body.gameObject);
    }
}
}