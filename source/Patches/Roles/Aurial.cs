<<<<<<< Updated upstream
﻿using System;
using System.Collections.Generic;
using TownOfUs.Patches.ScreenEffects;
=======
﻿using System.Collections;
>>>>>>> Stashed changes
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;
using TownOfUsFusion.Extensions;

namespace TownOfUs.Roles
{
    public class Aurial : Role
    {
<<<<<<< Updated upstream
        public Dictionary<byte,int> knownPlayerRoles = new Dictionary<byte,int>();
        public static Material ViewMat = TownOfUs.bundledAssets.Get<Material>("SoundV");
        public DateTime LastRadiated { get; set; }
        public DateTime CannotSeeDelay { get; set; }
        public bool NormalVision { get; set; } = false;
        public bool Loaded { get; set; } = false;
        public Aurial(PlayerControl player) : base(player)
        {
            Name = "Aurial";
            ImpostorText = () => "Radiate To See The Auras Of Other Players";
            TaskText = () => "Radiate to discover evildoers";
            Color = Patches.Colors.Aurial;
            RoleType = RoleEnum.Aurial;
            AddToRoleHistory(RoleType);

            LastRadiated = DateTime.UtcNow;
            if (Player == PlayerControl.LocalPlayer) ApplyEffect();
        }

        public float RadiateTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastRadiated;
            var num = CustomGameOptions.RadiateCooldown * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0f;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public float SeeDelay()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - CannotSeeDelay;
            var num = CustomGameOptions.RadiateInvis * 1000f;
            if (num > CustomGameOptions.RadiateCooldown * 1000f) num = CustomGameOptions.RadiateCooldown * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0f;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public void ApplyEffect()
        {
            CameraEffect.Initialize();
            CameraEffect.singleton.materials.Clear();
            CameraEffect.singleton.materials.Add(ViewMat);     
        }

        public void ClearEffect()
        {
            CameraEffect.singleton.materials.Clear();
=======
        public Dictionary<(Vector3, int), ArrowBehaviour> SenseArrows = new Dictionary<(Vector3, int), ArrowBehaviour>();
        public static Sprite Sprite => TownOfUsFusion.Arrow;
        public Aurial(PlayerControl player) : base(player)
        {
            Name = "Aurial";
            ImpostorText = () => "Sense Disturbances In Your Aura";
            TaskText = () => "Any player ability uses inside your aura you will sense";
            Color = Patches.Colors.Aurial;
            RoleType = RoleEnum.Aurial;
            AddToRoleHistory(RoleType);
        }

        public IEnumerator Sense(PlayerControl player)
        {
            if (!CheckRange(player, CustomGameOptions.AuraOuterRadius)) yield break;
            var position = player.GetTruePosition();
            var gameObj = new GameObject();
            var arrow = gameObj.AddComponent<ArrowBehaviour>();
            gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
            var renderer = gameObj.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite;
            int colourID = player.GetDefaultOutfit().ColorId;
            if (CheckRange(player, CustomGameOptions.AuraInnerRadius) && !CamouflageUnCamouflage.IsCamoed)
            {
                if (RainbowUtils.IsRainbow(colourID))
                {
                    renderer.color = RainbowUtils.Rainbow;
                }
                else
                {
                    renderer.color = Palette.PlayerColors[colourID];
                }
            }
            arrow.image = renderer;
            gameObj.layer = 5;
            arrow.target = player.transform.position;

            try { DestroyArrow(position, colourID); }
            catch { }

            SenseArrows.Add((position, colourID), arrow);

            yield return (object)new WaitForSeconds(CustomGameOptions.SenseDuration);

            try { DestroyArrow(position, colourID); }
            catch { }
        }

        public bool CheckRange(PlayerControl player, float radius)
        {
            float lightRadius = radius * ShipStatus.Instance.MaxLightRadius;
            Vector2 vector2 = new Vector2(player.GetTruePosition().x - Player.GetTruePosition().x, player.GetTruePosition().y - Player.GetTruePosition().y);
            float magnitude = vector2.magnitude;
            if (magnitude <= lightRadius) return true;
            else return false;
        }

        public void DestroyArrow(Vector3 targetArea, int colourID)
        {
            var arrow = SenseArrows.FirstOrDefault(x => x.Key == (targetArea, colourID));
            if (arrow.Value != null)
                Object.Destroy(arrow.Value);
            if (arrow.Value.gameObject != null)
                Object.Destroy(arrow.Value.gameObject);
            SenseArrows.Remove(arrow.Key);
>>>>>>> Stashed changes
        }
    }
}
