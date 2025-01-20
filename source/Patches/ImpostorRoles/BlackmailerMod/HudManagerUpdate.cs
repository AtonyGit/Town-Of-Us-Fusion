using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using System.Linq;
using TownOfUsFusion.Extensions;
using AmongUs.GameOptions;
using TownOfUsFusion.Roles.Modifiers;

namespace TownOfUsFusion.ImpostorRoles.BlackmailerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
<<<<<<< Updated upstream
        public static Sprite Blackmail => TownOfUsFusion.BlackmailSprite;
=======
        public static Sprite Blackmail => TownOfUsFusion.BlackmailSprite;
>>>>>>> Stashed changes

        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Blackmailer)) return;
            var role = Role.GetRole<Blackmailer>(PlayerControl.LocalPlayer);
            if (role.BlackmailButton == null)
            {
                role.BlackmailButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.BlackmailButton.graphic.enabled = true;
                role.BlackmailButton.gameObject.SetActive(false);
            }

<<<<<<< Updated upstream
            role.BlackmailButton.graphic.sprite = Blackmail;
            role.BlackmailButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            var notBlackmailed = PlayerControl.AllPlayerControls.ToArray().Where(
                player => role.Blackmailed?.PlayerId != player.PlayerId
            ).ToList();

            Utils.SetTarget(ref role.ClosestPlayer, role.BlackmailButton, GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance], notBlackmailed);

            role.BlackmailButton.SetCoolDown(role.BlackmailTimer(), CustomGameOptions.BlackmailCd);

            if (role.Blackmailed != null && !role.Blackmailed.Data.IsDead && !role.Blackmailed.Data.Disconnected)
            {
                role.Blackmailed.myRend().material.SetFloat("_Outline", 1f);
                role.Blackmailed.myRend().material.SetColor("_OutlineColor", new Color(0.3f, 0.0f, 0.0f));
                if (role.Blackmailed.GetCustomOutfitType() != CustomPlayerOutfitType.Camouflage &&
                    role.Blackmailed.GetCustomOutfitType() != CustomPlayerOutfitType.Swooper)
                    role.Blackmailed.nameText().color = new Color(0.3f, 0.0f, 0.0f);
                else role.Blackmailed.nameText().color = Color.clear;
            }

            var imps = PlayerControl.AllPlayerControls.ToArray().Where(
                player => player.Data.IsImpostor() && player != role.Blackmailed
            ).ToList();

            foreach (var imp in imps)
            {
                if ((imp.GetCustomOutfitType() == CustomPlayerOutfitType.Camouflage ||
                    imp.GetCustomOutfitType() == CustomPlayerOutfitType.Swooper)) imp.nameText().color = Color.clear;
                else if (imp.nameText().color == Color.clear ||
                    imp.nameText().color == new Color(0.3f, 0.0f, 0.0f)) imp.nameText().color = Patches.Colors.Impostor;
=======
            if (PlayerControl.LocalPlayer.Data.IsDead) role.BlackmailButton.SetTarget(null);

            role.BlackmailButton.graphic.sprite = Blackmail;
            role.BlackmailButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            var notBlackmailed = PlayerControl.AllPlayerControls.ToArray().Where(
                player => role.Blackmailed?.PlayerId != player.PlayerId
            ).ToList();

            role.BlackmailButton.SetCoolDown(role.BlackmailTimer(), CustomGameOptions.BlackmailCd);
            if (PlayerControl.LocalPlayer.moveable) Utils.SetTarget(ref role.ClosestPlayer, role.BlackmailButton, float.NaN, notBlackmailed);
            else role.BlackmailButton.SetTarget(null);

            if (!PlayerControl.LocalPlayer.IsHypnotised())
            {
                if (role.Blackmailed != null && !role.Blackmailed.Data.IsDead && !role.Blackmailed.Data.Disconnected)
                {
                    if (role.Blackmailed.GetCustomOutfitType() != CustomPlayerOutfitType.Camouflage &&
                        role.Blackmailed.GetCustomOutfitType() != CustomPlayerOutfitType.Swooper)
                    {
                        var colour = new Color(0.3f, 0f, 0f);
                        if (role.Blackmailed.Is(ModifierEnum.Shy)) colour.a = Modifier.GetModifier<Shy>(role.Blackmailed).Opacity;
                        role.Blackmailed.nameText().color = colour;
                    }
                    else role.Blackmailed.nameText().color = Color.clear;
                }

                var imps = PlayerControl.AllPlayerControls.ToArray().Where(
                    player => player.Data.IsImpostor() && player != role.Blackmailed
                ).ToList();

                foreach (var imp in imps)
                {
                    if (imp.GetCustomOutfitType() == CustomPlayerOutfitType.Camouflage ||
                        imp.GetCustomOutfitType() == CustomPlayerOutfitType.Swooper) imp.nameText().color = Color.clear;
                    else if (imp.nameText().color == Color.clear ||
                        imp.nameText().color == new Color(0.3f, 0.0f, 0.0f)) imp.nameText().color = Patches.Colors.Impostor;
                }
>>>>>>> Stashed changes
            }
        }
    }
}