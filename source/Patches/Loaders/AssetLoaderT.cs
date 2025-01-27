using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Patches;
using UnityEngine;

namespace TownOfUsFusion.Loaders;

public abstract class AssetLoader<T> : AssetLoader where T : Asset
{
    private bool Running;

    public IEnumerator CoFetch()
    {
        if (Running)
            yield break;

        Running = true;
        UpdateSplashPatch.SetText($"Fetching {Manifest}");
        yield return Utils.EndFrame();

        var jsonText = Utils.ReadDiskText($"{Manifest}.json", DirectoryInfo);
        var jsonLocation = Path.Combine(DirectoryInfo, $"{Manifest}.json");
        var response = JsonSerializer.Deserialize<List<T>>(jsonText);
        Debug.Log($"LOADING JSON FROM: {jsonLocation}");
        //Debug.Log($"RAWJSON: {jsonText}");
        
        if (Downloading)
        {
            UpdateSplashPatch.SetText($"Downloading {Manifest}");
            yield return BeginDownload(response);
        }

        UpdateSplashPatch.SetText($"Preloading {Manifest}");
        yield return AfterLoading(response);
        yield return Utils.EndFrame();
        yield break;
    }
}