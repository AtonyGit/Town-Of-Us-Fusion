using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace TownOfUsFusion.Roles
{
    public class Cannibal : Role
{
    public Dictionary<byte, ArrowBehaviour> BodyArrows = new Dictionary<byte, ArrowBehaviour>();
    public int EatNeed { get; set; } = CustomGameOptions.BodiesNeededToWin;
    public bool Eaten { get; set; } = false;
    public bool EatWin => EatNeed == 0;
//    public bool CanEat => !Eaten;
    public Cannibal(PlayerControl player) : base(player)
    {
        Name = "Cannibal";
        ImpostorText = () => "Feast on The Flesh Of The Dead";
        TaskText = () => !Eaten ? $"You are satiated. Consume {EatNeed} bodies\nFake Tasks:" : $"Eat {EatNeed} {(EatNeed == 1 ? "one more body!" : "bodies in total!")}";
        Color = Patches.Colors.Cannibal;
        RoleType = RoleEnum.Cannibal;
        AddToRoleHistory(RoleType);
        Faction = Faction.NeutralChaos;
    }

    public DeadBody CurrentTarget;

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
    internal override bool GameEnd(LogicGameFlowNormal __instance)
    {
        if (Player.Data.IsDead) return true;
        if (!EatWin) return true;
        Utils.EndGame();
        return false;
    }
    public void Wins()
    {
        //EatWin = true;
        //System.Console.WriteLine("Reached Here - Jester edition");
    }
}
}