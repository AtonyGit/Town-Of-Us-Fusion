using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;

namespace TownOfUsFusion.NeutralRoles.NeoNecromancerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public static class HudManagerUpdate
{
    public static Sprite ResurrectSprite => TownOfUsFusion.ResurrectSprite;
    public static byte DontResurrect = byte.MaxValue;

    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.NeoNecromancer)) return;
        var role = Role.GetRole<NeoNecromancer>(PlayerControl.LocalPlayer);
        
            var killButton = __instance.KillButton;
        killButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            killButton.SetCoolDown(role.NecroKillTimer(), CustomGameOptions.NecroKillCooldown);
            Utils.SetTarget(ref role.ClosestPlayer, killButton, float.NaN);
            

        if (role.ResurrectButton == null)
        {
            role.ResurrectButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
            role.ResurrectButton.graphic.enabled = true;
            role.ResurrectButton.gameObject.SetActive(false);
        }
        

        role.ResurrectButton.graphic.sprite = ResurrectSprite;

        role.ResurrectButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

        var data = PlayerControl.LocalPlayer.Data;
        var isDead = data.IsDead;
        var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
        var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
        var flag = (GameOptionsManager.Instance.currentNormalGameOptions.GhostsDoTasks || !data.IsDead) &&
                   (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) &&
                   PlayerControl.LocalPlayer.CanMove;
        var allocs = Physics2D.OverlapCircleAll(truePosition, maxDistance,
            LayerMask.GetMask(new[] { "Players", "Ghost" }));

        DeadBody closestBody = null;
        var closestDistance = float.MaxValue;

        foreach (var collider2D in allocs)
        {
            if (!flag || isDead || collider2D.tag != "DeadBody") continue;
            var component = collider2D.GetComponent<DeadBody>();


            if (!(Vector2.Distance(truePosition, component.TruePosition) <=
                  maxDistance)) continue;

            var distance = Vector2.Distance(truePosition, component.TruePosition);
            if (!(distance < closestDistance)) continue;
            closestBody = component;
            closestDistance = distance;
        }

            var position = __instance.KillButton.transform.localPosition;
            role.ResurrectButton.transform.localPosition = new Vector3(position.x - 1f, position.y, position.z);

        role.ResurrectButton.SetCoolDown(role.ResurrectTimer(),
            CustomGameOptions.NecroResurrectCooldown + CustomGameOptions.NecroIncreasedCooldownPerResurrect * role.ResurrectCount);

        if (role.CurrentTarget && role.CurrentTarget != closestBody)
        {
            foreach (var body in role.CurrentTarget.bodyRenderers) body.material.SetFloat("_Outline", 0f);
        }

        if (closestBody != null && closestBody.ParentId == DontResurrect) closestBody = null;
        role.CurrentTarget = closestBody;
        if (role.CurrentTarget == null)
        {
            role.ResurrectButton.graphic.color = Palette.DisabledClear;
            role.ResurrectButton.graphic.material.SetFloat("_Desat", 1f);
            return;
        }
        var player = Utils.PlayerById(role.CurrentTarget.ParentId);
        if (role.CurrentTarget && role.ResurrectButton.enabled &&
            (!player.Is(AllianceEnum.Lover) || !player.Is(AllianceEnum.Crewpocalypse) || !player.Is(AllianceEnum.Crewpostor) || !player.Is(AllianceEnum.Recruit) || 
            !player.Is(Faction.Impostors) || !player.Is(RoleEnum.Vampire) || !player.Is(Faction.NeutralApocalypse) || !player.Is(Faction.NeutralChaos)
             || !player.Is(RoleEnum.Detective) || !player.Is(RoleEnum.Seer) || !player.Is(RoleEnum.Investigator) || !player.Is(RoleEnum.Tracker) || !player.Is(RoleEnum.Snitch)
            || !player.Is(RoleEnum.Spy) || !player.Is(RoleEnum.Trapper) || !player.Is(RoleEnum.VampireHunter) || !player.Is(RoleEnum.Veteran) || !player.Is(RoleEnum.Vigilante)
             || !player.Is(RoleEnum.Mayor) || !player.Is(RoleEnum.Prosecutor) || !player.Is(RoleEnum.Amnesiac) || !player.Is(RoleEnum.GuardianAngel)) &&
            !(PlayerControl.LocalPlayer.killTimer > GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown - 0.5f))
        {
            SpriteRenderer component = null;
            foreach (var body in role.CurrentTarget.bodyRenderers) component = body;
            component.material.SetFloat("_Outline", 1f);
            component.material.SetColor("_OutlineColor", Color.red);
            role.ResurrectButton.graphic.color = Palette.EnabledColor;
            role.ResurrectButton.graphic.material.SetFloat("_Desat", 0f);
            return;
        }

        role.ResurrectButton.graphic.color = Palette.DisabledClear;
        role.ResurrectButton.graphic.material.SetFloat("_Desat", 1f);
    }
}/*
public class NecroKillHudManagerUpdate
{
    public static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.NeoNecromancer)) return;
        var role = Role.GetRole<NeoNecromancer>(PlayerControl.LocalPlayer);
        
        __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
        __instance.KillButton.SetCoolDown(role.KillTimer(), CustomGameOptions.NecroKillCooldown);

        if(__instance.KillButton == null)
        {
        var notNecroTeam = PlayerControl.AllPlayerControls.ToArray()
            .Where(x => !x.Is(RoleEnum.NeoNecromancer) || !x.Is(RoleEnum.Apparitionist) || !x.Is(RoleEnum.Scourge)
            || !x.Is(RoleEnum.Enchanter) || !x.Is(RoleEnum.Husk)).ToList();
            
            if (role.ClosestPlayer != null)
            {
                role.ClosestPlayer.myRend().material.SetColor("_OutlineColor", Colors.NeoNecromancer);
            }
        //if(!role.ClosestPlayer.Is(RoleEnum.NeoNecromancer) || !role.ClosestPlayer.Is(RoleEnum.Apparitionist) || !role.ClosestPlayer.Is(RoleEnum.Scourge)
        // || !role.ClosestPlayer.Is(RoleEnum.Enchanter) || !role.ClosestPlayer.Is(RoleEnum.Husk))
        Utils.SetTarget(ref role.ClosestPlayer, __instance.KillButton, float.NaN, notNecroTeam);
            return;
        }

    }
}*/
}