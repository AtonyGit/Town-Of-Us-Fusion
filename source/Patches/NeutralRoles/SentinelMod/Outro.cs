using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.SentinelMod
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
public class Outro
{
    public static void Postfix(EndGameManager __instance)
    {
        var role = Role.AllRoles.FirstOrDefault(x =>
            x.RoleType == RoleEnum.Sentinel && ((Sentinel)x).SentinelWins);
        if (role == null) return;
        PoolablePlayer[] array = Object.FindObjectsOfType<PoolablePlayer>();
        foreach (var player in array) player.NameText().text = role.ColorString + player.NameText().text + "</color>";
        __instance.BackgroundBar.material.color = role.Color;
        var text = Object.Instantiate(__instance.WinText);
        text.text = "Sentinel Wins!";
        text.color = role.Color;
        var pos = __instance.WinText.transform.localPosition;
        pos.y = 1.5f;
        text.transform.position = pos;
        text.text = $"<size=4>{text.text}</size>";
    }
}
}