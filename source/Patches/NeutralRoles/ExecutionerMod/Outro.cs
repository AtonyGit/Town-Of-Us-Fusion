using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.ExecutionerMod
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
public static class Outro
{
    public static void Postfix(EndGameManager __instance)
    {
        if (!CustomGameOptions.NeutralEvilWinEndsGame) return;
        var role = Role.AllRoles.FirstOrDefault(x =>
            x.RoleType == RoleEnum.Executioner && ((Executioner)x).TargetVotedOut);
        if (role == null) return;
        PoolablePlayer[] array = Object.FindObjectsOfType<PoolablePlayer>();
        array[0].NameText().text = role.ColorString + array[0].NameText().text + "</color>";
        __instance.BackgroundBar.material.color = role.Color;
        var text = Object.Instantiate(__instance.WinText);
        text.text = "Executioner Wins!";
        text.color = role.Color;
        var pos = __instance.WinText.transform.localPosition;
        pos.y = 1.5f;
        text.transform.position = pos;
        text.text = $"<size=4>{text.text}</size>";
    }
}
}