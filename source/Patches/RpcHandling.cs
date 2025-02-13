using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Hazel;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using Reactor.Networking.Extensions;
using TownOfUsFusion.CrewmateRoles.AltruistMod;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.CrewmateRoles.SwapperMod;
using TownOfUsFusion.CrewmateRoles.VigilanteMod;
using TownOfUsFusion.NeutralRoles.DoomsayerMod;
using TownOfUsFusion.CultistRoles.NecromancerMod;
using TownOfUsFusion.CustomOption;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Modifiers.AssassinMod;
using TownOfUsFusion.NeutralRoles.ExecutionerMod;
using TownOfUsFusion.NeutralRoles.GuardianAngelMod;
using TownOfUsFusion.ImpostorRoles.MinerMod;
using TownOfUsFusion.CrewmateRoles.HaunterMod;
using TownOfUsFusion.NeutralRoles.PhantomMod;
using TownOfUsFusion.ImpostorRoles.TraitorMod;
using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Cultist;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;
using Coroutine = TownOfUsFusion.ImpostorRoles.JanitorMod.Coroutine;
using EatCoroutine = TownOfUsFusion.NeutralRoles.CannibalMod.EatCoroutine;
using Object = UnityEngine.Object;
using PerformKillButton = TownOfUsFusion.NeutralRoles.AmnesiacMod.PerformKillButton;
using MeetingSwap = TownOfUsFusion.NeutralRoles.CursedSoulMod.MeetingSwap;
using Random = UnityEngine.Random;
using TownOfUsFusion.Patches;
using AmongUs.GameOptions;
using TownOfUsFusion.NeutralRoles.VampireMod;
using TownOfUsFusion.CrewmateRoles.MayorMod;
using TownOfUsFusion.NeutralRoles.TyrantMod;
using TownOfUsFusion.NeutralRoles.JokerMod;
using System.Reflection;
using TownOfUsFusion.Patches.NeutralRoles;
using BepInEx.Logging;
using TownOfUsFusion.NeutralRoles.NeoNecromancerMod;
using TownOfUsFusion.NeutralRoles.ApparitionistMod;
using TownOfUsFusion.Roles.Alliances;
using TownOfUsFusion.Roles.Tags;
using TownOfUsFusion.Roles.Apocalypse;
using TownOfUsFusion.NeutralRoles.HuskMod;
using TownOfUsFusion.CrewmateRoles.BodyguardMod;
using TownOfUsFusion.NeutralRoles.InquisitorMod;

namespace TownOfUsFusion
{
    public static class RpcHandling
    {
        private static readonly List<(Type, int, bool)> CrewmateRoles = new();
        private static readonly List<(Type, int, bool)> NeutralBenignRoles = new();
        private static readonly List<(Type, int, bool)> NeutralEvilRoles = new();
        private static readonly List<(Type, int, bool)> NeutralChaosRoles = new();
        private static readonly List<(Type, int, bool)> NeutralKillingRoles = new();
        private static readonly List<(Type, int, bool)> NeutralNeophyteRoles = new();
        private static readonly List<(Type, int, bool)> NeutralApocalypseRoles = new();
        private static readonly List<(Type, int, bool)> ImpostorRoles = new();
        private static readonly List<(Type, int)> CrewmateModifiers = new();
        private static readonly List<(Type, int)> GlobalModifiers = new();
        private static readonly List<(Type, int)> ImpostorModifiers = new();
        private static readonly List<(Type, int)> ButtonModifiers = new();
        private static readonly List<(Type, int)> AssassinModifiers = new();
        private static readonly List<(Type, CustomRPC, int)> AssassinAbility = new();
        private static readonly List<(Type, int)> CrewmateAlliances = new();
        private static readonly List<(Type, int)> GlobalAlliances = new();
        private static readonly List<(Type, int)> GlobalTags = new();
        private static bool PhantomOn;
        private static bool HaunterOn;
        private static bool TraitorOn;

        internal static bool Check(int probability)
        {
            if (probability == 0) return false;
            if (probability == 100) return true;
            var num = Random.RandomRangeInt(1, 101);
            return num <= probability;
        }
        private static int PickRoleCount(int min, int max)
        {
            if (min > max) min = max;
            return Random.RandomRangeInt(min, max + 1);
        }

        private static void SortRoles(this List<(Type, int, bool)> roles, int max)
        {
            if (max <= 0)
            {
                roles.Clear();
                return;
            }

            var chosenRoles = roles.Where(x => x.Item2 == 100).ToList();
            // Shuffle to ensure that the same 100% roles do not appear in
            // every game if there are more than the maximum.
            chosenRoles.Shuffle();
            // Truncate the list if there are more 100% roles than the max.
            chosenRoles = chosenRoles.GetRange(0, Math.Min(max, chosenRoles.Count));

            if (chosenRoles.Count < max)
            {
                // These roles MAY appear in this game, but they may not.
                var potentialRoles = roles.Where(x => x.Item2 < 100).ToList();
                // Determine which roles appear in this game.
                var optionalRoles = potentialRoles.Where(x => Check(x.Item2)).ToList();
                potentialRoles = potentialRoles.Where(x => !optionalRoles.Contains(x)).ToList();

                optionalRoles.Shuffle();
                chosenRoles.AddRange(optionalRoles.GetRange(0, Math.Min(max - chosenRoles.Count, optionalRoles.Count)));

                // If there are not enough roles after that, randomly add
                // ones which were previously eliminated, up to the max.
                if (chosenRoles.Count < max)
                {
                    potentialRoles.Shuffle();
                    chosenRoles.AddRange(potentialRoles.GetRange(0, Math.Min(max - chosenRoles.Count, potentialRoles.Count)));
                }
            }

            // This list will be shuffled later in GenEachRole.
            roles.Clear();
            roles.AddRange(chosenRoles);
        }

        private static void SortModifiers(this List<(Type, int)> roles, int max)
        {
            var newList = roles.Where(x => x.Item2 == 100).ToList();
            newList.Shuffle();

            if (roles.Count < max)
                max = roles.Count;

            var roles2 = roles.Where(x => x.Item2 < 100).ToList();
            roles2.Shuffle();
            newList.AddRange(roles2.Where(x => Check(x.Item2)));

            while (newList.Count > max)
            {
                newList.Shuffle();
                newList.RemoveAt(newList.Count - 1);
            }

            roles = newList;
            roles.Shuffle();
        }

        private static void SortAlliances(this List<(Type, int)> roles, int max)
        {
            var newList = roles.Where(x => x.Item2 == 100).ToList();
            newList.Shuffle();

            if (roles.Count < max)
                max = roles.Count;

            var roles2 = roles.Where(x => x.Item2 < 100).ToList();
            roles2.Shuffle();
            newList.AddRange(roles2.Where(x => Check(x.Item2)));

            while (newList.Count > max)
            {
                newList.Shuffle();
                newList.RemoveAt(newList.Count - 1);
            }

            roles = newList;
            roles.Shuffle();
        }

