using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Tempest : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Tempest(PlayerControl player) : base(player)
    {
        Name = "Tempest";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Tempest;
        RoleType = RoleEnum.Tempest;
        AddToRoleHistory(RoleType);
    }
}
}