using System;
using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine;
using AmongUs.GameOptions;
using TownOfUsFusion.CrewmateRoles.InvestigatorMod;
using TownOfUsFusion.CrewmateRoles.TrapperMod;
using TownOfUsFusion.CrewmateRoles.ImitatorMod;
using System.Linq;
using TownOfUsFusion.Roles.Modifiers;
using TownOfUsFusion.CrewmateRoles.AurialMod;
using TownOfUsFusion.Patches.ScreenEffects;
using Hazel;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.Roles.Apocalypse;

namespace TownOfUsFusion.NeutralRoles.VampireMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
public class Bite
{
    public static Sprite BiteSprite => TownOfUsFusion.BiteSprite;
    public static Sprite BittenSprite => TownOfUsFusion.BittenSprite;
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
        if (role.Enabled == true) return false;
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
        if (role.ClosestPlayer.Is(RoleEnum.VampireHunter))
        {
            role.LastBit = DateTime.UtcNow;
            Utils.RpcMurderPlayer(role.ClosestPlayer, PlayerControl.LocalPlayer);
            return false;
        }
        
        var canBeConverted = ((role.ClosestPlayer.Is(Faction.Crewmates) || (role.ClosestPlayer.Is(Faction.NeutralBenign)
            && CustomGameOptions.CanBiteNeutralBenign) || (role.ClosestPlayer.Is(Faction.NeutralChaos)
            && CustomGameOptions.CanBiteNeutralChaos) || (role.ClosestPlayer.Is(Faction.NeutralEvil)
            && CustomGameOptions.CanBiteNeutralEvil)) && !role.ClosestPlayer.Is(AllianceEnum.Lover) &&
            !role.ClosestPlayer.Is(AllianceEnum.Crewpocalypse) && !role.ClosestPlayer.Is(AllianceEnum.Crewpostor) && !role.ClosestPlayer.Is(AllianceEnum.Recruit) &&
            aliveVamps.Count == 1 && vamps.Count < CustomGameOptions.MaxVampiresPerGame);


            if (role.ClosestPlayer.Is(RoleEnum.Pestilence))
            {
                if (role.Player.IsShielded())
                {
                    var medic = role.Player.GetMedic().Player.PlayerId;
                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                    writer.Write(medic);
                    writer.Write(role.Player.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    if (CustomGameOptions.ShieldBreaks) role.LastBit = DateTime.UtcNow;
                    __instance.SetCoolDown(0.01f, 1f);

                    StopKill.BreakShield(medic, role.Player.PlayerId,
                        CustomGameOptions.ShieldBreaks);
                }
                if (role.Player.IsProtected())
                {
                    role.LastBit.AddSeconds(CustomGameOptions.ProtectKCReset);
                    __instance.SetCoolDown(0.01f, 1f);
                    return false;
                }
                Utils.RpcMultiMurderPlayer(role.ClosestPlayer, PlayerControl.LocalPlayer);
                return false;
            }
            if (role.ClosestPlayer.IsInfected() || role.Player.IsInfected())
            {
                foreach (var pb in Role.GetRoles(RoleEnum.Plaguebearer)) ((Plaguebearer)pb).RpcSpreadInfection(role.ClosestPlayer, role.Player);
            }
            if (role.ClosestPlayer.IsOnAlert())
            {
                if (role.Player.IsShielded())
                {
                    var medic = role.Player.GetMedic().Player.PlayerId;
                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                    writer.Write(medic);
                    writer.Write(role.Player.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    if (CustomGameOptions.ShieldBreaks) role.LastBit = DateTime.UtcNow;
                    __instance.SetCoolDown(0.01f, 1f);

                    StopKill.BreakShield(medic, role.Player.PlayerId,
                        CustomGameOptions.ShieldBreaks);
                    if (CustomGameOptions.KilledOnAlert && !role.ClosestPlayer.IsProtected())
                    {
                        role.BittenPlayer = role.ClosestPlayer;
                        __instance.SetTarget(null);
                        DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);

                        role.TimeRemaining = CustomGameOptions.BiteDuration;
                        __instance.SetCoolDown(role.TimeRemaining, CustomGameOptions.BiteDuration);
                        var writer4 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                            (byte)CustomRPC.RemoteBite,
                        SendOption.Reliable, -1);
                        Convert(role.ClosestPlayer);
                        //Utils.Rpc(CustomRPC.Bite, role.ClosestPlayer.PlayerId);

                        writer4.Write(PlayerControl.LocalPlayer.PlayerId);
                        writer4.Write(role.BittenPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer4);
                    }
                }
                else
                {
                    if (!PlayerControl.LocalPlayer.IsProtected())
                    {
                        Utils.RpcMultiMurderPlayer(role.ClosestPlayer, role.Player);
                    }
                    else
                    {
                        role.LastBit.AddSeconds(CustomGameOptions.ProtectKCReset + 0.01f);
                        __instance.SetCoolDown(0.01f, 1f);
                    }
                }
                return false;
            }
            else if (role.ClosestPlayer.IsShielded() && !canBeConverted)
            {
                var medic = role.ClosestPlayer.GetMedic().Player.PlayerId;
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                writer.Write(medic);
                writer.Write(role.ClosestPlayer.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

                if (CustomGameOptions.ShieldBreaks) role.LastBit = DateTime.UtcNow;
                __instance.SetCoolDown(0.01f, 1f);

                StopKill.BreakShield(medic, role.ClosestPlayer.PlayerId,
                    CustomGameOptions.ShieldBreaks);

                return false;
            }
            /*
            else if (role.ClosestPlayer.IsVesting())
            {
                role.LastBit.AddSeconds(CustomGameOptions.VestKCReset + 0.01f);
                __instance.SetCoolDown(0.01f, 1f);
                return false;
            }*/
            else if (role.ClosestPlayer.IsProtected() && !canBeConverted)
            {
                role.LastBit.AddSeconds(CustomGameOptions.ProtectKCReset + 0.01f);
                __instance.SetCoolDown(0.01f, 1f);
                return false;
            }
            role.BittenPlayer = role.ClosestPlayer;
            __instance.SetTarget(null);
            DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);
            role.TimeRemaining = CustomGameOptions.BiteDuration;
            __instance.SetCoolDown(role.TimeRemaining, CustomGameOptions.BiteDuration);
            var writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte)CustomRPC.RemoteBite,
            SendOption.Reliable, -1);
            writer2.Write(PlayerControl.LocalPlayer.PlayerId);
            writer2.Write(role.BittenPlayer.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer2);


            // role.Player.SetKillTimer(0);
            return false;


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

        if (CustomGameOptions.NewVampCanAssassin) new Assassin(newVamp);
    }
}
}
