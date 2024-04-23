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
                {27, "ligher"},// dandelion
                {28, "lighter"},// amber
                {29, "lighter"},// cotton candy
                {20, "lighter"},// aqua
                {31, "lighter"},// lemon
                {32, "ligher"},// apple
                {33, "darker"},// blood
                {34, "darker"},// grass
                {35, "lighter"},// mandarin
                {36, "lighter"},// glass
                {37, "darker"},// ash
                {38, "darker"},// midnight
                {39, "darker"},// steel
                
                {40, "darker"},// mahogany
                {41, "lighter"},// salmon
                {42, "lighter"},// pear
                {43, "darker"},// wine
                {44, "lighter"},// true red
                {45, "lighter"},// silver
                {46, "lighter"},// shimmer
                {47, "darker"},// crimson
                {48, "darker"},// crow
                // TOU COLORS
                {49, "darker"},// watermelon
                {50, "darker"},// chocolate
                {51, "lighter"},// sky blue
                {52, "lighter"},// beige
                {53, "darker"},// magenta
                {54, "lighter"},// Sea Green
                {55, "lighter"},// lilac
                {56, "darker"},// olive
                {57, "lighter"},// azure
                {58, "darker"},// plum
                {59, "darker"},// jungle
                {60, "lighter"},// mint
                {61, "lighter"},// chartreuse
                {62, "darker"},// macau
                {63, "darker"},// tawny
                {64, "lighter"},// gold
                {65, "lighter"},// rainbow
                {66, "darker"},// galaxy
                {67, "lighter"},// fire
                {68, "lighter"},// acid
            };
        var typeOfColor = colors[br.Killer.GetDefaultOutfit().ColorId];
        return
            $"Body Report: The killer appears to be a {typeOfColor} color. (Killed {Math.Round(br.KillAge / 1000)}s ago)";
    }
}
}
