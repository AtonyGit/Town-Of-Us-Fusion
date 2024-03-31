using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Bartender : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Bartender(PlayerControl player) : base(player)
    {
        Name = "Bartender";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Bartender;
        RoleType = RoleEnum.Bartender;
        AddToRoleHistory(RoleType);
    }
}
}