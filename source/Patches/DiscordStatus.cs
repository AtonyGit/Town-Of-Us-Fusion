using Discord;
using System;
using System.Collections;
using InnerNet;
using UnityEngine;
using UnityEngine.SceneManagement;
using HarmonyLib;
namespace TownOfUsFusion.Patches
{
    internal class DiscordStatus
{
/*    [HarmonyPatch(typeof(DiscordManager), nameof(DiscordManager.Start))]
    [HarmonyPrefix]
	// Token: 0x06000B39 RID: 2873 RVA: 0x0002E210 File Offset: 0x0002C410
	public void Start()
	{
		if (DestroyableSingleton<DiscordManager>.Instance != this)
		{
			return;
		}
		try
		{
			this.presence = new Discord(1213612619173335121, 1UL);
			ActivityManager activityManager = this.presence.GetActivityManager();
			activityManager.RegisterSteam(945360U);
			activityManager.OnActivityJoin += new ActivityManager.ActivityJoinHandler(this.HandleJoinRequest);
			this.SetInMenus();
			SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode mode)
			{
				this.OnSceneChange(scene.name);
			};
		}
		catch
		{
			Debug.LogWarning("DiscordManager: Discord messed up");
		}
	}*/
    [HarmonyPatch(typeof(ActivityManager), nameof(ActivityManager.UpdateActivity))]
    [HarmonyPrefix]
/*    public void Start()
    {
        discord = new Discord.Discord(1213612619173335121, 1UL);
    }*/
    public static void Prefix([HarmonyArgument(0)] Activity activity)
    {
	    //ClientId = 1213612619173335121;
	    //activity.Details.ClientId = 1213612619173335121;
		//activityManager = new Discord(1213612619173335121, 1UL);
        //activity.discord(1213612619173335121, (ulong)Discord.CreateFlags.NoRequireDiscord);
        activity.Details += $" Town of Us Fusion v{TownOfUsFusion.VersionString}";
        //activity.Assets.LargeImage = "vicicon";
    }
}
}