        private static void SortTags(this List<(Type, int)> roles, int max)
        {
            var newList = roles.Where(x => x.Item2 == 100).ToList();
            newList.Shuffle();

            if (roles.Count < max)
                max = roles.Count;

            var roles2 = roles.Where(x => x.Item2 < 100).ToList();
            roles2.Shuffle();
            newList.AddRange(roles2.Where(x => Check(x.Item2)));

            while (newList.Count > max)
            {
                newList.Shuffle();
                newList.RemoveAt(newList.Count - 1);
            }

            roles = newList;
            roles.Shuffle();
        }
        private static void GenEachRole(List<GameData.PlayerInfo> infected)
        {
            var impostors = Utils.GetImpostors(infected);
            var crewmates = Utils.GetCrewmates(impostors);
            // I do not shuffle impostors/crewmates because roles should be shuffled before they are assigned to them anyway.
            // Assigning shuffled roles across a shuffled list may mess with the statistics? I dunno, I didn't major in math.
            // One Fisher-Yates shuffle should have statistically equal permutation probability on its own, anyway.

            var crewRoles = new List<(Type, int, bool)>();
            var neutRoles = new List<(Type, int, bool)>();
            var impRoles = new List<(Type, int, bool)>();

            if (CustomGameOptions.GameMode == GameMode.Classic)
            {
                var benign = PickRoleCount(CustomGameOptions.MinNeutralBenignRoles, Math.Min(CustomGameOptions.MaxNeutralBenignRoles, NeutralBenignRoles.Count));
                var evil = PickRoleCount(CustomGameOptions.MinNeutralEvilRoles, Math.Min(CustomGameOptions.MaxNeutralEvilRoles, NeutralEvilRoles.Count));
                var chaos = PickRoleCount(CustomGameOptions.MinNeutralChaosRoles, Math.Min(CustomGameOptions.MaxNeutralChaosRoles, NeutralChaosRoles.Count));
                var killing = PickRoleCount(CustomGameOptions.MinNeutralKillingRoles, Math.Min(CustomGameOptions.MaxNeutralKillingRoles, NeutralKillingRoles.Count));
                var neophyte = PickRoleCount(CustomGameOptions.MinNeutralNeophyteRoles, Math.Min(CustomGameOptions.MaxNeutralNeophyteRoles, NeutralNeophyteRoles.Count));
                var apocalypse = PickRoleCount(CustomGameOptions.MinNeutralApocalypseRoles, Math.Min(CustomGameOptions.MaxNeutralApocalypseRoles, NeutralApocalypseRoles.Count));

                var canSubtract = (int faction, int minFaction) => { return faction > minFaction; };
                var factions = new List<string>() { "Benign", "Evil", "Chaos", "Killing", "Neophyte", "Apocalypse" };

                // Crew must always start out outnumbering neutrals, so subtract roles until that can be guaranteed.
                while (Math.Ceiling((double)crewmates.Count/2) <= benign + evil + killing)
                {
                    bool canSubtractBenign = canSubtract(benign, CustomGameOptions.MinNeutralBenignRoles);
                    bool canSubtractEvil = canSubtract(evil, CustomGameOptions.MinNeutralEvilRoles);
                    bool canSubtractChaos = canSubtract(chaos, CustomGameOptions.MinNeutralChaosRoles);
                    bool canSubtractKilling = canSubtract(killing, CustomGameOptions.MinNeutralKillingRoles);
                    bool canSubtractNeophyte = canSubtract(neophyte, CustomGameOptions.MinNeutralNeophyteRoles);
                    bool canSubtractApocalypse = canSubtract(apocalypse, CustomGameOptions.MinNeutralApocalypseRoles);
                    bool canSubtractNone = !canSubtractBenign && !canSubtractEvil && !canSubtractChaos && !canSubtractKilling && !canSubtractNeophyte && !canSubtractApocalypse;

                    factions.Shuffle();
                    switch(factions.First())
                    {
                        case "Benign":
                            if (benign > 0 && (canSubtractBenign || canSubtractNone))
                            {
                                benign -= 1;
                                break;
                            }
                            goto case "Evil";
                        case "Evil":
                            if (evil > 0 && (canSubtractEvil || canSubtractNone))
                            {
                                evil -= 1;
                                break;
                            }
                            goto case "Chaos";
                        case "Chaos":
                            if (chaos > 0 && (canSubtractChaos || canSubtractNone))
                            {
                                chaos -= 1;
                                break;
                            }
                            goto case "Killing";
                        case "Killing":
                            if (killing > 0 && (canSubtractKilling || canSubtractNone))
                            {
                                killing -= 1;
                                break;
                            }
                            goto case "Neophyte";
                        case "Neophyte":
                            if (neophyte > 0 && (canSubtractNeophyte || canSubtractNone))
                            {
                                neophyte -= 1;
                                break;
                            }
                            goto case "Apocalypse";
                        case "Apocalypse":
                            if (apocalypse > 0 && (canSubtractApocalypse || canSubtractNone))
                            {
                                apocalypse -= 1;
                                break;
                            }
                            goto default;
                        default:
                            if (benign > 0)
                            {
                                benign -= 1;
                            }
                            else if (evil > 0)
                            {
                                evil -= 1;
                            }
                            else if (chaos > 0)
                            {
                                chaos -= 1;
                            }
                            else if (killing > 0)
                            {
                                killing -= 1;
                            }
                            else if (neophyte > 0)
                            {
                                neophyte -= 1;
                            }
                            else if (apocalypse > 0)
                            {
                                apocalypse -= 1;
                            }
                            break;
                    }

                    if (benign + evil + chaos + killing + neophyte + apocalypse == 0)
                        break;
                }

                NeutralBenignRoles.SortRoles(benign);
                NeutralEvilRoles.SortRoles(evil);
                NeutralChaosRoles.SortRoles(chaos);
                NeutralKillingRoles.SortRoles(killing);
                NeutralNeophyteRoles.SortRoles(neophyte);
                NeutralApocalypseRoles.SortRoles(apocalypse);


                if (NeutralKillingRoles.Contains((typeof(Vampire), CustomGameOptions.VampireOn, true)) && CustomGameOptions.VampireHunterOn > 0)
                    CrewmateRoles.Add((typeof(VampireHunter), CustomGameOptions.VampireHunterOn, true));


                CrewmateRoles.SortRoles(crewmates.Count - NeutralBenignRoles.Count - NeutralEvilRoles.Count - NeutralChaosRoles.Count - NeutralKillingRoles.Count - NeutralNeophyteRoles.Count - NeutralApocalypseRoles.Count);
                ImpostorRoles.SortRoles(impostors.Count);

                crewRoles.AddRange(CrewmateRoles);
                impRoles.AddRange(ImpostorRoles);
            }
            neutRoles.AddRange(NeutralBenignRoles);
            neutRoles.AddRange(NeutralEvilRoles);
            neutRoles.AddRange(NeutralChaosRoles);
            neutRoles.AddRange(NeutralKillingRoles);
            neutRoles.AddRange(NeutralNeophyteRoles);
            neutRoles.AddRange(NeutralApocalypseRoles);
            // Roles are not, at this point, shuffled yet.

            // In All/Any mode, there is at least one neutral and one crewmate, but duplicates are allowed and probability is ignored.
            if (CustomGameOptions.GameMode == GameMode.AllAny)
            {
                // Add one neutral role to the game, if any are enabled.
                // This guarantees at least one neutral role's presence.
                if (neutRoles.Count > 0)
                {
                    neutRoles.Shuffle();
                    crewRoles.Add(neutRoles[0]);
                    // If it's unique, remove it from the list.
                    if (neutRoles[0].Item3 == true) neutRoles.Remove(neutRoles[0]);
                }
                // Add one crewmate role to the game, or vanilla Crewmate if none are enabled.
                // This guarantees at least one crewmate role's presence.
                if (CrewmateRoles.Count > 0)
                {
                    CrewmateRoles.Shuffle();
                    crewRoles.Add(CrewmateRoles[0]);
                    if (CrewmateRoles[0].Item3 == true) CrewmateRoles.Remove(CrewmateRoles[0]);
                }
                else
                {
                    crewRoles.Add((typeof(Crewmate), 100, false));
                }
                // Now add all the roles together.
                var allAnyRoles = new List<(Type, int, bool)>();
                allAnyRoles.AddRange(CrewmateRoles);
                allAnyRoles.AddRange(neutRoles);
                allAnyRoles.Shuffle();
                // Add crew & neutral roles up to the crewmate count, including duplicates (unless defined as unique).
                while (crewRoles.Count < crewmates.Count && allAnyRoles.Count > 0)
                {
                    crewRoles.Add(allAnyRoles[0]);
                    if (allAnyRoles[0].Item3 == true) allAnyRoles.Remove(allAnyRoles[0]);
                }
                // Add impostor roles up to the impostor count, including duplicates (unless defined as unique).
                ImpostorRoles.Shuffle();
                while (impRoles.Count < impostors.Count && ImpostorRoles.Count > 0)
                {
                    impRoles.Add(ImpostorRoles[0]);
                    if (ImpostorRoles[0].Item3 == true) ImpostorRoles.Remove(ImpostorRoles[0]);
                }
            }
            else
            {
                // Roles have already been sorted for Classic mode.
                // So just add in the neutral roles.
                crewRoles.AddRange(neutRoles);
            }

            // Shuffle roles before handing them out.
            // This should ensure a statistically equal chance of all permutations of roles.
            crewRoles.Shuffle();
            impRoles.Shuffle();

            // Hand out appropriate roles to crewmates and impostors.
            foreach (var (type, _, unique) in crewRoles)
            {
                Role.GenRole<Role>(type, crewmates);
            }
            foreach (var (type, _, unique) in impRoles)
            {
                Role.GenRole<Role>(type, impostors);
            }

            // Assign vanilla roles to anyone who did not receive a role.
            foreach (var crewmate in crewmates)
                Role.GenRole<Role>(typeof(Crewmate), crewmate);

            foreach (var impostor in impostors)
                Role.GenRole<Role>(typeof(Impostor), impostor);

            // Hand out assassin ability to killers according to the settings.
            var canHaveAbility = PlayerControl.AllPlayerControls.ToArray().Where(player => player.Is(Faction.Impostors) || player.Is(Faction.ImpSentinel)).ToList();
            canHaveAbility.Shuffle();
            var canHaveAbility2 = PlayerControl.AllPlayerControls.ToArray().Where(player => player.Is(Faction.NeutralKilling) || player.Is(Faction.NeutralNeophyte) ||
            player.Is(Faction.NeutralNecro) || player.Is(Faction.NeutralApocalypse) || player.Is(Faction.NeutralSentinel) || player.Is(Faction.CrewSentinel)).ToList();
            canHaveAbility2.Shuffle();

            var assassinConfig = new (List<PlayerControl>, int)[]
            {
                (canHaveAbility, CustomGameOptions.NumberOfImpostorAssassins),
                (canHaveAbility2, CustomGameOptions.NumberOfNeutralAssassins)
            };
            foreach ((var abilityList, int maxNumber) in assassinConfig)
            {
                int assassinNumber = maxNumber;
                while (abilityList.Count > 0 && assassinNumber > 0)
                {
                    var (type, rpc, _) = AssassinAbility.Ability();
                    Role.Gen<Ability>(type, abilityList.TakeFirst(), rpc);
                    assassinNumber -= 1;
                }
            }

            // Hand out assassin modifiers, if enabled, to impostor assassins.
            var canHaveAssassinModifier = PlayerControl.AllPlayerControls.ToArray().Where(player => player.Is(Faction.Impostors) && player.Is(AbilityEnum.Assassin)).ToList();
            canHaveAssassinModifier.Shuffle();
            AssassinModifiers.SortModifiers(canHaveAssassinModifier.Count);
            AssassinModifiers.Shuffle();

            foreach (var (type, _) in AssassinModifiers)
            {
                if (canHaveAssassinModifier.Count == 0) break;
                Role.GenModifier<Modifier>(type, canHaveAssassinModifier);
            }

            // Hand out impostor modifiers.
            var canHaveImpModifier = PlayerControl.AllPlayerControls.ToArray().Where(player => player.Is(Faction.Impostors) && !player.Is(ModifierEnum.DoubleShot)).ToList();
            canHaveImpModifier.Shuffle();
            ImpostorModifiers.SortModifiers(canHaveImpModifier.Count);
            ImpostorModifiers.Shuffle();

            foreach (var (type, _) in ImpostorModifiers)
            {
                if (canHaveImpModifier.Count == 0) break;
                Role.GenModifier<Modifier>(type, canHaveImpModifier);
            }

            // Hand out global modifiers.
            var canHaveModifier = PlayerControl.AllPlayerControls.ToArray()
                .Where(player => !player.Is(ModifierEnum.Disperser) && !player.Is(ModifierEnum.DoubleShot) && !player.Is(ModifierEnum.Underdog))
                .ToList();
            canHaveModifier.Shuffle();
            GlobalModifiers.SortModifiers(canHaveModifier.Count);
            GlobalModifiers.Shuffle();

            foreach (var (type, id) in GlobalModifiers)
            {
                if (canHaveModifier.Count == 0) break;
                Role.GenModifier<Modifier>(type, canHaveModifier);
            }

            // The Glitch cannot have Button Modifiers.
            canHaveModifier.RemoveAll(player => player.Is(RoleEnum.Glitch));
            // If The Sentinel can vent, they have their place button, which cannot allow The Sentinel to have Button Modifiers
            if (CustomGameOptions.SentinelVent) canHaveModifier.RemoveAll(player => player.Is(RoleEnum.Sentinel));
            ButtonModifiers.SortModifiers(canHaveModifier.Count);

            foreach (var (type, id) in ButtonModifiers)
            {
                if (canHaveModifier.Count == 0) break;
                Role.GenModifier<Modifier>(type, canHaveModifier);
            }

            // Now hand out Crewmate Modifiers to all remaining eligible players.
            canHaveModifier.RemoveAll(player => !player.Is(Faction.Crewmates));
            CrewmateModifiers.SortModifiers(canHaveModifier.Count);
            CrewmateModifiers.Shuffle();

            while (canHaveModifier.Count > 0 && CrewmateModifiers.Count > 0)
            {
                var (type, _) = CrewmateModifiers.TakeFirst();
                Role.GenModifier<Modifier>(type, canHaveModifier.TakeFirst());
            }

            // ALLIANCE STUFF

            // Hand out global alliances
            var canHaveAlliance = PlayerControl.AllPlayerControls.ToArray().ToList();
            var jackalList = PlayerControl.AllPlayerControls.ToArray().ToList();
            jackalList.RemoveAll(player => !player.Is(RoleEnum.Jackal));
            canHaveAlliance.RemoveAll(player => player.Is(RoleEnum.Inquisitor));
            canHaveAlliance.Shuffle();
            GlobalAlliances.SortAlliances(canHaveAlliance.Count);
            GlobalAlliances.Shuffle();

            foreach (var (type, id) in GlobalAlliances)
            {
                if (canHaveAlliance.Count == 0) break;
                if (type.FullName.Contains("Recruit") && jackalList.Count != 0)
                {
                    if (canHaveAlliance.Count == 1) continue;
                    Recruit.Gen(canHaveAlliance);
                }
                else if (type.FullName.Contains("Lover"))
                {
                    if (canHaveAlliance.Count == 1) continue;
                    Lover.Gen(canHaveAlliance);
                }
                else if (!type.FullName.Contains("Recruit"))/*if(!type.FullName.Contains("Crewpocalypse") || !type.FullName.Contains("Crewpostor"))*/
                {
                    Role.GenAlliance<Alliance>(type, canHaveAlliance);
                }
                if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage($"{type} was spawned");
            }

            // Hand out crewmate alliances
            canHaveAlliance.RemoveAll(player => !player.Is(Faction.Crewmates));
            canHaveAlliance.Shuffle();
            CrewmateAlliances.SortAlliances(canHaveAlliance.Count);
            CrewmateAlliances.Shuffle();
            var impList = PlayerControl.AllPlayerControls.ToArray().ToList();
            var apocList = PlayerControl.AllPlayerControls.ToArray().ToList();
            impList.RemoveAll(player => !player.Is(Faction.Impostors));
            apocList.RemoveAll(player => !player.Is(Faction.NeutralApocalypse));

            while (canHaveAlliance.Count > 0 && CrewmateAlliances.Count > 0)
            {
                var (type, _) = CrewmateAlliances.TakeFirst();
                if((type.FullName.Contains("Crewpocalypse") && apocList.Count != 0) || (type.FullName.Contains("Crewpostor") && impList.Count != 0))
                {
                Role.GenAlliance<Alliance>(type, canHaveAlliance.TakeFirst());
                } else if (!type.FullName.Contains("Crewpocalypse") && !type.FullName.Contains("Crewpostor"))
                Role.GenAlliance<Alliance>(type, canHaveAlliance.TakeFirst());
            }

            // TAG STUFF
            var canHaveTag = PlayerControl.AllPlayerControls.ToArray().ToList();
            canHaveTag.Shuffle();
            foreach (var (type, id) in GlobalTags)
            {
                if (canHaveTag.Count == 0) break;
                if (type.FullName.Contains("Heretic"))
                {
                    if (canHaveTag.Count == 1) continue;
                    Heretic.Gen(canHaveTag);
                }
                else
                {
                    Role.GenTag<Tag>(type, canHaveTag);
                }
            }
            
            var trueSentList = PlayerControl.AllPlayerControls.ToArray().ToList();
            foreach (var role in Role.GetRoles(RoleEnum.Sentinel))
            {
                var sentinel = (Sentinel)role;
                    if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("SENTINEL CODE IS BEING LOADED FROM RPC HANDLING");
                Sentinel.Gen(trueSentList);
            }
            // Set the Traitor, if there is one enabled.
            var toChooseFromCrew = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Crewmates) && !x.Is(RoleEnum.Mayor) && !x.Is(AllianceEnum.Lover)  && !x.Is(AllianceEnum.Recruit) && !x.Is(AllianceEnum.Crewpostor) && !x.Is(AllianceEnum.Crewpocalypse)).ToList();
            if (TraitorOn && toChooseFromCrew.Count != 0)
            {
                var rand = Random.RandomRangeInt(0, toChooseFromCrew.Count);
                var pc = toChooseFromCrew[rand];

                SetTraitor.WillBeTraitor = pc;

                Utils.Rpc(CustomRPC.SetTraitor, pc.PlayerId);
            }
            else
            {
                Utils.Rpc(CustomRPC.SetTraitor, byte.MaxValue);
            }
            toChooseFromCrew.RemoveAll(player => SetTraitor.WillBeTraitor == player);

