using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Witch : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Witch(PlayerControl player) : base(player)
    {
        Name = "Witch";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Witch;
        RoleType = RoleEnum.Witch;
        AddToRoleHistory(RoleType);
    }
}
}