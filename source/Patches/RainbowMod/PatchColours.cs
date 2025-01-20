using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace TownOfUsFusion.RainbowMod
{
    [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetString),
        new[] { typeof(StringNames), typeof(Il2CppReferenceArray<Il2CppSystem.Object>) })]
    public class PatchColours
    {
        public static bool Prefix(ref string __result, [HarmonyArgument(0)] StringNames name)
        {
            var newResult = (int)name switch
            {
            // TOU FUSION COLORS
            999800 => "Snow",
            999801 => "Turquoise",
            999802 => "Nacho",
            999803 => "Galacta",
            999804 => "Charcoal",
            999805 => "Violet",
            999806 => "Denim",
            999807 => "Air Force",
            999808 => "Wood",
            999809 => "Dandelion",
            999810 => "Amber",
            999811 => "Cotton Candy",
            999812 => "Aqua",
            999813 => "Lemon",
            999814 => "Apple",
            999815 => "Blood",
            999816 => "Grass",
            999817 => "Mandarin",
            999818 => "Pearl",
            999819 => "Ash",
            999820 => "Midnight",
            999821 => "Steel",
            
            999822 => "Mahogany",
            999823 => "Salmon",
            999824 => "Pear",
            999825 => "Wine",
            999826 => "True Red",
            999827 => "Silver",
            999828 => "Shimmer",
            999829 => "Crimson",
            999830 => "Crow",
            // TOU COLORS
            999983 => "Watermelon",
            999984 => "Chocolate",
            999985 => "Sky Blue",
            999986 => "Beige",
            999987 => "Magenta",
            999988 => "Sea Green",
            999989 => "Lilac",
            999990 => "Olive",
            999991 => "Azure",
            999992 => "Plum",
            999993 => "Jungle",
            999994 => "Mint",
            999995 => "Chartreuse",
            999996 => "Macau",
            999997 => "Tawny",
            999998 => "Gold",
            999999 => "Rainbow",
            1000000 => "Galaxy",
            1000001 => "Fire",
            1000002 => "Acid",
            1000003 => "Monochrome",
                _ => null
            };
            if (newResult != null)
            {
                __result = newResult;
                return false;
            }

            return true;
        }
    }
}
