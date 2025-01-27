using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TownOfUsFusion.Patches;
using static TownOfUsFusion.Cosmetics.CustomNameplates.CustomNameplateManager;
using Cpp2IL.Core.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Loaders;

public class NameplateLoader : AssetLoader<CustomNameplate>
{
    public override string DirectoryInfo => "TownOfUsFusion.Resources.Nameplates";
    public override bool Downloading => true;
    public override string Manifest => "Nameplates";
    public override string FileExtension => "png";

    public static NameplateLoader Instance { get; set; }


    public override IEnumerator AfterLoading(object response)
    {
        // if (TownOfUsFusion.IsStream)
        // {
        //     var filePath = Path.Combine(TownOfUsFusion.Nameplates, "Stream", "Nameplates.json");

        //     if (File.Exists(filePath))
        //     {
        //         var data = JsonSerializer.Deserialize<List<CustomNameplate>>(File.ReadAllText(filePath));
        //         data.ForEach(x => x.StreamOnly = true);
        //         UnregisteredNameplates.AddRange(data);
        //     }
        // }

        var cache = UnregisteredNameplates.Clone();
        var time = 0f;

        for (var i = 0; i < cache.Count; i++)
        {
            var file = cache[i];
            RegisteredNameplates.Add(CreateNameplateBehaviour(file));
            UnregisteredNameplates.Remove(file);
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0f;
                UpdateSplashPatch.SetText($"Loading Nameplates ({i + 1}/{cache.Count})");
                yield return Utils.EndFrame();
            }
        }

        cache.Clear();
        yield break;
    }
}