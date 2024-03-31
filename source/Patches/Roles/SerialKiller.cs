using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class SerialKiller : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public SerialKiller(PlayerControl player) : base(player)
    {
        Name = "Serial Killer";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        Color = Patches.Colors.SerialKiller;
        RoleType = RoleEnum.SerialKiller;
        AddToRoleHistory(RoleType);
    }
}
}