using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.JokerMod
{
    public enum JokerOnTargetDead
{
    Crew,
    Amnesiac,
    Survivor,
    Jester,
    Joker
}

[HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class JkTargetColor
{
    private static void UpdateMeeting(MeetingHud __instance, Joker role)
    {
        foreach (var player in __instance.playerStates)
            if (player.TargetPlayerId == role.target.PlayerId)
                player.NameText.color = Color.black;
    }

    private static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Joker)) return;
        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!Role.GetRole<Joker>(PlayerControl.LocalPlayer).UsedAbility) return;

        var role = Role.GetRole<Joker>(PlayerControl.LocalPlayer);

        if (MeetingHud.Instance != null) UpdateMeeting(MeetingHud.Instance, role);

        if (role.target && role.target.nameText()) role.target.nameText().color = Color.black;
        if (!role.target.Data.IsDead && !role.target.Data.Disconnected) return;
        if (role.TargetVotedOut) return;

        Utils.Rpc(CustomRPC.JokerToJester, PlayerControl.LocalPlayer.PlayerId);

        JkToJes(PlayerControl.LocalPlayer);
    }

    public static void JkToJes(PlayerControl player)
    {
        player.myTasks.RemoveAt(0);
        Role.RoleDictionary.Remove(player.PlayerId);


        if (CustomGameOptions.JokerOnTargetDead == JokerOnTargetDead.Jester)
        {
            var jester = new Jester(player);
            jester.SpawnedAs = false;
            jester.RegenTask();
        }
        else if (CustomGameOptions.JokerOnTargetDead == JokerOnTargetDead.Amnesiac)
        {
            var amnesiac = new Amnesiac(player);
            amnesiac.SpawnedAs = false;
            amnesiac.RegenTask();
        }
        else if (CustomGameOptions.JokerOnTargetDead == JokerOnTargetDead.Survivor)
        {
            var surv = new Survivor(player);
            surv.SpawnedAs = false;
            surv.RegenTask();
        }
        else if (CustomGameOptions.JokerOnTargetDead == JokerOnTargetDead.Joker)
        {
            var joker = new Joker(player);
            joker.UsedAbility = false;
            joker.RegenTask();
        }
        else
        {
            new Crewmate(player);
        }
    }
}
}