using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Linq;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using AmongUs.GameOptions;

namespace TownOfUsFusion.NeutralRoles.CannibalMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class HudManagerUpdate
{
    public static Sprite Arrow => TownOfUsFusion.Arrow;

    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal)) return;

        var role = Role.GetRole<Cannibal>(PlayerControl.LocalPlayer);

        var data = PlayerControl.LocalPlayer.Data;
        var isDead = data.IsDead;
        var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
        var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];

        var killButton = __instance.KillButton;
        DeadBody closestBody = null;
        var closestDistance = float.MaxValue;
        var allBodies = Object.FindObjectsOfType<DeadBody>();

        killButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        killButton.SetCoolDown(0f, 0f);
        
        foreach (var body in allBodies.Where(x => Vector2.Distance(x.TruePosition, truePosition) <= maxDistance))
        {
            var distance = Vector2.Distance(truePosition, body.TruePosition);
            if (!(distance < closestDistance)) continue;

            closestBody = body;
            closestDistance = distance;
        }

        if (CustomGameOptions.CannibalArrows && !PlayerControl.LocalPlayer.Data.IsDead)
        {
            var validBodies = Object.FindObjectsOfType<DeadBody>().Where(x =>
                Murder.KilledPlayers.Any(y => y.PlayerId == x.ParentId && y.KillTime.AddSeconds(CustomGameOptions.CannibalArrowDelay) < System.DateTime.UtcNow));

            foreach (var bodyArrow in role.BodyArrows.Keys)
            {
                if (!validBodies.Any(x => x.ParentId == bodyArrow))
                {
                    role.DestroyArrow(bodyArrow);
                }
            }

            foreach (var body in validBodies)
            {
                if (!role.BodyArrows.ContainsKey(body.ParentId))
                {
                    var gameObj = new GameObject();
                    var arrow = gameObj.AddComponent<ArrowBehaviour>();
                    gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                    var renderer = gameObj.AddComponent<SpriteRenderer>();
                    renderer.sprite = Arrow;
                    arrow.image = renderer;
                    gameObj.layer = 5;
                    role.BodyArrows.Add(body.ParentId, arrow);
                }
                role.BodyArrows.GetValueSafe(body.ParentId).target = body.TruePosition;
            }
        }
        else
        {
            if (role.BodyArrows.Count != 0)
            {
                role.BodyArrows.Values.DestroyAll();
                role.BodyArrows.Clear();
            }
        }

        /*__instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        KillButtonTarget.SetTarget(killButton, closestBody, role);
        __instance.KillButton.SetCoolDown(0f, 1f);*/
    }
}
}
