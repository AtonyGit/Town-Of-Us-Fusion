using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Captain : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public DateTime StartingCooldown { get; set; }
    public Captain(PlayerControl player) : base(player)
    {
        Name = "Captain";
        ImpostorText = () => "placeholder1";
        TaskText = () => "placeholder2";
        AlignmentText = () => "Crew Sovereign";
        Color = Patches.Colors.Captain;
        RoleType = RoleEnum.Captain;
        AddToRoleHistory(RoleType);
    }
}
}