using HarmonyLib;
using System.Linq;
using UnityEngine;
using TownOfUsFusion.ImpostorRoles.TraitorMod;

namespace TownOfUsFusion.Patches
{
    [HarmonyPatch(typeof(GameData))]
public class DisconnectHandler
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(GameData.HandleDisconnect), typeof(PlayerControl), typeof(DisconnectReasons))]
    public static void Prefix([HarmonyArgument(0)] PlayerControl player)
    {

        if (CustomGameOptions.GameMode == GameMode.Cultist)
        {
            if (player.Is(RoleEnum.Necromancer) || player.Is(RoleEnum.Whisperer))
            {
                foreach (var player2 in PlayerControl.AllPlayerControls)
                {
                    if (player2.Is(Faction.Impostors)) Utils.MurderPlayer(player2, player2, true);
                }
            }
        } else
            if (player.Is(RoleEnum.NeoNecromancer))
            {
                foreach (var player2 in PlayerControl.AllPlayerControls)
                {
                    if (!player2.Is(RoleEnum.NeoNecromancer) && player2.Is(Faction.NeutralNecro)) Utils.MurderPlayer(player2, player2, true);
                }
            }
        else
        {
            if (AmongUsClient.Instance.AmHost)
            {
                if (player == SetTraitor.WillBeTraitor)
                {
                    var toChooseFrom = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Crewmates) &&
                        !x.Is(AllianceEnum.Recruit) && !x.Is(AllianceEnum.Lover) && !x.Is(AllianceEnum.Crewpocalypse) && !x.Is(AllianceEnum.Crewpostor) && !x.Is(AllianceEnum.Egotist) && !x.Data.IsDead && !x.Data.Disconnected && !x.IsExeTarget()).ToList();
                    if (toChooseFrom.Count == 0) return;
                    var rand = Random.RandomRangeInt(0, toChooseFrom.Count);
                    var pc = toChooseFrom[rand];

                    SetTraitor.WillBeTraitor = pc;

                    Utils.Rpc(CustomRPC.SetTraitor, pc.PlayerId);
                }
            }
        }
    }
}
}