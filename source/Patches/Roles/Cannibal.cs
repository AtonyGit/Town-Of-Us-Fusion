using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace TownOfUsFusion.Roles
{
    public class Cannibal : Role
{
    public KillButton _eatButton;
    public Dictionary<byte, ArrowBehaviour> BodyArrows = new Dictionary<byte, ArrowBehaviour>();
    public int EatNeed { get; set; } = CustomGameOptions.BodiesNeededToWin;
    public bool Eaten { get; set; }
    public bool EatWin => EatNeed == 0;
//    public bool CanEat => !Eaten;
    public Cannibal(PlayerControl player) : base(player)
    {
        Name = "Cannibal";
        ImpostorText = () => "Feast on The Flesh Of The Dead";
        TaskText = () => Eaten ? "You are satiated" : $"Eat {EatNeed} {(EatNeed == 1 ? "one more body!" : "bodies in total!")}\nFake Tasks:";
        Color = Patches.Colors.Cannibal;
        RoleType = RoleEnum.Cannibal;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralChaos;
    }

    public DeadBody CurrentTarget;

    public KillButton EatButton
    {
        get => _eatButton;
        set
        {
            _eatButton = value;
            ExtraButtons.Clear();
            ExtraButtons.Add(value);
        }
    }
    protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
    {
        var cannibalTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
        cannibalTeam.Add(PlayerControl.LocalPlayer);
        __instance.teamToShow = cannibalTeam;
    }

    public void DestroyArrow(byte targetPlayerId)
    {
        var arrow = BodyArrows.FirstOrDefault(x => x.Key == targetPlayerId);
        if (arrow.Value != null)
            Object.Destroy(arrow.Value);
        if (arrow.Value.gameObject != null)
            Object.Destroy(arrow.Value.gameObject);
        BodyArrows.Remove(arrow.Key);
    }
    internal override bool NeutralWin(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead || Player.Data.Disconnected) return true;

        if (EatWin)
        {
            Utils.Rpc(CustomRPC.CannibalWin, Player.PlayerId);
            Wins();
            Utils.EndGame();

            return false;
        }

        return false;
    }
    public void Wins()
    {
        //EatWin = true;
        //System.Console.WriteLine("Reached Here - Jester edition");
    }
}
}