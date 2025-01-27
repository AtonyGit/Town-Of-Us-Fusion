/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TownOfUsFusion.Patches;

namespace TownOfUsFusion.Loaders;

public class ImageLoader : AssetLoader<Asset>
{
    public override string DirectoryInfo => TownOfUsFusion.Images;
    public override bool Downloading => true;
    public override string Manifest => "Images";
    public override string FileExtension => "png";

    public static ImageLoader Instance { get; set; }

    public override IEnumerator BeginDownload(object response)
    {
        var mainResponse = (List<Asset>)response;
        Message($"Found {mainResponse.Count} assets");
        var toDownload = mainResponse.Select(x => x.ID).Where(ShouldDownload);
        Message($"Downloading {toDownload.Count()} assets");
        yield return CoDownloadAssets(toDownload);
    }

    public override IEnumerator AfterLoading(object response)
    {
        var images = (List<Asset>)response;
        var textures = new List<Texture2D>();
        images.Select(x => Path.Combine(TownOfUsFusion.Images, $"{x.ID}.png")).ForEach(x => textures.Add(LoadDiskTexture(x)));
        var time = 0f;

        for (var i = 0; i < images.Count; i++)
        {
            var image = images[i];
            Utils.AddAsset(image.ID, CreateSprite(textures[i], image.ID));
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0f;
                UpdateSplashPatch.SetText($"Loading Images ({i + 1}/{images.Count})");
                yield return Utils.EndFrame();
            }
        }

        images.Clear();
        yield break;
    }

    private static bool ShouldDownload(string id) => !File.Exists(Path.Combine(TownOfUsFusion.Images, $"{id}.png"));
}*/