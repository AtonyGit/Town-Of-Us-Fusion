using static TownOfUsFusion.Cosmetics.CustomVisors.CustomVisorManager;
using Cpp2IL.Core.Extensions;
using UnityEngine;
using TownOfUsFusion.Patches;
using System.Collections;

namespace TownOfUsFusion.Loaders;

public class VisorLoader : AssetLoader<CustomVisor>
{
    public override string DirectoryInfo => "TownOfUsFusion.Resources.Visors";
    public override bool Downloading => true;
    public override string Manifest => "Visors";
    //public override string Manifest => "metadata";
    public override string FileExtension => "png";

    public static VisorLoader Instance { get; set; }


    public override IEnumerator AfterLoading(object response)
    {
        // if (TownOfUsFusion.IsStream)
        // {
        //     var filePath = Path.Combine(TownOfUsFusion.Visors, "Stream", "Visors.json");

        //     if (File.Exists(filePath))
        //     {
        //         var data = JsonSerializer.Deserialize<List<CustomVisor>>(File.ReadAllText(filePath));
        //         data.ForEach(x => x.StreamOnly = true);
        //         UnregisteredVisors.AddRange(data);
        //     }
        // }

        var cache = UnregisteredVisors.Clone();
        var time = 0f;

        for (var i = 0; i < cache.Count; i++)
        {
            var file = cache[i];
            RegisteredVisors.Add(CreateVisorBehaviour(file));
            UnregisteredVisors.Remove(file);
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0f;
                UpdateSplashPatch.SetText($"Loading Visors ({i + 1}/{cache.Count})");
                yield return Utils.EndFrame();
            }
        }

        cache.Clear();
        yield break;
    }
}