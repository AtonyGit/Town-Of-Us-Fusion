using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TownOfUsFusion.Patches;

using static TownOfUsFusion.Cosmetics.CustomHats.CustomHatManager;
using Cpp2IL.Core.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Loaders;

public class HatLoader : AssetLoader<CustomHat>
{
    public override string DirectoryInfo => "TownOfUsFusion.Resources.Hats";
    public override bool Downloading => true;
    public override string Manifest => "Hats";
    //public override string Manifest => "metadata";
    public override string FileExtension => "png";

    public static HatLoader Instance { get; set; }

    public override IEnumerator AfterLoading(object response)
    {
        UnregisteredHats.ForEach(ch => ch.Behind = ch.BackID != null || ch.BackFlipID != null);

        // if (TownOfUsFusion.IsStream)
        // {
        //     var filePath = Path.Combine(TownOfUsFusion.Hats, "Stream", "Hats.json");

        //     if (File.Exists(filePath))
        //     {
        //         var data = JsonSerializer.Deserialize<List<CustomHat>>(File.ReadAllText(filePath));
        //         data.ForEach(x => x.StreamOnly = true);
        //         UnregisteredHats.AddRange(data);
        //     }
        // }

        var cache = UnregisteredHats.Clone();
        var time = 0f;

        for (var i = 0; i < cache.Count; i++)
        {
            var file = cache[i];
            RegisteredHats.Add(CreateHatBehaviour(file));
            UnregisteredHats.Remove(file);
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0f;
                UpdateSplashPatch.SetText($"Loading Hats ({i + 1}/{cache.Count})");
                yield return Utils.EndFrame();
            }
        }

        cache.Clear();
        yield break;
    }
}