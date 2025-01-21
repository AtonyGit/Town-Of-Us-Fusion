using HarmonyLib;
using System;
using TownOfUsFusion.Patches;
using TownOfUsFusion.CustomOption;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using Reactor.Networking.Attributes;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Reactor.Utilities;
//using LevelImposter.Shop.Util.MapSync;
using AmongUs.GameOptions;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Roles;
using BepInEx.Unity.IL2CPP.Utils.Collections;

namespace TownOfUsFusion
{
    [HarmonyPatch]
class RandomMap
{
    public static byte previousMap;
    public static float vision;
    public static int commonTasks;
    public static int shortTasks;
    public static int longTasks;

    [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.BeginGame))]
    [HarmonyPrefix]
    public static bool Prefix(GameStartManager __instance)
    {
        if (AmongUsClient.Instance.AmHost)
        {
            previousMap = GameOptionsManager.Instance.currentNormalGameOptions.MapId;
            vision = GameOptionsManager.Instance.currentNormalGameOptions.CrewLightMod;
            commonTasks = GameOptionsManager.Instance.currentNormalGameOptions.NumCommonTasks;
            shortTasks = GameOptionsManager.Instance.currentNormalGameOptions.NumShortTasks;
            longTasks = GameOptionsManager.Instance.currentNormalGameOptions.NumLongTasks;
            byte map = GameOptionsManager.Instance.currentNormalGameOptions.MapId;
            if (CustomGameOptions.RandomMapEnabled)
            {
                map = GetRandomMap();
                GameOptionsManager.Instance.currentNormalGameOptions.MapId = map;
            }
            GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Scientist, 0, 0);
            GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Engineer, 0, 0);
            GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.GuardianAngel, 0, 0);
            GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Shapeshifter, 0, 0);
            Utils.Rpc(CustomRPC.SetSettings, map);
            if (CustomGameOptions.AutoAdjustSettings) AdjustSettings(map);
        }
        return true;
    }

    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    [HarmonyPostfix]
    public static void Postfix(AmongUsClient __instance)
    {
        if (__instance.AmHost)
        {
            if (CustomGameOptions.AutoAdjustSettings)
            {
                if (CustomGameOptions.SmallMapHalfVision && vision != 0) GameOptionsManager.Instance.currentNormalGameOptions.CrewLightMod = vision;
                if (GameOptionsManager.Instance.currentNormalGameOptions.MapId == 1) AdjustCooldowns(CustomGameOptions.SmallMapDecreasedCooldown);
                if (GameOptionsManager.Instance.currentNormalGameOptions.MapId is 4 or 5 or 6) AdjustCooldowns(-CustomGameOptions.LargeMapIncreasedCooldown);
            }
            if (CustomGameOptions.RandomMapEnabled) GameOptionsManager.Instance.currentNormalGameOptions.MapId = previousMap;
            
                GameOptionsManager.Instance.currentNormalGameOptions.NumCommonTasks = commonTasks/* + CustomGameOptions.TMCommonTasks*/;
                GameOptionsManager.Instance.currentNormalGameOptions.NumShortTasks = shortTasks/* + CustomGameOptions.TMShortTasks*/;
                GameOptionsManager.Instance.currentNormalGameOptions.NumLongTasks = longTasks/* + CustomGameOptions.TMLongTasks*/;
        }
    }

    public static byte GetRandomMap()
    {
        Random _rnd = new Random();
        float totalWeight = 0;
        totalWeight += CustomGameOptions.RandomMapSkeld;
        totalWeight += CustomGameOptions.RandomMapMira;
        totalWeight += CustomGameOptions.RandomMapPolus;
        totalWeight += CustomGameOptions.RandomMapdlekS;
        totalWeight += CustomGameOptions.RandomMapAirship;
        totalWeight += CustomGameOptions.RandomMapFungle;
        if (SubmergedCompatibility.Loaded) totalWeight += CustomGameOptions.RandomMapSubmerged;
        if (LevelImpCheck.Loaded) totalWeight += CustomGameOptions.RandomMapLevelImp;

        if (totalWeight == 0) return GameOptionsManager.Instance.currentNormalGameOptions.MapId;

        float randomNumber = _rnd.Next(0, (int)totalWeight);
        if (randomNumber < CustomGameOptions.RandomMapSkeld) return 0;
        randomNumber -= CustomGameOptions.RandomMapSkeld;
        if (randomNumber < CustomGameOptions.RandomMapMira) return 1;
        randomNumber -= CustomGameOptions.RandomMapMira;
        if (randomNumber < CustomGameOptions.RandomMapPolus) return 2;
        randomNumber -= CustomGameOptions.RandomMapPolus;
        if (randomNumber < CustomGameOptions.RandomMapdlekS) return 3;
        randomNumber -= CustomGameOptions.RandomMapdlekS;
        if (randomNumber < CustomGameOptions.RandomMapAirship) return 4;
        randomNumber -= CustomGameOptions.RandomMapAirship;
        if (randomNumber < CustomGameOptions.RandomMapFungle) return 5;
        randomNumber -= CustomGameOptions.RandomMapFungle;
        if (SubmergedCompatibility.Loaded && randomNumber < CustomGameOptions.RandomMapSubmerged) return 6;
        randomNumber -= CustomGameOptions.RandomMapSubmerged;
        if (LevelImpCheck.Loaded && randomNumber < CustomGameOptions.RandomMapLevelImp) return 7;

        return GameOptionsManager.Instance.currentNormalGameOptions.MapId;
    }
    /*
    public static byte GetRandomMap()
    {
        var map = (byte)CustomGameOptions.Map;

        if (map < 8)
            return map;

        float totalWeight = 0;
        totalWeight += CustomGameOptions.RandomMapSkeld;
        totalWeight += CustomGameOptions.RandomMapMira;
        totalWeight += CustomGameOptions.RandomMapPolus;
        totalWeight += CustomGameOptions.RandomMapdlekS;
        totalWeight += CustomGameOptions.RandomMapAirship;
        totalWeight += CustomGameOptions.RandomMapFungle;
        totalWeight += CustomGameOptions.RandomMapSubmerged;
        totalWeight += CustomGameOptions.RandomMapLevelImp;
        var maps = new List<byte>() { 0, 1, 2, 3, 4, 5 };

        if (SubmergedCompatibility.Loaded)
            maps.Add(6);

        if (LevelImpCheck.Loaded)
            maps.Add(7);

        maps.Shuffle();

        if (totalWeight == 0)
            return maps.Random();

        var randoms = new List<byte>();
        var num = CustomGameOptions.RandomMapSkeld / 5;

        while (num > 0)
        {
            randoms.Add(0);
            num--;
        }

        num = CustomGameOptions.RandomMapMira / 5;

        while (num > 0)
        {
            randoms.Add(1);
            num--;
        }

        num = CustomGameOptions.RandomMapPolus / 5;

        while (num > 0)
        {
            randoms.Add(2);
            num--;
        }

        num = CustomGameOptions.RandomMapdlekS / 5;

        while (num > 0)
        {
            randoms.Add(3);
            num--;
        }

        num = CustomGameOptions.RandomMapAirship / 5;

        while (num > 0)
        {
            randoms.Add(4);
            num--;
        }

        num = CustomGameOptions.RandomMapFungle / 5;

        while (num > 0)
        {
            randoms.Add(5);
            num--;
        }

        if (SubmergedCompatibility.Loaded)
        {
            num = CustomGameOptions.RandomMapSubmerged / 5;

            while (num > 0)
            {
                randoms.Add(6);
                num--;
            }
        }

        if (LevelImpCheck.Loaded)
        {
            num = CustomGameOptions.RandomMapLevelImp / 5;

            while (num > 0)
            {
                randoms.Add(7);
                num--;
            }
        }

        randoms.Shuffle();
        return (randoms.Count > 0 ? randoms : maps).Random();
    }*/
    
    public static void AdjustSettings(byte map)
    {
        if (map is 0 or 1 or 3)
        {
            if (CustomGameOptions.SmallMapHalfVision) GameOptionsManager.Instance.currentNormalGameOptions.CrewLightMod *= 0.5f;
            GameOptionsManager.Instance.currentNormalGameOptions.NumShortTasks += CustomGameOptions.SmallMapIncreasedShortTasks;
            GameOptionsManager.Instance.currentNormalGameOptions.NumLongTasks += CustomGameOptions.SmallMapIncreasedLongTasks;
            if (map == 1) AdjustCooldowns(-CustomGameOptions.SmallMapDecreasedCooldown);
        }
        if (map is 4 or 5 or 6)
        {
            GameOptionsManager.Instance.currentNormalGameOptions.NumShortTasks -= CustomGameOptions.LargeMapDecreasedShortTasks;
            GameOptionsManager.Instance.currentNormalGameOptions.NumLongTasks -= CustomGameOptions.LargeMapDecreasedLongTasks;
            AdjustCooldowns(CustomGameOptions.LargeMapIncreasedCooldown);
        }
        return;
    }

    public static void AdjustCooldowns(float change)
    {
        Generate.ExamineCooldown.Set((float)Generate.ExamineCooldown.Value + change, false);
        Generate.SeerCooldown.Set((float)Generate.SeerCooldown.Value + change, false);
        Generate.TrackCooldown.Set((float)Generate.TrackCooldown.Value + change, false);
        Generate.TrapCooldown.Set((float)Generate.TrapCooldown.Value + change, false);
        Generate.SheriffKillCd.Set((float)Generate.SheriffKillCd.Value + change, false);
        Generate.AlertCooldown.Set((float)Generate.AlertCooldown.Value + change, false);
        Generate.TransportCooldown.Set((float)Generate.TransportCooldown.Value + change, false);
        Generate.ProtectCd.Set((float)Generate.ProtectCd.Value + change, false);
        Generate.VestCd.Set((float)Generate.VestCd.Value + change, false);
        Generate.DouseCooldown.Set((float)Generate.DouseCooldown.Value + change, false);
        Generate.InfectCooldown.Set((float)Generate.InfectCooldown.Value + change, false);
        Generate.PestKillCooldown.Set((float)Generate.PestKillCooldown.Value + change, false);
        Generate.MimicCooldownOption.Set((float)Generate.MimicCooldownOption.Value + change, false);
        Generate.HackCooldownOption.Set((float)Generate.HackCooldownOption.Value + change, false);
        Generate.GlitchKillCooldownOption.Set((float)Generate.GlitchKillCooldownOption.Value + change, false);
        Generate.RampageCooldown.Set((float)Generate.RampageCooldown.Value + change, false);
        Generate.GrenadeCooldown.Set((float)Generate.GrenadeCooldown.Value + change, false);
        Generate.MorphlingCooldown.Set((float)Generate.MorphlingCooldown.Value + change, false);
        Generate.SwoopCooldown.Set((float)Generate.SwoopCooldown.Value + change, false);
        //Generate.PoisonCooldown.Set((float)Generate.PoisonCooldown.Value + change, false);
        Generate.MineCooldown.Set((float)Generate.MineCooldown.Value + change, false);
        Generate.DragCooldown.Set((float)Generate.DragCooldown.Value + change, false);
        Generate.EscapeCooldown.Set((float)Generate.EscapeCooldown.Value + change, false);
        Generate.JuggKillCooldown.Set((float)Generate.JuggKillCooldown.Value + change, false);
        Generate.ObserveCooldown.Set((float)Generate.ObserveCooldown.Value + change, false);
        Generate.BiteCooldown.Set((float)Generate.BiteCooldown.Value + change, false);
        Generate.StakeCooldown.Set((float)Generate.StakeCooldown.Value + change, false);
        Generate.TrickCooldown.Set((float)Generate.TrickCooldown.Value + change, false);
        
        Generate.InquireCooldown.Set((float)Generate.InquireCooldown.Value + change, false);
        Generate.VanquishCooldown.Set((float)Generate.VanquishCooldown.Value + change, false);

        Generate.ConfessCooldown.Set((float)Generate.ConfessCooldown.Value + change, false);
        Generate.ChargeUpDuration.Set((float)Generate.ChargeUpDuration.Value + change, false);
        Generate.AbilityCooldown.Set((float)Generate.AbilityCooldown.Value + change, false);
        Generate.RadiateCooldown.Set((float)Generate.RadiateCooldown.Value + change, false);
        Generate.ReviveCooldown.Set((float)Generate.ReviveCooldown.Value + change, false);
        Generate.WhisperCooldown.Set((float)Generate.WhisperCooldown.Value + change, false);

        Generate.SentinelKillCooldown.Set((float)Generate.SentinelKillCooldown.Value + change, false);
        Generate.SentinelChargeCooldown.Set((float)Generate.SentinelChargeCooldown.Value + change, false);
        Generate.SentinelPlaceCooldown.Set((float)Generate.SentinelPlaceCooldown.Value + change, false);
        Generate.SentinelStunCooldown.Set((float)Generate.SentinelStunCooldown.Value + change, false);
        GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown += change;
        if (change % 5 != 0)
        {
            if (change > 0) change -= 2.5f;
            else if (change < 0) change += 2.5f;
        }
        GameOptionsManager.Instance.currentNormalGameOptions.EmergencyCooldown += (int)change;
        return;
    }
}
}