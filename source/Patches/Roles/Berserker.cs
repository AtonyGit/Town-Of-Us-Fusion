using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Berserker : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Berserker(PlayerControl player) : base(player)
    {
        Name = "Berserker";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.Berserker;
        RoleType = RoleEnum.Berserker;
        AddToRoleHistory(RoleType);
    }
}
}