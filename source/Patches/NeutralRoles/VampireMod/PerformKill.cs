using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;
using TownOfUsFusion.CrewmateRoles.InvestigatorMod;
using TownOfUsFusion.CrewmateRoles.TrapperMod;
using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using System.Linq;
<<<<<<< Updated upstream
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.CrewmateRoles.AurialMod;
using TownOfUsFusion.Patches.ScreenEffects;
=======
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.Patches.NeutralRoles;
>>>>>>> Stashed changes

namespace TownOfUsFusion.NeutralRoles.VampireMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class Bite
    {
        public static bool Prefix(KillButton __instance)
        {
            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Vampire);
            if (!flag) return true;
            var role = Role.GetRole<Vampire>(PlayerControl.LocalPlayer);
            if (!PlayerControl.LocalPlayer.CanMove || role.ClosestPlayer == null) return false;
            var flag2 = role.BiteTimer() == 0f;
            if (!flag2) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            if (role.ClosestPlayer == null) return false;

            var vamps = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(RoleEnum.Vampire)).ToList();
            foreach (var phantom in Role.GetRoles(RoleEnum.Phantom))
            {
                var phantomRole = (Phantom)phantom;
                if (phantomRole.formerRole == RoleEnum.Vampire) vamps.Add(phantomRole.Player);
            }
            var aliveVamps = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(RoleEnum.Vampire) && !x.Data.IsDead && !x.Data.Disconnected).ToList();
<<<<<<< Updated upstream
            if (role.ClosestPlayer.Is(RoleEnum.VampireHunter))
            {
                role.LastBit = DateTime.UtcNow;
                Utils.RpcMurderPlayer(role.ClosestPlayer, PlayerControl.LocalPlayer);
                return false;
            }
            else if ((role.ClosestPlayer.Is(Faction.Crewmates) || (role.ClosestPlayer.Is(Faction.NeutralBenign)
=======
            if ((role.ClosestPlayer.Is(Faction.Crewmates) || (role.ClosestPlayer.Is(Faction.NeutralBenign)
>>>>>>> Stashed changes
                && CustomGameOptions.CanBiteNeutralBenign) || (role.ClosestPlayer.Is(Faction.NeutralEvil)
                && CustomGameOptions.CanBiteNeutralEvil)) && !role.ClosestPlayer.Is(ModifierEnum.Lover) &&
                aliveVamps.Count == 1 && vamps.Count < CustomGameOptions.MaxVampiresPerGame)
            {
                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer);
                if (interact[4] == true)
                {
                    Convert(role.ClosestPlayer);
                    Utils.Rpc(CustomRPC.Bite, role.ClosestPlayer.PlayerId);
                }
                if (interact[0] == true)
                {
                    role.LastBit = DateTime.UtcNow;
                    return false;
                }
                else if (interact[1] == true)
<<<<<<< Updated upstream
                {
                    role.LastBit = DateTime.UtcNow;
                    role.LastBit = role.LastBit.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[3] == true) return false;
                return false;
            }
            else
            {
                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
                if (interact[4] == true) return false;
                if (interact[0] == true)
                {
                    role.LastBit = DateTime.UtcNow;
                    return false;
                }
                else if (interact[1] == true)
                {
                    role.LastBit = DateTime.UtcNow;
                    role.LastBit = role.LastBit.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[2] == true)
                {
                    role.LastBit = DateTime.UtcNow;
                    role.LastBit = role.LastBit.AddSeconds(CustomGameOptions.VestKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[3] == true) return false;
                return false;
            }
        }

        public static void Convert(PlayerControl newVamp)
        {
            var oldRole = Role.GetRole(newVamp);
            var killsList = (oldRole.CorrectKills, oldRole.IncorrectKills, oldRole.CorrectAssassinKills, oldRole.IncorrectAssassinKills);

            if (newVamp.Is(RoleEnum.Snitch))
            {
                var snitch = Role.GetRole<Snitch>(newVamp);
                snitch.SnitchArrows.Values.DestroyAll();
                snitch.SnitchArrows.Clear();
                snitch.ImpArrows.DestroyAll();
                snitch.ImpArrows.Clear();
            }

            if (newVamp == StartImitate.ImitatingPlayer) StartImitate.ImitatingPlayer = null;

            if (newVamp.Is(RoleEnum.GuardianAngel))
            {
                var ga = Role.GetRole<GuardianAngel>(newVamp);
                ga.UnProtect();
            }

            if (newVamp.Is(RoleEnum.Medium))
            {
                var medRole = Role.GetRole<Medium>(newVamp);
                medRole.MediatedPlayers.Values.DestroyAll();
                medRole.MediatedPlayers.Clear();
            }

            if (PlayerControl.LocalPlayer == newVamp)
            {
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator)) Footprint.DestroyAll(Role.GetRole<Investigator>(PlayerControl.LocalPlayer));

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Sheriff)) HudManager.Instance.KillButton.buttonLabelText.gameObject.SetActive(false);

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer))
                {
                    var engineerRole = Role.GetRole<Engineer>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(engineerRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
                {
                    var trackerRole = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);
                    trackerRole.TrackerArrows.Values.DestroyAll();
                    trackerRole.TrackerArrows.Clear();
                    UnityEngine.Object.Destroy(trackerRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Mystic))
                {
                    var mysticRole = Role.GetRole<Mystic>(PlayerControl.LocalPlayer);
                    mysticRole.BodyArrows.Values.DestroyAll();
                    mysticRole.BodyArrows.Clear();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
                {
                    var transporterRole = Role.GetRole<Transporter>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(transporterRole.UsesText);
                    if (transporterRole.TransportList != null)
                    {
                        transporterRole.TransportList.Toggle();
                        transporterRole.TransportList.SetVisible(false);
                        transporterRole.TransportList = null;
                        transporterRole.PressedButton = false;
                        transporterRole.TransportPlayer1 = null;
                    }
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
                {
                    var veteranRole = Role.GetRole<Veteran>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(veteranRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Trapper))
                {
                    var trapperRole = Role.GetRole<Trapper>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(trapperRole.UsesText);
                    trapperRole.traps.ClearTraps();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Detective))
                {
                    var detecRole = Role.GetRole<Detective>(PlayerControl.LocalPlayer);
                    detecRole.ExamineButton.gameObject.SetActive(false);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Aurial))
                {
                    var aurialRole = Role.GetRole<Aurial>(PlayerControl.LocalPlayer);
                    aurialRole.NormalVision = true;
                    SeeAll.AllToNormal();
                    CameraEffect.singleton.materials.Clear();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Survivor))
                {
                    var survRole = Role.GetRole<Survivor>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(survRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel))
                {
                    var gaRole = Role.GetRole<GuardianAngel>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(gaRole.UsesText);
                }
            }

            Role.RoleDictionary.Remove(newVamp.PlayerId);

            if (PlayerControl.LocalPlayer == newVamp)
            {
                var role = new Vampire(PlayerControl.LocalPlayer);
                role.CorrectKills = killsList.CorrectKills;
                role.IncorrectKills = killsList.IncorrectKills;
                role.CorrectAssassinKills = killsList.CorrectAssassinKills;
                role.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
                role.RegenTask();
            }
            else
            {
                var role = new Vampire(newVamp);
                role.CorrectKills = killsList.CorrectKills;
                role.IncorrectKills = killsList.IncorrectKills;
                role.CorrectAssassinKills = killsList.CorrectAssassinKills;
                role.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
            }

