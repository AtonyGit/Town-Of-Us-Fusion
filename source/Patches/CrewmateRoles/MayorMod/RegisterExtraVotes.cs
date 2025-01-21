using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Linq;
using InnerNet;
using Hazel;

namespace TownOfUsFusion.CrewmateRoles.MayorMod
{
    [HarmonyPatch(typeof(MeetingHud))]
public class RegisterExtraVotes
{

        [HarmonyPatch(nameof(MeetingHud.Update))]
        public static void Postfix(MeetingHud __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Tyrant)) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (__instance.TimerText.text.Contains("Can Vote")) return;
            var role = Role.GetRole<Tyrant>(PlayerControl.LocalPlayer);
            __instance.TimerText.text = "Can Vote: " + role.VoteBank + " time(s) | " + __instance.TimerText.text;
        }
    public static Dictionary<byte, int> CalculateAllVotes(MeetingHud __instance)
    {
        var dictionary = new Dictionary<byte, int>();
        for (var i = 0; i < __instance.playerStates.Length; i++)
        {
            var playerVoteArea = __instance.playerStates[i];
            var player = Utils.PlayerById(playerVoteArea.TargetPlayerId);
            if (!player.Is(RoleEnum.Prosecutor)) continue;
            var pros = Role.GetRole<Prosecutor>(player);
            if (pros.Player.Data.IsDead || pros.Player.Data.Disconnected) continue;
            if (!playerVoteArea.DidVote
                || playerVoteArea.AmDead
                || playerVoteArea.VotedFor == PlayerVoteArea.MissedVote
                || playerVoteArea.VotedFor == PlayerVoteArea.DeadVote)
            {
                pros.ProsecuteThisMeeting = false;
                continue;
            }
            else if (pros.ProsecuteThisMeeting)
            {
                if (dictionary.TryGetValue(playerVoteArea.VotedFor, out var num2))
                    dictionary[playerVoteArea.VotedFor] = num2 + 5;
                else
                    dictionary[playerVoteArea.VotedFor] = 5;
                return dictionary;
            }
        }

        for (var i = 0; i < __instance.playerStates.Length; i++)
        {
            var playerVoteArea = __instance.playerStates[i];
            if (!playerVoteArea.DidVote
                || playerVoteArea.AmDead
                || playerVoteArea.VotedFor == PlayerVoteArea.MissedVote
                || playerVoteArea.VotedFor == PlayerVoteArea.DeadVote) continue;

            var player = Utils.PlayerById(playerVoteArea.TargetPlayerId);
            if (player.Is(RoleEnum.Mayor))
            {
                var mayor = Role.GetRole<Mayor>(player);
                if (mayor.Revealed)
                {
                    if (dictionary.TryGetValue(playerVoteArea.VotedFor, out var num2))
                        dictionary[playerVoteArea.VotedFor] = num2 + 2;
                    else
                        dictionary[playerVoteArea.VotedFor] = 2;
                }
            }

            if (dictionary.TryGetValue(playerVoteArea.VotedFor, out var num))
                dictionary[playerVoteArea.VotedFor] = num + 1;
            else
                dictionary[playerVoteArea.VotedFor] = 1;

            foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
            foreach (var number in ((Tyrant)role).ExtraVotes)
                if (dictionary.TryGetValue(number, out var num2))
                    dictionary[number] = num2 + 1;
        }

        dictionary.MaxPair(out var tie);

        if (tie)
            foreach (var player in __instance.playerStates)
            {
                if (!player.DidVote
                    || player.AmDead
                    || player.VotedFor == PlayerVoteArea.MissedVote
                    || player.VotedFor == PlayerVoteArea.DeadVote) continue;

                var modifier = Modifier.GetModifier(player);
                if (modifier == null) continue;
                if (modifier.ModifierType == ModifierEnum.Tiebreaker)
                {
                    if (dictionary.TryGetValue(player.VotedFor, out var num))
                        dictionary[player.VotedFor] = num + 1;
                    else
                        dictionary[player.VotedFor] = 1;
                }
            }

        return dictionary;
    }

        [HarmonyPatch(nameof(MeetingHud.Start))]
        public static void Prefix()
        {
            foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
            {
                var mayor = (Tyrant)role;
                mayor.ExtraVotes.Clear();
                if (mayor.VoteBank < 0)
                    mayor.VoteBank = 0;

                mayor.VoteBank++;
                mayor.SelfVote = false;
                mayor.VotedOnce = false;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(
            nameof(MeetingHud.HandleDisconnect),
            typeof(PlayerControl), typeof(DisconnectReasons)
        )]
        public static void Prefix(
            MeetingHud __instance, [HarmonyArgument(0)] PlayerControl player)
        {
            if (AmongUsClient.Instance.AmHost && MeetingHud.Instance)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
                {
                    if (role is Tyrant mayor)
                    {
                        if (mayor.VotedOnce)
                        {
                            var votesRegained = mayor.ExtraVotes.RemoveAll(x => x == player.PlayerId);

                            if (mayor.Player == PlayerControl.LocalPlayer)
                                mayor.VoteBank += votesRegained;

                            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                                (byte)CustomRPC.AddTyrantVoteBank, SendOption.Reliable, -1);
                            writer.Write(mayor.Player.PlayerId);
                            writer.Write(mayor.VoteBank);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                        }
                    }
                }
            }
        }
        
        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
        public static class Confirm
        {
            public static bool Prefix(MeetingHud __instance)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Tyrant)) return true;
                if (__instance.state != MeetingHud.VoteStates.Voted) return true;
                __instance.state = MeetingHud.VoteStates.NotVoted;
                return true;
            }

            [HarmonyPriority(Priority.First)]
            public static void Postfix(MeetingHud __instance)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Tyrant)) return;
                var role = Role.GetRole<Tyrant>(PlayerControl.LocalPlayer);
                if (role.CanVote) __instance.SkipVoteButton.gameObject.SetActive(true);
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.CastVote))]
        public static class CastVote
        {
            public static bool Prefix(MeetingHud __instance, [HarmonyArgument(0)] byte srcPlayerId,
                [HarmonyArgument(1)] byte suspectPlayerId)
            {
                var player = PlayerControl.AllPlayerControls.ToArray().FirstOrDefault(x => x.PlayerId == srcPlayerId);
                if (!player.Is(RoleEnum.Tyrant)) return true;

                var playerVoteArea = __instance.playerStates.ToArray().First(pv => pv.TargetPlayerId == srcPlayerId);

                if (playerVoteArea.AmDead)
                    return false;

                if (PlayerControl.LocalPlayer.PlayerId == srcPlayerId || AmongUsClient.Instance.NetworkMode != NetworkModes.LocalGame)
                {
                    SoundManager.Instance.PlaySound(__instance.VoteLockinSound, false, 1f);
                }

                var role = Role.GetRole<Tyrant>(player);
                if (playerVoteArea.DidVote)
                {
                    role.ExtraVotes.Add(suspectPlayerId);
                }
                else
                {
                    playerVoteArea.SetVote(suspectPlayerId);
                    playerVoteArea.Flag.enabled = true;
                    PlayerControl.LocalPlayer.RpcSendChatNote(srcPlayerId, ChatNoteTypes.DidVote);
                }
                __instance.Cast<InnerNetObject>().SetDirtyBit(1U);
                __instance.CheckForEndVoting();

                return false;
            }
        }
        
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))]
    public static class VotingComplete
    {
        public static void Postfix(MeetingHud __instance,
            [HarmonyArgument(0)] Il2CppStructArray<MeetingHud.VoterState> states,
            [HarmonyArgument(1)] GameData.PlayerInfo exiled,
            [HarmonyArgument(2)] bool tie)
        {
            // __instance.exiledPlayer = __instance.wasTie ? null : __instance.exiledPlayer;
            var exiledString = exiled == null ? "null" : exiled.PlayerName;
            PluginSingleton < TownOfUsFusion >.Instance.Log.LogMessage($"Exiled PlayerName = {exiledString}");
            PluginSingleton < TownOfUsFusion >.Instance.Log.LogMessage($"Was a tie = {tie}");
        }
    }

    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.PopulateResults))]
    public static class PopulateResults
    {
        public static bool Prefix(MeetingHud __instance,
            [HarmonyArgument(0)] Il2CppStructArray<MeetingHud.VoterState> states)
        {
            var allNums = new Dictionary<int, int>();

            __instance.TitleText.text = Object.FindObjectOfType<TranslationController>()
                .GetString(StringNames.MeetingVotingResults, Array.Empty<Il2CppSystem.Object>());
            var amountOfSkippedVoters = 0;

            var isProsecuting = false;
            foreach (var pros in Role.GetRoles(RoleEnum.Prosecutor))
            {
                var prosRole = (Prosecutor)pros;
                if (pros.Player.Data.IsDead || pros.Player.Data.Disconnected) continue;
                if (prosRole.ProsecuteThisMeeting)
                {
                    isProsecuting = true;
                }
            }

            for (var i = 0; i < __instance.playerStates.Length; i++)
            {
                var playerVoteArea = __instance.playerStates[i];
                playerVoteArea.ClearForResults();
                allNums.Add(i, 0);

                for (var stateIdx = 0; stateIdx < states.Length; stateIdx++)
                {
                    var voteState = states[stateIdx];
                    var playerInfo = GameData.Instance.GetPlayerById(voteState.VoterId);
                    foreach (var pros in Role.GetRoles(RoleEnum.Prosecutor))
                    {
                        var prosRole = (Prosecutor)pros;

                        if (pros.Player.Data.IsDead || pros.Player.Data.Disconnected) continue;
                        if (prosRole.ProsecuteThisMeeting)
                        {
                            if (voteState.VoterId == prosRole.Player.PlayerId)
                            {
                                if (playerInfo == null)
                                {
                                    Debug.LogError(string.Format("Couldn't find player info for voter: {0}",
                                        voteState.VoterId));
                                    prosRole.Prosecuted = true;
                                }
                                else if (i == 0 && voteState.SkippedVote)
                                {
                                    __instance.BloopAVoteIcon(playerInfo, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                                    __instance.BloopAVoteIcon(playerInfo, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                                    __instance.BloopAVoteIcon(playerInfo, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                                    __instance.BloopAVoteIcon(playerInfo, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                                    __instance.BloopAVoteIcon(playerInfo, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                                    amountOfSkippedVoters += 5;
                                    prosRole.Prosecuted = true;
                                }
                                else if (voteState.VotedForId == playerVoteArea.TargetPlayerId)
                                {
                                    __instance.BloopAVoteIcon(playerInfo, allNums[i], playerVoteArea.transform);
                                    __instance.BloopAVoteIcon(playerInfo, allNums[i], playerVoteArea.transform);
                                    __instance.BloopAVoteIcon(playerInfo, allNums[i], playerVoteArea.transform);
                                    __instance.BloopAVoteIcon(playerInfo, allNums[i], playerVoteArea.transform);
                                    __instance.BloopAVoteIcon(playerInfo, allNums[i], playerVoteArea.transform);
                                    allNums[i] += 5;
                                    prosRole.Prosecuted = true;
                                }
                            }
                        }
                    }

                    if (isProsecuting) continue;

                    if (playerInfo == null)
                    {
                        Debug.LogError(string.Format("Couldn't find player info for voter: {0}",
                            voteState.VoterId));
                    }
                    else if (i == 0 && voteState.SkippedVote)
                    {
                        __instance.BloopAVoteIcon(playerInfo, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                        amountOfSkippedVoters++;
                    }
                    else if (voteState.VotedForId == playerVoteArea.TargetPlayerId)
                    {
                        __instance.BloopAVoteIcon(playerInfo, allNums[i], playerVoteArea.transform);
                        allNums[i]++;
                    }
                    foreach (var mayor in Role.GetRoles(RoleEnum.Mayor))
                    {
                        var mayorRole = (Mayor)mayor;
                        if (mayorRole.Revealed)
                        {
                            if (voteState.VoterId == mayorRole.Player.PlayerId)
                            {
                                if (playerInfo == null)
                                {
                                    Debug.LogError(string.Format("Couldn't find player info for voter: {0}",
                                        voteState.VoterId));
                                }
                                else if (i == 0 && voteState.SkippedVote)
                                {
                                    __instance.BloopAVoteIcon(playerInfo, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                                    __instance.BloopAVoteIcon(playerInfo, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                                    amountOfSkippedVoters++;
                                    amountOfSkippedVoters++;
                                }
                                else if (voteState.VotedForId == playerVoteArea.TargetPlayerId)
                                {
                                    __instance.BloopAVoteIcon(playerInfo, allNums[i], playerVoteArea.transform);
                                    __instance.BloopAVoteIcon(playerInfo, allNums[i], playerVoteArea.transform);
                                    allNums[i]++;
                                    allNums[i]++;
                                }
                            }
                        }
                    }
                    
                foreach (var role in Role.GetRoles(RoleEnum.Tyrant))
                {
                    var mayor = (Tyrant)role;
                    var playerInfo2 = GameData.Instance.GetPlayerById(role.Player.PlayerId);

                    var anonVotesOption = GameOptionsManager.Instance.currentNormalGameOptions.AnonymousVotes;
                    GameOptionsManager.Instance.currentNormalGameOptions.AnonymousVotes = true;

                    foreach (var extraVote in mayor.ExtraVotes)
                    {
                        if (extraVote == PlayerVoteArea.HasNotVoted ||
                            extraVote == PlayerVoteArea.MissedVote ||
                            extraVote == PlayerVoteArea.DeadVote)
                        {
                            continue;
                        }
                        if (extraVote == PlayerVoteArea.SkippedVote)
                        {

                            __instance.BloopAVoteIcon(playerInfo2, amountOfSkippedVoters, __instance.SkippedVoting.transform);
                            amountOfSkippedVoters++;
                        }
                        else if (voteState.VotedForId == playerVoteArea.TargetPlayerId)
                        {
                            __instance.BloopAVoteIcon(playerInfo2, allNums[i], playerVoteArea.transform);
                            allNums[i]++;
                        }
                        
                        else
                        {
                            for (var i2 = 0; i2 < __instance.playerStates.Length; i2++)
                            {
                                var area = __instance.playerStates[i2];
                                if (extraVote != area.TargetPlayerId) continue;
                                __instance.BloopAVoteIcon(playerInfo2, allNums[i2], area.transform);
                                allNums[i2]++;
                            }
                        }
                    }

                    GameOptionsManager.Instance.currentNormalGameOptions.AnonymousVotes = anonVotesOption;
                }
                }
            }
            return false;
        }
    }
}
}