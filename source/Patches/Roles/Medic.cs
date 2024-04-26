using System.Collections.Generic;
using UnityEngine;
using System;

namespace TownOfUsFusion.Roles
{
    public class Medic : Role
{
    public readonly List<GameObject> Buttons = new List<GameObject>();
    public Dictionary<int, string> LightDarkColors = new Dictionary<int, string>();
    public DateTime StartingCooldown { get; set; }
    public Medic(PlayerControl player) : base(player)
    {
        Name = "Medic";
        ImpostorText = () => "Create A Shield To Protect A Crewmate";
        TaskText = () => "Protect a crewmate with a shield";
        AlignmentText = () => "Crew Protective";
        Color = Patches.Colors.Medic;
        StartingCooldown = DateTime.UtcNow;
        RoleType = RoleEnum.Medic;
        AddToRoleHistory(RoleType);
        ShieldedPlayer = null;

        LightDarkColors.Add(0, "darker"); // Red
        LightDarkColors.Add(1, "darker"); // Blue
        LightDarkColors.Add(2, "darker"); // Green
        LightDarkColors.Add(3, "lighter"); // Pink
        LightDarkColors.Add(4, "lighter"); // Orange
        LightDarkColors.Add(5, "lighter"); // Yellow
        LightDarkColors.Add(6, "darker"); // Black
        LightDarkColors.Add(7, "lighter"); // White
        LightDarkColors.Add(8, "darker"); // Purple
        LightDarkColors.Add(9, "darker"); // Brown
        LightDarkColors.Add(10, "lighter"); // Cyan
        LightDarkColors.Add(11, "lighter"); // Lime
        LightDarkColors.Add(12, "darker"); // Maroon
        LightDarkColors.Add(13, "lighter"); // Rose
        LightDarkColors.Add(14, "lighter"); // Banana
        LightDarkColors.Add(15, "darker"); // Grey
        LightDarkColors.Add(16, "darker"); // Tan
        LightDarkColors.Add(17, "lighter"); // Coral
        // TOU FUSION COLORS
        LightDarkColors.Add(18, "lighter"); // Snow
        LightDarkColors.Add(19, "lighter"); // Turquoise
        LightDarkColors.Add(20, "lighter"); // Nacho
        LightDarkColors.Add(21, "darker"); // Galacta
        LightDarkColors.Add(22, "darker"); // Charcoal
        LightDarkColors.Add(23, "darker"); // Violet
        LightDarkColors.Add(24, "darker"); // Denim
        LightDarkColors.Add(25, "lighter"); // Air Force
        LightDarkColors.Add(26, "darker"); // Wood
        LightDarkColors.Add(27, "lighter"); // Dandelion
        LightDarkColors.Add(28, "lighter"); // Amber
        LightDarkColors.Add(29, "lighter"); // Cotton Candy
        LightDarkColors.Add(30, "lighter"); // Aqua
        LightDarkColors.Add(31, "lighter"); // Lemon
        LightDarkColors.Add(32, "lighter"); // Apple
        LightDarkColors.Add(33, "darker"); // Blood
        LightDarkColors.Add(34, "darker"); // Grass
        LightDarkColors.Add(35, "lighter"); // Mandarin
        LightDarkColors.Add(36, "lighter"); // Glass
        LightDarkColors.Add(37, "darker"); // Ash
        LightDarkColors.Add(38, "darker"); // Midnight
        LightDarkColors.Add(39, "darker"); // Steel

        
        LightDarkColors.Add(40, "darker"); // Mahogany
        LightDarkColors.Add(41, "lighter"); // Salmon
        LightDarkColors.Add(42, "lighter"); // Pear
        LightDarkColors.Add(43, "darker"); // Wine
        LightDarkColors.Add(44, "lighter"); // True Red
        LightDarkColors.Add(45, "lighter"); // Silver
        LightDarkColors.Add(46, "lighter"); // Shimmer
        LightDarkColors.Add(47, "darker"); // Crimson
        LightDarkColors.Add(48, "darker"); // Crow

        // TOU COLORS
        LightDarkColors.Add(49, "darker"); // Watermelon
        LightDarkColors.Add(50, "darker"); // Chocolate
        LightDarkColors.Add(51, "lighter"); // Sky Blue
        LightDarkColors.Add(52, "lighter"); // Biege
        LightDarkColors.Add(53, "darker"); // Magenta
        LightDarkColors.Add(54, "lighter"); // Sea Green
        LightDarkColors.Add(55, "lighter"); // Lilac
        LightDarkColors.Add(56, "darker"); // Olive
        LightDarkColors.Add(57, "lighter"); // Azure
        LightDarkColors.Add(58, "darker"); // Plum
        LightDarkColors.Add(59, "darker"); // Jungle
        LightDarkColors.Add(60, "lighter"); // Mint
        LightDarkColors.Add(61, "lighter"); // Chartreuse
        LightDarkColors.Add(62, "darker"); // Macau
        LightDarkColors.Add(63, "darker"); // Tawny
        LightDarkColors.Add(64, "lighter"); // Gold
        LightDarkColors.Add(65, "lighter"); // Rainbow
        LightDarkColors.Add(66, "darker"); // Galaxy
        LightDarkColors.Add(67, "lighter"); // Fire
        LightDarkColors.Add(68, "lighter"); // Acid
        LightDarkColors.Add(69, "lighter"); // Monochrome
    }
    public float StartTimer()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - StartingCooldown;
        var num = 10000f;
        var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
        if (flag2) return 0;
        return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
    }

    public PlayerControl ClosestPlayer;
    public bool UsedAbility { get; set; } = false;
    public PlayerControl ShieldedPlayer { get; set; }
    public PlayerControl exShielded { get; set; }
}
}