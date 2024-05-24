using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;
using Object = UnityEngine.Object;
using TownOfUsFusion.Roles.Modifiers;
using AmongUs.GameOptions;
using TownOfUsFusion.Roles.Alliances;

namespace TownOfUsFusion.CrewmateRoles.AltruistMod
{
    public class Coroutine
{
    public static Dictionary<PlayerControl, ArrowBehaviour> Revived = new();
    public static Sprite Sprite => TownOfUsFusion.Arrow;

    public static IEnumerator AltruistRevive(DeadBody target, Altruist role)
    {
        var parentId = target.ParentId;
        var position = target.TruePosition;

        var revived = new List<PlayerControl>();

        if (AmongUsClient.Instance.AmHost) Utils.RpcMurderPlayer(role.Player, role.Player);

        if (CustomGameOptions.AltruistTargetBody)
            if (target != null)
            {
                foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
                {
                    if (deadBody.ParentId == target.ParentId) deadBody.gameObject.Destroy();
                }
            }

        var startTime = DateTime.UtcNow;
        while (true)
        {
            var now = DateTime.UtcNow;
            var seconds = (now - startTime).TotalSeconds;
            if (seconds < CustomGameOptions.ReviveDuration)
                yield return null;
            else break;

            if (MeetingHud.Instance) yield break;
        }

        foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
        {
            if (deadBody.ParentId == role.Player.PlayerId) deadBody.gameObject.Destroy();
        }

        var player = Utils.PlayerById(parentId);

        foreach (var poisoner in Role.GetRoles(RoleEnum.Poisoner))
        {
            var poisonerRole = (Poisoner)poisoner;
            if (poisonerRole.PoisonedPlayer == player) poisonerRole.PoisonedPlayer = poisonerRole.Player;
        }

        foreach (var vamp in Role.GetRoles(RoleEnum.Vampire))
        {
            var vampRole = (Vampire)vamp;
            if (vampRole.BittenPlayer == player) vampRole.BittenPlayer = vampRole.Player;
        }

        player.Revive();
        if (player.Is(Faction.Impostors)) RoleManager.Instance.SetRole(player, RoleTypes.Impostor);
        else RoleManager.Instance.SetRole(player, RoleTypes.Crewmate);
        Murder.KilledPlayers.Remove(
            Murder.KilledPlayers.FirstOrDefault(x => x.PlayerId == player.PlayerId));
        revived.Add(player);
        player.NetTransform.SnapTo(new Vector2(position.x, position.y + 0.3636f));

        if (Patches.SubmergedCompatibility.isSubmerged() && PlayerControl.LocalPlayer.PlayerId == player.PlayerId)
        {
            Patches.SubmergedCompatibility.ChangeFloor(player.transform.position.y > -7);
        }
        if (target != null) Object.Destroy(target.gameObject);

        if (player.IsLover() && CustomGameOptions.BothLoversDie)
        {
            var lover = Alliance.GetAlliance<Lover>(player).OtherLover.Player;

            lover.Revive();
            if (lover.Is(Faction.Impostors)) RoleManager.Instance.SetRole(lover, RoleTypes.Impostor);
            else RoleManager.Instance.SetRole(lover, RoleTypes.Crewmate);
            Murder.KilledPlayers.Remove(
                Murder.KilledPlayers.FirstOrDefault(x => x.PlayerId == lover.PlayerId));
            revived.Add(lover);

            foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
            {
                if (deadBody.ParentId == lover.PlayerId)
                {
                    deadBody.gameObject.Destroy();
                }
            }
        }

        if (player.IsRecruit() && CustomGameOptions.DoJackalRecruitsDie)
        {
            var recruit = Alliance.GetAlliance<Recruit>(player).OtherRecruit.Player;

            recruit.Revive();
            if (recruit.Is(Faction.Impostors)) RoleManager.Instance.SetRole(recruit, RoleTypes.Impostor);
            else RoleManager.Instance.SetRole(recruit, RoleTypes.Crewmate);
            Murder.KilledPlayers.Remove(
                Murder.KilledPlayers.FirstOrDefault(x => x.PlayerId == recruit.PlayerId));
            revived.Add(recruit);

            foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
            {
                if (deadBody.ParentId == recruit.PlayerId)
                {
                    deadBody.gameObject.Destroy();
                }
            }
        }

        if (revived.Any(x => x.AmOwner))
            try
            {
                Minigame.Instance.Close();
                Minigame.Instance.Close();
            }
            catch
            {
            }

        if (PlayerControl.LocalPlayer.Data.IsImpostor() || PlayerControl.LocalPlayer.Is(Faction.NeutralKilling) || PlayerControl.LocalPlayer.Is(Faction.NeutralNeophyte) || PlayerControl.LocalPlayer.Is(Faction.NeutralNecro) || PlayerControl.LocalPlayer.Is(Faction.NeutralApocalypse))
        {
            var gameObj = new GameObject();
            var Arrow = gameObj.AddComponent<ArrowBehaviour>();
            gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
            var renderer = gameObj.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite;
            Arrow.image = renderer;
            gameObj.layer = 5;
            Revived.Add(player, Arrow);
            //Target = player;
            yield return Utils.FlashCoroutine(role.Color, 1f, 0.5f);
        }
    }
}
}