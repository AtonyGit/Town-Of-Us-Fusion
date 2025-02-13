using System;
using HarmonyLib;
using Hazel;
using TownOfUsFusion.CrewmateRoles.BodyguardMod;
using TownOfUsFusion.NeutralRoles.ExecutionerMod;
using TownOfUsFusion.NeutralRoles.GuardianAngelMod;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Apocalypse;
using TownOfUsFusion.Roles.Cultist;
using TownOfUsFusion.Roles.Modifiers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.Patches
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__35), nameof(IntroCutscene._CoBegin_d__35.MoveNext))]
public static class Start
{
    public static Sprite Sprite => TownOfUsFusion.Arrow;
    public static void Postfix(IntroCutscene._CoBegin_d__35 __instance)
    {
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Detective))
        {
            var detective = Role.GetRole<Detective>(PlayerControl.LocalPlayer);
            detective.LastExamined = DateTime.UtcNow;
            detective.LastExamined = detective.LastExamined.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.ExamineCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Medium))
        {
            var medium = Role.GetRole<Medium>(PlayerControl.LocalPlayer);
            medium.LastMediated = DateTime.UtcNow;
            medium.LastMediated = medium.LastMediated.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.MediateCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Seer))
        {
            var seer = Role.GetRole<Seer>(PlayerControl.LocalPlayer);
            seer.LastInvestigated = DateTime.UtcNow;
            seer.LastInvestigated = seer.LastInvestigated.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SeerCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Oracle))
        {
            var oracle = Role.GetRole<Oracle>(PlayerControl.LocalPlayer);
            oracle.LastConfessed = DateTime.UtcNow;
            oracle.LastConfessed = oracle.LastConfessed.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.ConfessCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Aurial))
        {
            var aurial = Role.GetRole<Aurial>(PlayerControl.LocalPlayer);
            aurial.LastRadiated = DateTime.UtcNow;
            aurial.LastRadiated = aurial.LastRadiated.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.RadiateCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.CursedSoul))
        {
            var cs = Role.GetRole<CursedSoul>(PlayerControl.LocalPlayer);
            cs.LastSoulSwapped = DateTime.UtcNow;
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Sheriff))
        {
            var sheriff = Role.GetRole<Sheriff>(PlayerControl.LocalPlayer);
            sheriff.LastKilled = DateTime.UtcNow;
            sheriff.LastKilled = sheriff.LastKilled.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SheriffKillCd);
        }
        if (CustomGameOptions.SheriffShootRoundOne)
        {
            foreach (var sh in Role.GetRoles(RoleEnum.Sheriff))
            {
                var shRole = (Sheriff)sh;
                shRole.CanShoot = true;
            }
        }
        
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Inquisitor))
        {
            var inquis = Role.GetRole<Inquisitor>(PlayerControl.LocalPlayer);
            inquis.LastInquired = DateTime.UtcNow;
            inquis.LastInquired = inquis.LastInquired.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.InquireCooldown);
            inquis.LastVanquished = DateTime.UtcNow;
            inquis.LastVanquished = inquis.LastVanquished.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.VanquishCooldown);
        }
        if (CustomGameOptions.VanquishRoundOne && CustomGameOptions.VanquishEnabled)
        {
            foreach (var sh in Role.GetRoles(RoleEnum.Inquisitor))
            {
                var shRole = (Inquisitor)sh;
                shRole.canVanquish = true;
            }
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
        {
            var tracker = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);
            tracker.LastTracked = DateTime.UtcNow;
            tracker.LastTracked = tracker.LastTracked.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.TrackCd);
        }

            if (PlayerControl.LocalPlayer.Is(RoleEnum.Hunter))
            {
                var hunter = Role.GetRole<Hunter>(PlayerControl.LocalPlayer);
                hunter.LastKilled = DateTime.UtcNow;
                hunter.LastKilled = hunter.LastKilled.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.HunterKillCd);
            }
            
        if (PlayerControl.LocalPlayer.Is(RoleEnum.VampireHunter))
        {
            var vh = Role.GetRole<VampireHunter>(PlayerControl.LocalPlayer);
            vh.LastStaked = DateTime.UtcNow;
            vh.LastStaked = vh.LastStaked.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.StakeCd);
        }

        if (CustomGameOptions.CanStakeRoundOne)
        {
            foreach (var vh in Role.GetRoles(RoleEnum.VampireHunter))
            {
                var vhRole = (VampireHunter)vh;
                vhRole.UsesLeft = CustomGameOptions.MaxFailedStakesPerGame;
                vhRole.AddedStakes = true;
            }
        }
            
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Trickster))
        {
            var vh = Role.GetRole<Trickster>(PlayerControl.LocalPlayer);
            vh.LastKilled = DateTime.UtcNow;
            vh.LastKilled = vh.LastKilled.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.TrickCd);
        }

        if (CustomGameOptions.CanTrickRoundOne)
        {
            foreach (var vh in Role.GetRoles(RoleEnum.Trickster))
            {
                var vhRole = (Trickster)vh;
                vhRole.UsesLeft = CustomGameOptions.MaxFailedTricksPerGame;
                vhRole.AddedTricks = true;
            }
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Jackal))
        {
            var jackal = Role.GetRole<Jackal>(PlayerControl.LocalPlayer);
            jackal.LastKill = DateTime.UtcNow;
            jackal.LastKill = jackal.LastKill.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.JackalKillCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Scourge))
        {
            var scourge = Role.GetRole<Scourge>(PlayerControl.LocalPlayer);
            scourge.LastKilled = DateTime.UtcNow;
            scourge.LastKilled = scourge.LastKilled.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.ScourgeKillCooldown);
        }
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Apparitionist))
        {
            var appa = Role.GetRole<Apparitionist>(PlayerControl.LocalPlayer);
            appa.LastResurrected = DateTime.UtcNow;
            appa.LastResurrected = appa.LastResurrected.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.AppaResurrectCooldown);
        }
        if (PlayerControl.LocalPlayer.Is(RoleEnum.NeoNecromancer))
        {
            var necro = Role.GetRole<NeoNecromancer>(PlayerControl.LocalPlayer);
            necro.LastResurrected = DateTime.UtcNow;
            necro.LastKilled = DateTime.UtcNow;
            necro.LastResurrected = necro.LastResurrected.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.NecroResurrectCooldown);
            necro.LastKilled = necro.LastKilled.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.NecroKillCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Sentinel))
        {
            var sent = Role.GetRole<Sentinel>(PlayerControl.LocalPlayer);
            sent.LastCharged = DateTime.UtcNow;
            sent.LastKilled = DateTime.UtcNow;
            sent.LastPlaced = DateTime.UtcNow;
            sent.LastStunned = DateTime.UtcNow;
            sent.LastStunned = sent.LastStunned.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SentinelStunCd);
            sent.LastPlaced = sent.LastPlaced.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SentinelPlaceCd);
            sent.LastCharged = sent.LastCharged.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SentinelChargeCd);
            sent.LastKilled = sent.LastKilled.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SentinelChargeCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
        {
            var transporter = Role.GetRole<Transporter>(PlayerControl.LocalPlayer);
            transporter.LastTransported = DateTime.UtcNow;
            transporter.LastTransported = transporter.LastTransported.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.TransportCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Trapper))
        {
            var trapper = Role.GetRole<Trapper>(PlayerControl.LocalPlayer);
            trapper.LastTrapped = DateTime.UtcNow;
            trapper.LastTrapped = trapper.LastTrapped.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.TrapCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
        {
            var veteran = Role.GetRole<Veteran>(PlayerControl.LocalPlayer);
            veteran.LastAlerted = DateTime.UtcNow;
            veteran.LastAlerted = veteran.LastAlerted.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.AlertCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Chameleon))
        {
            var chameleon = Role.GetRole<Chameleon>(PlayerControl.LocalPlayer);
            chameleon.LastSwooped = DateTime.UtcNow;
            chameleon.LastSwooped = chameleon.LastSwooped.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SwoopCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Necromancer))
        {
            var necro = Role.GetRole<Necromancer>(PlayerControl.LocalPlayer);
            necro.LastRevived = DateTime.UtcNow;
            necro.LastRevived = necro.LastRevived.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.ReviveCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.CultistSeer))
        {
            var seer = Role.GetRole<CultistSeer>(PlayerControl.LocalPlayer);
            seer.LastInvestigated = DateTime.UtcNow;
            seer.LastInvestigated = seer.LastInvestigated.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SeerCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Whisperer))
        {
            var whisperer = Role.GetRole<Whisperer>(PlayerControl.LocalPlayer);
            whisperer.LastWhispered = DateTime.UtcNow;
            whisperer.LastWhispered = whisperer.LastWhispered.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.WhisperCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Blackmailer))
        {
            var blackmailer = Role.GetRole<Blackmailer>(PlayerControl.LocalPlayer);
            blackmailer.LastBlackmailed = DateTime.UtcNow;
            blackmailer.LastBlackmailed = blackmailer.LastBlackmailed.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.BlackmailCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Escapist))
        {
            var escapist = Role.GetRole<Escapist>(PlayerControl.LocalPlayer);
            escapist.LastEscape = DateTime.UtcNow;
            escapist.LastEscape = escapist.LastEscape.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.EscapeCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Grenadier))
        {
            var grenadier = Role.GetRole<Grenadier>(PlayerControl.LocalPlayer);
            grenadier.LastFlashed = DateTime.UtcNow;
            grenadier.LastFlashed = grenadier.LastFlashed.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.GrenadeCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Miner))
        {
            var miner = Role.GetRole<Miner>(PlayerControl.LocalPlayer);
            miner.LastMined = DateTime.UtcNow;
            miner.LastMined = miner.LastMined.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.MineCd);
            var vents = Object.FindObjectsOfType<Vent>();
            miner.VentSize =
                Vector2.Scale(vents[0].GetComponent<BoxCollider2D>().size, vents[0].transform.localScale) * 0.75f;
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Morphling))
        {
            var morphling = Role.GetRole<Morphling>(PlayerControl.LocalPlayer);
            morphling.LastMorphed = DateTime.UtcNow;
            morphling.LastMorphed = morphling.LastMorphed.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.MorphlingCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Swooper))
        {
            var swooper = Role.GetRole<Swooper>(PlayerControl.LocalPlayer);
            swooper.LastSwooped = DateTime.UtcNow;
            swooper.LastSwooped = swooper.LastSwooped.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SwoopCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Venerer))
        {
            var venerer = Role.GetRole<Venerer>(PlayerControl.LocalPlayer);
            venerer.LastCamouflaged = DateTime.UtcNow;
            venerer.LastCamouflaged = venerer.LastCamouflaged.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.AbilityCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Undertaker))
        {
            var undertaker = Role.GetRole<Undertaker>(PlayerControl.LocalPlayer);
            undertaker.LastDragged = DateTime.UtcNow;
            undertaker.LastDragged = undertaker.LastDragged.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.DragCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Arsonist))
        {
            var arsonist = Role.GetRole<Arsonist>(PlayerControl.LocalPlayer);
            arsonist.LastDoused = DateTime.UtcNow;
            arsonist.LastDoused = arsonist.LastDoused.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.DouseCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer))
        {
            var doomsayer = Role.GetRole<Doomsayer>(PlayerControl.LocalPlayer);
            doomsayer.LastObserved = DateTime.UtcNow;
            doomsayer.LastObserved = doomsayer.LastObserved.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.ObserveCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Executioner))
        {
            var exe = Role.GetRole<Executioner>(PlayerControl.LocalPlayer);
            if (exe.target == null)
            {
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.ExecutionerToJester, SendOption.Reliable, -1);
                writer.Write(exe.Player.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

                TargetColor.ExeToJes(exe.Player);
            }
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Glitch))
        {
            var glitch = Role.GetRole<Glitch>(PlayerControl.LocalPlayer);
            glitch.LastKill = DateTime.UtcNow;
            glitch.LastHack = DateTime.UtcNow;
            glitch.LastMimic = DateTime.UtcNow;
            glitch.LastKill = glitch.LastKill.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.GlitchKillCooldown);
            glitch.LastHack = glitch.LastHack.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.HackCooldown);
            glitch.LastMimic = glitch.LastMimic.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.MimicCooldown);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Inquisitor))
        {
            var inquis = Role.GetRole<Inquisitor>(PlayerControl.LocalPlayer);
            inquis.LastInquired = DateTime.UtcNow;
            inquis.LastVanquished = DateTime.UtcNow;
            //inquis.canVanquish = true;
            inquis.LastInquired = inquis.LastInquired.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.InquireCooldown);
            inquis.LastVanquished = inquis.LastVanquished.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.VanquishCooldown);
        }
            foreach (var bg1 in Role.GetRoles(RoleEnum.Inquisitor))
            {
                var bgRole = (Inquisitor)bg1;
                bgRole.canVanquish = true;
            }

            foreach (var bg in Role.GetRoles(RoleEnum.Bodyguard))
            {
                var bgRole = (Bodyguard)bg;
                bgRole.exGuarded = bgRole.GuardedPlayer;
                bgRole.GuardedPlayer = null;
            }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Bodyguard))
        {
            var bg = Role.GetRole<Bodyguard>(PlayerControl.LocalPlayer);
            if (bg.GuardedPlayer != null)
            {
        foreach (var role in Role.GetRoles(RoleEnum.Bodyguard))
            {
                ((Bodyguard)role).exGuarded = ((Bodyguard)role).GuardedPlayer;
                ((Bodyguard)role).GuardedPlayer = null;
                System.Console.WriteLine(((Bodyguard)role).GuardedPlayer.name + " Is Ex-Guarded");
            }

                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.GuardReset, SendOption.Reliable, -1);
                writer.Write(bg.Player.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

                ShowGuarded.GuardReset(bg.Player);
            }
            //bg.LastGuarded = bg.LastGuarded.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.GuardCd);
        }/*
            foreach (var bg1 in Role.GetRoles(RoleEnum.Bodyguard))
            {
                var bgRole = (Bodyguard)bg1;
                var writer = AmongUsClient.Instance.StartRpcImmediately(bgRole.NetId,
                    (byte)CustomRPC.GuardReset, SendOption.Reliable, -1);
                writer.Write(bgRole.Player.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

                ShowGuarded.GuardReset(bgRole.Player);
            }*/

        if (PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel))
        {
            var ga = Role.GetRole<GuardianAngel>(PlayerControl.LocalPlayer);
            ga.LastProtected = DateTime.UtcNow;
            ga.LastProtected = ga.LastProtected.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.ProtectCd);
            if (ga.target == null)
            {
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.GAToSurv, SendOption.Reliable, -1);
                writer.Write(ga.Player.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

                GATargetColor.GAToSurv(ga.Player);
            }
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Berserker))
        {
            var Berserker = Role.GetRole<Berserker>(PlayerControl.LocalPlayer);
            Berserker.LastKill = DateTime.UtcNow;
            Berserker.LastKill = Berserker.LastKill.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.JuggKCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Plaguebearer))
        {
            var plaguebearer = Role.GetRole<Plaguebearer>(PlayerControl.LocalPlayer);
            plaguebearer.LastInfected = DateTime.UtcNow;
            plaguebearer.LastInfected = plaguebearer.LastInfected.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.InfectCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Survivor))
        {
            var surv = Role.GetRole<Survivor>(PlayerControl.LocalPlayer);
            surv.LastVested = DateTime.UtcNow;
            surv.LastVested = surv.LastVested.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.VestCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Werewolf))
        {
            var werewolf = Role.GetRole<Werewolf>(PlayerControl.LocalPlayer);
            werewolf.LastRampaged = DateTime.UtcNow;
            werewolf.LastKilled = DateTime.UtcNow;
            werewolf.LastRampaged = werewolf.LastRampaged.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.RampageCd);
            werewolf.LastKilled = werewolf.LastKilled.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.RampageKillCd);
        }

        if (PlayerControl.LocalPlayer.Is(RoleEnum.Vampire))
        {
            var vamp = Role.GetRole<Vampire>(PlayerControl.LocalPlayer);
            vamp.LastBit = DateTime.UtcNow;
            vamp.LastBit = vamp.LastBit.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.BiteCd);
        }

        if (PlayerControl.LocalPlayer.Is(ModifierEnum.Radar))
        {
            var radar = Modifier.GetModifier<Radar>(PlayerControl.LocalPlayer);
            var gameObj = new GameObject();
            var arrow = gameObj.AddComponent<ArrowBehaviour>();
            gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
            var renderer = gameObj.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite;
            renderer.color = Colors.Radar;
            arrow.image = renderer;
            gameObj.layer = 5;
            arrow.target = PlayerControl.LocalPlayer.transform.position;
            radar.RadarArrow.Add(arrow);
        }
    }
}
}