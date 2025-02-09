using AmongUs.GameOptions;
using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.Start))]
    public static class KillButtonAwake
    {
        public static void Prefix(KillButton __instance)
        {
            //__instance.transform.Find("Text_TMP").gameObject.SetActive(false);
        }
    }

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

            var flag = false;
            var buttonKills = false;
            var otherButtonKills = false;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Psychic))
            {
                __instance.KillButton.graphic.sprite = Psychic;
                __instance.KillButton.buttonLabelText.text = "Reveal";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Psychic);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Medic))
            {
                __instance.KillButton.graphic.sprite = Medic;
                __instance.KillButton.buttonLabelText.text = "Shield";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Medic);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Arsonist))
            {
                __instance.KillButton.graphic.sprite = Douse;
                __instance.KillButton.buttonLabelText.text = "Douse";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Arsonist);
                flag = true;
                otherButtonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Altruist))
            {
                __instance.KillButton.graphic.sprite = Revive;
                __instance.KillButton.buttonLabelText.text = "Altruist";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Altruist);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
            {
                __instance.KillButton.graphic.sprite = Alert;
                __instance.KillButton.buttonLabelText.text = "Alert";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Veteran);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Amnesiac))
            {
                __instance.KillButton.graphic.sprite = Remember;
                __instance.KillButton.buttonLabelText.text = "Remember";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Amnesiac);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
            {
                __instance.KillButton.graphic.sprite = Track;
                __instance.KillButton.buttonLabelText.text = "Track";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Tracker);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
            {
                __instance.KillButton.graphic.sprite = Transport;
                __instance.KillButton.buttonLabelText.text = "Transport";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Transporter);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Medium))
            {
                __instance.KillButton.graphic.sprite = Mediate;
                __instance.KillButton.buttonLabelText.text = "Mediate";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Medium);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Survivor))
            {
                __instance.KillButton.graphic.sprite = Vest;
                __instance.KillButton.buttonLabelText.text = "Vest";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Survivor);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel))
            {
                __instance.KillButton.graphic.sprite = Protect;
                __instance.KillButton.buttonLabelText.text = "Protect";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.GuardianAngel);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Plaguebearer))
            {
                __instance.KillButton.graphic.sprite = Infect;
                __instance.KillButton.buttonLabelText.text = "Infect";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Apocalypse);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer))
            {
                __instance.KillButton.graphic.sprite = Fix;
                __instance.KillButton.buttonLabelText.text = "Fix";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Engineer);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.TimeLord))
            {
                __instance.KillButton.graphic.sprite = Rewind;
                __instance.KillButton.buttonLabelText.text = "Rewind";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.TimeLord);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Trapper))
            {
                __instance.KillButton.graphic.sprite = Trap;
                __instance.KillButton.buttonLabelText.text = "Trap";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Trapper);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator))
            {
                __instance.KillButton.graphic.sprite = Autopsy;
                __instance.KillButton.buttonLabelText.text = "Autopsy";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Investigator);
                flag = true;
                otherButtonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer))
            {
                __instance.KillButton.graphic.sprite = Observe;
                __instance.KillButton.buttonLabelText.text = "Observe";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Doomsayer);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal))
            {
                __instance.KillButton.graphic.sprite = Consume;
                __instance.KillButton.buttonLabelText.text = "Consume";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Cannibal);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Vampire))
            {
                __instance.KillButton.graphic.sprite = Bite;
                __instance.KillButton.buttonLabelText.text = "Bite";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Vampire);
                flag = true;
                buttonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Politician))
            {
                __instance.KillButton.graphic.sprite = Campaign;
                __instance.KillButton.buttonLabelText.text = "Campaign";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Politician);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Oracle))
            {
                __instance.KillButton.graphic.sprite = Fortify;
                __instance.KillButton.buttonLabelText.text = "Bless";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Oracle);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Jailor))
            {
                __instance.KillButton.graphic.sprite = Jail;
                __instance.KillButton.buttonLabelText.text = "Jail";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Jailor);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.SoulCollector))
            {
                __instance.KillButton.graphic.sprite = Collect;
                __instance.KillButton.buttonLabelText.text = "Collect";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Apocalypse);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Lookout))
            {
                __instance.KillButton.graphic.sprite = Watch;
                __instance.KillButton.buttonLabelText.text = "Watch";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Lookout);
                flag = true;
                buttonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Deputy))
            {
                __instance.KillButton.graphic.sprite = Camp;
                __instance.KillButton.buttonLabelText.text = "Camp";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Deputy);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard))
            {
                __instance.KillButton.graphic.sprite = Guard;
                __instance.KillButton.buttonLabelText.text = "Guard";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Bodyguard);
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.MirrorMaster))
            {
                __instance.KillButton.graphic.sprite = Unleash;
                __instance.KillButton.buttonLabelText.text = "Unleash";
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.MirrorMaster);
                flag = true;
                buttonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Sheriff))
            {
                __instance.KillButton.graphic.sprite = SheriffKill;
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Sheriff);
                flag = true;
                buttonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Pestilence))
            {
                //__instance.KillButton.graphic.sprite = PestKill;
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Apocalypse);
                flag = true;
                buttonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Werewolf))
            {
                __instance.KillButton.graphic.sprite = WerewolfKill;
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Werewolf);
                flag = true;
                buttonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.SerialKiller))
            {
                __instance.KillButton.graphic.sprite = SkKill;
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.SerialKiller);
                flag = true;
                buttonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Juggernaut))
            {
                //__instance.KillButton.graphic.sprite = JuggKill;
                __instance.KillButton.buttonLabelText.SetOutlineColor(Patches.Colors.Apocalypse);
                flag = true;
                buttonKills = true;
            }
            
            if (!PlayerControl.LocalPlayer.Is(Faction.Impostors) &&
                GameOptionsManager.Instance.CurrentGameOptions.GameMode != GameModes.HideNSeek)
            {
                __instance.KillButton.transform.localPosition = new Vector3(0f, 1f, 0f);
                buttonKills = true;
            }
            
            if(PlayerControl.LocalPlayer.Is(RoleEnum.Glitch))
            {
                __instance.ImpostorVentButton.transform.localPosition = new Vector3(-2f, 0f, 0f);
                __instance.ImpostorVentButton.graphic.sprite = GlitchVent;
                __instance.ImpostorVentButton.buttonLabelText.SetOutlineColor(Patches.Colors.Glitch);
                buttonKills = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Pestilence))
            {
                __instance.ImpostorVentButton.transform.localPosition = new Vector3(-2f, 0f, 0f);
                //__instance.ImpostorVentButton.graphic.sprite = PestVent;
                __instance.ImpostorVentButton.buttonLabelText.SetOutlineColor(Patches.Colors.Apocalypse);
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Juggernaut))
            {
                __instance.ImpostorVentButton.transform.localPosition = new Vector3(-2f, 0f, 0f);
                //__instance.ImpostorVentButton.graphic.sprite = JuggVent;
                __instance.ImpostorVentButton.buttonLabelText.SetOutlineColor(Patches.Colors.Apocalypse);
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Vampire))
            {
                __instance.ImpostorVentButton.transform.localPosition = new Vector3(-2f, 0f, 0f);
                __instance.ImpostorVentButton.graphic.sprite = VampireVent;
                __instance.ImpostorVentButton.buttonLabelText.SetOutlineColor(Patches.Colors.Vampire);
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer))
            {
                __instance.ImpostorVentButton.transform.localPosition = new Vector3(-2f, 0f, 0f);
                __instance.ImpostorVentButton.graphic.sprite = EngiVent;
                __instance.ImpostorVentButton.buttonLabelText.SetOutlineColor(Patches.Colors.Engineer);
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Werewolf))
            {
                __instance.ImpostorVentButton.transform.localPosition = new Vector3(-1f, 1f, 0f);
                __instance.ImpostorVentButton.graphic.sprite = WerewolfVent;
                __instance.ImpostorVentButton.buttonLabelText.SetOutlineColor(Patches.Colors.Werewolf);
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.SerialKiller))
            {
                __instance.ImpostorVentButton.transform.localPosition = new Vector3(-1f, 1f, 0f);
                __instance.ImpostorVentButton.graphic.sprite = SkVent;
                __instance.ImpostorVentButton.buttonLabelText.SetOutlineColor(Patches.Colors.SerialKiller);
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Jester))
            {
                __instance.ImpostorVentButton.graphic.sprite = JesterVent;
                __instance.ImpostorVentButton.buttonLabelText.SetOutlineColor(Patches.Colors.Jester);
            }

            bool KillKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionSecondary");
            if (!buttonKills) KillKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionQuaternary");
            var controller = ConsoleJoystick.player.GetButtonDown(8);
            if ((KillKey || controller) && __instance.KillButton != null && flag && !PlayerControl.LocalPlayer.Data.IsDead)
                __instance.KillButton.DoClick();

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