            // Set the Haunter, if there is one enabled.
            if (HaunterOn && toChooseFromCrew.Count != 0)
            {
                var rand = Random.RandomRangeInt(0, toChooseFromCrew.Count);
                var pc = toChooseFromCrew[rand];

                SetHaunter.WillBeHaunter = pc;

                Utils.Rpc(CustomRPC.SetHaunter, pc.PlayerId);
            }
            else
            {
                Utils.Rpc(CustomRPC.SetHaunter, byte.MaxValue);
            }

            var toChooseFromNeut = PlayerControl.AllPlayerControls.ToArray().Where(x => (x.Is(Faction.NeutralBenign) || x.Is(Faction.NeutralEvil) || x.Is(Faction.NeutralChaos) || x.Is(Faction.NeutralKilling)
            || x.Is(Faction.NeutralSentinel) || x.Is(Faction.ChaosSentinel) || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro)) && !x.Is(AllianceEnum.Lover) && !x.Is(AllianceEnum.Recruit)).ToList();
            if (PhantomOn && toChooseFromNeut.Count != 0)
            {
                var rand = Random.RandomRangeInt(0, toChooseFromNeut.Count);
                var pc = toChooseFromNeut[rand];

                SetPhantom.WillBePhantom = pc;

                Utils.Rpc(CustomRPC.SetPhantom, pc.PlayerId);
            }
            else
            {
                Utils.Rpc(CustomRPC.SetPhantom, byte.MaxValue);
            }

            var exeTargets = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Crewmates) && !x.Is(AllianceEnum.Lover) && !x.Is(AllianceEnum.Recruit) && !x.Is(RoleEnum.Mayor) && !x.Is(RoleEnum.Swapper) && !x.Is(RoleEnum.Vigilante) && x != SetTraitor.WillBeTraitor && !x.Is(AllianceEnum.Crewpocalypse) && !x.Is(AllianceEnum.Crewpostor)).ToList();
            foreach (var role in Role.GetRoles(RoleEnum.Executioner))
            {
                var exe = (Executioner)role;
                if (exeTargets.Count > 0)
                {
                    exe.target = exeTargets[Random.RandomRangeInt(0, exeTargets.Count)];
                    exeTargets.Remove(exe.target);

                    Utils.Rpc(CustomRPC.SetTarget, role.Player.PlayerId, exe.target.PlayerId);
                }
            }

            var goodGATargets = PlayerControl.AllPlayerControls.ToArray().Where(x => (x.Is(Faction.Crewmates) || x.Is(Faction.CrewSentinel)) && !x.Is(AllianceEnum.Lover) && !x.Is(AllianceEnum.Crewpostor) && !x.Is(AllianceEnum.Crewpocalypse) && !x.Is(AllianceEnum.Recruit)).ToList();
            var evilGATargets = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Impostors) || x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralNeophyte) || x.Is(Faction.NeutralNecro) || x.Is(Faction.NeutralApocalypse)
            || x.Is(Faction.ChaosSentinel) || x.Is(Faction.NeutralSentinel) || x.Is(Faction.ImpSentinel) || x.Is(AllianceEnum.Lover) || x.Is(AllianceEnum.Recruit) || x.Is(AllianceEnum.Crewpocalypse) || x.Is(AllianceEnum.Crewpostor)).ToList();
            foreach (var role in Role.GetRoles(RoleEnum.GuardianAngel))
            {
                var ga = (GuardianAngel)role;
                if (!(goodGATargets.Count == 0 && CustomGameOptions.EvilTargetPercent == 0) ||
                    (evilGATargets.Count == 0 && CustomGameOptions.EvilTargetPercent == 100) ||
                    goodGATargets.Count == 0 && evilGATargets.Count == 0)
                {
                    if (goodGATargets.Count == 0)
                    {
                        ga.target = evilGATargets[Random.RandomRangeInt(0, evilGATargets.Count)];
                        evilGATargets.Remove(ga.target);
                    }
                    else if (evilGATargets.Count == 0 || !Check(CustomGameOptions.EvilTargetPercent))
                    {
                        ga.target = goodGATargets[Random.RandomRangeInt(0, goodGATargets.Count)];
                        goodGATargets.Remove(ga.target);
                    }
                    else
                    {
                        ga.target = evilGATargets[Random.RandomRangeInt(0, evilGATargets.Count)];
                        evilGATargets.Remove(ga.target);
                    }

                    Utils.Rpc(CustomRPC.SetGATarget, role.Player.PlayerId, ga.target.PlayerId);
                }
            }
