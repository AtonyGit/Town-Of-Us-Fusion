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
        __instance.transform.Find("Text_TMP").gameObject.SetActive(false);
    }
}

[HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class KillButtonSprite
{
    private static Sprite Fix => TownOfUsFusion.EngineerFix;
    private static Sprite Medic => TownOfUsFusion.MedicSprite;
    private static Sprite Seer => TownOfUsFusion.SeerSprite;
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
    private static Sprite Inspect => TownOfUsFusion.InspectSprite;
    private static Sprite Swoop => TownOfUsFusion.SwoopSprite;
    private static Sprite Observe => TownOfUsFusion.ObserveSprite;
    private static Sprite Bite => TownOfUsFusion.BiteSprite;
    private static Sprite Stake => TownOfUsFusion.StakeSprite;
    private static Sprite Confess => TownOfUsFusion.ConfessSprite;
    private static Sprite Radiate => TownOfUsFusion.RadiateSprite;

    private static Sprite Kill;


    public static void Postfix(HudManager __instance)
    {
        if (__instance.KillButton == null) return;

        if (!Kill) Kill = __instance.KillButton.graphic.sprite;

        var flag = false;
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Seer) || PlayerControl.LocalPlayer.Is(RoleEnum.CultistSeer))
        {
            __instance.KillButton.graphic.sprite = Seer;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Medic))
        {
            __instance.KillButton.graphic.sprite = Medic;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Arsonist))
        {
            __instance.KillButton.graphic.sprite = Douse;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Altruist))
        {
            __instance.KillButton.graphic.sprite = Revive;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
        {
            __instance.KillButton.graphic.sprite = Alert;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Amnesiac))
        {
            __instance.KillButton.graphic.sprite = Remember;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
        {
            __instance.KillButton.graphic.sprite = Track;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
        {
            __instance.KillButton.graphic.sprite = Transport;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Medium))
        {
            __instance.KillButton.graphic.sprite = Mediate;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Survivor))
        {
            __instance.KillButton.graphic.sprite = Vest;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel))
        {
            __instance.KillButton.graphic.sprite = Protect;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Plaguebearer))
        {
            __instance.KillButton.graphic.sprite = Infect;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer) && CustomGameOptions.GameMode != GameMode.Cultist)
        {
            __instance.KillButton.graphic.sprite = Fix;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Trapper))
        {
            __instance.KillButton.graphic.sprite = Trap;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Detective))
        {
            __instance.KillButton.graphic.sprite = Inspect;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Chameleon))
        {
            __instance.KillButton.graphic.sprite = Swoop;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer))
        {
            __instance.KillButton.graphic.sprite = Observe;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Vampire))
        {
            __instance.KillButton.graphic.sprite = Bite;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.VampireHunter))
        {
            __instance.KillButton.graphic.sprite = Stake;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Oracle))
        {
            __instance.KillButton.graphic.sprite = Confess;
            flag = true;
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Aurial))
        {
            __instance.KillButton.graphic.sprite = Radiate;
            flag = true;
        }
        else
        {
            __instance.KillButton.graphic.sprite = Kill;
            __instance.KillButton.buttonLabelText.gameObject.SetActive(true);
            __instance.KillButton.buttonLabelText.text = "Kill";
            flag = PlayerControl.LocalPlayer.Is(RoleEnum.Sheriff) || PlayerControl.LocalPlayer.Is(RoleEnum.Pestilence) ||
                PlayerControl.LocalPlayer.Is(RoleEnum.Werewolf) || PlayerControl.LocalPlayer.Is(RoleEnum.Juggernaut);
        }
        if (!PlayerControl.LocalPlayer.Is(Faction.Impostors) &&
            GameOptionsManager.Instance.CurrentGameOptions.GameMode != GameModes.HideNSeek)
        {
            __instance.KillButton.transform.localPosition = new Vector3(0f, 1f, 0f);
        }
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer) || PlayerControl.LocalPlayer.Is(RoleEnum.Glitch)
             || PlayerControl.LocalPlayer.Is(RoleEnum.Pestilence) || PlayerControl.LocalPlayer.Is(RoleEnum.Juggernaut)
             || PlayerControl.LocalPlayer.Is(RoleEnum.Vampire))
        {
            __instance.ImpostorVentButton.transform.localPosition = new Vector3(-2f, 0f, 0f);
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Werewolf))
        {
            __instance.ImpostorVentButton.transform.localPosition = new Vector3(-1f, 1f, 0f);
        }

        bool KillKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("Kill");
        var controller = ConsoleJoystick.player.GetButtonDown(8);
        if ((KillKey || controller) && __instance.KillButton != null && flag && !PlayerControl.LocalPlayer.Data.IsDead)
            __instance.KillButton.DoClick();

        var role = Role.GetRole(PlayerControl.LocalPlayer);
        bool AbilityKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ToU imp/nk");
        if (role?.ExtraButtons != null && AbilityKey && !PlayerControl.LocalPlayer.Data.IsDead)
            role?.ExtraButtons[0]?.DoClick();

        if (Modifier.GetModifier<ButtonBarry>(PlayerControl.LocalPlayer)?.ButtonUsed == false &&
            Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ToU bb/disperse/mimic") &&
            !PlayerControl.LocalPlayer.Data.IsDead)
        {
            Modifier.GetModifier<ButtonBarry>(PlayerControl.LocalPlayer).ButtonButton.DoClick();
        }
        else if (Modifier.GetModifier<Disperser>(PlayerControl.LocalPlayer)?.ButtonUsed == false &&
                 Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ToU bb/disperse/mimic") &&
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
