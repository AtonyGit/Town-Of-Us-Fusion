using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using System.Linq;
using System;
using TownOfUsFusion.Extensions;
using Object = UnityEngine.Object;
using TownOfUsFusion.CrewmateRoles.MedicMod;

namespace TownOfUsFusion.CrewmateRoles.MediumMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class HUDMediate
    {
        public static Sprite Arrow => TownOfUsFusion.Arrow;
        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            UpdateButton(__instance);
        }
        public static void UpdateButton(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            var data = PlayerControl.LocalPlayer.Data;

            if (PlayerControl.LocalPlayer.Is(RoleEnum.Medium))
            {
                var mediateButton = __instance.KillButton;

                var role = Role.GetRole<Medium>(PlayerControl.LocalPlayer);
                mediateButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
                if (data.IsDead) return;
                    
                var isDead = data.IsDead;
                var truePosition = PlayerControl.LocalPlayer.GetTruePosition();

                if (!PlayerControl.LocalPlayer.Data.IsDead)
                {
                    var validBodies = Object.FindObjectsOfType<DeadBody>().Where(x =>
                        Murder.KilledPlayers.Any(y => y.PlayerId == x.ParentId && y.KillTime.AddSeconds(CustomGameOptions.MediumArrowDuration) > System.DateTime.UtcNow));

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
                            var renderer2 = gameObj.AddComponent<SpriteRenderer>();
                            renderer2.sprite = Arrow;
                            arrow.image = renderer2;
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

                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (role.MediatedPlayers.Keys.Contains(player.PlayerId))
                    {
                        role.MediatedPlayers.GetValueSafe(player.PlayerId).target = player.transform.position;
                        player.Visible = true;
                        if (!CustomGameOptions.ShowMediatePlayer)
                        {
                            player.SetOutfit(CustomPlayerOutfitType.Camouflage, new NetworkedPlayerInfo.PlayerOutfit()
                            {
                                ColorId = player.GetDefaultOutfit().ColorId,
                                HatId = "",
                                SkinId = "",
                                VisorId = "",
                                PlayerName = " ",
                                PetId = ""
                            });
                            PlayerMaterial.SetColors(Color.grey, player.myRend());
                        }
                    }
                }
                mediateButton.SetCoolDown(role.MediateTimer(), CustomGameOptions.MediateCooldown);

                var renderer = mediateButton.graphic;
                if (!mediateButton.isCoolingDown && PlayerControl.LocalPlayer.moveable)
                {
                    renderer.color = Palette.EnabledColor;
                    renderer.material.SetFloat("_Desat", 0f);
                    return;
                }

                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
            }
            else if (CustomGameOptions.ShowMediumToDead && Role.AllRoles.Any(x => x.RoleType == RoleEnum.Medium && ((Medium) x).MediatedPlayers.Keys.Contains(PlayerControl.LocalPlayer.PlayerId)))
            {
                var role = (Medium) Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Medium && ((Medium) x).MediatedPlayers.Keys.Contains(PlayerControl.LocalPlayer.PlayerId));
                role.MediatedPlayers.GetValueSafe(PlayerControl.LocalPlayer.PlayerId).target = role.Player.transform.position;
            }
        }
    }
}