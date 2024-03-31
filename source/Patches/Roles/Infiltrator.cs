using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Infiltrator : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Infiltrator(PlayerControl player) : base(player)
    {
        Name = "Infiltrator";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Infiltrator;
        RoleType = RoleEnum.Infiltrator;
        AddToRoleHistory(RoleType);
    }
}
}