using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Fraud : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Fraud(PlayerControl player) : base(player)
    {
        Name = "Fraud";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Fraud;
        RoleType = RoleEnum.Fraud;
        AddToRoleHistory(RoleType);
    }
}
}