=======
                {
                    role.LastBit = DateTime.UtcNow;
                    role.LastBit = role.LastBit.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[3] == true) return false;
                return false;
            }
            else
            {
                var interact = Utils.Interact(PlayerControl.LocalPlayer, role.ClosestPlayer, true);
                if (interact[4] == true) return false;
                if (interact[0] == true)
                {
                    role.LastBit = DateTime.UtcNow;
                    return false;
                }
                else if (interact[1] == true)
                {
                    role.LastBit = DateTime.UtcNow;
                    role.LastBit = role.LastBit.AddSeconds(CustomGameOptions.ProtectKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[2] == true)
                {
                    role.LastBit = DateTime.UtcNow;
                    role.LastBit = role.LastBit.AddSeconds(CustomGameOptions.VestKCReset - CustomGameOptions.BiteCd);
                    return false;
                }
                else if (interact[3] == true) return false;
                return false;
            }
        }

        public static void Convert(PlayerControl newVamp)
        {
            var oldRole = Role.GetRole(newVamp);
            var killsList = (oldRole.CorrectKills, oldRole.IncorrectKills, oldRole.CorrectAssassinKills, oldRole.IncorrectAssassinKills);

            if (newVamp.Is(RoleEnum.Snitch))
            {
                var snitch = Role.GetRole<Snitch>(newVamp);
                snitch.SnitchArrows.Values.DestroyAll();
                snitch.SnitchArrows.Clear();
                snitch.ImpArrows.DestroyAll();
                snitch.ImpArrows.Clear();
            }

            if (newVamp == StartImitate.ImitatingPlayer) StartImitate.ImitatingPlayer = null;

            if (newVamp.Is(RoleEnum.GuardianAngel))
            {
                var ga = Role.GetRole<GuardianAngel>(newVamp);
                ga.UnProtect();
            }

            if (newVamp.Is(RoleEnum.Medium))
            {
                var medRole = Role.GetRole<Medium>(newVamp);
                medRole.MediatedPlayers.Values.DestroyAll();
                medRole.MediatedPlayers.Clear();
            }

            if (newVamp.Is(RoleEnum.SoulCollector))
            {
                var scRole = Role.GetRole<SoulCollector>(newVamp);
                foreach (GameObject soul in scRole.Souls)
                {
                    UnityEngine.Object.Destroy(soul);
                }
            }

            if (PlayerControl.LocalPlayer == newVamp)
            {
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Detective)) Role.GetRole<Detective>(PlayerControl.LocalPlayer).ExamineButton.SetTarget(null);
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Hunter)) Role.GetRole<Hunter>(PlayerControl.LocalPlayer).StalkButton.SetTarget(null);
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.SoulCollector)) Role.GetRole<SoulCollector>(PlayerControl.LocalPlayer).ReapButton.SetTarget(null);
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Altruist)) CrewmateRoles.AltruistMod.KillButtonTarget.SetTarget(HudManager.Instance.KillButton, null, Role.GetRole<Altruist>(PlayerControl.LocalPlayer));
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Amnesiac)) AmnesiacMod.KillButtonTarget.SetTarget(HudManager.Instance.KillButton, null, Role.GetRole<Amnesiac>(PlayerControl.LocalPlayer));

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator)) Footprint.DestroyAll(Role.GetRole<Investigator>(PlayerControl.LocalPlayer));

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Sheriff)) HudManager.Instance.KillButton.buttonLabelText.gameObject.SetActive(false);

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Engineer))
                {
                    var engineerRole = Role.GetRole<Engineer>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(engineerRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
                {
                    var trackerRole = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);
                    trackerRole.TrackerArrows.Values.DestroyAll();
                    trackerRole.TrackerArrows.Clear();
                    UnityEngine.Object.Destroy(trackerRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Aurial))
                {
                    var aurialRole = Role.GetRole<Aurial>(PlayerControl.LocalPlayer);
                    aurialRole.SenseArrows.Values.DestroyAll();
                    aurialRole.SenseArrows.Clear();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Mystic))
                {
                    var mysticRole = Role.GetRole<Mystic>(PlayerControl.LocalPlayer);
                    mysticRole.BodyArrows.Values.DestroyAll();
                    mysticRole.BodyArrows.Clear();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
                {
                    var transporterRole = Role.GetRole<Transporter>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(transporterRole.UsesText);
                    try
                    {
                        PlayerMenu.singleton.Menu.Close();
                    }
                    catch { }
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
                {
                    var veteranRole = Role.GetRole<Veteran>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(veteranRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Trapper))
                {
                    var trapperRole = Role.GetRole<Trapper>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(trapperRole.UsesText);
                    trapperRole.traps.ClearTraps();
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Detective))
                {
                    var detecRole = Role.GetRole<Detective>(PlayerControl.LocalPlayer);
                    detecRole.ExamineButton.gameObject.SetActive(false);
                    foreach (GameObject scene in detecRole.CrimeScenes)
                    {
                        UnityEngine.Object.Destroy(scene);
                    }
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.SoulCollector))
                {
                    var scRole = Role.GetRole<SoulCollector>(PlayerControl.LocalPlayer);
                    scRole.ReapButton.gameObject.SetActive(false);
                    UnityEngine.Object.Destroy(scRole.CollectedText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Hunter))
                {
                    var hunterRole = Role.GetRole<Hunter>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(hunterRole.UsesText);
                    hunterRole.StalkButton.SetTarget(null);
                    hunterRole.StalkButton.gameObject.SetActive(false);
                    HudManager.Instance.KillButton.buttonLabelText.gameObject.SetActive(false);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Survivor))
                {
                    var survRole = Role.GetRole<Survivor>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(survRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel))
                {
                    var gaRole = Role.GetRole<GuardianAngel>(PlayerControl.LocalPlayer);
                    UnityEngine.Object.Destroy(gaRole.UsesText);
                }
            }

            Role.RoleDictionary.Remove(newVamp.PlayerId);

            if (PlayerControl.LocalPlayer == newVamp)
            {
                var role = new Vampire(PlayerControl.LocalPlayer);
                role.CorrectKills = killsList.CorrectKills;
                role.IncorrectKills = killsList.IncorrectKills;
                role.CorrectAssassinKills = killsList.CorrectAssassinKills;
                role.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
                role.RegenTask();
            }
            else
            {
                var role = new Vampire(newVamp);
                role.CorrectKills = killsList.CorrectKills;
                role.IncorrectKills = killsList.IncorrectKills;
                role.CorrectAssassinKills = killsList.CorrectAssassinKills;
                role.IncorrectAssassinKills = killsList.IncorrectAssassinKills;
            }

>>>>>>> Stashed changes
            if (CustomGameOptions.NewVampCanAssassin) new Assassin(newVamp);
        }
    }
}
