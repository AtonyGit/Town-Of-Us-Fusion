using System;
using System.Collections.Generic;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.CrewmateRoles.MedicMod
{
    public class DeadPlayer
{
    public byte KillerId { get; set; }
    public byte PlayerId { get; set; }
    public DateTime KillTime { get; set; }
}

//body report class for when medic reports a body
public class BodyReport
{
    public PlayerControl Killer { get; set; }
    public PlayerControl Reporter { get; set; }
    public PlayerControl Body { get; set; }
    public float KillAge { get; set; }

    public static string ParseBodyReport(BodyReport br)
    {
        //System.Console.WriteLine(br.KillAge);
        if (br.KillAge > CustomGameOptions.MedicReportColorDuration * 1000)
            return
                $"Body Report: The corpse is too old to gain information from. (Killed {Math.Round(br.KillAge / 1000)}s ago)";

        if (br.Killer.PlayerId == br.Body.PlayerId)
            return
                $"Body Report: The kill appears to have been a suicide! (Killed {Math.Round(br.KillAge / 1000)}s ago)";

        if (br.KillAge < CustomGameOptions.MedicReportNameDuration * 1000)
            return
                $"Body Report: The killer appears to be {br.Killer.Data.PlayerName}! (Killed {Math.Round(br.KillAge / 1000)}s ago)";

        var colors = new Dictionary<int, string>
            {
                {0, "darker"},// red
                {1, "darker"},// blue
                {2, "darker"},// green
                {3, "lighter"},// pink
                {4, "lighter"},// orange
                {5, "lighter"},// yellow
                {6, "darker"},// black
                {7, "lighter"},// white
                {8, "darker"},// purple
                {9, "darker"},// brown
                {10, "lighter"},// cyan
                {11, "lighter"},// lime
                {12, "darker"},// maroon
                {13, "lighter"},// rose
                {14, "lighter"},// banana
                {15, "darker"},// gray
                {16, "darker"},// tan
                {17, "lighter"},// coral
                // TOU FUSION COLORS
                {18, "lighter"},// snow white
                {19, "lighter"},// turquoise
                {20, "lighter"},// nacho
                {21, "darker"},// galacta
                {22, "darker"},// charcoal
                {23, "lighter"},// violet
                {24, "lighter"},// denim
                {25, "lighter"},// air force
                {26, "darker"},// wood
                {27, "darker"},// dandelion
                {28, "darker"},// amber
                // TOU COLORS
                {29, "darker"},// watermelon
                {30, "darker"},// chocolate
                {31, "lighter"},// sky blue
                {32, "lighter"},// beige
                {33, "darker"},// magenta
                {34, "lighter"},// Sea Green
                {35, "lighter"},// lilac
                {36, "darker"},// olive
                {37, "lighter"},// azure
                {38, "darker"},// plum
                {39, "darker"},// jungle
                {40, "lighter"},// mint
                {41, "lighter"},// chartreuse
                {42, "darker"},// macau
                {43, "darker"},// tawny
                {44, "lighter"},// gold
                {45, "lighter"},// rainbow
            };
        var typeOfColor = colors[br.Killer.GetDefaultOutfit().ColorId];
        return
            $"Body Report: The killer appears to be a {typeOfColor} color. (Killed {Math.Round(br.KillAge / 1000)}s ago)";
    }
}
}
