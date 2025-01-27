using System.Collections;
using UnityEngine;
using TownOfUsFusion.Patches;
using System.Collections.Generic;

namespace TownOfUsFusion.Loaders;

public abstract class AssetLoader
{
    public virtual string Manifest => "";
    public virtual string DirectoryInfo => "";
    public virtual string FileExtension => "";
    public virtual bool Downloading => false;

    public IEnumerator CoDownloadAssets(IEnumerable<string> files)
    {
        /*
        var count = files.Count();

        if (count == 0)
            yield break;

        for (var i = 0; i < count; i++)
        {
            var fileName = files.ElementAt(i);
            UpdateSplashPatch.SetText($"Downloading {Manifest} ({i}/{count})");
            var trueName = $"{fileName.Replace(" ", "%20")}{(IsNullEmptyOrWhiteSpace(FileExtension) ? "" : $".{FileExtension}")}";
            Message($"Downloading: {Manifest}/{fileName}");
            var www = UnityWebRequest.Get($"{RepositoryUrl}/{Manifest}/{trueName}");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Error(www.error);
                www.downloadHandler.Dispose();
                www.Dispose();
                yield break;
            }

            var filePath = Path.Combine(DirectoryInfo, trueName);
            filePath = filePath.Replace("%20", " ");
            var persistTask = File.WriteAllBytesAsync(filePath, www.downloadHandler.data);

            while (!persistTask.IsCompleted)
            {
                if (persistTask.Exception != null)
                {
                    Error(persistTask.Exception);
                    break;
                }

                yield return EndFrame();
            }

            www.downloadHandler.Dispose();
            www.Dispose();
            yield return EndFrame();
        }
        */
        yield break;
    }
    public static IEnumerator InitLoaders()
    {
        UpdateSplashPatch.SetText("Initialising Loaders");
        HatLoader.Instance = new();
        //ImageLoader.Instance = new();
        NameplateLoader.Instance = new();
        //SoundLoader.Instance = new();
        VisorLoader.Instance = new();
        //BundleLoader.Instance = new();
        Debug.Log($"INITIALISING LOADERS");
        yield return Utils.EndFrame();
        yield break;
    }

    public virtual IEnumerator BeginDownload(object response) => Utils.EndFrame();
    public virtual IEnumerator AfterLoading(object response) => Utils.EndFrame();
}