using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BepInEx.Logging;
using System.Text.Json;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;
using System.Linq;

namespace TownOfUsFusion.Patches.CustomGuide
{
    public static class GuideLoader
{
    private const string HAT_METADATA_JSON = "roleList.json";
    private const int HAT_ORDER_BASELINE = 99;

    private static ManualLogSource Log => PluginSingleton < TownOfUsFusion >.Instance.Log;
    private static Assembly Assembly => typeof(TownOfUsFusion).Assembly;

        private static bool LoadedGuideInfo = false;
/*
    private static List<HatData> DiscoverHatBehaviours(GuideMetadataJson CrewRoleMetadata)
    {
        var hatBehaviours = new List<HatData>();

        foreach (var hatCredit in CrewRoleMetadata.crewmates)
        {
            try
            {
                var stream = Assembly.GetManifestResourceStream($"{HAT_RESOURCE_NAMESPACE}.{hatCredit.Id}.png");
                if (stream != null)
                {
                    var hatBehaviour = GenerateHatBehaviour(hatCredit.Id, stream.ReadFully());
                    hatBehaviour.StoreName = hatCredit.Artist;
                    hatBehaviour.ProductId = hatCredit.Id;
                    hatBehaviour.name = hatCredit.Name;
                    hatBehaviour.Free = true;
                    hatBehaviours.Add(hatBehaviour);

                }
            }
            catch (Exception e)
            {
                Log.LogError(
                     $"Error loading hat {hatCredit.Id} in metadata file ({HAT_METADATA_JSON})");
                Log.LogError($"{e.Message}\nStack:{e.StackTrace}");
            }
        }

        return hatBehaviours;
    }*/
}
}