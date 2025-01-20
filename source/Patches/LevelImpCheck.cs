using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Reactor.Utilities;
using TownOfUsFusion.Roles;

namespace TownOfUsFusion.Patches
{
    public static class LevelImpCheck
    {
        public const string LEVELIMP_GUID = "com.DigiWorm.LevelImposter";
        public const ShipStatus.MapType LI_MAP_TYPE = (ShipStatus.MapType)7;
        private static bool _isLevelImpEnabled = false;
        public static bool isLevelImp => _isLevelImpEnabled && ShipStatus.Instance.Type == LI_MAP_TYPE && Loaded && ShipStatus.Instance && GameOptionsManager.Instance.currentNormalGameOptions.MapId == 7;
        public static bool Loaded { get; private set; }
            public static void Init()
            {
                _isLevelImpEnabled = IsPlugin(LEVELIMP_GUID);
            Loaded = IL2CPPChainloader.Instance.Plugins.TryGetValue(LEVELIMP_GUID, out PluginInfo plugin);
            if (!Loaded) return;

                if (_isLevelImpEnabled)
                    PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("LevelImposter was detected, adding to map list.");
            }

            private static bool IsPlugin(string guid)
            {
                return IL2CPPChainloader.Instance.Plugins.TryGetValue(guid, out _);
            }
    }
}
