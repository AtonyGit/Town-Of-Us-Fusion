using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Baker : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Baker(PlayerControl player) : base(player)
    {
        Name = "Baker";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Baker;
        RoleType = RoleEnum.Baker;
        AddToRoleHistory(RoleType);
    }
}
}