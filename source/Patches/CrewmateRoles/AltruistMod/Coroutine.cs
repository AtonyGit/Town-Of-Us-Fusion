using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities.Extensions;
<<<<<<< Updated upstream
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;
using Object = UnityEngine.Object;
using TownOfUsFusion.Roles.Modifiers;
using AmongUs.GameOptions;
=======
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.Roles.Modifiers;
using AmongUs.GameOptions;
using Reactor.Utilities;
>>>>>>> Stashed changes

namespace TownOfUsFusion.CrewmateRoles.AltruistMod
{
    public class Coroutine
    {
        public static Dictionary<PlayerControl, ArrowBehaviour> Revived = new();
<<<<<<< Updated upstream
        public static Sprite Sprite => TownOfUsFusion.Arrow;

        public static IEnumerator AltruistRevive(DeadBody target, Altruist role)
        {
            var parentId = target.ParentId;
            var position = target.TruePosition;

            var revived = new List<PlayerControl>();

            if (AmongUsClient.Instance.AmHost) Utils.RpcMurderPlayer(role.Player, role.Player);

=======
        public static Sprite Sprite => TownOfUsFusion.Arrow;

        public static IEnumerator AltruistRevive(DeadBody target, Altruist role)
        {
            var parent = Utils.PlayerById(target.ParentId);
            var position = target.TruePosition;
            var altruist = role.Player;

            if (AmongUsClient.Instance.AmHost) Utils.RpcMurderPlayer(role.Player, role.Player);

>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
            foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
            {
                if (deadBody.ParentId == role.Player.PlayerId) deadBody.gameObject.Destroy();
            }

            var player = Utils.PlayerById(parentId);

=======
            if (!AmongUsClient.Instance.AmHost || parent.Data.Disconnected) yield break;

            AltruistReviveEnd(altruist, parent, position.x, position.y + 0.3636f);
            Utils.Rpc(CustomRPC.AltruistRevive, altruist.PlayerId, (byte)1, parent.PlayerId, position.x, position.y + 0.3636f);
        }

        public static void AltruistReviveEnd(PlayerControl altruist, PlayerControl player, float x, float y)
        {
            var revived = new List<PlayerControl>();

            foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
            {
                if (deadBody.ParentId == altruist.PlayerId) deadBody.gameObject.Destroy();
            }

>>>>>>> Stashed changes
            player.Revive();
            if (player.Is(Faction.Impostors)) RoleManager.Instance.SetRole(player, RoleTypes.Impostor);
            else RoleManager.Instance.SetRole(player, RoleTypes.Crewmate);
            Murder.KilledPlayers.Remove(
                Murder.KilledPlayers.FirstOrDefault(x => x.PlayerId == player.PlayerId));
            revived.Add(player);
<<<<<<< Updated upstream
            player.NetTransform.SnapTo(new Vector2(position.x, position.y + 0.3636f));
=======
            player.transform.position = new Vector2(x, y);
            if (PlayerControl.LocalPlayer == player) PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(new Vector2(x, y));
>>>>>>> Stashed changes

            if (Patches.SubmergedCompatibility.isSubmerged() && PlayerControl.LocalPlayer.PlayerId == player.PlayerId)
            {
                Patches.SubmergedCompatibility.ChangeFloor(player.transform.position.y > -7);
            }
<<<<<<< Updated upstream
            if (target != null) Object.Destroy(target.gameObject);
=======
            foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
            {
                if (deadBody.ParentId == player.PlayerId) deadBody.gameObject.Destroy();
            }
>>>>>>> Stashed changes

            if (player.IsLover() && CustomGameOptions.BothLoversDie)
            {
                var lover = Modifier.GetModifier<Lover>(player).OtherLover.Player;

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
<<<<<<< Updated upstream
=======
                        lover.transform.position = new Vector2(deadBody.TruePosition.x, deadBody.TruePosition.y + 0.3636f);
                        if (PlayerControl.LocalPlayer == lover) PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(new Vector2(deadBody.TruePosition.x, deadBody.TruePosition.y + 0.3636f));

                        if (Patches.SubmergedCompatibility.isSubmerged() && PlayerControl.LocalPlayer.PlayerId == lover.PlayerId)
                        {
                            Patches.SubmergedCompatibility.ChangeFloor(lover.transform.position.y > -7);
                        }
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
            if (PlayerControl.LocalPlayer.Data.IsImpostor() || PlayerControl.LocalPlayer.Is(Faction.NeutralKilling))
=======
            if ((PlayerControl.LocalPlayer.Data.IsImpostor() || PlayerControl.LocalPlayer.Is(Faction.NeutralKilling)) && !revived.Contains(PlayerControl.LocalPlayer))
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                yield return Utils.FlashCoroutine(role.Color, 1f, 0.5f);
            }
=======
                Coroutines.Start(Utils.FlashCoroutine(Colors.Altruist, 1f, 0.5f));
            }

            foreach (var revive in revived) Utils.Unmorph(revive);
>>>>>>> Stashed changes
        }
    }
}