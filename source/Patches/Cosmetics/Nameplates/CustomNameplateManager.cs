using System.Collections.Generic;
using System.IO;
using Reactor.Utilities.Extensions;
using UnityEngine;
namespace TownOfUsFusion.Cosmetics.CustomNameplates;

public static class CustomNameplateManager
{
    public static readonly List<CustomNameplate> UnregisteredNameplates = [];
    public static readonly List<NamePlateData> RegisteredNameplates = [];
    public static readonly Dictionary<string, NameplateExtension> CustomNameplateRegistry = [];
    public static readonly Dictionary<string, NamePlateViewData> CustomNameplateViewDatas = [];

    public static NamePlateData CreateNameplateBehaviour(CustomNameplate cn)
    {
        var path = Path.Combine(TownOfUsFusion.Nameplates, $"{cn.ID}.png");

        if (cn.StreamOnly)
            path = Path.Combine(TownOfUsFusion.Nameplates, "Stream", $"{cn.ID}.png");
        else if (cn.TestOnly)
            path = Path.Combine(TownOfUsFusion.Nameplates, "Test", $"{cn.ID}.png");

        var nameplate = ScriptableObject.CreateInstance<NamePlateData>().DontDestroy();

        var viewData = ScriptableObject.CreateInstance<NamePlateViewData>().DontDestroy();
        viewData.Image = CustomCosmeticsManager.CreateCosmeticSprite(path, CosmeticTypeEnum.Nameplate);

        Debug.Log($"LOADING NAMEPLATE: {cn.Name}");
        nameplate.PreviewCrewmateColor = false;
        nameplate.name = cn.Name;
        nameplate.displayOrder = 99;
        nameplate.ProductId = "customNameplate_" + cn.Name.Replace(' ', '_');
        nameplate.ChipOffset = new(0f, 0.2f);
        nameplate.Free = true;
        nameplate.NotInStore = true;
        nameplate.ViewDataRef = new(viewData.Pointer);
        nameplate.CreateAddressableAsset();

        var extend = new NameplateExtension()
        {
            Artist = cn.Artist ?? "Unknown",
            StreamOnly = cn.StreamOnly,
            TestOnly = cn.TestOnly
        };
        CustomNameplateRegistry.TryAdd(nameplate.name, extend);
        CustomNameplateViewDatas.TryAdd(nameplate.ProductId, viewData);
        return nameplate;
    }
/*
    public static List<string> GenerateDownloadList(List<CustomNameplate> nameplates)
    {
        var markedfordownload = new List<string>();

        foreach (var nameplate in nameplates)
        {
            if (nameplate.StreamOnly && !TownOfUsFusion.IsStream)
                continue;

            if (!File.Exists(Path.Combine("TownOfUsFusion.Resources.Nameplates", $"{nameplate.ID}.png")))
                markedfordownload.Add(nameplate.ID);
        }

        return markedfordownload;
    }
*/
    public static NameplateExtension GetExtention(this NamePlateData nameplate)
    {
        if (!nameplate)
            return null;

        CustomNameplateRegistry.TryGetValue(nameplate.name, out var ret);
        return ret;
    }
}