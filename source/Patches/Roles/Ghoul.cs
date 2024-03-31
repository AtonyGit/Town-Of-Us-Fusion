using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Ghoul : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Ghoul(PlayerControl player) : base(player)
    {
        Name = "Ghoul";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Ghoul;
        RoleType = RoleEnum.Ghoul;
        AddToRoleHistory(RoleType);
    }
}
}