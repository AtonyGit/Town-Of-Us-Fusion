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
        private static Sprite Fix => TownOfUsFusion.EngineerFix;
        private static Sprite Rewind => TownOfUsFusion.RewindSprite;
        private static Sprite SoulSwap => TownOfUsFusion.SoulSwapSprite;
        private static Sprite EngiVent => TownOfUsFusion.EngineerVent;
        private static Sprite Medic => TownOfUsFusion.MedicSprite;
        private static Sprite Psychic => TownOfUsFusion.PsychicSprite;
        private static Sprite Douse => TownOfUsFusion.DouseSprite;
        private static Sprite Revive => TownOfUsFusion.ReviveSprite;
        private static Sprite Alert => TownOfUsFusion.AlertSprite;
        private static Sprite Remember => TownOfUsFusion.RememberSprite;
        private static Sprite Track => TownOfUsFusion.TrackSprite;
        private static Sprite Transport => TownOfUsFusion.TransportSprite;
        private static Sprite Mediate => TownOfUsFusion.MediateSprite;
        private static Sprite Vest => TownOfUsFusion.VestSprite;
        private static Sprite Protect => TownOfUsFusion.ProtectSprite;
        private static Sprite Infect => TownOfUsFusion.InfectSprite;
        private static Sprite Trap => TownOfUsFusion.TrapSprite;
        private static Sprite Autopsy => TownOfUsFusion.AutopsySprite;
        private static Sprite Observe => TownOfUsFusion.ObserveSprite;
        private static Sprite Bite => TownOfUsFusion.BiteSprite;
        private static Sprite Guard => TownOfUsFusion.GuardSprite;
        private static Sprite Unleash => TownOfUsFusion.MirrorUnleashSprite;
        private static Sprite Campaign => TownOfUsFusion.CampaignSprite;
        private static Sprite Fortify => TownOfUsFusion.BlessSprite;
        private static Sprite Jail => TownOfUsFusion.JailSprite;
        private static Sprite Collect => TownOfUsFusion.CollectSprite;
        private static Sprite Watch => TownOfUsFusion.WatchSprite;
        private static Sprite Camp => TownOfUsFusion.CampSprite;
        private static Sprite Consume => TownOfUsFusion.ConsumeSprite;

        private static Sprite Kill;
        private static Sprite SheriffKill => TownOfUsFusion.SheriffKill;
        private static Sprite SkKill => TownOfUsFusion.SkKill;
        private static Sprite SkVent => TownOfUsFusion.SkVent;
        private static Sprite WerewolfKill => TownOfUsFusion.WerewolfKill;
        private static Sprite GlitchKill => TownOfUsFusion.GlitchKill;
        private static Sprite WerewolfVent => TownOfUsFusion.WerewolfVent;
        private static Sprite GlitchVent => TownOfUsFusion.GlitchVent;
        private static Sprite VampireVent => TownOfUsFusion.VampireVent;
        private static Sprite JesterVent => TownOfUsFusion.JesterVent;


        public static void Postfix(HudManager __instance)
        {
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
                    flag = true;
                    button.transform.localPosition = new Vector3(0f, 1f, 0f);
                    break;
                case RoleEnum.Investigator:
                case RoleEnum.Arsonist:
                    otherButtonKills = true;
                    flag = true;
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
                    button.buttonLabelText.SetOutlineColor(curRole.Color);
                    vent.buttonLabelText.SetOutlineColor(curRole.Color);
                    if (curRole.AbilitySprite != null) button.graphic.sprite = curRole.AbilitySprite;
                    if (curRole.AbilityText != null) button.buttonLabelText.text = curRole.AbilityText;
                    if (curRole.VentSprite != null) vent.graphic.sprite = curRole.VentSprite;
                    if (curRole.AbilitySprite != null || curRole.AbilityText != null) flag = true;
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

            var role = Role.GetRole(PlayerControl.LocalPlayer);
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
