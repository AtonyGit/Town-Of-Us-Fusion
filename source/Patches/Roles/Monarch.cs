using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Monarch : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Monarch(PlayerControl player) : base(player)
    {
        Name = "Monarch";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Monarch;
        RoleType = RoleEnum.Monarch;
        AddToRoleHistory(RoleType);
    }
}
}