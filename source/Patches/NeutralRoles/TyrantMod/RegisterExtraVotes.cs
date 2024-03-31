using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.CrewmateRoles.MayorMod;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.NeutralRoles.TyrantMod
{
    [HarmonyPatch(typeof(MeetingHud))]
public class RegisterExtraVotes
{
    public static Dictionary<byte, int> CalculateAllVotes(MeetingHud __instance)
    {
        var dictionary = new Dictionary<byte, int>();


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
}
}