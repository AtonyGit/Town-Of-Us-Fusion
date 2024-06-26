using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.CustomOption;
using AmongUs.GameOptions;
using System.Linq;
using UnityEngine;

namespace TownOfUsFusion
{
    [HarmonyPatch]
public static class GameSettings
{
    public static int SettingsPage = -1;

    [HarmonyPatch(typeof(IGameOptionsExtensions), nameof(IGameOptionsExtensions.ToHudString))]
    private static class GameOptionsDataPatch
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            return typeof(GameOptionsData).GetMethods(typeof(string), typeof(int));
        }

        private static void Postfix(ref string __result)
        {
            if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek) return;

            var builder = new StringBuilder();
            builder.AppendLine("Press Tab To Change Page");
            builder.AppendLine($"Currently Viewing Page ({SettingsPage + 2}/8)");
            if (SettingsPage == 0) builder.AppendLine("Map Settings");
            if (SettingsPage == 1) builder.AppendLine("General Mod Settings");
            else if (SettingsPage == 2) builder.AppendLine("Crewmate Settings");
            else if (SettingsPage == 3) builder.AppendLine("Neutral Settings");
            else if (SettingsPage == 4) builder.AppendLine("Impostor Settings");
            else if (SettingsPage == 5) builder.AppendLine("Modifier Settings");
            else if (SettingsPage == 6) builder.AppendLine("Alliance Settings");


            var tobedisplayed = CustomOption.CustomOption.AllOptions/*.Where(x => x.Active)*/.ToList();

                if (SettingsPage == -1)
                {
                    var num = RoleManager.Instance.AllRoles.Count(
                            x => x.Role != RoleTypes.Crewmate && x.Role != RoleTypes.Impostor && x.Role != RoleTypes.CrewmateGhost && x.Role != RoleTypes.ImpostorGhost);

                    for (int i = 0;i < num; i++)
                    {
                        __result = __result.Remove(__result.LastIndexOf("\n"), 1).Remove(__result.LastIndexOf(":"), 1);
                    }
                    builder.Append(new StringBuilder(__result));
                }
            else
            {
                foreach (var option in CustomOption.CustomOption.AllOptions.Where(x => x.Menu == (MultiMenu)SettingsPage))
                {
                    if (option.Type == CustomOptionType.Button)
                        continue;
/*
            var title = $"<b><size=160%>{$"GameSettings.Page{(int)option.Menu + 1}"}</size></b>";
            
            if (!builder.ToString().Contains(title))
                builder.AppendLine(title);*/

            var index = tobedisplayed.IndexOf(option);
            var thing = option is CustomHeaderOption ? "" : (index == tobedisplayed.Count - 1 || tobedisplayed[index + 1].Type == CustomOptionType.Header ? "┗ " : "┣ " );
                    if (option.Type == CustomOptionType.Header)
                        builder.AppendLine($"\n<b>{thing}{option.Name}</b>");
                    else
                        builder.AppendLine($" {thing}{option.Name}: {option}");
                }
            }

            __result = $"<size=1.25>{builder.ToString()}</size>";
        }
    }

    [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Update))]
    public static class Update
    {
        public static void Postfix(ref GameOptionsMenu __instance)
        {
            //__instance.GetComponentInParent<Scroller>().ContentYBounds.max = (__instance.Children.Length - 6.5f) / 2;
            __instance.GetComponentInParent<Scroller>().ContentYBounds.max = __instance.Children.Length / 2f;
        }
    }

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class LobbyPatch
    {
        public static void Postfix(HudManager __instance)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (SettingsPage > 5)
                    SettingsPage = -1;
                else
                    SettingsPage++;
            }
        }
    }
}
}