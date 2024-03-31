using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class SoulCollector : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public SoulCollector(PlayerControl player) : base(player)
    {
        Name = "Soul Collector";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.SoulCollector;
        RoleType = RoleEnum.SoulCollector;
        AddToRoleHistory(RoleType);
    }
}
}