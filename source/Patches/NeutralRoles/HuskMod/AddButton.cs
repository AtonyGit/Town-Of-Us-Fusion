using System;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Alliances;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.NeutralRoles.HuskMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
public class AddButton
{
    private static Sprite CycleBackSprite => TownOfUsFusion.CycleBackSprite;
    private static Sprite CycleForwardSprite => TownOfUsFusion.CycleForwardSprite;

    private static Sprite GuessSprite => TownOfUsFusion.GuessSprite;

    private static bool IsExempt(PlayerVoteArea voteArea)
    {
        if (voteArea.AmDead) return true;
        var player = Utils.PlayerById(voteArea.TargetPlayerId);
        if (!PlayerControl.LocalPlayer.Is(Faction.Impostors))
        {
            if (
                player == null ||
                player.Data.IsDead ||
                player.Data.Disconnected
            ) return true;
        }
        else
        {
            if (
                player == null ||
                player.Data.IsImpostor() ||
                player.Data.IsDead ||
                player.Data.Disconnected
            ) return true;
        }
        var role = Role.GetRole(player);
        return role != null && role.Criteria();
    }


    public static void GenButton(Husk role, PlayerVoteArea voteArea)
    {
        var targetId = voteArea.TargetPlayerId;
        if (IsExempt(voteArea))
        {
            role.Buttons[targetId] = (null, null, null, null);
            return;
        }

        var confirmButton = voteArea.Buttons.transform.GetChild(0).gameObject;
        var parent = confirmButton.transform.parent.parent;

        var nameText = Object.Instantiate(voteArea.NameText, voteArea.transform);
        voteArea.NameText.transform.localPosition = new Vector3(0.55f, 0.12f, -0.1f);
        nameText.transform.localPosition = new Vector3(0.55f, -0.12f, -0.1f);
        nameText.text = "Guess";

        var cycleBack = Object.Instantiate(confirmButton, voteArea.transform);
        var cycleRendererBack = cycleBack.GetComponent<SpriteRenderer>();
        cycleRendererBack.sprite = CycleBackSprite;
        cycleBack.transform.localPosition = new Vector3(-0.5f, 0.15f, -2f);
        cycleBack.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        cycleBack.layer = 5;
        cycleBack.transform.parent = parent;
        var cycleEventBack = new Button.ButtonClickedEvent();
        cycleEventBack.AddListener(Cycle(role, voteArea, nameText, false));
        cycleBack.GetComponent<PassiveButton>().OnClick = cycleEventBack;
        var cycleColliderBack = cycleBack.GetComponent<BoxCollider2D>();
        cycleColliderBack.size = cycleRendererBack.sprite.bounds.size;
        cycleColliderBack.offset = Vector2.zero;
        cycleBack.transform.GetChild(0).gameObject.Destroy();

        var cycleForward = Object.Instantiate(confirmButton, voteArea.transform);
        var cycleRendererForward = cycleForward.GetComponent<SpriteRenderer>();
        cycleRendererForward.sprite = CycleForwardSprite;
        cycleForward.transform.localPosition = new Vector3(-0.2f, 0.15f, -2f);
        cycleForward.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        cycleForward.layer = 5;
        cycleForward.transform.parent = parent;
        var cycleEventForward = new Button.ButtonClickedEvent();
        cycleEventForward.AddListener(Cycle(role, voteArea, nameText, true));
        cycleForward.GetComponent<PassiveButton>().OnClick = cycleEventForward;
        var cycleColliderForward = cycleForward.GetComponent<BoxCollider2D>();
        cycleColliderForward.size = cycleRendererForward.sprite.bounds.size;
        cycleColliderForward.offset = Vector2.zero;
        cycleForward.transform.GetChild(0).gameObject.Destroy();

        var guess = Object.Instantiate(confirmButton, voteArea.transform);
        var guessRenderer = guess.GetComponent<SpriteRenderer>();
        guessRenderer.sprite = GuessSprite;
        guess.transform.localPosition = new Vector3(-0.35f, -0.15f, -2f);
        guess.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        guess.layer = 5;
        guess.transform.parent = parent;
        var guessEvent = new Button.ButtonClickedEvent();
        guessEvent.AddListener(Guess(role, voteArea));
        guess.GetComponent<PassiveButton>().OnClick = guessEvent;
        var bounds = guess.GetComponent<SpriteRenderer>().bounds;
        bounds.size = new Vector3(0.52f, 0.3f, 0.16f);
        var guessCollider = guess.GetComponent<BoxCollider2D>();
        guessCollider.size = guessRenderer.sprite.bounds.size;
        guessCollider.offset = Vector2.zero;
        guess.transform.GetChild(0).gameObject.Destroy();

        role.Guesses.Add(targetId, "None");
        role.Buttons[targetId] = (cycleBack, cycleForward, guess, nameText);
    }

