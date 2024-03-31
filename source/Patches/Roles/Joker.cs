using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Joker : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Joker(PlayerControl player) : base(player)
    {
        Name = "Joker";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Joker;
        RoleType = RoleEnum.Joker;
        AddToRoleHistory(RoleType);
    }
}
}