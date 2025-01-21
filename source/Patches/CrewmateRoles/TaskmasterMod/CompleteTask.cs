using System.Linq;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;
using static InnerNet.InnerNetServer;

namespace TownOfUsFusion.CrewmateRoles.TaskmasterMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CompleteTask))]
public class CompleteTask
{
    public static Sprite Sprite => TownOfUsFusion.Arrow;

    public static void Postfix(PlayerControl __instance)
    {
        if (!__instance.Is(RoleEnum.Taskmaster)) return;
        if (__instance.Data.IsDead) return;
        var taskinfos = __instance.Data.Tasks.ToArray();

        var tasksLeft = taskinfos.Count(x => !x.Complete);
        var role = Role.GetRole<Taskmaster>(__instance);
        var localRole = Role.GetRole(PlayerControl.LocalPlayer);
        switch (tasksLeft)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            if (role.hasExtraTasks) role.RegenTask();
                if (tasksLeft == CustomGameOptions.TMTasksRemaining && role.hasExtraTasks)
                {
                    Coroutines.Start(Utils.FlashCoroutine(role.Color));
                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Taskmaster))
                    {
                        role.RegenTask();
                    }
                    else if (PlayerControl.LocalPlayer.Data.IsImpostor() || PlayerControl.LocalPlayer.Is(Faction.ImpSentinel)
                        || ((PlayerControl.LocalPlayer.Is(Faction.NeutralKilling) || PlayerControl.LocalPlayer.Is(Faction.NeutralNeophyte) || PlayerControl.LocalPlayer.Is(Faction.NeutralNecro) || PlayerControl.LocalPlayer.Is(Faction.NeutralApocalypse)
                        || PlayerControl.LocalPlayer.Is(Faction.ChaosSentinel) || PlayerControl.LocalPlayer.Is(Faction.NeutralSentinel)) && CustomGameOptions.SnitchSeesNeutrals))
                    {
                        Coroutines.Start(Utils.FlashCoroutine(role.Color));
                        var gameObj = new GameObject();
                        var arrow = gameObj.AddComponent<ArrowBehaviour>();
                        gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                        var renderer = gameObj.AddComponent<SpriteRenderer>();
                        renderer.sprite = Sprite;
                        arrow.image = renderer;
                        gameObj.layer = 5;
                        role.ImpArrows.Add(arrow);
                    }
                }
                break;

            case 0:
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Taskmaster))
                {
                Coroutines.Start(Utils.FlashCoroutine(role.Color));
        if (!role.hasExtraTasks) {
            var totalTasks = CustomGameOptions.TMCommonTasks + CustomGameOptions.TMLongTasks + CustomGameOptions.TMShortTasks;
                foreach (var task in __instance.myTasks)
                    if (task.TryCast<NormalPlayerTask>() != null)
                    {
                        var normalPlayerTask = task.Cast<NormalPlayerTask>();

                        var updateArrow = normalPlayerTask.taskStep > 0;

                normalPlayerTask.taskStep = 0;
                normalPlayerTask.Initialize();
                if (normalPlayerTask.TaskType == TaskTypes.PickUpTowels)
                    foreach (var console in Object.FindObjectsOfType<TowelTaskConsole>())
                        console.Image.color = Color.white;
                normalPlayerTask.taskStep = 0;
                if (normalPlayerTask.TaskType == TaskTypes.UploadData)
                    normalPlayerTask.taskStep = 1;
                if ((normalPlayerTask.TaskType == TaskTypes.EmptyGarbage || normalPlayerTask.TaskType == TaskTypes.EmptyChute)
                    && (GameOptionsManager.Instance.currentNormalGameOptions.MapId == 0 ||
                    GameOptionsManager.Instance.currentNormalGameOptions.MapId == 3 ||
                    GameOptionsManager.Instance.currentNormalGameOptions.MapId == 4))
                    normalPlayerTask.taskStep = 1;
                if (updateArrow)
                    normalPlayerTask.UpdateArrowAndLocation();

                        var taskInfo = __instance.Data.FindTaskById(task.Id);
                        taskInfo.Complete = false;
                    }
                role.hasExtraTasks = true;
                role.RegenTask();
        }
                else {
                    if (AmongUsClient.Instance.AmHost)
                    {
                        Utils.EndGameAlt();
                    }
                }

        }
                break;
    }
}
}
}