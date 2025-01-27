using System.Collections;
using UnityEngine;
using TownOfUsFusion.Patches;

namespace TownOfUsFusion.Loaders;

public abstract class AssetLoader
{
    public virtual string Manifest => "";
    public virtual string DirectoryInfo => "";
    public virtual string FileExtension => "";
    public virtual bool Downloading => false;

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

    public virtual IEnumerator AfterLoading(object response) => Utils.EndFrame();
}