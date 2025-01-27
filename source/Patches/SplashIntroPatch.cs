using System.Collections.Generic;
using System.IO;
using Reactor.Utilities.Extensions;
using UObject = UnityEngine.Object;
using UColor = UnityEngine.Color;
using UnityEngine;
using TownOfUsFusion.Loaders;
using HarmonyLib;
using TMPro;
using Reactor.Utilities;
using System.Collections;

namespace TownOfUsFusion.Patches;

[HarmonyPatch(typeof(SplashManager), nameof(SplashManager.Update))]
public static class UpdateSplashPatch
{
    private static bool Loading;
    private static TextMeshPro TMP;
    private static bool DataSet;

    public static bool Prefix(SplashManager __instance)
    {
        if (__instance.doneLoadingRefdata && !__instance.startedSceneLoad && Time.time - __instance.startTime > 4.2f)
            Coroutines.Start(LoadingScreen(__instance));

        return false;
    }

    private static IEnumerator LoadingScreen(SplashManager __instance)
    {
        if (Loading)
            yield break;

        Loading = true;
        var loading = new GameObject("LoadingLogo");
        loading.transform.localPosition = new(0f, 1.4f, -5f);
        loading.transform.localScale = new(1f, 1f, 1f);
        var rend = loading.AddComponent<SpriteRenderer>();
        rend.sprite = TownOfUsFusion.ToUBanner;
        rend.transform.localScale = Vector3.one * 0.9f;
        rend.color = UColor.clear;
        var num = 0f;

        while (num < 1f)
        {
            num += Time.deltaTime;
            rend.color = UColor.white.AlphaMultiplied(num);
            yield return Utils.EndFrame();
        }

        rend.color = UColor.white;
        TMP = UObject.Instantiate(__instance.errorPopup.InfoText, loading.transform);
        TMP.transform.localPosition = new(0f, -2.1f, -10f);
        TMP.fontStyle = FontStyles.Bold;
        TMP.color = UColor.clear;
        TMP.transform.localScale /= 0.9f;
        Utils.AddAsset("Placeholder", TMP.font);

        SetText("Loading...");
        yield return Utils.EndFrame();

        num = 0f;

        while (num < 1f)
        {
            num += Time.deltaTime;
            TMP.color = UColor.white.AlphaMultiplied(num);
            yield return Utils.EndFrame();
        }

        yield return AssetLoader.InitLoaders();
        yield return HatLoader.Instance.CoFetch();
        yield return VisorLoader.Instance.CoFetch();
        yield return NameplateLoader.Instance.CoFetch();
        //yield return ImageLoader.Instance.CoFetch();
        //yield return SoundLoader.Instance.CoFetch();
        //yield return BundleLoader.Instance.CoFetch();

        yield return LoadModData();

        SetText("Loaded!");
        yield return Utils.Wait(0.5f);

        num = 0.5f;

        while (num > 0f)
        {
            num -= Time.deltaTime * 2;
            TMP.color = UColor.white.AlphaMultiplied(num);
            yield return Utils.EndFrame();
        }

        SetText("");
        num = 1f;

        while (num > 0f)
        {
            num -= Time.deltaTime;
            rend.color = UColor.white.AlphaMultiplied(num);
            yield return Utils.EndFrame();
        }

        rend.color = UColor.clear;
        loading.Destroy();
        yield return Utils.Wait(0.1f);

        __instance.sceneChanger.AllowFinishLoadingScene();
        __instance.startedSceneLoad = true;
        yield break;
    }

    public static void SetText(string text) => TMP.SetText(text);

    private static IEnumerator LoadModData()
    {
        if (DataSet)
            yield break;

        SetText("Setting Mod Data");
        /*Message("Setting mod data");

        ModUpdater.CanDownloadSubmerged = !SubLoaded && ModUpdater.URLs.ContainsKey("Submerged");
        ModUpdater.CanDownloadLevelImpostor = !LILoaded && ModUpdater.URLs.ContainsKey("LevelImpostor");

        Generate.GenerateAll();
        Modules.Info.SetAllInfo();
        RegionInfoOpenPatch.UpdateRegions();*/

        DataSet = true;

        yield return Utils.EndFrame();
        yield break;
    }
}