    private static Action Cycle(Husk role, PlayerVoteArea voteArea, TextMeshPro nameText, bool forwardsCycle = true)
    {
        void Listener()
        {
            if (MeetingHud.Instance.state == MeetingHud.VoteStates.Discussion) return;
            var currentGuess = role.Guesses[voteArea.TargetPlayerId];
            var guessIndex = currentGuess == "None"
                ? -1
                : role.PossibleGuesses.IndexOf(currentGuess);
            if (forwardsCycle)
            {
                if (++guessIndex >= role.PossibleGuesses.Count)
                    guessIndex = 0;
            }
            else
            {
                if (--guessIndex < 0)
                    guessIndex = role.PossibleGuesses.Count - 1;
            }

            var newGuess = role.Guesses[voteArea.TargetPlayerId] = role.PossibleGuesses[guessIndex];

            nameText.text = newGuess == "None"
                ? "Guess"
                : $"<color=#{role.SortedColorMapping[newGuess].ToHtmlStringRGBA()}>{newGuess}</color>";
        }

        return Listener;
    }

    private static Action Guess(Husk role, PlayerVoteArea voteArea)
    {
        void Listener()
        {
            if (
                MeetingHud.Instance.state == MeetingHud.VoteStates.Discussion ||
                IsExempt(voteArea) || PlayerControl.LocalPlayer.Data.IsDead
            ) return;
            var targetId = voteArea.TargetPlayerId;
            var currentGuess = role.Guesses[targetId];
            if (currentGuess == "None") return;

            var playerRole = Role.GetRole(voteArea);
            var playerModifier = Modifier.GetModifier(voteArea);
            var playerAlliance = Alliance.GetAlliance(voteArea);

            var toDie = playerRole.Name == currentGuess ? playerRole.Player : role.Player;
            if (playerModifier != null)
                toDie = (playerRole.Name == currentGuess || playerModifier.Name == currentGuess) ? playerRole.Player : role.Player;

            if (toDie.Is(RoleEnum.Necromancer) || toDie.Is(RoleEnum.Whisperer))
            {
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (player.Data.IsImpostor()) Utils.RpcMurderPlayer(player, player);
                }
            }

            if (!toDie.Is(RoleEnum.Pestilence))
            {
                HuskAssassinKill.RpcMurderPlayer(toDie, PlayerControl.LocalPlayer);
                role.RemainingKills--;
                ShowHideButtonsHusk.HideSingle(role, targetId, toDie == role.Player);
                if (toDie.IsLover() && CustomGameOptions.BothLoversDie)
                {
                    var lover = ((Lover)playerAlliance).OtherLover.Player;
                    if (!lover.Is(RoleEnum.Pestilence)) ShowHideButtonsHusk.HideSingle(role, lover.PlayerId, false);
                } else
                if (toDie.IsRecruit() && CustomGameOptions.DoJackalRecruitsDie)
                {
                    var recruit = ((Recruit)playerAlliance).OtherRecruit.Player;
                    if (!recruit.Is(RoleEnum.Pestilence)) ShowHideButtonsHusk.HideSingle(role, recruit.PlayerId, false);
                }
            }
        }

        return Listener;
    }

    public static void Postfix(MeetingHud __instance)
    {
        foreach (var role in Role.GetRoles(RoleEnum.Husk))
        {
            var retributionist = (Husk)role;
            retributionist.Guesses.Clear();
            retributionist.Buttons.Clear();
            retributionist.GuessedThisMeeting = false;
        }

        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Husk)) return;

        var retributionistRole = Role.GetRole<Husk>(PlayerControl.LocalPlayer);
        if (retributionistRole.RemainingKills <= 0) return;
        foreach (var voteArea in __instance.playerStates)
        {
            GenButton(retributionistRole, voteArea);
        }
    }
}
}
