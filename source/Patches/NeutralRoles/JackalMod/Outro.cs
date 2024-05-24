using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Patches;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.JackalMod
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
public class Outro
{
    public static void Postfix(EndGameManager __instance)
    {
        if (CustomGameOptions.NeutralEvilWinEndsGame)
        {
            if (Role.GetRoles(RoleEnum.Jester).Any(x => ((Jester)x).VotedOut)) return;
            if (Role.GetRoles(RoleEnum.Executioner).Any(x => ((Executioner)x).TargetVotedOut)) return;
            if (Role.GetRoles(RoleEnum.Doomsayer).Any(x => ((Doomsayer)x).WonByGuessing)) return;
        }
        if (Role.GetRoles(RoleEnum.Joker).Any(x => ((Joker)x).TargetVotedOut)) return;
        var role = Role.AllRoles.FirstOrDefault(x =>
            (x.RoleType == RoleEnum.Jackal || x.IsAlliance == AllianceEnum.Recruit) && Role.JackalWins);
        if (role == null) return;
        PoolablePlayer[] array = Object.FindObjectsOfType<PoolablePlayer>();
        foreach (var player in array) player.NameText().text = Utils.GradientColorText("B7B9BA", "5E576B", player.NameText().text);
        __instance.BackgroundBar.material.color = Colors.Recruit;
        var text = Object.Instantiate(__instance.WinText);
        text.text = Utils.GradientColorText("B7B9BA", "5E576B", "Jackals Win!");
        text.color = role.Color;
        var pos = __instance.WinText.transform.localPosition;
        pos.y = 1.5f;
        text.transform.position = pos;
        text.text = $"<size=4>{text.text}</size>";
    }
}
}