﻿using System.Linq;
using TownOfUsFusion.Roles;
using UnityEngine;
using UnityEngine.UI;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.CrewmateRoles.SwapperMod;
using TownOfUsFusion.CrewmateRoles.VigilanteMod;
using TownOfUsFusion.NeutralRoles.DoomsayerMod;
using TownOfUsFusion.ImpostorRoles.BlackmailerMod;
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using TownOfUsFusion.Patches;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Roles.Alliances;
using TownOfUsFusion.NeutralRoles.HuskMod;
using Hazel;

namespace TownOfUsFusion.Modifiers.AssassinMod
{
    public class AssassinKill
{
    public static void RpcMurderPlayer(PlayerControl player, PlayerControl assassin)
    {
        PlayerVoteArea voteArea = MeetingHud.Instance.playerStates.First(
            x => x.TargetPlayerId == player.PlayerId
        );
        RpcMurderPlayer(voteArea, player, assassin);
    }
    public static void RpcMurderPlayer(PlayerVoteArea voteArea, PlayerControl player, PlayerControl assassin)
    {
        MurderPlayer(voteArea, player);
        AssassinKillCount(player, assassin);
        Utils.Rpc(CustomRPC.AssassinKill, player.PlayerId, assassin.PlayerId);
    }

    public static void MurderPlayer(PlayerControl player, bool checkLover = true)
    {
        PlayerVoteArea voteArea = MeetingHud.Instance.playerStates.First(
            x => x.TargetPlayerId == player.PlayerId
        );
        MurderPlayer(voteArea, player, checkLover);
    }
    public static void AssassinKillCount(PlayerControl player, PlayerControl assassin)
    {
        var assassinPlayer = Role.GetRole(assassin);
        if (player == assassin) assassinPlayer.IncorrectAssassinKills += 1;
        else assassinPlayer.CorrectAssassinKills += 1;
    }
    public static void MurderPlayer(
        PlayerVoteArea voteArea,
        PlayerControl player,
        bool checkLover = true
    )
    {
        var hudManager = DestroyableSingleton<HudManager>.Instance;
        if (checkLover)
        {
            SoundManager.Instance.PlaySound(player.KillSfx, false, 0.8f);
            hudManager.KillOverlay.ShowKillAnimation(player.Data, player.Data);
        }
        var amOwner = player.AmOwner;
        if (amOwner)
        {
            Utils.ShowDeadBodies = true;
            hudManager.ShadowQuad.gameObject.SetActive(false);
            player.nameText().GetComponent<MeshRenderer>().material.SetInt("_Mask", 0);
            player.RpcSetScanner(false);
            ImportantTextTask importantTextTask = new GameObject("_Player").AddComponent<ImportantTextTask>();
            importantTextTask.transform.SetParent(AmongUsClient.Instance.transform, false);
            if (!GameOptionsManager.Instance.currentNormalGameOptions.GhostsDoTasks)
            {
                for (int i = 0; i < player.myTasks.Count; i++)
                {
                    PlayerTask playerTask = player.myTasks.ToArray()[i];
                    playerTask.OnRemove();
                    Object.Destroy(playerTask.gameObject);
                }

                player.myTasks.Clear();
                importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(
                    StringNames.GhostIgnoreTasks,
                    new Il2CppReferenceArray<Il2CppSystem.Object>(0)
                );
            }
            else
            {
                importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(
                    StringNames.GhostDoTasks,
                    new Il2CppReferenceArray<Il2CppSystem.Object>(0));
            }

            player.myTasks.Insert(0, importantTextTask);

            if (player.Is(RoleEnum.Swapper))
            {
                var swapper = Role.GetRole<Swapper>(PlayerControl.LocalPlayer);
                swapper.ListOfActives.Clear();
                swapper.Buttons.Clear();
                SwapVotes.Swap1 = null;
                SwapVotes.Swap2 = null;
                Utils.Rpc(CustomRPC.SetSwaps, sbyte.MaxValue, sbyte.MaxValue);
                var buttons = Role.GetRole<Swapper>(player).Buttons;
                foreach (var button in buttons)
                {
                    button.SetActive(false);
                    button.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                }
            }

            if (player.Is(RoleEnum.Imitator))
            {
                var imitator = Role.GetRole<Imitator>(PlayerControl.LocalPlayer);
                imitator.ListOfActives.Clear();
                imitator.Buttons.Clear();
                SetImitate.Imitate = null;
                var buttons = Role.GetRole<Imitator>(player).Buttons;
                foreach (var button in buttons)
                {
                    button.SetActive(false);
                    button.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                }
            }

            if (player.Is(RoleEnum.Vigilante))
            {
                var retributionist = Role.GetRole<Vigilante>(PlayerControl.LocalPlayer);
                ShowHideButtonsVigi.HideButtonsVigi(retributionist);
            }

            if (player.Is(RoleEnum.Husk))
            {
                var husk = Role.GetRole<Husk>(PlayerControl.LocalPlayer);
                ShowHideButtonsHusk.HideButtonsHusk(husk);
            }
            
            if (player.Is(AbilityEnum.Assassin))
            {
                var assassin = Ability.GetAbility<Assassin>(PlayerControl.LocalPlayer);
                ShowHideButtons.HideButtons(assassin);
            }

            if (player.Is(RoleEnum.Doomsayer))
            {
                var doomsayer = Role.GetRole<Doomsayer>(PlayerControl.LocalPlayer);
                ShowHideButtonsDoom.HideButtonsDoom(doomsayer);
            }

            if (player.Is(RoleEnum.Mayor))
            {
                var mayor = Role.GetRole<Mayor>(PlayerControl.LocalPlayer);
                mayor.RevealButton.Destroy();
            }
        }
        player.Die(DeathReason.Kill, false);
        if (checkLover && player.IsLover() && CustomGameOptions.BothLoversDie)
        {
            var otherLover = Alliance.GetAlliance<Lover>(player).OtherLover.Player;
            if (!otherLover.Is(RoleEnum.Pestilence)) MurderPlayer(otherLover, false);
        }/* else
        if (checkLover && player.IsRecruit() && CustomGameOptions.DoJackalRecruitsDie)
        {
            var otherRecruit = Alliance.GetAlliance<Recruit>(player).OtherRecruit.Player;
            if (!otherRecruit.Is(RoleEnum.Pestilence)) MurderPlayer(otherRecruit, false);
        }*/

            var role2 = Role.GetRole(player);
            var assassinPlayer = Ability.GetAbility<Assassin>(player);
            role2.DeathReason = DeathReasonEnum.Guessed;
            role2.KilledBy = " By " + Utils.ColorString(Colors.Impostor, assassinPlayer.PlayerName);

        var deadPlayer = new DeadPlayer
        {
            PlayerId = player.PlayerId,
            KillerId = player.PlayerId,
            KillTime = System.DateTime.UtcNow,
        };

        Murder.KilledPlayers.Add(deadPlayer);
        if (voteArea == null) return;
        if (voteArea.DidVote) voteArea.UnsetVote();
        voteArea.AmDead = true;
        voteArea.Overlay.gameObject.SetActive(true);
        voteArea.Overlay.color = Color.white;
        voteArea.XMark.gameObject.SetActive(true);
        voteArea.XMark.transform.localScale = Vector3.one;

        var meetingHud = MeetingHud.Instance;
        if (amOwner)
        {
            meetingHud.SetForegroundForDead();
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

        foreach (var playerVoteArea in meetingHud.playerStates)
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
            meetingHud.ClearVote();
        }

        if (AmongUsClient.Instance.AmHost) 
        {
                foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
                {
                    if (role is Tyrant tyrant)
                    {
                        if (role.Player == player)
                        {
                            tyrant.ExtraVotes.Clear();
                        }
                        else
                        {
                            var votesRegained = tyrant.ExtraVotes.RemoveAll(x => x == player.PlayerId);

                            if (tyrant.Player == PlayerControl.LocalPlayer)
                                tyrant.VoteBank += votesRegained;

                            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                                (byte) CustomRPC.AddTyrantVoteBank, SendOption.Reliable, -1);
                            writer.Write(tyrant.Player.PlayerId);
                            writer.Write(votesRegained);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                        }
                    }
                }
            meetingHud.CheckForEndVoting();
        }

        AddHauntPatch.AssassinatedPlayers.Add(player);
    }
}
}
