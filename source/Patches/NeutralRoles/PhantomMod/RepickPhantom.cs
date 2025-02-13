using HarmonyLib;
using System.Linq;
using UnityEngine;

namespace TownOfUsFusion.NeutralRoles.PhantomMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public class RepickPhantom
{
    private static void Postfix(HudManager __instance)
    {
        if (PlayerControl.AllPlayerControls.Count <= 1) return;
        if (PlayerControl.LocalPlayer == null) return;
        if (PlayerControl.LocalPlayer.Data == null) return;
        if (PlayerControl.LocalPlayer != SetPhantom.WillBePhantom) return;
        if (PlayerControl.LocalPlayer.Data.IsDead) return;
        if (!PlayerControl.LocalPlayer.Is(Faction.NeutralKilling) && !PlayerControl.LocalPlayer.Is(Faction.NeutralSentinel) && !PlayerControl.LocalPlayer.Is(Faction.ChaosSentinel) && !PlayerControl.LocalPlayer.Is(Faction.NeutralChaos) && !PlayerControl.LocalPlayer.Is(Faction.NeutralEvil) && !PlayerControl.LocalPlayer.Is(Faction.NeutralBenign))
        {
            var toChooseFromAlive = PlayerControl.AllPlayerControls.ToArray().Where(x => (x.Is(Faction.NeutralKilling) || x.Is(Faction.NeutralChaos) || x.Is(Faction.NeutralSentinel) || x.Is(Faction.ChaosSentinel) || x.Is(Faction.NeutralEvil) || x.Is(Faction.NeutralBenign))
            && !x.Is(AllianceEnum.Lover) && !x.Is(AllianceEnum.Crewpocalypse) && !x.Is(AllianceEnum.Crewpostor) && !x.Is(AllianceEnum.Egotist) && !x.Is(AllianceEnum.Recruit) && !x.Data.Disconnected).ToList();
            if (toChooseFromAlive.Count == 0)
            {
                SetPhantom.WillBePhantom = null;

                Utils.Rpc(CustomRPC.SetPhantom, byte.MaxValue);
            }
            else
            {
                var rand2 = Random.RandomRangeInt(0, toChooseFromAlive.Count);
                var pc2 = toChooseFromAlive[rand2];

                SetPhantom.WillBePhantom = pc2;

                Utils.Rpc(CustomRPC.SetPhantom, pc2.PlayerId);
            }
            return;
        }
        var toChooseFrom = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Is(Faction.Crewmates) && !x.Is(Faction.Impostors) && !x.Is(Faction.ImpSentinel) && !x.Is(Faction.CrewSentinel)
            && !x.Is(AllianceEnum.Lover) && !x.Is(AllianceEnum.Crewpocalypse) && !x.Is(AllianceEnum.Crewpostor) && !x.Is(AllianceEnum.Egotist) && !x.Is(AllianceEnum.Recruit) && x.Data.IsDead && !x.Data.Disconnected).ToList();
        if (toChooseFrom.Count == 0) return;
        var rand = Random.RandomRangeInt(0, toChooseFrom.Count);
        var pc = toChooseFrom[rand];

        SetPhantom.WillBePhantom = pc;

        Utils.Rpc(CustomRPC.SetPhantom, pc.PlayerId);
        return;
    }
}
}