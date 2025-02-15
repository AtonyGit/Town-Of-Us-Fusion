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
            var otherButtonKills = false;
            switch(Role.GetRole(PlayerControl.LocalPlayer)?.RoleType) {
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
                    button.buttonLabelText.SetOutlineColor(Role.GetRole(PlayerControl.LocalPlayer).Color);
                    vent.buttonLabelText.SetOutlineColor(Role.GetRole(PlayerControl.LocalPlayer).Color);
                    button.transform.localPosition = new Vector3(0f, 1f, 0f);
                    break;
                case RoleEnum.Investigator:
                case RoleEnum.Arsonist:
                    otherButtonKills = true;
                    flag = true;
                    button.buttonLabelText.SetOutlineColor(Role.GetRole(PlayerControl.LocalPlayer).Color);
                    vent.buttonLabelText.SetOutlineColor(Role.GetRole(PlayerControl.LocalPlayer).Color);
                    button.transform.localPosition = new Vector3(0f, 1f, 0f);
                    break;
                case null:
                    break;
                default:
                    button.buttonLabelText.SetOutlineColor(Role.GetRole(PlayerControl.LocalPlayer).Color);
                    vent.buttonLabelText.SetOutlineColor(Role.GetRole(PlayerControl.LocalPlayer).Color);
                    if (!PlayerControl.LocalPlayer.Is(Faction.Impostors) &&
                        GameOptionsManager.Instance.CurrentGameOptions.GameMode != GameModes.HideNSeek)
                    {
                        button.transform.localPosition = new Vector3(0f, 1f, 0f);
                    }
                    break;

            }
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Psychic))
            {
                button.graphic.sprite = Psychic;
                button.buttonLabelText.text = "Reveal";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Medic))
            {
                button.graphic.sprite = Medic;
                button.buttonLabelText.text = "Shield";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Arsonist))
            {
                button.graphic.sprite = Douse;
                button.buttonLabelText.text = "Douse";
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Altruist))
            {
                button.graphic.sprite = Revive;
                button.buttonLabelText.text = "Altruist";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
            {
                button.graphic.sprite = Alert;
                button.buttonLabelText.text = "Alert";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Amnesiac))
            {
                button.graphic.sprite = Remember;
                button.buttonLabelText.text = "Remember";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
            {
                button.graphic.sprite = Track;
                button.buttonLabelText.text = "Track";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
            {
                button.graphic.sprite = Transport;
                button.buttonLabelText.text = "Transport";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Medium))
            {
                button.graphic.sprite = Mediate;
                button.buttonLabelText.text = "Mediate";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Survivor))
            {
                button.graphic.sprite = Vest;
                button.buttonLabelText.text = "Vest";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel))
            {
                button.graphic.sprite = Protect;
                button.buttonLabelText.text = "Protect";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Plaguebearer))
            {
                button.graphic.sprite = Infect;
                button.buttonLabelText.text = "Infect";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer))
            {
                button.graphic.sprite = Fix;
                button.buttonLabelText.text = "Fix";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.TimeLord))
            {
                button.graphic.sprite = Rewind;
                button.buttonLabelText.text = "Rewind";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Trapper))
            {
                button.graphic.sprite = Trap;
                button.buttonLabelText.text = "Trap";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator))
            {
                button.graphic.sprite = Autopsy;
                button.buttonLabelText.text = "Autopsy";
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer))
            {
                button.graphic.sprite = Observe;
                button.buttonLabelText.text = "Observe";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal))
            {
                button.graphic.sprite = Consume;
                button.buttonLabelText.text = "Consume";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Vampire))
            {
                button.graphic.sprite = Bite;
                button.buttonLabelText.text = "Bite";
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Politician))
            {
                button.graphic.sprite = Campaign;
                button.buttonLabelText.text = "Campaign";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Oracle))
            {
                button.graphic.sprite = Fortify;
                button.buttonLabelText.text = "Bless";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Jailor))
            {
                button.graphic.sprite = Jail;
                button.buttonLabelText.text = "Jail";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.SoulCollector))
            {
                button.graphic.sprite = Collect;
                button.buttonLabelText.text = "Collect";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Lookout))
            {
                button.graphic.sprite = Watch;
                button.buttonLabelText.text = "Watch";
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Deputy))
            {
                button.graphic.sprite = Camp;
                button.buttonLabelText.text = "Camp";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard))
            {
                button.graphic.sprite = Guard;
                button.buttonLabelText.text = "Guard";
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.MirrorMaster))
            {
                button.graphic.sprite = Unleash;
                button.buttonLabelText.text = "Unleash";
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Sheriff))
            {
                button.graphic.sprite = SheriffKill;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Pestilence))
            {
                //button.graphic.sprite = PestKill;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Werewolf))
            {
                button.graphic.sprite = WerewolfKill;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.SerialKiller))
            {
                button.graphic.sprite = SkKill;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Juggernaut))
            {
                //button.graphic.sprite = JuggKill;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Armaggeddon))
            {
                //button.graphic.sprite = JuggKill;
            }
            
            switch(Role.GetRole(PlayerControl.LocalPlayer)?.RoleType) {
                case RoleEnum.Glitch:
                vent.transform.localPosition = new Vector3(-2f, 0f, 0f);
                vent.graphic.sprite = GlitchVent;
                break;
                case RoleEnum.Werewolf:
                vent.transform.localPosition = new Vector3(-1f, 0f, 0f);
                vent.graphic.sprite = WerewolfVent;
                break;
                case RoleEnum.SerialKiller:
                vent.transform.localPosition = new Vector3(-1f, 0f, 0f);
                vent.graphic.sprite = SkVent;
                break;
                case RoleEnum.Vampire:
                vent.transform.localPosition = new Vector3(-2f, 0f, 0f);
                vent.graphic.sprite = VampireVent;
                break;
                case RoleEnum.Juggernaut:
                case RoleEnum.Armaggeddon:
                case RoleEnum.Pestilence:
                vent.transform.localPosition = new Vector3(-2f, 0f, 0f);
                //vent.graphic.sprite = JuggVent;
                break;
                case RoleEnum.Engineer:
                vent.transform.localPosition = new Vector3(-2f, 0f, 0f);
                vent.graphic.sprite = EngiVent;
                break;
                case RoleEnum.Jester:
                vent.graphic.sprite = JesterVent;
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
