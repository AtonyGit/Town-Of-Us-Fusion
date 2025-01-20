using HarmonyLib;
using System.Linq;
using UnityEngine;
<<<<<<< Updated upstream
using TownOfUsFusion.ImpostorRoles.TraitorMod;
=======
using TownOfUsFusion.ImpostorRoles.TraitorMod;
using TownOfUsFusion.Roles;
using TownOfUsFusion.NeutralRoles.SoulCollectorMod;
using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using TownOfUsFusion.CrewmateRoles.SwapperMod;
using TownOfUsFusion.CrewmateRoles.VigilanteMod;
using TownOfUsFusion.Modifiers.AssassinMod;
using TownOfUsFusion.NeutralRoles.DoomsayerMod;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine.UI;
using TownOfUsFusion.ImpostorRoles.BlackmailerMod;
using Reactor.Utilities.Extensions;
>>>>>>> Stashed changes

namespace TownOfUsFusion.Patches
{
    [HarmonyPatch(typeof(GameData))]
    public class DisconnectHandler
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameData.HandleDisconnect), typeof(PlayerControl), typeof(DisconnectReasons))]
        public static void Prefix([HarmonyArgument(0)] PlayerControl player)
        {
<<<<<<< Updated upstream
            if (CustomGameOptions.GameMode == GameMode.Cultist)
            {
                if (player.Is(RoleEnum.Necromancer) || player.Is(RoleEnum.Whisperer))
                {
                    foreach (var player2 in PlayerControl.AllPlayerControls)
                    {
                        if (player2.Is(Faction.Impostors)) Utils.MurderPlayer(player2, player2, true);
                    }
                }
            }
            else
            {
                /*if (player.IsLover() && CustomGameOptions.BothLoversDie)
                {
                    var otherLover = Modifier.GetModifier<Lover>(player).OtherLover;
                    if (!otherLover.Is(RoleEnum.Pestilence) && !otherLover.Data.IsDead
                         && !otherLover.Data.Disconnected) MurderPlayer(otherLover, otherLover, true);
                    if (otherLover.Is(RoleEnum.Sheriff))
                    {
                        var sheriff = Role.GetRole<Sheriff>(otherLover);
                        sheriff.IncorrectKills -= 1;
                    }
                }*/
                if (AmongUsClient.Instance.AmHost)
                {
                    if (player == SetTraitor.WillBeTraitor)
                    {
                        var toChooseFrom = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Crewmates) &&
                            !x.Is(ModifierEnum.Lover) && !x.Data.IsDead && !x.Data.Disconnected && !x.IsExeTarget()).ToList();
                        if (toChooseFrom.Count == 0) return;
                        var rand = Random.RandomRangeInt(0, toChooseFrom.Count);
                        var pc = toChooseFrom[rand];
=======
            if (AmongUsClient.Instance.AmHost)
            {
                if (player == SetTraitor.WillBeTraitor)
                {
                    var toChooseFrom = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Crewmates) &&
                        !x.Is(ModifierEnum.Lover) && !x.Data.IsDead && !x.Data.Disconnected && !x.Is(RoleEnum.Mayor) && !x.Is(RoleEnum.Politician) && !x.IsExeTarget()).ToList();
                    if (toChooseFrom.Count == 0) return;
                    var rand = Random.RandomRangeInt(0, toChooseFrom.Count);
                    var pc = toChooseFrom[rand];
>>>>>>> Stashed changes

                        SetTraitor.WillBeTraitor = pc;

                        Utils.Rpc(CustomRPC.SetTraitor, pc.PlayerId);
                    }
                }
            }
            if (player.Is(RoleEnum.Hypnotist))
            {
                var hypno = Role.GetRole<Hypnotist>(player);
                if (hypno.HysteriaActive && hypno.HypnotisedPlayers.Contains(PlayerControl.LocalPlayer.PlayerId)) hypno.UnHysteria();
            }
            if (player.Is(RoleEnum.SoulCollector))
            {
                var sc = Role.GetRole<SoulCollector>(player);
                SoulExtensions.ClearSouls(sc.Souls);
            }
            if (player.Is(ModifierEnum.Lover))
            {
                var lover = Modifier.GetModifier<Lover>(player);
                Modifier.ModifierDictionary.Remove(lover.OtherLover.Player.PlayerId);
            }
            if (MeetingHud.Instance)
            {
                PlayerVoteArea voteArea = MeetingHud.Instance.playerStates.First(x => x.TargetPlayerId == player.PlayerId);

                if (!player.Data.IsDead)
                {
                    if (voteArea == null) return;
                    if (voteArea.DidVote) voteArea.UnsetVote();
                    voteArea.AmDead = true;
                    voteArea.Overlay.gameObject.SetActive(true);
                    voteArea.Overlay.color = Color.white;
                    voteArea.XMark.gameObject.SetActive(true);
                    voteArea.XMark.transform.localScale = Vector3.one;
                }

                var blackmailers = Role.AllRoles.Where(x => x.RoleType == RoleEnum.Blackmailer && x.Player != null).Cast<Blackmailer>();
                foreach (var role in blackmailers)
                {
                    if (role.Blackmailed != null && voteArea.TargetPlayerId == role.Blackmailed.PlayerId)
                    {
                        if (BlackmailMeetingUpdate.PrevXMark != null && BlackmailMeetingUpdate.PrevOverlay != null)
                        {
                            voteArea.XMark.sprite = BlackmailMeetingUpdate.PrevXMark;
                            voteArea.Overlay.sprite = BlackmailMeetingUpdate.PrevOverlay;
                            voteArea.XMark.transform.localPosition = new Vector3(
                                voteArea.XMark.transform.localPosition.x - BlackmailMeetingUpdate.LetterXOffset,
                                voteArea.XMark.transform.localPosition.y - BlackmailMeetingUpdate.LetterYOffset,
                                voteArea.XMark.transform.localPosition.z);
                        }
                    }
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Vigilante) && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    var vigi = Role.GetRole<Vigilante>(PlayerControl.LocalPlayer);
                    ShowHideButtonsVigi.HideTarget(vigi, voteArea.TargetPlayerId);
                }

                if (PlayerControl.LocalPlayer.Is(AbilityEnum.Assassin) && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    var assassin = Ability.GetAbility<Assassin>(PlayerControl.LocalPlayer);
                    ShowHideButtons.HideTarget(assassin, voteArea.TargetPlayerId);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer) && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    var doom = Role.GetRole<Doomsayer>(PlayerControl.LocalPlayer);
                    ShowHideButtonsDoom.HideTarget(doom, voteArea.TargetPlayerId);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Jailor) && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    var jailor = Role.GetRole<Jailor>(PlayerControl.LocalPlayer);
                    jailor.ExecuteButton.Destroy();
                    jailor.UsesText.Destroy();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Swapper) && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    var swapper = Role.GetRole<Swapper>(PlayerControl.LocalPlayer);
                    var button = swapper.Buttons[voteArea.TargetPlayerId];
                    if (button.GetComponent<SpriteRenderer>().sprite == TownOfUsFusion.SwapperSwitch)
                    {
                        swapper.ListOfActives[voteArea.TargetPlayerId] = false;
                        if (SwapVotes.Swap1 == voteArea) SwapVotes.Swap1 = null;
                        if (SwapVotes.Swap2 == voteArea) SwapVotes.Swap2 = null;
                        Utils.Rpc(CustomRPC.SetSwaps, sbyte.MaxValue, sbyte.MaxValue);
                    }
                    button.SetActive(false);
                    button.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                    swapper.Buttons[voteArea.TargetPlayerId] = null;
                }

                foreach (var playerVoteArea in MeetingHud.Instance.playerStates)
                {
                    if (playerVoteArea.VotedFor != player.PlayerId) continue;
                    playerVoteArea.UnsetVote();
                    var voteAreaPlayer = Utils.PlayerById(playerVoteArea.TargetPlayerId);
                    if (voteAreaPlayer.Is(RoleEnum.Prosecutor))
                    {
                        var pros = Role.GetRole<Prosecutor>(voteAreaPlayer);
                        pros.ProsecuteThisMeeting = false;
                    }
                    if (!voteAreaPlayer.AmOwner) continue;
                    MeetingHud.Instance.ClearVote();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Imitator) && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    var imitatorRole = Role.GetRole<Imitator>(PlayerControl.LocalPlayer);
                    var button = imitatorRole.Buttons[voteArea.TargetPlayerId];
                    if (button.GetComponent<SpriteRenderer>().sprite == TownOfUsFusion.ImitateSelectSprite)
                    {
                        imitatorRole.ListOfActives[voteArea.TargetPlayerId] = false;
                        if (SetImitate.Imitate == voteArea) SetImitate.Imitate = null;
                    }
                    button.SetActive(false);
                    button.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                    imitatorRole.Buttons[voteArea.TargetPlayerId] = null;
                }
            }
        }
    }
}