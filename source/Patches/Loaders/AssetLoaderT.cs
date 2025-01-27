using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Patches;

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

        var assembly = Assembly.GetExecutingAssembly();
        var jsonText = assembly.GetManifestResourceStream($"{DirectoryInfo}.{Manifest}.json");

        var response = JsonSerializer.Deserialize<List<T>>(Encoding.UTF8.GetString(jsonText.ReadFully()), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            });

        UpdateSplashPatch.SetText($"Preloading {Manifest}");
        yield return AfterLoading(response);
        yield return Utils.EndFrame();
        yield break;
    }
}