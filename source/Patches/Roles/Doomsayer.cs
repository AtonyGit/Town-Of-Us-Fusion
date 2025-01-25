﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.NeutralRoles.ExecutionerMod;
using TownOfUsFusion.NeutralRoles.GuardianAngelMod;
using System;

namespace TownOfUsFusion.Roles
{
    public class Doomsayer : Role, IGuesser
    {
        public Dictionary<byte, (GameObject, GameObject, GameObject, TMP_Text)> Buttons { get; set; } = new();
        public Dictionary<byte, TMP_Text> RoleGuess { get; set; } = new();

        private Dictionary<string, Color> ColorMapping = new();

        public Dictionary<string, Color> SortedColorMapping;

        public Dictionary<byte, string> Guesses = new();
        public DateTime LastObserved;
        public PlayerControl ClosestPlayer;
        public PlayerControl LastObservedPlayer;

        public Doomsayer(PlayerControl player) : base(player)
        {
            Name = "Doomsayer";
            ImpostorText = () => "Guess People's Roles To Win!";
            TaskText = () => "Win by guessing player's roles\nFake Tasks:";
            Color = Patches.Colors.Doomsayer;
            RoleType = RoleEnum.Doomsayer;
            LastObserved = DateTime.UtcNow;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralEvil;

            ColorMapping.Add("Crewmate", Colors.Crewmate);
            if (CustomGameOptions.PoliticianOn > 0) ColorMapping.Add("Politician", Colors.Politician);
            if (CustomGameOptions.SheriffOn > 0) ColorMapping.Add("Sheriff", Colors.Sheriff);
            if (CustomGameOptions.EngineerOn > 0) ColorMapping.Add("Engineer", Colors.Engineer);
            if (CustomGameOptions.SwapperOn > 0) ColorMapping.Add("Swapper", Colors.Swapper);
            if (CustomGameOptions.MedicOn > 0) ColorMapping.Add("Medic", Colors.Medic);
            if (CustomGameOptions.PsychicOn > 0) ColorMapping.Add("Psychic", Colors.Psychic);
            if (CustomGameOptions.SpyOn > 0) ColorMapping.Add("Spy", Colors.Spy);
            if (CustomGameOptions.AltruistOn > 0) ColorMapping.Add("Altruist", Colors.Altruist);
            if (CustomGameOptions.VigilanteOn > 0) ColorMapping.Add("Vigilante", Colors.Vigilante);
            if (CustomGameOptions.VeteranOn > 0) ColorMapping.Add("Veteran", Colors.Veteran);
            if (CustomGameOptions.HunterOn > 0) ColorMapping.Add("Hunter", Colors.Hunter);
            if (CustomGameOptions.TrackerOn > 0) ColorMapping.Add("Tracker", Colors.Tracker);
            if (CustomGameOptions.TrapperOn > 0) ColorMapping.Add("Trapper", Colors.Trapper);
            if (CustomGameOptions.TransporterOn > 0) ColorMapping.Add("Transporter", Colors.Transporter);
            if (CustomGameOptions.MediumOn > 0) ColorMapping.Add("Medium", Colors.Medium);
            if (CustomGameOptions.InvestigatorOn > 0) ColorMapping.Add("Investigator", Colors.Investigator);
            if (CustomGameOptions.ImitatorOn > 0) ColorMapping.Add("Imitator", Colors.Imitator);
            if (CustomGameOptions.ProsecutorOn > 0) ColorMapping.Add("Prosecutor", Colors.Prosecutor);
            if (CustomGameOptions.OracleOn > 0) ColorMapping.Add("Oracle", Colors.Oracle);
            if (CustomGameOptions.AurialOn > 0) ColorMapping.Add("Aurial", Colors.Aurial);
            if (CustomGameOptions.JailorOn > 0) ColorMapping.Add("Jailor", Colors.Jailor);
            if (CustomGameOptions.LookoutOn > 0) ColorMapping.Add("Lookout", Colors.Lookout);
            if (CustomGameOptions.DeputyOn > 0) ColorMapping.Add("Deputy", Colors.Deputy);

            if (CustomGameOptions.DoomsayerGuessImpostors && !PlayerControl.LocalPlayer.Is(Faction.Impostors))
            {
                ColorMapping.Add("Impostor", Colors.Impostor);
                if (CustomGameOptions.JanitorOn > 0) ColorMapping.Add("Janitor", Colors.Impostor);
                if (CustomGameOptions.MorphlingOn > 0) ColorMapping.Add("Morphling", Colors.Impostor);
                if (CustomGameOptions.MinerOn > 0) ColorMapping.Add("Miner", Colors.Impostor);
                if (CustomGameOptions.SwooperOn > 0) ColorMapping.Add("Swooper", Colors.Impostor);
                if (CustomGameOptions.UndertakerOn > 0) ColorMapping.Add("Undertaker", Colors.Impostor);
                if (CustomGameOptions.EscapistOn > 0) ColorMapping.Add("Escapist", Colors.Impostor);
                if (CustomGameOptions.GrenadierOn > 0) ColorMapping.Add("Grenadier", Colors.Impostor);
                if (CustomGameOptions.TraitorOn > 0) ColorMapping.Add("Traitor", Colors.Impostor);
                if (CustomGameOptions.BlackmailerOn > 0) ColorMapping.Add("Blackmailer", Colors.Impostor);
                if (CustomGameOptions.BomberOn > 0) ColorMapping.Add("Bomber", Colors.Impostor);
                if (CustomGameOptions.PoisonerOn > 0) ColorMapping.Add("Poisoner", Colors.Impostor);
                if (CustomGameOptions.WarlockOn > 0) ColorMapping.Add("Warlock", Colors.Impostor);
                if (CustomGameOptions.VenererOn > 0) ColorMapping.Add("Venerer", Colors.Impostor);
                if (CustomGameOptions.HypnotistOn > 0) ColorMapping.Add("Hypnotist", Colors.Impostor);
            }

            if (CustomGameOptions.DoomsayerGuessNeutralBenign)
            {
                if (CustomGameOptions.AmnesiacOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Amnesiac) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Amnesiac)) ColorMapping.Add("Amnesiac", Colors.Amnesiac);
                if (CustomGameOptions.GuardianAngelOn > 0) ColorMapping.Add("Guardian Angel", Colors.GuardianAngel);
                if (CustomGameOptions.SurvivorOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Survivor) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Survivor)) ColorMapping.Add("Survivor", Colors.Survivor);
            }
            if (CustomGameOptions.AssassinGuessNeutralChaos)
            {
                if (CustomGameOptions.CannibalOn > 0) ColorMapping.Add("Cannibal", Colors.Cannibal);
            }
            if (CustomGameOptions.DoomsayerGuessNeutralEvil)
            {
                if (!CustomGameOptions.UniqueRoles) ColorMapping.Add("Doomsayer", Colors.Doomsayer);
                if (CustomGameOptions.ExecutionerOn > 0) ColorMapping.Add("Executioner", Colors.Executioner);
                if (CustomGameOptions.JesterOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Jester) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Jester)) ColorMapping.Add("Jester", Colors.Jester);
            }
            if (CustomGameOptions.DoomsayerGuessNeutralKilling)
            {
                if (CustomGameOptions.ArsonistOn > 0) ColorMapping.Add("Arsonist", Colors.Arsonist);
                if (CustomGameOptions.SerialKillerOn > 0) ColorMapping.Add("Serial Killer", Colors.SerialKiller);
                if (CustomGameOptions.GlitchOn > 0) ColorMapping.Add("The Glitch", Colors.Glitch);
                if (CustomGameOptions.WerewolfOn > 0) ColorMapping.Add("Werewolf", Colors.Werewolf);
            }
            if (CustomGameOptions.DoomsayerGuessNeutralNeophyte)
            {
                if (CustomGameOptions.VampireOn > 0) ColorMapping.Add("Vampire", Colors.Vampire);
            }
            if (CustomGameOptions.DoomsayerGuessNeutralApocalypse)
            {
                if (CustomGameOptions.PlaguebearerOn > 0) ColorMapping.Add("Plaguebearer", Colors.Apocalypse);
                if (CustomGameOptions.JuggernautOn > 0) ColorMapping.Add("Juggernaut", Colors.Apocalypse);
                if (CustomGameOptions.SoulCollectorOn > 0) ColorMapping.Add("Soul Collector", Colors.Apocalypse);
            }

            SortedColorMapping = ColorMapping.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public float ObserveTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastObserved;
            var num = CustomGameOptions.ObserveCooldown * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public int NumberOfGuesses = 0;
        public int IncorrectGuesses = 0;
        public bool WonByGuessing = false;

        public List<string> PossibleGuesses => SortedColorMapping.Keys.ToList();

        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
            var doomTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            doomTeam.Add(PlayerControl.LocalPlayer);
            __instance.teamToShow = doomTeam;
        }

        internal override bool GameEnd(LogicGameFlowNormal __instance)
        {
            if (Player.Data.IsDead) return true;
            if (!CustomGameOptions.NeutralEvilWinEndsGame) return true;
            if (!WonByGuessing) return true;
            Utils.EndGame();
            return false;
        }
    }
}
