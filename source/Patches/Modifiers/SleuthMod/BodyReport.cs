using HarmonyLib;
using TownOfUsFusion.Roles.Modifiers;

namespace TownOfUsFusion.Modifiers
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CmdReportDeadBody))]
public class BodyReport
{
    private static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] GameData.PlayerInfo info)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (!PlayerControl.LocalPlayer.Is(ModifierEnum.Sleuth)) return;

        Modifier.GetModifier<Sleuth>(PlayerControl.LocalPlayer).Reported.Add(info.PlayerId);
    }
}
}