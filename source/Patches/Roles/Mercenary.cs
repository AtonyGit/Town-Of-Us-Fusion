using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Mercenary : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Mercenary(PlayerControl player) : base(player)
    {
        Name = "Mercenary";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Mercenary;
        RoleType = RoleEnum.Mercenary;
        AddToRoleHistory(RoleType);
    }
}
}