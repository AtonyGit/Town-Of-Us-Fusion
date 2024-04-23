using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Mage : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Mage(PlayerControl player) : base(player)
    {
        Name = "Mage";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        AlignmentText = () => "Crew Support";
        Color = Patches.Colors.Mage;
        RoleType = RoleEnum.Mage;
        AddToRoleHistory(RoleType);
    }
}
}