/*
        foreach (var playerControl in PlayerControl.AllPlayerControls)
            {
                if (!playerControl.Is(RoleEnum.Taskmaster)) Utils.RemoveBasicTasks(playerControl);
            }*/
        }
        private static void GenEachRoleKilling(List<GameData.PlayerInfo> infected)
        {
            var impostors = Utils.GetImpostors(infected);
            var crewmates = Utils.GetCrewmates(impostors);
            crewmates.Shuffle();
            impostors.Shuffle();

            ImpostorRoles.Add((typeof(Undertaker), 10, true));
            ImpostorRoles.Add((typeof(Morphling), 10, false));
            ImpostorRoles.Add((typeof(Escapist), 10, false));
            ImpostorRoles.Add((typeof(Miner), 10, true));
            ImpostorRoles.Add((typeof(Swooper), 10, false));
            ImpostorRoles.Add((typeof(Grenadier), 10, true));

            ImpostorRoles.SortRoles(impostors.Count);

            NeutralKillingRoles.Add((typeof(Glitch), 10, true));
            NeutralKillingRoles.Add((typeof(Werewolf), 10, true));
            NeutralKillingRoles.Add((typeof(Berserker), 10, true));
            if (CustomGameOptions.AddArsonist)
                NeutralKillingRoles.Add((typeof(Arsonist), 10, true));
            if (CustomGameOptions.AddPlaguebearer)
                NeutralKillingRoles.Add((typeof(Plaguebearer), 10, true));

            var neutrals = 0;
            if (NeutralKillingRoles.Count < CustomGameOptions.NeutralRoles) neutrals = NeutralKillingRoles.Count;
            else neutrals = CustomGameOptions.NeutralRoles;
            var spareCrew = crewmates.Count - neutrals;
            if (spareCrew > 2) NeutralKillingRoles.SortRoles(neutrals);
            else NeutralKillingRoles.SortRoles(crewmates.Count - 3);

            var veterans = CustomGameOptions.VeteranCount;
            while (veterans > 0)
            {
                CrewmateRoles.Add((typeof(Veteran), 10, false));
                veterans -= 1;
            }
            var vigilantes = CustomGameOptions.VigilanteCount;
            while (vigilantes > 0)
            {
                CrewmateRoles.Add((typeof(Vigilante), 10, false));
                vigilantes -= 1;
            }
            if (CrewmateRoles.Count + NeutralKillingRoles.Count > crewmates.Count)
            {
                CrewmateRoles.SortRoles(crewmates.Count - NeutralKillingRoles.Count);
            }
            else if (CrewmateRoles.Count + NeutralKillingRoles.Count < crewmates.Count)
            {
                var sheriffs = crewmates.Count - NeutralKillingRoles.Count - CrewmateRoles.Count;
                while (sheriffs > 0)
                {
                    CrewmateRoles.Add((typeof(Sheriff), 10, false));
                    sheriffs -= 1;
                }
            }

            var crewAndNeutralRoles = new List<(Type, int, bool)>();
            crewAndNeutralRoles.AddRange(CrewmateRoles);
            crewAndNeutralRoles.AddRange(NeutralKillingRoles);
            crewAndNeutralRoles.Shuffle();
            ImpostorRoles.Shuffle();

            foreach (var (type, _, _) in crewAndNeutralRoles)
            {
                Role.GenRole<Role>(type, crewmates);
            }
            foreach (var (type, _, _) in ImpostorRoles)
            {
                Role.GenRole<Role>(type, impostors);
            }
        }
        private static void GenEachRoleCultist(List<GameData.PlayerInfo> infected)
        {
            var impostors = Utils.GetImpostors(infected);
            var crewmates = Utils.GetCrewmates(impostors);
            crewmates.Shuffle();
            impostors.Shuffle();

            var specialRoles = new List<(Type, int, bool)>();
            var crewRoles = new List<(Type, int, bool)>();
            var impRole = new List<(Type, int, bool)>();
            if (CustomGameOptions.MayorCultistOn > 0) specialRoles.Add((typeof(Mayor), CustomGameOptions.MayorCultistOn, true));
            if (CustomGameOptions.SeerCultistOn > 0) specialRoles.Add((typeof(CultistSeer), CustomGameOptions.SeerCultistOn, true));
            if (CustomGameOptions.SheriffCultistOn > 0) specialRoles.Add((typeof(Sheriff), CustomGameOptions.SheriffCultistOn, true));
            if (CustomGameOptions.SurvivorCultistOn > 0) specialRoles.Add((typeof(Survivor), CustomGameOptions.SurvivorCultistOn, true));
            if (specialRoles.Count > CustomGameOptions.SpecialRoleCount) specialRoles.SortRoles(CustomGameOptions.SpecialRoleCount);
            if (specialRoles.Count > crewmates.Count) specialRoles.SortRoles(crewmates.Count);
            if (specialRoles.Count < crewmates.Count)
            {
                var chameleons = CustomGameOptions.MaxChameleons;
                var engineers = CustomGameOptions.MaxEngineers;
                var investigators = CustomGameOptions.MaxInvestigators;
                var mystics = CustomGameOptions.MaxMystics;
                var snitches = CustomGameOptions.MaxSnitches;
                var spies = CustomGameOptions.MaxSpies;
                var transporters = CustomGameOptions.MaxTransporters;
                var vigilantes = CustomGameOptions.MaxVigilantes;
                while (chameleons > 0)
                {
                    crewRoles.Add((typeof(Chameleon), 10, false));
                    chameleons--;
                }
                while (engineers > 0)
                {
                    crewRoles.Add((typeof(Engineer), 10, false));
                    engineers--;
                }
                while (investigators > 0)
                {
                    crewRoles.Add((typeof(Investigator), 10, false));
                    investigators--;
                }
                while (mystics > 0)
                {
                    crewRoles.Add((typeof(CultistMystic), 10, false));
                    mystics--;
                }
                while (snitches > 0)
                {
                    crewRoles.Add((typeof(CultistSnitch), 10, false));
                    snitches--;
                }
                while (spies > 0)
                {
                    crewRoles.Add((typeof(Spy), 10, false));
                    spies--;
                }
                while (transporters > 0)
                {
                    crewRoles.Add((typeof(Transporter), 10, false));
                    transporters--;
                }
                while (vigilantes > 0)
                {
                    crewRoles.Add((typeof(Vigilante), 10, false));
                    vigilantes--;
                }
                crewRoles.SortRoles(crewmates.Count - specialRoles.Count);
            }
            impRole.Add((typeof(Necromancer), 100, true));
            impRole.Add((typeof(Whisperer), 100, true));
            impRole.SortRoles(1);

            foreach (var (type, _, unique) in specialRoles)
            {
                Role.GenRole<Role>(type, crewmates);
            }
            foreach (var (type, _, unique) in crewRoles)
            {
                Role.GenRole<Role>(type, crewmates);
            }
            foreach (var (type, _, unique) in impRole)
            {
                Role.GenRole<Role>(type, impostors);
            }

            foreach (var crewmate in crewmates)
                Role.GenRole<Role>(typeof(Crewmate), crewmate);
        }


        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
        public static class HandleRpc
        {
            public static void Postfix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
            {
                Assembly asm = typeof(Role).Assembly;

                byte readByte, readByte1, readByte2;
                sbyte readSByte, readSByte2;
                switch ((CustomRPC) callId)
                {
                    case CustomRPC.SetRole:
                        var player = Utils.PlayerById(reader.ReadByte());
                        var rstring = reader.ReadString();
                        Activator.CreateInstance(asm.GetType(rstring), new object[] { player });
                        break;
                    case CustomRPC.SetModifier:
                        var player2 = Utils.PlayerById(reader.ReadByte());
                        var mstring = reader.ReadString();
                        Activator.CreateInstance(asm.GetType(mstring), new object[] { player2 });
                        break;
                    case CustomRPC.SetAlliance:
                        var player42 = Utils.PlayerById(reader.ReadByte());
                        var mastring = reader.ReadString();
                        Activator.CreateInstance(asm.GetType(mastring), new object[] { player42 });
                        break;
                    case CustomRPC.SetTag:
                        var player22 = Utils.PlayerById(reader.ReadByte());
                        var mbstring = reader.ReadString();
                        Activator.CreateInstance(asm.GetType(mbstring), new object[] { player22 });
                        break;

                    case CustomRPC.LoveWin:
                        var winnerlover = Utils.PlayerById(reader.ReadByte());
                        Alliance.GetAlliance<Lover>(winnerlover).Win();
                        break;

                    case CustomRPC.NobodyWins:
                        Role.NobodyWinsFunc();
                        break;

                    case CustomRPC.SurvivorOnlyWin:
                        Role.SurvOnlyWin();
                        break;

                    case CustomRPC.SentinelCrewWin:
                        Role.SentinelCrewWin();
                        break;

                    case CustomRPC.JackalWin:
                        /*var winnerrecruits = Utils.PlayerById(reader.ReadByte());
                        Alliance.GetAlliance<Recruit>(winnerrecruits).Win();*/
                        Role.JackalWin();
                        break;

                    case CustomRPC.CSWin:
                        Role.CSWin();
                        break;

                    case CustomRPC.VampireWin:
                        Role.VampWin();
                        break;

                    case CustomRPC.NecroWin:
                        Role.NecroWin();
                        break;

                    case CustomRPC.ApocWin:
                        Role.ApocWin();
                        break;

                    case CustomRPC.SetRecruits:
                        var id3 = reader.ReadByte();
                        var id4 = reader.ReadByte();
                        var recruit1 = Utils.PlayerById(id3);
                        var recruit2 = Utils.PlayerById(id4);

                        var modifierRecruit1 = new Recruit(recruit1);
                        var modifierRecruit2 = new Recruit(recruit2);

                        modifierRecruit1.OtherRecruit = modifierRecruit2;
                        modifierRecruit2.OtherRecruit = modifierRecruit1;

                        break;
                    case CustomRPC.SetHeretics:
                        var id5 = reader.ReadByte();
                        var id6 = reader.ReadByte();
                        var id7 = reader.ReadByte();
                        var heretic1 = Utils.PlayerById(id5);
                        var heretic2 = Utils.PlayerById(id6);
                        var heretic3 = Utils.PlayerById(id7);

                        var tagHeretic1 = new Heretic(heretic1);
                        var tagHeretic2 = new Heretic(heretic2);
                        var tagHeretic3 = new Heretic(heretic3);

                        tagHeretic1.OtherHeretic = tagHeretic2;
                        tagHeretic1.OtherHeretic2 = tagHeretic3;

                        tagHeretic2.OtherHeretic = tagHeretic1;
                        tagHeretic2.OtherHeretic2 = tagHeretic3;

                        tagHeretic3.OtherHeretic = tagHeretic2;
                        tagHeretic3.OtherHeretic2 = tagHeretic1;
                        break;

                    case CustomRPC.SetCouple:
                        var id = reader.ReadByte();
                        var id2 = reader.ReadByte();
                        var lover1 = Utils.PlayerById(id);
                        var lover2 = Utils.PlayerById(id2);

                        var modifierLover1 = new Lover(lover1);
                        var modifierLover2 = new Lover(lover2);

                        modifierLover1.OtherLover = modifierLover2;
                        modifierLover2.OtherLover = modifierLover1;

                        break;

                    case CustomRPC.Start:
                        readByte = reader.ReadByte();
                        Utils.ShowDeadBodies = false;
                        ShowRoundOneShield.FirstRoundShielded = readByte == byte.MaxValue ? null : Utils.PlayerById(readByte);
                        ShowRoundOneShield.DiedFirst = "";
                        Murder.KilledPlayers.Clear();
                        Role.NobodyWins = false;
                        Role.SurvOnlyWins = false;
                        Role.SentinelCrewWins = false;
                        Role.VampireWins = false;
                        Role.CSWins = false;
                        Role.JackalWins = false;
                        Role.NecroWins = false;
                        Role.ApocWins = false;
                        ExileControllerPatch.lastExiled = null;
                        PatchKillTimer.GameStarted = false;
                        StartImitate.ImitatingPlayer = null;
                        KillButtonTarget.DontRevive = byte.MaxValue;
                        ReviveHudManagerUpdate.DontRevive = byte.MaxValue;
                        AddHauntPatch.AssassinatedPlayers.Clear();
                        HudUpdate.Zooming = false;
                        HudUpdate.ZoomStart();
                        break;

                    case CustomRPC.JanitorClean:
                        readByte1 = reader.ReadByte();
                        var janitorPlayer = Utils.PlayerById(readByte1);
                        var janitorRole = Role.GetRole<Janitor>(janitorPlayer);
                        readByte = reader.ReadByte();
                        var deadBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in deadBodies)
                            if (body.ParentId == readByte)
                                Coroutines.Start(Coroutine.CleanCoroutine(body, janitorRole));

                        break;

                    case CustomRPC.CannibalEat:
                        readByte1 = reader.ReadByte();
                        var cannibalPlayer = Utils.PlayerById(readByte1);
                        var cannibalRole = Role.GetRole<Cannibal>(cannibalPlayer);
                        readByte = reader.ReadByte();
                        var deadCBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in deadCBodies)
                            if (body.ParentId == readByte)
                                Coroutines.Start(EatCoroutine.CannibalCoroutine(body, cannibalRole));
                        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Cannibal needs " + cannibalRole.EatNeed + " more bodies to win!");

                        break;
                    case CustomRPC.CannibalWin:
                        var cannibalWinner = Role.GetRole<Cannibal>(Utils.PlayerById(reader.ReadByte()));
                        cannibalWinner.Wins();
                        break;

                    case CustomRPC.EngineerFix:
                        if (ShipStatus.Instance.Systems.ContainsKey(SystemTypes.MushroomMixupSabotage))
                        {
                            var mushroom = ShipStatus.Instance.Systems[SystemTypes.MushroomMixupSabotage].Cast<MushroomMixupSabotageSystem>();
                            if (mushroom.IsActive) mushroom.currentSecondsUntilHeal = 0.1f;
                        }
                        break;

                    case CustomRPC.FixLights:
                        var lights = ShipStatus.Instance.Systems[SystemTypes.Electrical].Cast<SwitchSystem>();
                        lights.ActualSwitches = lights.ExpectedSwitches;
                        break;

                    case CustomRPC.Reveal:
                        var mayor = Utils.PlayerById(reader.ReadByte());
                        var mayorRole = Role.GetRole<Mayor>(mayor);
                        mayorRole.Revealed = true;
                        AddRevealButton.RemoveAssassin(mayorRole);
                        break;

                    case CustomRPC.Prosecute:
                        var host = reader.ReadBoolean();
                        if (host && AmongUsClient.Instance.AmHost)
                        {
                            var prosecutor = Utils.PlayerById(reader.ReadByte());
                            var prosRole = Role.GetRole<Prosecutor>(prosecutor);
                            prosRole.ProsecuteThisMeeting = true;
                        }
                        else if (!host && !AmongUsClient.Instance.AmHost)
                        {
                            var prosecutor = Utils.PlayerById(reader.ReadByte());
                            var prosRole = Role.GetRole<Prosecutor>(prosecutor);
                            prosRole.ProsecuteThisMeeting = true;
                        }
                        break;

                    case CustomRPC.Bite:
                        var newVamp = Utils.PlayerById(reader.ReadByte());
                        Bite.Convert(newVamp);
                        break;

                    case CustomRPC.SetSwaps:
                        readSByte = reader.ReadSByte();
                        SwapVotes.Swap1 =
                            MeetingHud.Instance.playerStates.FirstOrDefault(x => x.TargetPlayerId == readSByte);
                        readSByte2 = reader.ReadSByte();
                        SwapVotes.Swap2 =
                            MeetingHud.Instance.playerStates.FirstOrDefault(x => x.TargetPlayerId == readSByte2);
                        if (TownOfUsFusion.isDevBuild) PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Bytes received - " + readSByte + " - " +
                                                                          readSByte2);
                        break;

                    case CustomRPC.SoulSwap:
                        readByte1 = reader.ReadByte();
                        readByte2 = reader.ReadByte();
                        var cs = Utils.PlayerById(readByte1);
                        var other3 = Utils.PlayerById(readByte2);
                        MeetingSwap.SoulSwap(Role.GetRole<CursedSoul>(cs), other3);
                        break;

                    case CustomRPC.Imitate:
                        var imitator = Utils.PlayerById(reader.ReadByte());
                        var imitatorRole = Role.GetRole<Imitator>(imitator);
                        var imitateTarget = Utils.PlayerById(reader.ReadByte());
                        imitatorRole.ImitatePlayer = imitateTarget;
                        break;
                    case CustomRPC.StartImitate:
                        var imitator2 = Utils.PlayerById(reader.ReadByte());
                        if (imitator2.Is(RoleEnum.Traitor)) break;
                        var imitatorRole2 = Role.GetRole<Imitator>(imitator2);
                        StartImitate.Imitate(imitatorRole2);
                        break;
                    case CustomRPC.Remember:
                        readByte1 = reader.ReadByte();
                        readByte2 = reader.ReadByte();
                        var amnesiac = Utils.PlayerById(readByte1);
                        var other = Utils.PlayerById(readByte2);
                        PerformKillButton.Remember(Role.GetRole<Amnesiac>(amnesiac), other);
                        break;
                    case CustomRPC.Protect:
                        readByte1 = reader.ReadByte();
                        readByte2 = reader.ReadByte();

                        var medic = Utils.PlayerById(readByte1);
                        var shield = Utils.PlayerById(readByte2);
                        Role.GetRole<Medic>(medic).ShieldedPlayer = shield;
                        Role.GetRole<Medic>(medic).UsedAbility = true;
                        break;
                    case CustomRPC.AttemptSound:
                        var medicId = reader.ReadByte();
                        readByte = reader.ReadByte();
                        StopKill.BreakShield(medicId, readByte, CustomGameOptions.ShieldBreaks);
                        break;
                    case CustomRPC.BypassKill:
                        var killer = Utils.PlayerById(reader.ReadByte());
                        var target = Utils.PlayerById(reader.ReadByte());

                        Utils.MurderPlayer(killer, target, true);
                        break;
                    case CustomRPC.BypassMultiKill:
                        var killer2 = Utils.PlayerById(reader.ReadByte());
                        var target2 = Utils.PlayerById(reader.ReadByte());

                        Utils.MurderPlayer(killer2, target2, false);
                        break;
                    case CustomRPC.AssassinKill:
                        var toDie = Utils.PlayerById(reader.ReadByte());
                        var assassin = Utils.PlayerById(reader.ReadByte());
                        AssassinKill.MurderPlayer(toDie);
                        AssassinKill.AssassinKillCount(toDie, assassin);
                        break;
                    case CustomRPC.HuskAssassinKill:
                        var toDie4 = Utils.PlayerById(reader.ReadByte());
                        var husk = Utils.PlayerById(reader.ReadByte());
                        HuskAssassinKill.MurderPlayer(toDie4);
                        HuskAssassinKill.HuskKillCount(toDie4, husk);
                        break;
                    case CustomRPC.VigilanteKill:
                        var toDie2 = Utils.PlayerById(reader.ReadByte());
                        var vigi = Utils.PlayerById(reader.ReadByte());
                        VigilanteKill.MurderPlayer(toDie2);
                        VigilanteKill.VigiKillCount(toDie2, vigi);
                        break;
                    case CustomRPC.DoomsayerKill:
                        var toDie3 = Utils.PlayerById(reader.ReadByte());
                        var doom = Utils.PlayerById(reader.ReadByte());
                        DoomsayerKill.DoomKillCount(toDie3, doom);
                        DoomsayerKill.MurderPlayer(toDie3);
                        break;
                    case CustomRPC.SetMimic:
                        var glitchPlayer = Utils.PlayerById(reader.ReadByte());
                        var mimicPlayer = Utils.PlayerById(reader.ReadByte());
                        var glitchRole = Role.GetRole<Glitch>(glitchPlayer);
                        glitchRole.MimicTarget = mimicPlayer;
                        glitchRole.IsUsingMimic = true;
                        Utils.Morph(glitchPlayer, mimicPlayer);
                        break;
                    case CustomRPC.RpcResetAnim:
                        var animPlayer = Utils.PlayerById(reader.ReadByte());
                        var theGlitchRole = Role.GetRole<Glitch>(animPlayer);
                        theGlitchRole.MimicTarget = null;
                        theGlitchRole.IsUsingMimic = false;
                        Utils.Unmorph(theGlitchRole.Player);
                        break;
                    case CustomRPC.GlitchWin:
                        var theGlitch = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Glitch);
                        ((Glitch) theGlitch)?.Wins();
                        break;
                    case CustomRPC.SetHacked:
                        var hackPlayer = Utils.PlayerById(reader.ReadByte());
                        if (hackPlayer.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                        {
                            var glitch = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Glitch);
                            ((Glitch) glitch)?.SetHacked(hackPlayer);
                        }

                        break;
                    case CustomRPC.SetStunned:
                        var stunPlayer = Utils.PlayerById(reader.ReadByte());
                        if (stunPlayer.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                        {
                            var senti = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Sentinel);
                            ((Sentinel) senti)?.SetStunned(stunPlayer);
                        }

                        break;
                    case CustomRPC.Morph:
                        var morphling = Utils.PlayerById(reader.ReadByte());
                        var morphTarget = Utils.PlayerById(reader.ReadByte());
                        var morphRole = Role.GetRole<Morphling>(morphling);
                        morphRole.TimeRemaining = CustomGameOptions.MorphlingDuration;
                        morphRole.MorphedPlayer = morphTarget;
                        break;
                    case CustomRPC.Poison:
                        var poisoner = Utils.PlayerById(reader.ReadByte());
                        var poisoned = Utils.PlayerById(reader.ReadByte());
                        var poisonerRole = Role.GetRole<Poisoner>(poisoner);
                        poisonerRole.PoisonedPlayer = poisoned;
                        break;
                    case CustomRPC.RemoteBite:
                        var vamp = Utils.PlayerById(reader.ReadByte());
                        var bitten = Utils.PlayerById(reader.ReadByte());
                        var vampRole = Role.GetRole<Vampire>(vamp);
                        vampRole.BittenPlayer = bitten;
                        break;
                    case CustomRPC.SetTarget:
                        var exe = Utils.PlayerById(reader.ReadByte());
                        var exeTarget = Utils.PlayerById(reader.ReadByte());
                        var exeRole = Role.GetRole<Executioner>(exe);
                        exeRole.target = exeTarget;
                        break;
                    case CustomRPC.SetJkTarget:
                        var jk = Utils.PlayerById(reader.ReadByte());
                        var jkTarget = Utils.PlayerById(reader.ReadByte());
                        var jkRole = Role.GetRole<Joker>(jk);
                        jkRole.target = jkTarget;
                        jkRole.UsedAbility = true;
                        jkRole.TaskText = () => "Get {target.name} lynched to start your master plan.\nFake Tasks:";
                        jkTarget.nameText().color = Color.black;
                        break;
                    case CustomRPC.SetGATarget:
                        var ga = Utils.PlayerById(reader.ReadByte());
                        var gaTarget = Utils.PlayerById(reader.ReadByte());
                        var gaRole = Role.GetRole<GuardianAngel>(ga);
                        gaRole.target = gaTarget;
                        break;
                    case CustomRPC.Blackmail:
                        var blackmailer = Role.GetRole<Blackmailer>(Utils.PlayerById(reader.ReadByte()));
                        blackmailer.Blackmailed = Utils.PlayerById(reader.ReadByte());
                        break;
                    case CustomRPC.SnitchCultistReveal:
                        var snitch = Role.GetRole<CultistSnitch>(Utils.PlayerById(reader.ReadByte()));
                        snitch.CompletedTasks = true;
                        snitch.RevealedPlayer = Utils.PlayerById(reader.ReadByte());
                        break;
                    case CustomRPC.Confess:
                        var oracle = Role.GetRole<Oracle>(Utils.PlayerById(reader.ReadByte()));
                        oracle.Confessor = Utils.PlayerById(reader.ReadByte());
                        var faction = reader.ReadInt32();
                        if (faction == 0) oracle.RevealedFaction = Faction.Crewmates;
                        else if (faction == 1) oracle.RevealedFaction = Faction.NeutralEvil;
                        else if (faction == 2) oracle.RevealedFaction = Faction.NeutralChaos;
                        else oracle.RevealedFaction = Faction.Impostors;
                        break;
                    case CustomRPC.Bless:
                        var oracle2 = Role.GetRole<Oracle>(Utils.PlayerById(reader.ReadByte()));
                        oracle2.SavedConfessor = true;
                        break;

                    case CustomRPC.HunterStalk:
                        var stalker = Utils.PlayerById(reader.ReadByte());
                        var stalked = Utils.PlayerById(reader.ReadByte());
                        Hunter hunterRole = Role.GetRole<Hunter>(stalker);
                        hunterRole.StalkDuration = CustomGameOptions.HunterStalkDuration;
                        hunterRole.StalkedPlayer = stalked;
                        hunterRole.Stalk();
                        break;
                    case CustomRPC.HunterCatchPlayer:
                        var hunter = Utils.PlayerById(reader.ReadByte());
                        var prey = Utils.PlayerById(reader.ReadByte());
                        Hunter hunter2 = Role.GetRole<Hunter>(hunter);
                        hunter2.CatchPlayer(prey);
                        break;

                    case CustomRPC.Retribution:
                        var lastVoted = Utils.PlayerById(reader.ReadByte());
                        AssassinKill.MurderPlayer(lastVoted);
                        break;

                    case CustomRPC.ExecutionerToJester:
                        TargetColor.ExeToJes(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.JokerToJester:
                        JkTargetColor.JkToJes(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.GAToSurv:
                        GATargetColor.GAToSurv(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.GuardReset:
                        ShowGuarded.GuardReset(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.AddTyrantVoteBank:
                        Role.GetRole<Tyrant>(Utils.PlayerById(reader.ReadByte())).VoteBank += reader.ReadInt32();
                        break;
                    case CustomRPC.Mine:
                        var ventId = reader.ReadInt32();
                        var miner = Utils.PlayerById(reader.ReadByte());
                        var minerRole = Role.GetRole<Miner>(miner);
                        var pos = reader.ReadVector2();
                        var zAxis = reader.ReadSingle();
                        PlaceVent.SpawnVent(ventId, minerRole, pos, zAxis);
                        break;
                    case CustomRPC.Swoop:
                        var swooper = Utils.PlayerById(reader.ReadByte());
                        var swooperRole = Role.GetRole<Swooper>(swooper);
                        swooperRole.TimeRemaining = CustomGameOptions.SwoopDuration;
                        swooperRole.Swoop();
                        break;
                    case CustomRPC.ChameleonSwoop:
                        var chameleon = Utils.PlayerById(reader.ReadByte());
                        var chameleonRole = Role.GetRole<Chameleon>(chameleon);
                        chameleonRole.TimeRemaining = CustomGameOptions.SwoopDuration;
                        chameleonRole.Swoop();
                        break;
                    case CustomRPC.Camouflage:
                        var venerer = Utils.PlayerById(reader.ReadByte());
                        var venererRole = Role.GetRole<Venerer>(venerer);
                        venererRole.TimeRemaining = CustomGameOptions.AbilityDuration;
                        venererRole.KillsAtStartAbility = reader.ReadInt32();
                        venererRole.Ability();
                        break;
                    case CustomRPC.Alert:
                        var veteran = Utils.PlayerById(reader.ReadByte());
                        var veteranRole = Role.GetRole<Veteran>(veteran);
                        veteranRole.TimeRemaining = CustomGameOptions.AlertDuration;
                        veteranRole.Alert();
                        break;
                    case CustomRPC.Vest:
                        var surv = Utils.PlayerById(reader.ReadByte());
                        var survRole = Role.GetRole<Survivor>(surv);
                        survRole.TimeRemaining = CustomGameOptions.VestDuration;
                        survRole.Vest();
                        break;
                    case CustomRPC.BGGuard:
                        var bg = Utils.PlayerById(reader.ReadByte());
                        var bgRole = Role.GetRole<Bodyguard>(bg);
                        bgRole.TimeRemaining = CustomGameOptions.GuardDuration;
                        bgRole.Guard();
                        break;
                    case CustomRPC.GAProtect:
                        var ga2 = Utils.PlayerById(reader.ReadByte());
                        var ga2Role = Role.GetRole<GuardianAngel>(ga2);
                        ga2Role.TimeRemaining = CustomGameOptions.ProtectDuration;
                        ga2Role.Protect();
                        break;
                    case CustomRPC.Transport:
                        Coroutines.Start(Transporter.TransportPlayers(reader.ReadByte(), reader.ReadByte(), reader.ReadBoolean()));
                        break;
                    case CustomRPC.SetUntransportable:
                        if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
                        {
                            Role.GetRole<Transporter>(PlayerControl.LocalPlayer).UntransportablePlayers.Add(reader.ReadByte(), DateTime.UtcNow);
                        }
                        break;
                    case CustomRPC.Mediate:
                        var mediatedPlayer = Utils.PlayerById(reader.ReadByte());
                        var medium = Role.GetRole<Medium>(Utils.PlayerById(reader.ReadByte()));
                        if (PlayerControl.LocalPlayer.PlayerId != mediatedPlayer.PlayerId) break;
                        medium.AddMediatePlayer(mediatedPlayer.PlayerId);
                        break;
                    case CustomRPC.FlashGrenade:
                        var grenadier = Utils.PlayerById(reader.ReadByte());
                        var grenadierRole = Role.GetRole<Grenadier>(grenadier);
                        grenadierRole.TimeRemaining = CustomGameOptions.GrenadeDuration;
                        grenadierRole.Flash();
                        break;
                    case CustomRPC.ArsonistWin:
                        var theArsonistTheRole = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Arsonist);
                        ((Arsonist) theArsonistTheRole)?.Wins();
                        break;
                    case CustomRPC.WerewolfWin:
                        var theWerewolfTheRole = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Werewolf);
                        ((Werewolf)theWerewolfTheRole)?.Wins();
                        break;
                    case CustomRPC.SentinelWin:
                        var theSentinelTheRole = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Sentinel);
                        ((Sentinel)theSentinelTheRole)?.Wins();
                        break;
                    case CustomRPC.Infect:
                        var pb = Role.GetRole<Plaguebearer>(Utils.PlayerById(reader.ReadByte()));
                        if (!Utils.PlayerById(reader.ReadByte()).Is(Faction.NeutralApocalypse)) pb.SpreadInfection(Utils.PlayerById(reader.ReadByte()), Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.TurnPestilence:
                        Role.GetRole<Plaguebearer>(Utils.PlayerById(reader.ReadByte())).TurnPestilence();
                        break;
                    case CustomRPC.TurnWar:
                        Role.GetRole<Berserker>(Utils.PlayerById(reader.ReadByte())).TurnWar();
                        break;
                    case CustomRPC.SyncCustomSettings:
                        Rpc.ReceiveRpc(reader);
                        PluginSingleton <TownOfUsFusion>.Instance.Log.LogInfo("Options were successfully synced with the host!");
                        break;
                    case CustomRPC.AltruistRevive:
                        readByte1 = reader.ReadByte();
                        var altruistPlayer = Utils.PlayerById(readByte1);
                        var altruistRole = Role.GetRole<Altruist>(altruistPlayer);
                        readByte = reader.ReadByte();
                        var theDeadBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in theDeadBodies)
                            if (body.ParentId == readByte)
                            {
                                if (body.ParentId == PlayerControl.LocalPlayer.PlayerId)
                                    Coroutines.Start(Utils.FlashCoroutine(altruistRole.Color,
                                        CustomGameOptions.ReviveDuration, 0.5f));

                                Coroutines.Start(
                                    global::TownOfUsFusion.CrewmateRoles.AltruistMod.Coroutine.AltruistRevive(body,
                                        altruistRole));
                            }

                        break;
                    case CustomRPC.FixAnimation:
                        var player3 = Utils.PlayerById(reader.ReadByte());
                        player3.MyPhysics.ResetMoveState();
                        player3.Collider.enabled = true;
                        player3.moveable = true;
                        player3.NetTransform.enabled = true;
                        break;
                    case CustomRPC.BarryButton:
                        var buttonBarry = Utils.PlayerById(reader.ReadByte());

                        if (AmongUsClient.Instance.AmHost)
                        {
                            MeetingRoomManager.Instance.reporter = buttonBarry;
                            MeetingRoomManager.Instance.target = null;
                            AmongUsClient.Instance.DisconnectHandlers.AddUnique(MeetingRoomManager.Instance
                                .Cast<IDisconnectHandler>());
                            if (GameManager.Instance.CheckTaskCompletion()) return;

                            DestroyableSingleton<HudManager>.Instance.OpenMeetingRoom(buttonBarry);
                            buttonBarry.RpcStartMeeting(null);
                        }
                        break;
                    case CustomRPC.Disperse:
                        byte teleports = reader.ReadByte();
                        Dictionary<byte, Vector2> coordinates = new Dictionary<byte, Vector2>();
                        for (int i = 0; i < teleports; i++)
                        {
                            byte playerId = reader.ReadByte();
                            Vector2 location = reader.ReadVector2();
                            coordinates.Add(playerId, location);
                        }
                        Disperser.DispersePlayersToCoordinates(coordinates);
                        break;
                    case CustomRPC.BaitReport:
                        var baitKiller = Utils.PlayerById(reader.ReadByte());
                        var bait = Utils.PlayerById(reader.ReadByte());
                        baitKiller.ReportDeadBody(bait.Data);
                        break;
                    case CustomRPC.CheckMurder:
                        var murderKiller = Utils.PlayerById(reader.ReadByte());
                        var murderTarget = Utils.PlayerById(reader.ReadByte());
                        murderKiller.CheckMurder(murderTarget);
                        break;
                    case CustomRPC.Drag:
                        readByte1 = reader.ReadByte();
                        var dienerPlayer = Utils.PlayerById(readByte1);
                        var dienerRole = Role.GetRole<Undertaker>(dienerPlayer);
                        readByte = reader.ReadByte();
                        var dienerBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in dienerBodies)
                            if (body.ParentId == readByte)
                                dienerRole.CurrentlyDragging = body;

                        break;
                    case CustomRPC.Drop:
                        readByte1 = reader.ReadByte();
                        var v2 = reader.ReadVector2();
                        var v2z = reader.ReadSingle();
                        var dienerPlayer2 = Utils.PlayerById(readByte1);
                        var dienerRole2 = Role.GetRole<Undertaker>(dienerPlayer2);
                        var body2 = dienerRole2.CurrentlyDragging;
                        dienerRole2.CurrentlyDragging = null;

                        body2.transform.position = new Vector3(v2.x, v2.y, v2z);

                        break;
                    case CustomRPC.SetAssassin:
                        new Assassin(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetPhantom:
                        readByte = reader.ReadByte();
                        SetPhantom.WillBePhantom = readByte == byte.MaxValue ? null : Utils.PlayerById(readByte);
                        break;
                    case CustomRPC.CatchPhantom:
                        var phantomPlayer = Utils.PlayerById(reader.ReadByte());
                        Role.GetRole<Phantom>(phantomPlayer).Caught = true;
                        if (PlayerControl.LocalPlayer == phantomPlayer) HudManager.Instance.AbilityButton.gameObject.SetActive(true);
                        phantomPlayer.Exiled();
                        break;
                    case CustomRPC.PhantomWin:
                        var phantomWinner = Role.GetRole<Phantom>(Utils.PlayerById(reader.ReadByte()));
                        phantomWinner.CompletedTasks = true;
                        if (!CustomGameOptions.NeutralEvilWinEndsGame)
                        {
                            phantomWinner.Caught = true;
                            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Phantom) || !CustomGameOptions.PhantomSpook || MeetingHud.Instance) return;
                            byte[] toKill = MeetingHud.Instance.playerStates.Where(x => !Utils.PlayerById(x.TargetPlayerId).Is(RoleEnum.Pestilence)).Select(x => x.TargetPlayerId).ToArray();
                            Role.GetRole(PlayerControl.LocalPlayer).PauseEndCrit = true;
                            var pk = new PunishmentKill((x) => {
                                Utils.RpcMultiMurderPlayer(PlayerControl.LocalPlayer, x);
                                Role.GetRole(PlayerControl.LocalPlayer).PauseEndCrit = false;
                            }, (y) => {
                                return toKill.Contains(y.PlayerId);
                            });
                            Coroutines.Start(pk.Open(1f));
                        }
                        break;
                    case CustomRPC.TaskmasterWin:
                        Role.TaskmasterWin();
                        break;
                    case CustomRPC.SetHaunter:
                        readByte = reader.ReadByte();
                        SetHaunter.WillBeHaunter = readByte == byte.MaxValue ? null : Utils.PlayerById(readByte);
                        break;
                    case CustomRPC.CatchHaunter:
                        var haunterPlayer = Utils.PlayerById(reader.ReadByte());
                        Role.GetRole<Haunter>(haunterPlayer).Caught = true;
                        if (PlayerControl.LocalPlayer == haunterPlayer) HudManager.Instance.AbilityButton.gameObject.SetActive(true);
                        haunterPlayer.Exiled();
                        break;
                    case CustomRPC.SetTraitor:
                        readByte = reader.ReadByte();
                        SetTraitor.WillBeTraitor = readByte == byte.MaxValue ? null : Utils.PlayerById(readByte);
                        break;
                    case CustomRPC.TraitorSpawn:
                        var traitor = SetTraitor.WillBeTraitor;
                        if (traitor == StartImitate.ImitatingPlayer) StartImitate.ImitatingPlayer = null;
                        var oldRole = Role.GetRole(traitor);
                        var killsList = (oldRole.CorrectKills, oldRole.IncorrectKills, oldRole.CorrectAssassinKills, oldRole.IncorrectAssassinKills);
                        Role.RoleDictionary.Remove(traitor.PlayerId);
                        var traitorRole = new Traitor(traitor);
                        traitorRole.formerRole = oldRole.RoleType;
                        traitorRole.CorrectKills = killsList.CorrectKills;
                        traitorRole.IncorrectKills = killsList.IncorrectKills;
                        traitorRole.CorrectAssassinKills = killsList.CorrectAssassinKills;
                        traitorRole.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
                        traitorRole.RegenTask();
                        SetTraitor.TurnImp(traitor);
                        break;
                    case CustomRPC.Escape:
                        var escapist = Utils.PlayerById(reader.ReadByte());
                        var escapistRole = Role.GetRole<Escapist>(escapist);
                        var escapePos = reader.ReadVector2();
                        escapistRole.EscapePoint = escapePos;
                        Escapist.Escape(escapist);
                        break;
                    case CustomRPC.Revive:
                        var necromancer = Utils.PlayerById(reader.ReadByte());
                        var necromancerRole = Role.GetRole<Necromancer>(necromancer);
                        var revived = reader.ReadByte();
                        var theDeadBodies2 = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in theDeadBodies2)
                            if (body.ParentId == revived)
                            {
                                PerformRevive.Revive(body, necromancerRole);
                            }
                        break;
                    case CustomRPC.Convert:
                        var convertedPlayer = Utils.PlayerById(reader.ReadByte());
                        Utils.Convert(convertedPlayer);
                        break;
                    case CustomRPC.Resurrect:
                        var neoNecro = Utils.PlayerById(reader.ReadByte());
                        var neoNecroRole = Role.GetRole<NeoNecromancer>(neoNecro);
                        var resurrected = reader.ReadByte();
                        var theDeadBodies3 = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in theDeadBodies3)
                            if (body.ParentId == resurrected)
                            {
                                PerformNecro.Resurrect(body, neoNecroRole);
                            }
                        break;
                    case CustomRPC.Resurrect2:
                        var resus = Utils.PlayerById(reader.ReadByte());
                        var resusRole = Role.GetRole<Apparitionist>(resus);
                        var resurrected2 = reader.ReadByte();
                        var theDeadBodies4 = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in theDeadBodies4)
                            if (body.ParentId == resurrected2)
                            {
                                PerformResurrect2.Resurrect2(body, resusRole);
                            }
                        break;
                    case CustomRPC.NeoConvert:
                        var convertedPlayer2 = Utils.PlayerById(reader.ReadByte());
                        Utils.NeoConvert(convertedPlayer2);
                        break;
                    case CustomRPC.RemoveAllBodies:
                        var buggedBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in buggedBodies)
                            body.gameObject.Destroy();
                        break;
                    case CustomRPC.SubmergedFixOxygen:
                        Patches.SubmergedCompatibility.RepairOxygen();
                        break;

                    case CustomRPC.SetPos:
                        var setplayer = Utils.PlayerById(reader.ReadByte());
                        setplayer.transform.position = new Vector2(reader.ReadSingle(), reader.ReadSingle());
                        break;
                    case CustomRPC.SetSettings:
                        readByte = reader.ReadByte();
                        GameOptionsManager.Instance.currentNormalGameOptions.MapId = readByte == byte.MaxValue ? (byte)0 : readByte;
                        GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Scientist, 0, 0);
                        GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Engineer, 0, 0);
                        GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.GuardianAngel, 0, 0);
                        GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Shapeshifter, 0, 0);
                        if (CustomGameOptions.AutoAdjustSettings) RandomMap.AdjustSettings(readByte);
                        break;
                }
            }
        }

        [HarmonyPatch(typeof(RoleManager), nameof(RoleManager.SelectRoles))]
        public static class RpcSetRole
        {
            public static void Postfix()
            {
                PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("RPC SET ROLE");
                var infected = GameData.Instance.AllPlayers.ToArray().Where(o => o.IsImpostor());

                Utils.ShowDeadBodies = false;
                if (ShowRoundOneShield.DiedFirst != null && CustomGameOptions.FirstDeathShield)
                {
                    var shielded = false;
                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        if (player.name == ShowRoundOneShield.DiedFirst)
                        {
                            ShowRoundOneShield.FirstRoundShielded = player;
                            shielded = true;
                        }
                    }
                    if (!shielded) ShowRoundOneShield.FirstRoundShielded = null;
                }
                else ShowRoundOneShield.FirstRoundShielded = null;
                ShowRoundOneShield.DiedFirst = "";
                Role.NobodyWins = false;
                Role.SurvOnlyWins = false;
                Role.SentinelCrewWins = false;
                Role.VampireWins = false;
                Role.CSWins = false;
                Role.JackalWins = false;
                Role.NecroWins = false;
                Role.ApocWins = false;
                ExileControllerPatch.lastExiled = null;
                PatchKillTimer.GameStarted = false;
                StartImitate.ImitatingPlayer = null;
                AddHauntPatch.AssassinatedPlayers.Clear();
                CrewmateRoles.Clear();
                NeutralBenignRoles.Clear();
                NeutralEvilRoles.Clear();
                NeutralChaosRoles.Clear();
                NeutralKillingRoles.Clear();
                NeutralNeophyteRoles.Clear();
                NeutralApocalypseRoles.Clear();
                ImpostorRoles.Clear();
                CrewmateModifiers.Clear();
                GlobalModifiers.Clear();
                ImpostorModifiers.Clear();
                ButtonModifiers.Clear();
                AssassinModifiers.Clear();
                AssassinAbility.Clear();

                CrewmateAlliances.Clear();
                GlobalAlliances.Clear();

                GlobalTags.Clear();
                Murder.KilledPlayers.Clear();
                KillButtonTarget.DontRevive = byte.MaxValue;
                ReviveHudManagerUpdate.DontRevive = byte.MaxValue;
                HudUpdate.Zooming = false;
                HudUpdate.ZoomStart();

                if (ShowRoundOneShield.FirstRoundShielded != null)
                {
                    Utils.Rpc(CustomRPC.Start, ShowRoundOneShield.FirstRoundShielded.PlayerId);
                }
                else
                {
                    Utils.Rpc(CustomRPC.Start, byte.MaxValue);
                }

                if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek) return;

                if (CustomGameOptions.GameMode == GameMode.Classic || CustomGameOptions.GameMode == GameMode.AllAny)
                {
                    PhantomOn = Check(CustomGameOptions.PhantomOn);
                    HaunterOn = Check(CustomGameOptions.HaunterOn);
                    TraitorOn = Check(CustomGameOptions.TraitorOn);
                }
                else
                {
                    PhantomOn = false;
                    HaunterOn = false;
                    TraitorOn = false;
                }

                if (CustomGameOptions.GameMode == GameMode.Classic || CustomGameOptions.GameMode == GameMode.AllAny)
                {
                    #region Crewmate Roles
                    if (CustomGameOptions.MayorOn > 0)
                        CrewmateRoles.Add((typeof(Mayor), CustomGameOptions.MayorOn, true));

                    if (CustomGameOptions.SheriffOn > 0)
                        CrewmateRoles.Add((typeof(Sheriff), CustomGameOptions.SheriffOn, false));

                    if (CustomGameOptions.EngineerOn > 0)
                        CrewmateRoles.Add((typeof(Engineer), CustomGameOptions.EngineerOn, false));

                    if (CustomGameOptions.SwapperOn > 0)
                        CrewmateRoles.Add((typeof(Swapper), CustomGameOptions.SwapperOn, true));

                    if (CustomGameOptions.InvestigatorOn > 0)
                        CrewmateRoles.Add((typeof(Investigator), CustomGameOptions.InvestigatorOn, false));

                    if (CustomGameOptions.MedicOn > 0)
                        CrewmateRoles.Add((typeof(Medic), CustomGameOptions.MedicOn, true));

                    if (CustomGameOptions.SeerOn > 0)
                        CrewmateRoles.Add((typeof(Seer), CustomGameOptions.SeerOn, false));

                    if (CustomGameOptions.SpyOn > 0)
                        CrewmateRoles.Add((typeof(Spy), CustomGameOptions.SpyOn, false));

                    if (CustomGameOptions.SnitchOn > 0)
                        CrewmateRoles.Add((typeof(Snitch), CustomGameOptions.SnitchOn, true));

                    if (CustomGameOptions.AltruistOn > 0)
                        CrewmateRoles.Add((typeof(Altruist), CustomGameOptions.AltruistOn, true));

                    if (CustomGameOptions.VigilanteOn > 0)
                        CrewmateRoles.Add((typeof(Vigilante), CustomGameOptions.VigilanteOn, false));

                    if (CustomGameOptions.VeteranOn > 0)
                        CrewmateRoles.Add((typeof(Veteran), CustomGameOptions.VeteranOn, false));

                    if (CustomGameOptions.TrackerOn > 0)
                        CrewmateRoles.Add((typeof(Tracker), CustomGameOptions.TrackerOn, false));

                    if (CustomGameOptions.TransporterOn > 0)
                        CrewmateRoles.Add((typeof(Transporter), CustomGameOptions.TransporterOn, false));

                    if (CustomGameOptions.MediumOn > 0)
                        CrewmateRoles.Add((typeof(Medium), CustomGameOptions.MediumOn, false));

                    if (CustomGameOptions.MysticOn > 0)
                        CrewmateRoles.Add((typeof(Mystic), CustomGameOptions.MysticOn, false));

                    if (CustomGameOptions.TrapperOn > 0)
                        CrewmateRoles.Add((typeof(Trapper), CustomGameOptions.TrapperOn, false));

                    if (CustomGameOptions.DetectiveOn > 0)
                        CrewmateRoles.Add((typeof(Detective), CustomGameOptions.DetectiveOn, false));

                    if (CustomGameOptions.ImitatorOn > 0)
                        CrewmateRoles.Add((typeof(Imitator), CustomGameOptions.ImitatorOn, true));

                    if (CustomGameOptions.ProsecutorOn > 0)
                        CrewmateRoles.Add((typeof(Prosecutor), CustomGameOptions.ProsecutorOn, true));

                    if (CustomGameOptions.OracleOn > 0)
                        CrewmateRoles.Add((typeof(Oracle), CustomGameOptions.OracleOn, true));

                    if (CustomGameOptions.AurialOn > 0)
                        CrewmateRoles.Add((typeof(Aurial), CustomGameOptions.AurialOn, false));

                    if (CustomGameOptions.HunterOn > 0)
                        CrewmateRoles.Add((typeof(Hunter), CustomGameOptions.HunterOn, false));

                    if (CustomGameOptions.TricksterOn > 0)
                        CrewmateRoles.Add((typeof(Trickster), CustomGameOptions.TricksterOn, false));

                    if (CustomGameOptions.BodyguardOn > 0)
                        CrewmateRoles.Add((typeof(Bodyguard), CustomGameOptions.BodyguardOn, true));

                    if (CustomGameOptions.TaskmasterOn > 0)
                        CrewmateRoles.Add((typeof(Taskmaster), CustomGameOptions.TaskmasterOn, true));
                    #endregion
                    #region Neutral Roles
                    // NEUTRAL BENIGN
                    if (CustomGameOptions.AmnesiacOn > 0)
                        NeutralBenignRoles.Add((typeof(Amnesiac), CustomGameOptions.AmnesiacOn, false));

                    if (CustomGameOptions.SurvivorOn > 0)
                        NeutralBenignRoles.Add((typeof(Survivor), CustomGameOptions.SurvivorOn, false));

                    if (CustomGameOptions.GuardianAngelOn > 0)
                        NeutralBenignRoles.Add((typeof(GuardianAngel), CustomGameOptions.GuardianAngelOn, false));

                    // NEUTRAL EVIL
                    if (CustomGameOptions.ExecutionerOn > 0)
                        NeutralEvilRoles.Add((typeof(Executioner), CustomGameOptions.ExecutionerOn, false));

                    if (CustomGameOptions.DoomsayerOn > 0)
                        NeutralEvilRoles.Add((typeof(Doomsayer), CustomGameOptions.DoomsayerOn, false));

                    if (CustomGameOptions.JesterOn > 0)
                        NeutralEvilRoles.Add((typeof(Jester), CustomGameOptions.JesterOn, false));

                    // NEUTRAL CHAOS
                    if (CustomGameOptions.CursedSoulOn > 0)
                        NeutralChaosRoles.Add((typeof(CursedSoul), CustomGameOptions.CursedSoulOn, false));

                    if (CustomGameOptions.TyrantOn > 0)
                        NeutralChaosRoles.Add((typeof(Tyrant), CustomGameOptions.TyrantOn, false));

                    if (CustomGameOptions.InquisitorOn > 0)
                        NeutralChaosRoles.Add((typeof(Inquisitor), CustomGameOptions.InquisitorOn, false));

                    if (CustomGameOptions.CannibalOn > 0)
                        NeutralChaosRoles.Add((typeof(Cannibal), CustomGameOptions.CannibalOn, false));

                    if (CustomGameOptions.JokerOn > 0)
                        NeutralChaosRoles.Add((typeof(Joker), CustomGameOptions.JokerOn, false));

                    // NEUTRAL KILLING
                    if (CustomGameOptions.GlitchOn > 0)
                        NeutralKillingRoles.Add((typeof(Glitch), CustomGameOptions.GlitchOn, true));

                    if (CustomGameOptions.ArsonistOn > 0)
                        NeutralKillingRoles.Add((typeof(Arsonist), CustomGameOptions.ArsonistOn, true));

                    if (CustomGameOptions.SentinelOn > 0)
                        NeutralKillingRoles.Add((typeof(Sentinel), CustomGameOptions.SentinelOn, true));

                    if (CustomGameOptions.WerewolfOn > 0)
                        NeutralKillingRoles.Add((typeof(Werewolf), CustomGameOptions.WerewolfOn, true));

                    // NEUTRAL NEOPHYTE
                    if (CustomGameOptions.JackalOn > 0)
                        NeutralNeophyteRoles.Add((typeof(Jackal), CustomGameOptions.JackalOn, true));

                    if (CustomGameOptions.VampireOn > 0)
                        NeutralNeophyteRoles.Add((typeof(Vampire), CustomGameOptions.VampireOn, true));

                    if (CustomGameOptions.NeoNecromancerOn > 0)
                        NeutralNeophyteRoles.Add((typeof(NeoNecromancer), CustomGameOptions.NeoNecromancerOn, true));

                    // NEUTRAL APOCALYPSE
                    if (CustomGameOptions.PlaguebearerOn > 0)
                        NeutralApocalypseRoles.Add((typeof(Plaguebearer), CustomGameOptions.PlaguebearerOn, true));

                    if (CustomGameOptions.BerserkerOn > 0)
                        NeutralApocalypseRoles.Add((typeof(Berserker), CustomGameOptions.BerserkerOn, true));

                    if (CustomGameOptions.BakerOn > 0)
                        NeutralApocalypseRoles.Add((typeof(Baker), CustomGameOptions.BakerOn, true));

                    if (CustomGameOptions.SoulCollectorOn > 0)
                        NeutralApocalypseRoles.Add((typeof(SoulCollector), CustomGameOptions.SoulCollectorOn, true));
                    #endregion
                    #region Impostor Roles
                    if (CustomGameOptions.UndertakerOn > 0)
                        ImpostorRoles.Add((typeof(Undertaker), CustomGameOptions.UndertakerOn, true));

                    if (CustomGameOptions.MorphlingOn > 0)
                        ImpostorRoles.Add((typeof(Morphling), CustomGameOptions.MorphlingOn, false));

                    if (CustomGameOptions.BlackmailerOn > 0)
                        ImpostorRoles.Add((typeof(Blackmailer), CustomGameOptions.BlackmailerOn, true));

                    if (CustomGameOptions.MinerOn > 0)
                        ImpostorRoles.Add((typeof(Miner), CustomGameOptions.MinerOn, true));

                    if (CustomGameOptions.SwooperOn > 0)
                        ImpostorRoles.Add((typeof(Swooper), CustomGameOptions.SwooperOn, false));

                    if (CustomGameOptions.JanitorOn > 0)
                        ImpostorRoles.Add((typeof(Janitor), CustomGameOptions.JanitorOn, false));

                    if (CustomGameOptions.GrenadierOn > 0)
                        ImpostorRoles.Add((typeof(Grenadier), CustomGameOptions.GrenadierOn, true));

                    if (CustomGameOptions.PoisonerOn > 0 && CustomGameOptions.GameMode != GameMode.KillingOnly)
                        ImpostorRoles.Add((typeof(Poisoner), CustomGameOptions.PoisonerOn, true));

                    if (CustomGameOptions.EscapistOn > 0)
                        ImpostorRoles.Add((typeof(Escapist), CustomGameOptions.EscapistOn, false));

                    if (CustomGameOptions.BomberOn > 0)
                        ImpostorRoles.Add((typeof(Bomber), CustomGameOptions.BomberOn, true));

                    if (CustomGameOptions.WarlockOn > 0)
                        ImpostorRoles.Add((typeof(Warlock), CustomGameOptions.WarlockOn, false));

                    if (CustomGameOptions.VenererOn > 0)
                        ImpostorRoles.Add((typeof(Venerer), CustomGameOptions.VenererOn, true));
                    #endregion

                    #region Crewmate Alliances
                    if (Check(CustomGameOptions.EgotistOn))
                        CrewmateAlliances.Add((typeof(Egotist), CustomGameOptions.EgotistOn));
                    if (Check(CustomGameOptions.CrewpocalypseOn))
                        CrewmateAlliances.Add((typeof(Crewpocalypse), CustomGameOptions.CrewpocalypseOn));
                    if (Check(CustomGameOptions.CrewpostorOn))
                        CrewmateAlliances.Add((typeof(Crewpostor), CustomGameOptions.CrewpostorOn));
                    #endregion
                    #region Global Alliances
                    if (Check(CustomGameOptions.JackalOn))
                        GlobalAlliances.Add((typeof(Recruit), 100));
                    if (Check(CustomGameOptions.LoversOn))
                        GlobalAlliances.Add((typeof(Lover), CustomGameOptions.LoversOn));
                    #endregion

                    #region Crewmate Modifiers
                    if (Check(CustomGameOptions.TorchOn))
                        CrewmateModifiers.Add((typeof(Torch), CustomGameOptions.TorchOn));

                    if (Check(CustomGameOptions.EclipsedOn))
                        CrewmateModifiers.Add((typeof(Eclipsed), CustomGameOptions.EclipsedOn));

                    if (Check(CustomGameOptions.DiseasedOn))
                        CrewmateModifiers.Add((typeof(Diseased), CustomGameOptions.DiseasedOn));

                    if (Check(CustomGameOptions.BaitOn))
                        CrewmateModifiers.Add((typeof(Bait), CustomGameOptions.BaitOn));

                    if (Check(CustomGameOptions.AftermathOn))
                        CrewmateModifiers.Add((typeof(Aftermath), CustomGameOptions.AftermathOn));

                    if (Check(CustomGameOptions.MultitaskerOn))
                        CrewmateModifiers.Add((typeof(Multitasker), CustomGameOptions.MultitaskerOn));

                    if (Check(CustomGameOptions.FrostyOn))
                        CrewmateModifiers.Add((typeof(Frosty), CustomGameOptions.FrostyOn));
                    #endregion
                    #region Global Modifiers
                    if (Check(CustomGameOptions.TiebreakerOn))
                        GlobalModifiers.Add((typeof(Tiebreaker), CustomGameOptions.TiebreakerOn));

                    if (Check(CustomGameOptions.DwarfOn))
                        GlobalModifiers.Add((typeof(Dwarf), CustomGameOptions.DwarfOn));

                    if (Check(CustomGameOptions.GiantOn))
                        GlobalModifiers.Add((typeof(Giant), CustomGameOptions.GiantOn));

                    if (Check(CustomGameOptions.DrunkOn))
                        GlobalModifiers.Add((typeof(Drunk), CustomGameOptions.DrunkOn));

                    if (Check(CustomGameOptions.ObliviousOn))
                        GlobalModifiers.Add((typeof(Oblivious), CustomGameOptions.ObliviousOn));

                    if (Check(CustomGameOptions.ButtonBarryOn))
                        ButtonModifiers.Add((typeof(ButtonBarry), CustomGameOptions.ButtonBarryOn));

                    if (Check(CustomGameOptions.SleuthOn))
                        GlobalModifiers.Add((typeof(Sleuth), CustomGameOptions.SleuthOn));

                    if (Check(CustomGameOptions.RadarOn))
                        GlobalModifiers.Add((typeof(Radar), CustomGameOptions.RadarOn));
                    #endregion
                    #region Impostor Modifiers
                    if (Check(CustomGameOptions.DisperserOn))
                        ImpostorModifiers.Add((typeof(Disperser), CustomGameOptions.DisperserOn));

                    if (Check(CustomGameOptions.DoubleShotOn))
                        AssassinModifiers.Add((typeof(DoubleShot), CustomGameOptions.DoubleShotOn));

                    if (CustomGameOptions.UnderdogOn > 0)
                        ImpostorModifiers.Add((typeof(Underdog), CustomGameOptions.UnderdogOn));
                    #endregion
                    #region Assassin Ability
                    AssassinAbility.Add((typeof(Assassin), CustomRPC.SetAssassin, 100));
                    #endregion
                    #region Global Tags
                    if (Check(CustomGameOptions.InquisitorOn))
                        GlobalTags.Add((typeof(Heretic), 100));
                    #endregion
                }

                if (CustomGameOptions.GameMode == GameMode.KillingOnly) GenEachRoleKilling(infected.ToList());
                else if (CustomGameOptions.GameMode == GameMode.Cultist) GenEachRoleCultist(infected.ToList());
                else GenEachRole(infected.ToList());
            }
        }
    }
}