using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Pirate : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Pirate(PlayerControl player) : base(player)
    {
        Name = "Pirate";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Pirate;
        RoleType = RoleEnum.Pirate;
        AddToRoleHistory(RoleType);
    }
}
}