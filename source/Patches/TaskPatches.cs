using HarmonyLib;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion
{
    internal static class TaskPatches
{
    [HarmonyPatch(typeof(GameData), nameof(GameData.RecomputeTaskCounts))]
    private class GameData_RecomputeTaskCounts
    {
        private static bool Prefix(GameData __instance)
        {
            __instance.TotalTasks = 0;
            __instance.CompletedTasks = 0;
            for (var i = 0; i < __instance.AllPlayers.Count; i++)
            {
                var playerInfo = __instance.AllPlayers.ToArray()[i];
                if (!playerInfo.Disconnected && playerInfo.Tasks != null && playerInfo.Object &&
                    (GameOptionsManager.Instance.currentNormalGameOptions.GhostsDoTasks || !playerInfo.IsDead) && !playerInfo.IsImpostor() &&
                    !(
                        playerInfo._object.Is(RoleEnum.Jester) || playerInfo._object.Is(RoleEnum.Amnesiac) ||
                        playerInfo._object.Is(RoleEnum.Survivor) || playerInfo._object.Is(RoleEnum.GuardianAngel) ||
                        playerInfo._object.Is(RoleEnum.Glitch) || playerInfo._object.Is(RoleEnum.Executioner) ||
                        playerInfo._object.Is(RoleEnum.Arsonist) || playerInfo._object.Is(RoleEnum.Berserker) ||
                        playerInfo._object.Is(RoleEnum.Werewolf) || playerInfo._object.Is(RoleEnum.Doomsayer) ||
                        playerInfo._object.Is(RoleEnum.Vampire) || playerInfo._object.Is(RoleEnum.Jackal) || 
                        playerInfo._object.Is(RoleEnum.NeoNecromancer) || playerInfo._object.Is(RoleEnum.Scourge) ||
                        playerInfo._object.Is(RoleEnum.Apparitionist) ||
                        playerInfo._object.Is(RoleEnum.Husk) ||
                        playerInfo._object.Is(RoleEnum.Tyrant) ||
                        playerInfo._object.Is(RoleEnum.Joker) ||
                        // ADD A GAMEMODE CHECK SO TASKS DON'T IMMEDIATELY END IN MURDER MANIFESTO
                        playerInfo._object.Is(RoleEnum.Cannibal) ||
                        playerInfo._object.Is(RoleEnum.Inquisitor) ||
                        playerInfo._object.Is(RoleEnum.Phantom) || playerInfo._object.Is(RoleEnum.Haunter) ||
                        playerInfo._object.Is(RoleEnum.Baker) || playerInfo._object.Is(RoleEnum.Famine) ||
                        playerInfo._object.Is(RoleEnum.Berserker) || playerInfo._object.Is(RoleEnum.War) ||
                        playerInfo._object.Is(RoleEnum.Plaguebearer) || playerInfo._object.Is(RoleEnum.Pestilence) ||
                        playerInfo._object.Is(RoleEnum.SoulCollector) || playerInfo._object.Is(RoleEnum.Death) ||
                        playerInfo._object.Is(AllianceEnum.Crewpostor) || playerInfo._object.Is(AllianceEnum.Crewpocalypse)
                        || playerInfo._object.Is(AllianceEnum.Recruit)
                    ))
                    for (var j = 0; j < playerInfo.Tasks.Count; j++)
                    {
                        __instance.TotalTasks++;
                        if (playerInfo.Tasks.ToArray()[j].Complete) __instance.CompletedTasks++;
                    }
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(Console), nameof(Console.CanUse))]
    private class Console_CanUse
    {
        private static bool Prefix(Console __instance, [HarmonyArgument(0)] GameData.PlayerInfo playerInfo, ref float __result)
        {
            var playerControl = playerInfo.Object;

            var flag = playerControl.Is(RoleEnum.Glitch)
                       || playerControl.Is(RoleEnum.Tyrant)
                       || playerControl.Is(RoleEnum.Joker)
                       || playerControl.Is(RoleEnum.Cannibal)
                       || playerControl.Is(RoleEnum.Inquisitor)
                       || playerControl.Is(RoleEnum.Jester)
                       || playerControl.Is(RoleEnum.Executioner)
                       || playerControl.Is(RoleEnum.Berserker)
                       || playerControl.Is(RoleEnum.Arsonist)
                       || playerControl.Is(RoleEnum.NeoNecromancer) || playerControl.Is(RoleEnum.Scourge)
                       || playerControl.Is(RoleEnum.Apparitionist)
                       || playerControl.Is(RoleEnum.Husk) || playerInfo._object.Is(RoleEnum.Jackal)
                       || playerControl.Is(RoleEnum.Baker) || playerControl.Is(RoleEnum.Famine)
                       || playerControl.Is(RoleEnum.Berserker) || playerControl.Is(RoleEnum.War)
                       || playerControl.Is(RoleEnum.Plaguebearer) || playerControl.Is(RoleEnum.Pestilence)
                       || playerControl.Is(RoleEnum.SoulCollector) || playerControl.Is(RoleEnum.Death)
                       || playerControl.Is(RoleEnum.Werewolf)
                       || playerControl.Is(RoleEnum.Doomsayer)
                       || playerControl.Is(RoleEnum.Vampire);

            // If the console is not a sabotage repair console
            if (flag && !__instance.AllowImpostor)
            {
                __result = float.MaxValue;
                return false;
            }

            return true;
        }
    }
}
}