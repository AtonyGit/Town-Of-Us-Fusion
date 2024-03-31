using UnityEngine;

namespace TownOfUsFusion.RainbowMod
{
    public static class PalettePatch
{
    public static void Load()
    {
        Palette.ColorNames = new[]
        {
                StringNames.ColorRed,
                StringNames.ColorBlue,
                StringNames.ColorGreen,
                StringNames.ColorPink,
                StringNames.ColorOrange,
                StringNames.ColorYellow,
                StringNames.ColorBlack,

                StringNames.ColorWhite,
                StringNames.ColorPurple,
                StringNames.ColorBrown,
                StringNames.ColorCyan,
                StringNames.ColorLime,
                StringNames.ColorMaroon,
                StringNames.ColorRose,

                StringNames.ColorBanana,
                StringNames.ColorGray,
                StringNames.ColorTan,
                StringNames.ColorCoral,
                // TOU FUSION COLORS
                (StringNames)999800,//"Snow White",
                (StringNames)999801,//"Turquoise",
                (StringNames)999802,//"Nacho",

                (StringNames)999803,//"Galacta",
                (StringNames)999804,//"Charcoal",
                (StringNames)999805,//"Violet",
                (StringNames)999806,//"Denim",
                (StringNames)999807,//"Air Force",
                (StringNames)999808,//"Wood",
                (StringNames)999809,//"Dandelion",

                (StringNames)999810,//"Amber",
                // TOU COLORS
                (StringNames)999983,//"Watermelon",
                (StringNames)999984,//"Chocolate",
                (StringNames)999985,//"Sky Blue",
                (StringNames)999986,//"Beige",
                (StringNames)999987,//"Magenta",
                (StringNames)999988,//"Sea Green",

                (StringNames)999989,//"Lilac",
                (StringNames)999990,//"Olive",
                (StringNames)999991,//"Azure",
                (StringNames)999992,//"Plum",
                (StringNames)999993,//"Jungle",
                (StringNames)999994,//"Mint",
                (StringNames)999995,//"Chartreuse",

                (StringNames)999996,//"Macau",
                (StringNames)999997,//"Gold",
                (StringNames)999998,//"Tawny",
                (StringNames)999999,//"Rainbow",
            };
        Palette.PlayerColors = new[]
        {
                new Color32(198, 17, 17, byte.MaxValue),
                new Color32(19, 46, 210, byte.MaxValue),
                new Color32(17, 128, 45, byte.MaxValue),
                new Color32(238, 84, 187, byte.MaxValue),
                new Color32(240, 125, 13, byte.MaxValue),
                new Color32(246, 246, 87, byte.MaxValue),
                new Color32(63, 71, 78, byte.MaxValue),

                new Color32(215, 225, 241, byte.MaxValue),
                new Color32(107, 47, 188, byte.MaxValue),
                new Color32(113, 73, 30, byte.MaxValue),
                new Color32(56, byte.MaxValue, 221, byte.MaxValue),
                new Color32(80, 240, 57, byte.MaxValue),
                Palette.FromHex(6233390),
                Palette.FromHex(15515859),

                Palette.FromHex(15787944),
                Palette.FromHex(7701907),
                Palette.FromHex(9537655),
                Palette.FromHex(14115940),
                // TOU FUSION COLORS
                new Color32(255, 255, 255, byte.MaxValue),
                new Color32(31, 164, 159, byte.MaxValue),
                new Color32(242, 166, 38, byte.MaxValue),

                new Color32(46, 36, 89, byte.MaxValue),
                new Color32(50, 48, 78, byte.MaxValue),
                new Color32(128, 6, 178, byte.MaxValue),
                new Color32(54, 47, 188, byte.MaxValue),
                new Color32(62, 99, 171, byte.MaxValue),
                new Color32(174, 116, 84, byte.MaxValue),
                new Color32(255, 244, 133, byte.MaxValue),

                new Color32(255, 191, 0, byte.MaxValue),
                // TOU COLORS
                new Color32(168, 50, 62, byte.MaxValue),
                new Color32(60, 48, 44, byte.MaxValue),
                new Color32(61, 129, 255, byte.MaxValue),
                new Color32(240, 211, 165, byte.MaxValue),
                new Color32(255, 0, 127, byte.MaxValue),
                new Color32(61, 255, 181, byte.MaxValue),

                new Color32(186, 161, 255, byte.MaxValue),
                new Color32(97, 114, 24, byte.MaxValue),
                new Color32(1, 166, 255, byte.MaxValue),
                new Color32(79, 0, 127, byte.MaxValue),
                new Color32(0, 47, 0, byte.MaxValue),
                new Color32(151, 255, 151, byte.MaxValue),
                new Color32(207, 255, 0, byte.MaxValue),

                new Color32(0, 97, 93, byte.MaxValue),
                new Color32(205, 63, 0, byte.MaxValue),
                new Color32(255, 207, 0, byte.MaxValue),
                new Color32(0, 0, 0, byte.MaxValue),
            };
        Palette.ShadowColors = new[]
        {
                new Color32(122, 8, 56, byte.MaxValue),
                new Color32(9, 21, 142, byte.MaxValue),
                new Color32(10, 77, 46, byte.MaxValue),
                new Color32(172, 43, 174, byte.MaxValue),
                new Color32(180, 62, 21, byte.MaxValue),
                new Color32(195, 136, 34, byte.MaxValue),
                new Color32(30, 31, 38, byte.MaxValue),

                new Color32(132, 149, 192, byte.MaxValue),
                new Color32(59, 23, 124, byte.MaxValue),
                new Color32(94, 38, 21, byte.MaxValue),
                new Color32(36, 169, 191, byte.MaxValue),
                new Color32(21, 168, 66, byte.MaxValue),
                Palette.FromHex(4263706),
                Palette.FromHex(14586547),

                Palette.FromHex(13810825),
                Palette.FromHex(4609636),
                Palette.FromHex(5325118),
                Palette.FromHex(11813730),
                // TOU FUSION COLORS
                new Color32(163, 194, 223, byte.MaxValue),
                new Color32(4, 102, 141, byte.MaxValue),
                new Color32(185, 87, 25, byte.MaxValue),

                new Color32(15, 15, 75, byte.MaxValue),
                new Color32(12, 15, 46, byte.MaxValue),
                new Color32(78, 16, 145, byte.MaxValue),
                new Color32(21, 21, 129, byte.MaxValue),
                new Color32(12, 34, 113, byte.MaxValue),
                new Color32(129, 51, 44, byte.MaxValue),
                new Color32(241, 133, 38, byte.MaxValue),

                new Color32(231, 78, 27, byte.MaxValue),
                // TOU COLORS
                new Color32(101, 30, 37, byte.MaxValue),
                new Color32(30, 24, 22, byte.MaxValue),
                new Color32(31, 65, 128, byte.MaxValue),
                new Color32(120, 106, 83, byte.MaxValue),
                new Color32(191, 0, 95, byte.MaxValue),
                new Color32(31, 128, 91, byte.MaxValue),

                new Color32(93, 81, 128, byte.MaxValue),
                new Color32(66, 91, 15, byte.MaxValue),
                new Color32(17, 104, 151, byte.MaxValue),
                new Color32(55, 0, 95, byte.MaxValue),
                new Color32(0, 23, 0, byte.MaxValue),
                new Color32(109, 191, 109, byte.MaxValue),
                new Color32(143, 191, 61, byte.MaxValue),

                new Color32(0, 65, 61, byte.MaxValue),
                new Color32(141, 31, 0, byte.MaxValue),
                new Color32(191, 143, 0, byte.MaxValue),
                new Color32(0, 0, 0, byte.MaxValue),
            };
    }
}
}
