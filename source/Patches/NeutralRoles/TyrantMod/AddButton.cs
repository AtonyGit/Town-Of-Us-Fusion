using System;
using System.Linq;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Modifiers.AssassinMod;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.NeutralRoles.TyrantMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
public class AddRevealTyButton
{
    public static Sprite RevealSprite => TownOfUsFusion.RevealSprite;

    public static void GenButton(Tyrant role, int index)
    {
        var confirmButton = MeetingHud.Instance.playerStates[index].Buttons.transform.GetChild(0).gameObject;

        var newButton = Object.Instantiate(confirmButton, MeetingHud.Instance.playerStates[index].transform);
        var renderer = newButton.GetComponent<SpriteRenderer>();
        var passive = newButton.GetComponent<PassiveButton>();

        renderer.sprite = RevealSprite;
        newButton.transform.position = confirmButton.transform.position - new Vector3(0.75f, 0f, 0f);
        newButton.transform.localScale *= 0.8f;
        newButton.layer = 5;
        newButton.transform.parent = confirmButton.transform.parent.parent;

        passive.OnClick = new Button.ButtonClickedEvent();
        passive.OnClick.AddListener(Reveal(role));
        role.RevealButton = newButton;
    }


    private static Action Reveal(Tyrant role)
    {
        void Listener()
        {
            role.RevealButton.Destroy();
            role.Revealed = true;
            Utils.Rpc(CustomRPC.Reveal, role.Player.PlayerId);
        }

        return Listener;
    }

    public static void RemoveAssassin(Tyrant tyrant)
    {
        PlayerVoteArea voteArea = MeetingHud.Instance.playerStates.First(
            x => x.TargetPlayerId == tyrant.Player.PlayerId);
        if (PlayerControl.LocalPlayer.Is(AbilityEnum.Assassin))
        {
            var assassin = Ability.GetAbility<Assassin>(PlayerControl.LocalPlayer);
            ShowHideButtons.HideTarget(assassin, voteArea.TargetPlayerId);
            voteArea.NameText.transform.localPosition += new Vector3(-0.2f, -0.1f, 0f);
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer))
        {
            var doomsayer = Role.GetRole<Doomsayer>(PlayerControl.LocalPlayer);
            var (cycleBack, cycleForward, guess, guessText) = doomsayer.Buttons[voteArea.TargetPlayerId];
            if (cycleBack == null || cycleForward == null) return;
            cycleBack.SetActive(false);
            cycleForward.SetActive(false);
            guess.SetActive(false);
            guessText.gameObject.SetActive(false);

            cycleBack.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            cycleForward.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            guess.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            doomsayer.Buttons[voteArea.TargetPlayerId] = (null, null, null, null);
            doomsayer.Guesses.Remove(voteArea.TargetPlayerId);
            voteArea.NameText.transform.localPosition += new Vector3(-0.2f, -0.1f, 0f);
        }
        else if (PlayerControl.LocalPlayer.Is(RoleEnum.Vigilante))
        {
            var vigilante = Role.GetRole<Vigilante>(PlayerControl.LocalPlayer);
            var (cycleBack, cycleForward, guess, guessText) = vigilante.Buttons[voteArea.TargetPlayerId];
            if (cycleBack == null || cycleForward == null) return;
            cycleBack.SetActive(false);
            cycleForward.SetActive(false);
            guess.SetActive(false);
            guessText.gameObject.SetActive(false);

            cycleBack.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            cycleForward.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            guess.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            vigilante.Buttons[voteArea.TargetPlayerId] = (null, null, null, null);
            vigilante.Guesses.Remove(voteArea.TargetPlayerId);
            voteArea.NameText.transform.localPosition += new Vector3(-0.2f, -0.1f, 0f);
        }
        return;
    }

    public static void Postfix(MeetingHud __instance)
    {
        foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
        {
            var tyrant = (Tyrant)role;
            tyrant.RevealButton.Destroy();
        }

        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Tyrant)) return;
        var tyrantrole = Role.GetRole<Tyrant>(PlayerControl.LocalPlayer);
        if (tyrantrole.Revealed) return;
        for (var i = 0; i < __instance.playerStates.Length; i++)
            if (PlayerControl.LocalPlayer.PlayerId == __instance.playerStates[i].TargetPlayerId)
            {
                GenButton(tyrantrole, i);
            }
    }
}
}