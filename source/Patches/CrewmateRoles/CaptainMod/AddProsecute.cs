using System;
using System.Linq;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using TownOfUsFusion.CrewmateRoles.VigilanteMod;
using TownOfUsFusion.Modifiers.AssassinMod;
using TownOfUsFusion.NeutralRoles.DoomsayerMod;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.CrewmateRoles.CaptainMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class AddTribunal
    {
        public static Sprite TribunalSprite => TownOfUsFusion.TribunalSprite;

        public static void GenButton(Captain role, int index)
        {
            var confirmButton = MeetingHud.Instance.playerStates[index].Buttons.transform.GetChild(0).gameObject;

            var newButton = Object.Instantiate(confirmButton, MeetingHud.Instance.playerStates[index].transform);
            var renderer = newButton.GetComponent<SpriteRenderer>();
            var passive = newButton.GetComponent<PassiveButton>();

            renderer.sprite = TribunalSprite;
            newButton.transform.position = confirmButton.transform.position - new Vector3(0.75f, 0f, 0f);
            newButton.transform.localScale *= 0.8f;
            newButton.layer = 5;
            newButton.transform.parent = confirmButton.transform.parent.parent;

            passive.OnClick = new Button.ButtonClickedEvent();
            passive.OnClick.AddListener(Reveal(role));
            role.TribunalButton = newButton;
            //if (role.TribunalThisMeeting) role.TribunalButton.Destroy();
        }

        private static Action Reveal(Captain role)
        {
            void Listener()
            {
                role.TribunalButton.Destroy();
                role.HasRevealed = true;
            }

            return Listener;
        }
        public static void RemoveAssassin(Captain mayor)
        {
            PlayerVoteArea voteArea = MeetingHud.Instance.playerStates.First(
                x => x.TargetPlayerId == mayor.Player.PlayerId);

            if (PlayerControl.LocalPlayer.Is(AbilityEnum.Assassin))
            {
                var assassin = Ability.GetAbility<Assassin>(PlayerControl.LocalPlayer);
                ShowHideButtons.HideTarget(assassin, voteArea.TargetPlayerId);
                voteArea.NameText.transform.localPosition = new Vector3(0.3384f, 0.0311f, -0.1f);
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer))
            {
                var doomsayer = Role.GetRole<Doomsayer>(PlayerControl.LocalPlayer);
                ShowHideButtonsDoom.HideTarget(doomsayer, voteArea.TargetPlayerId);
                voteArea.NameText.transform.localPosition = new Vector3(0.3384f, 0.0311f, -0.1f);
                foreach (var (targetId, guessText) in doomsayer.RoleGuess)
                {
                    if (!guessText.isActiveAndEnabled || voteArea.TargetPlayerId != targetId) continue;
                    guessText.gameObject.SetActive(false);
                }
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Vigilante))
            {
                var vigilante = Role.GetRole<Vigilante>(PlayerControl.LocalPlayer);
                ShowHideButtonsVigi.HideTarget(vigilante, voteArea.TargetPlayerId);
                voteArea.NameText.transform.localPosition = new Vector3(0.3384f, 0.0311f, -0.1f);
            }
        }
        public static void Postfix(MeetingHud __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Captain))
            {
                var politician = (Captain)role;
                politician.TribunalButton.Destroy();
            }
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Captain)) return;
            if (PlayerControl.LocalPlayer.IsJailed()) return;
            var mayorrole = Role.GetRole<Captain>(PlayerControl.LocalPlayer);
            if (mayorrole.TribunalThisMeeting) return;
            for (var i = 0; i < __instance.playerStates.Length; i++)
                if (PlayerControl.LocalPlayer.PlayerId == __instance.playerStates[i].TargetPlayerId)
                {
                    GenButton(mayorrole, i);
                }
        }
    }
}