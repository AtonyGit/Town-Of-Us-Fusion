using System.Linq;
using HarmonyLib;
using Reactor.Utilities;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using UnityEngine;

namespace TownOfUsFusion.CrewmateRoles.SnitchMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CompleteTask))]
public class CompleteTask
{
    public static Sprite Sprite => TownOfUsFusion.Arrow;

    public static void Postfix(PlayerControl __instance)
    {
        if (!__instance.Is(RoleEnum.Snitch)) return;
        if (__instance.Data.IsDead) return;
        var taskinfos = __instance.Data.Tasks.ToArray();

        var tasksLeft = taskinfos.Count(x => !x.Complete);
        var role = Role.GetRole<Snitch>(__instance);
        var localRole = Role.GetRole(PlayerControl.LocalPlayer);
        switch (tasksLeft)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                if (tasksLeft == CustomGameOptions.SnitchTasksRemaining)
                {
                    role.RegenTask();
                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Snitch))
                    {
                        Coroutines.Start(Utils.FlashCoroutine(role.Color));
                    }
                    else if ((PlayerControl.LocalPlayer.Is(Faction.ImpSentinel) || PlayerControl.LocalPlayer.Data.IsImpostor() && (!PlayerControl.LocalPlayer.Is(RoleEnum.Traitor) || CustomGameOptions.SnitchSeesTraitor))
                        || ((PlayerControl.LocalPlayer.Is(Faction.NeutralSentinel) || PlayerControl.LocalPlayer.Is(Faction.ChaosSentinel) || PlayerControl.LocalPlayer.Is(Faction.NeutralKilling) || PlayerControl.LocalPlayer.Is(Faction.NeutralNeophyte) || PlayerControl.LocalPlayer.Is(Faction.NeutralNecro) || PlayerControl.LocalPlayer.Is(Faction.NeutralApocalypse)) && CustomGameOptions.SnitchSeesNeutrals))
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
                role.RegenTask();
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Snitch))
                {
                    Coroutines.Start(Utils.FlashCoroutine(Color.green));
                    var impostors = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Data.IsImpostor() || x.Is(Faction.ImpSentinel));
                    var traitor = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(RoleEnum.Traitor));
                    foreach (var imp in impostors)
                    {
                        if (!imp.Is(RoleEnum.Traitor) || CustomGameOptions.SnitchSeesTraitor)
                        {
                            var gameObj = new GameObject();
                            var arrow = gameObj.AddComponent<ArrowBehaviour>();
                            gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                            var renderer = gameObj.AddComponent<SpriteRenderer>();
                            renderer.sprite = Sprite;
                            arrow.image = renderer;
                            gameObj.layer = 5;
                            role.SnitchArrows.Add(imp.PlayerId, arrow);
                        }
                    }
                }
                else if (PlayerControl.LocalPlayer.Data.IsImpostor() || PlayerControl.LocalPlayer.Is(Faction.ImpSentinel) || PlayerControl.LocalPlayer.Is(Faction.ChaosSentinel) || PlayerControl.LocalPlayer.Is(Faction.NeutralSentinel) || (PlayerControl.LocalPlayer.Is(Faction.NeutralKilling) || PlayerControl.LocalPlayer.Is(Faction.NeutralNeophyte) || PlayerControl.LocalPlayer.Is(Faction.NeutralNecro) || PlayerControl.LocalPlayer.Is(Faction.NeutralApocalypse)) && CustomGameOptions.SnitchSeesNeutrals)
                {
                    Coroutines.Start(Utils.FlashCoroutine(Color.green));
                }

                break;
        }
    }
}
}