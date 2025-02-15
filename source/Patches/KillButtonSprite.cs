using AmongUs.GameOptions;
using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;

namespace TownOfUsFusion
{
    /*[HarmonyPatch(typeof(KillButton), nameof(KillButton.Start))]
    public static class KillButtonAwake
    {
        public static void Prefix(KillButton __instance)
        {
            //__instance.transform.Find("Text_TMP").gameObject.SetActive(false);
        }
    }*/

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class KillButtonSprite
    {
        private static Sprite Kill;

        public static void Postfix(HudManager __instance)
        {
            var role = Role.GetRole(PlayerControl.LocalPlayer);
            if (__instance.KillButton == null) return;

            if (!Kill) Kill = __instance.KillButton.graphic.sprite;
            var button = __instance.KillButton;
            var vent = __instance.ImpostorVentButton;

            var flag = false;
            var buttonKills = false;
            var curRole = Role.GetRole(PlayerControl.LocalPlayer);
            var otherButtonKills = false;
            switch(curRole?.RoleType) {
                case RoleEnum.Lookout:
                case RoleEnum.Sheriff:
                case RoleEnum.MirrorMaster:
                case RoleEnum.Glitch:
                case RoleEnum.SerialKiller:
                case RoleEnum.Werewolf:
                case RoleEnum.Vampire:
                case RoleEnum.Pestilence:
                case RoleEnum.Juggernaut:
                case RoleEnum.Armaggeddon:
                    buttonKills = true;
                    flag = true;
                    button.buttonLabelText.SetOutlineColor(curRole.Color);
                    vent.buttonLabelText.SetOutlineColor(curRole.Color);
                    if (curRole.AbilitySprite != null) button.graphic.sprite = curRole.AbilitySprite;
                    if (curRole.AbilityText != null) button.buttonLabelText.text = curRole.AbilityText;
                    if (curRole.VentSprite != null) vent.graphic.sprite = curRole.VentSprite;
                    button.transform.localPosition = new Vector3(0f, 1f, 0f);
                    break;
                case RoleEnum.Investigator:
                case RoleEnum.Arsonist:
                    otherButtonKills = true;
                    button.buttonLabelText.SetOutlineColor(curRole.Color);
                    vent.buttonLabelText.SetOutlineColor(curRole.Color);
                    if (curRole.AbilitySprite != null) button.graphic.sprite = curRole.AbilitySprite;
                    if (curRole.AbilityText != null) button.buttonLabelText.text = curRole.AbilityText;
                    if (curRole.VentSprite != null) vent.graphic.sprite = curRole.VentSprite;
                    flag = true;
                    button.transform.localPosition = new Vector3(0f, 1f, 0f);
                    break;
                case null:
                    break;
                default:
                    if (curRole.AbilitySprite != null || curRole.AbilityText != null) flag = true;
                    button.buttonLabelText.SetOutlineColor(curRole.Color);
                    vent.buttonLabelText.SetOutlineColor(curRole.Color);
                    if (curRole.AbilitySprite != null) button.graphic.sprite = curRole.AbilitySprite;
                    if (curRole.AbilityText != null) button.buttonLabelText.text = curRole.AbilityText;
                    if (curRole.VentSprite != null) vent.graphic.sprite = curRole.VentSprite;
                    if (!PlayerControl.LocalPlayer.Is(Faction.Impostors) &&
                        GameOptionsManager.Instance.CurrentGameOptions.GameMode != GameModes.HideNSeek)
                    {
                        button.transform.localPosition = new Vector3(0f, 1f, 0f);
                    }
                    break;
            }
            
            switch(Role.GetRole(PlayerControl.LocalPlayer)?.RoleType) {
                case RoleEnum.Werewolf:
                case RoleEnum.SerialKiller:
                vent.transform.localPosition = new Vector3(-1f, 0f, 0f);
                break;
                case RoleEnum.Glitch:
                case RoleEnum.Vampire:
                case RoleEnum.Juggernaut:
                case RoleEnum.Armaggeddon:
                case RoleEnum.Pestilence:
                case RoleEnum.Engineer:
                vent.transform.localPosition = new Vector3(-2f, 0f, 0f);
                break;
            }

            bool KillKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionSecondary");
            if (!buttonKills) KillKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionQuaternary");
            var controller = ConsoleJoystick.player.GetButtonDown(8);
            if ((KillKey || controller) && button != null && flag && !PlayerControl.LocalPlayer.Data.IsDead)
                button.DoClick();

            bool AbilityKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionSecondary");
            if (!otherButtonKills) AbilityKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionQuaternary");
            if (role?.ExtraButtons != null && AbilityKey && !PlayerControl.LocalPlayer.Data.IsDead)
                role?.ExtraButtons[0]?.DoClick();

            if (Modifier.GetModifier<ButtonBarry>(PlayerControl.LocalPlayer)?.ButtonUsed == false &&
                Rewired.ReInput.players.GetPlayer(0).GetButtonDown("TOU bb/disperse/mimic") &&
                !PlayerControl.LocalPlayer.Data.IsDead)
            {
                Modifier.GetModifier<ButtonBarry>(PlayerControl.LocalPlayer).ButtonButton.DoClick();
            }
            else if (Modifier.GetModifier<Disperser>(PlayerControl.LocalPlayer)?.ButtonUsed == false &&
                     Rewired.ReInput.players.GetPlayer(0).GetButtonDown("TOU bb/disperse/mimic") &&
                     !PlayerControl.LocalPlayer.Data.IsDead)
            {
                Modifier.GetModifier<Disperser>(PlayerControl.LocalPlayer).DisperseButton.DoClick();
            }

            if (role.AbilitySprite != null || role.AbilityText != null) 
            __instance.KillButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            //if (role.AbilitySprite != null) __instance.KillButton.graphic.sprite = role.AbilitySprite;
            //if (role.AbilityText != null) __instance.KillButton.buttonLabelText.text = role.AbilityText;

            if (role?.ExtraButtons != null) {
                if (role.ExtraButtons[0] == null)
                {
                    role.ExtraButtons[0] = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                    role.ExtraButtons[0].graphic.enabled = true;
                    role.ExtraButtons[0].gameObject.SetActive(false);
                }
                    if (role.SecondAbilitySprite != null) role.ExtraButtons[0].graphic.sprite = role.SecondAbilitySprite;
                    if (role.SecondAbilityText != null) role.ExtraButtons[0].buttonLabelText.text = role.SecondAbilityText;
                    role.ExtraButtons[0].buttonLabelText.SetOutlineColor(role.Color);
                    role.ExtraButtons[0].gameObject.SetActive((__instance.UseButton.isActiveAndEnabled || __instance.PetButton.isActiveAndEnabled)
                            && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                            && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);
            }
        }

        [HarmonyPatch(typeof(AbilityButton), nameof(AbilityButton.Update))]
        class AbilityButtonUpdatePatch
        {
            static void Postfix()
            {
                if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started)
                {
                    HudManager.Instance.AbilityButton.gameObject.SetActive(false);
                    return;
                }
                else if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek)
                {
                    HudManager.Instance.AbilityButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsImpostor());
                    return;
                }
                var ghostRole = false;
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Haunter))
                {
                    var haunter = Role.GetRole<Haunter>(PlayerControl.LocalPlayer);
                    if (!haunter.Caught) ghostRole = true;
                }
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Phantom))
                {
                    var phantom = Role.GetRole<Phantom>(PlayerControl.LocalPlayer);
                    if (!phantom.Caught) ghostRole = true;
                }
                HudManager.Instance.AbilityButton.gameObject.SetActive(!ghostRole && Utils.ShowDeadBodies && !MeetingHud.Instance);
            }
        }
    }
}
