/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TownOfUsFusion.Patches;

namespace TownOfUsFusion.Loaders;

public class SoundLoader : AssetLoader<Asset>
{
    public override string DirectoryInfo => TownOfUsFusion.Sounds;
    public override bool Downloading => true;
    public override string Manifest => "Sounds";
    public override string FileExtension => "wav";

    public static SoundLoader Instance { get; set; }

    public override IEnumerator BeginDownload(object response)
    {
        var mainResponse = (List<Asset>)response;
        Message($"Found {mainResponse.Count} sounds");
        var toDownload = mainResponse.Select(x => x.ID).Where(ShouldDownload);
        Message($"Downloading {toDownload.Count()} sounds");
        yield return CoDownloadAssets(toDownload);
    }

    public override IEnumerator AfterLoading(object response)
    {
        var sounds = (List<Asset>)response;
        var time = 0f;

        for (var i = 0; i < sounds.Count; i++)
        {
            var sound = sounds[i];
            Utils.AddAsset(sound.ID, CreateDiskAudio(Path.Combine(DirectoryInfo, $"{sound.ID}.wav")));
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0f;
                UpdateSplashPatch.SetText($"Loading Sounds ({i + 1}/{sounds.Count})");
                yield return Utils.EndFrame();
            }
        }

        sounds.Clear();
        yield break;
    }

    private static bool ShouldDownload(string id) => !File.Exists(Path.Combine(TownOfUsFusion.Sounds, $"{id}.wav"));
}*/