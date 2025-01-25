using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using Object = UnityEngine.Object;
using TownOfUsFusion.Patches;
using System.Linq;
using TownOfUsFusion.Extensions;

namespace TownOfUsFusion.CrewmateRoles.ImitatorMod
{
    [HarmonyPatch(typeof(AirshipExileController), nameof(AirshipExileController.WrapUpAndSpawn))]
    public static class AirshipExileController_WrapUpAndSpawn
    {
        public static void Postfix(AirshipExileController __instance) => StartImitate.ExileControllerPostfix(__instance);
    }
    
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public class StartImitate
    {
        public static PlayerControl ImitatingPlayer;
        public static void ExileControllerPostfix(ExileController __instance)
        {
            var exiled = __instance.initData.networkedPlayer?.Object;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Imitator)) return;
            if (PlayerControl.LocalPlayer.Data.IsDead || PlayerControl.LocalPlayer.Data.Disconnected) return;
            if (exiled == PlayerControl.LocalPlayer) return;

            var imitator = Role.GetRole<Imitator>(PlayerControl.LocalPlayer);
            if (imitator.ImitatePlayer == null) return;

            Imitate(imitator);

            Utils.Rpc(CustomRPC.StartImitate, imitator.Player.PlayerId);
        }

        public static void Postfix(ExileController __instance) => ExileControllerPostfix(__instance);

        [HarmonyPatch(typeof(Object), nameof(Object.Destroy), new Type[] { typeof(GameObject) })]
        public static void Prefix(GameObject obj)
        {
            if (!SubmergedCompatibility.Loaded || GameOptionsManager.Instance?.currentNormalGameOptions?.MapId != 6) return;
            if (obj.name?.Contains("ExileCutscene") == true) ExileControllerPostfix(ExileControllerPatch.lastExiled);
        }

        public static Sprite Sprite => TownOfUsFusion.Arrow;

        public static void Imitate(Imitator imitator)
        {
            if (imitator.ImitatePlayer == null) return;
            ImitatingPlayer = imitator.Player;
            var imitatorRole = Role.GetRole(imitator.ImitatePlayer).RoleType;
            if (imitatorRole == RoleEnum.Haunter)
            {
                var haunter = Role.GetRole<Haunter>(imitator.ImitatePlayer);
                imitatorRole = haunter.formerRole;
            }
            var role = Role.GetRole(ImitatingPlayer);
            var killsList = (role.Kills, role.CorrectKills, role.IncorrectKills, role.CorrectAssassinKills, role.IncorrectAssassinKills);
            Role.RoleDictionary.Remove(ImitatingPlayer.PlayerId);
            if (imitatorRole == RoleEnum.Crewmate) new Crewmate(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Aurial) new Aurial(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Investigator) new Investigator(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Lookout) new Lookout(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Oracle) new Oracle(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Psychic) new Psychic(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Spy)
            {
                var spy = new Spy(ImitatingPlayer);
                var taskinfos = ImitatingPlayer.Data.Tasks.ToArray();
                var tasksLeft = taskinfos.Count(x => !x.Complete);
                if (tasksLeft <= CustomGameOptions.SpyTasksRemaining && ((PlayerControl.LocalPlayer.Data.IsImpostor() && (!PlayerControl.LocalPlayer.Is(RoleEnum.Traitor) || CustomGameOptions.SpySeesTraitor))
                            || (PlayerControl.LocalPlayer.Is(Faction.NeutralKilling) && CustomGameOptions.SpySeesNeutrals)))
                {
                    var gameObj = new GameObject();
                    var arrow = gameObj.AddComponent<ArrowBehaviour>();
                    gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                    var renderer = gameObj.AddComponent<SpriteRenderer>();
                    renderer.sprite = Sprite;
                    arrow.image = renderer;
                    gameObj.layer = 5;
                    spy.ImpArrows.Add(arrow);
                }
                else if (tasksLeft == 0 && PlayerControl.LocalPlayer == ImitatingPlayer)
                {
                    var impostors = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Data.IsImpostor());
                    foreach (var imp in impostors)
                    {
                        if (!imp.Is(RoleEnum.Traitor) || CustomGameOptions.SpySeesTraitor)
                        {
                            var gameObj = new GameObject();
                            var arrow = gameObj.AddComponent<ArrowBehaviour>();
                            gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                            var renderer = gameObj.AddComponent<SpriteRenderer>();
                            renderer.sprite = Sprite;
                            arrow.image = renderer;
                            gameObj.layer = 5;
                            spy.SpyArrows.Add(imp.PlayerId, arrow);
                        }
                    }
                }
            }
            else if (imitatorRole == RoleEnum.Tracker) new Tracker(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Trapper) new Trapper(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Deputy)
            {
                var deputy = new Deputy(ImitatingPlayer);
                deputy.StartingCooldown = deputy.StartingCooldown.AddSeconds(-10f);
            }
            else if (imitatorRole == RoleEnum.Hunter) new Hunter(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Jailor) new Jailor(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Sheriff) new Sheriff(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Veteran) new Veteran(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Vigilante) new Vigilante(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Altruist) new Altruist(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Medic)
            {
                var medic = new Medic(ImitatingPlayer);
                medic.UsedAbility = true;
                medic.StartingCooldown = medic.StartingCooldown.AddSeconds(-10f);
            }
            else if (imitatorRole == RoleEnum.Engineer) new Engineer(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Mayor)
            {
                var mayor = new Mayor(ImitatingPlayer);
                if (CustomGameOptions.ImitatorCanBecomeMayor)
                {
                    mayor.Revealed = true;
                    if (PlayerControl.LocalPlayer == ImitatingPlayer) mayor.RegenTask();
                }
            }
            else if (imitatorRole == RoleEnum.Medium) new Medium(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Politician) new Politician(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Prosecutor) new Prosecutor(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Swapper) new Swapper(ImitatingPlayer);
            else if (imitatorRole == RoleEnum.Transporter) new Transporter(ImitatingPlayer);

            var newRole = Role.GetRole(ImitatingPlayer);
            if (imitatorRole != RoleEnum.Mayor || !CustomGameOptions.ImitatorCanBecomeMayor) newRole.RemoveFromRoleHistory(newRole.RoleType);
            else ImitatingPlayer = null;
            newRole.Kills = killsList.Kills;
            newRole.CorrectKills = killsList.CorrectKills;
            newRole.IncorrectKills = killsList.IncorrectKills;
            newRole.CorrectAssassinKills = killsList.CorrectAssassinKills;
            newRole.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
        }
    }
}