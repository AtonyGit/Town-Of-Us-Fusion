﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.NeutralRoles.ExecutionerMod;
using TownOfUsFusion.NeutralRoles.GuardianAngelMod;
using TownOfUsFusion.CrewmateRoles.VampireHunterMod;

namespace TownOfUsFusion.Roles.Modifiers
{
    public class Assassin : Ability, IGuesser
{
    public Dictionary<byte, (GameObject, GameObject, GameObject, TMP_Text)> Buttons { get; set; } = new();


    private Dictionary<string, Color> ColorMapping = new();

    public Dictionary<string, Color> SortedColorMapping;

    public Dictionary<byte, string> Guesses = new();


    public Assassin(PlayerControl player) : base(player)
    {
        Name = "Assassin";
        TaskText = () => "Guess the roles of the people and kill them mid-meeting";
        Color = Patches.Colors.Impostor;
        AbilityType = AbilityEnum.Assassin;

        RemainingKills = CustomGameOptions.AssassinKills;

        // Adds all the roles that have a non-zero chance of being in the game.
        if (CustomGameOptions.MayorOn > 0) ColorMapping.Add("Mayor", Colors.Mayor);
        if (CustomGameOptions.SheriffOn > 0 || (CustomGameOptions.VampireHunterOn > 0 && CustomGameOptions.VampireOn > 0 && CustomGameOptions.BecomeOnVampDeaths == BecomeEnum.Sheriff)) ColorMapping.Add("Sheriff", Colors.Sheriff);
        if (CustomGameOptions.EngineerOn > 0) ColorMapping.Add("Engineer", Colors.Engineer);
        if (CustomGameOptions.SwapperOn > 0) ColorMapping.Add("Swapper", Colors.Swapper);
        if (CustomGameOptions.MedicOn > 0) ColorMapping.Add("Medic", Colors.Medic);
        if (CustomGameOptions.AltruistOn > 0) ColorMapping.Add("Altruist", Colors.Altruist);
        if (CustomGameOptions.VigilanteOn > 0 || (CustomGameOptions.VampireHunterOn > 0 && CustomGameOptions.VampireOn > 0 && CustomGameOptions.BecomeOnVampDeaths == BecomeEnum.Vigilante)) ColorMapping.Add("Vigilante", Colors.Vigilante);
        if (CustomGameOptions.VeteranOn > 0 || (CustomGameOptions.VampireHunterOn > 0 && CustomGameOptions.VampireOn > 0 && CustomGameOptions.BecomeOnVampDeaths == BecomeEnum.Veteran)) ColorMapping.Add("Veteran", Colors.Veteran);
        if (CustomGameOptions.TransporterOn > 0) ColorMapping.Add("Transporter", Colors.Transporter);
        if (CustomGameOptions.MediumOn > 0) ColorMapping.Add("Medium", Colors.Medium);
        if (CustomGameOptions.MysticOn > 0) ColorMapping.Add("Mystic", Colors.Mystic);
        if (CustomGameOptions.TricksterOn > 0) ColorMapping.Add("Trickster", Colors.Trickster);
        if (CustomGameOptions.ImitatorOn > 0) ColorMapping.Add("Imitator", Colors.Imitator);
        if (CustomGameOptions.VampireHunterOn > 0 && CustomGameOptions.VampireOn > 0) ColorMapping.Add("Vampire Hunter", Colors.VampireHunter);
        if (CustomGameOptions.ProsecutorOn > 0) ColorMapping.Add("Prosecutor", Colors.Prosecutor);
        if (CustomGameOptions.OracleOn > 0) ColorMapping.Add("Oracle", Colors.Oracle);
        if (CustomGameOptions.AurialOn > 0) ColorMapping.Add("Aurial", Colors.Aurial);
        if (CustomGameOptions.TaskmasterOn > 0) ColorMapping.Add("Taskmaster", Colors.Taskmaster);
        if (CustomGameOptions.TricksterOn > 0) ColorMapping.Add("Trickster", Colors.Trickster);
        if (CustomGameOptions.BodyguardOn > 0) ColorMapping.Add("Bodyguard", Colors.Bodyguard);

        if (CustomGameOptions.AssassinGuessCrewInvestigative)
        {
        if (CustomGameOptions.TrackerOn > 0) ColorMapping.Add("Tracker", Colors.Tracker);
        if (CustomGameOptions.TrapperOn > 0) ColorMapping.Add("Trapper", Colors.Trapper);
        if (CustomGameOptions.DetectiveOn > 0) ColorMapping.Add("Detective", Colors.Detective);
        if (CustomGameOptions.InvestigatorOn > 0) ColorMapping.Add("Investigator", Colors.Investigator);
        if (CustomGameOptions.SeerOn > 0) ColorMapping.Add("Seer", Colors.Seer);
        if (CustomGameOptions.SnitchOn > 0) ColorMapping.Add("Snitch", Colors.Snitch);
        if (CustomGameOptions.SpyOn > 0) ColorMapping.Add("Spy", Colors.Spy);
        }
        // Add Neutral roles if enabled
        if (CustomGameOptions.AssassinGuessNeutralBenign)
        {
            if (CustomGameOptions.AmnesiacOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Amnesiac) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Amnesiac)) ColorMapping.Add("Amnesiac", Colors.Amnesiac);
            if (CustomGameOptions.GuardianAngelOn > 0) ColorMapping.Add("Guardian Angel", Colors.GuardianAngel);
            if (CustomGameOptions.SurvivorOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Survivor) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Survivor)) ColorMapping.Add("Survivor", Colors.Survivor);
        }
        if (CustomGameOptions.AssassinGuessNeutralEvil)
        {
            if (CustomGameOptions.DoomsayerOn > 0) ColorMapping.Add("Doomsayer", Colors.Doomsayer);
            if (CustomGameOptions.ExecutionerOn > 0) ColorMapping.Add("Executioner", Colors.Executioner);
            if (CustomGameOptions.JesterOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Jester) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Jester)) ColorMapping.Add("Jester", Colors.Jester);
        }
        if (CustomGameOptions.AssassinGuessNeutralChaos)
        {
            if (CustomGameOptions.TyrantOn > 0) ColorMapping.Add("Tyrant", Colors.Tyrant);
            if (CustomGameOptions.JokerOn > 0) ColorMapping.Add("Joker", Colors.Joker);
            if (CustomGameOptions.InquisitorOn > 0) ColorMapping.Add("Inquisitor", Colors.Inquisitor);
            if (CustomGameOptions.CursedSoulOn > 0) ColorMapping.Add(Utils.GradientColorText("79FFB3", "B579FF", "Cursed Soul"), Colors.CursedSoul);
            if (CustomGameOptions.CannibalOn > 0) ColorMapping.Add("Cannibal", Colors.Cannibal);
        }
        if (CustomGameOptions.AssassinGuessNeutralKilling)
        {
            if (CustomGameOptions.ArsonistOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.Arsonist)) ColorMapping.Add("Arsonist", Colors.Arsonist);
            if (CustomGameOptions.GlitchOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.Glitch)) ColorMapping.Add("The Glitch", Colors.Glitch);
            if (CustomGameOptions.WerewolfOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.Werewolf)) ColorMapping.Add("Werewolf", Colors.Werewolf);
            if (CustomGameOptions.SentinelOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.Sentinel)) ColorMapping.Add("The Sentinel", Colors.Sentinel);
        }
            if (CustomGameOptions.AssassinGuessNeutralNeophyte)
            {
                if (!player.Is(AllianceEnum.Recruit) && CustomGameOptions.JackalOn > 0) ColorMapping.Add(Utils.GradientColorText("B7B9BA", "5E576B", "Jackal"), Colors.Recruit);
                if (CustomGameOptions.NeoNecromancerOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.NeoNecromancer)) ColorMapping.Add("Necromancer", Colors.NeoNecromancer);
                if (CustomGameOptions.NeoNecromancerOn > 0) ColorMapping.Add("Husk", Colors.NeoNecromancer);
                if (CustomGameOptions.NeoNecromancerOn > 0) ColorMapping.Add("Scourge", Colors.NeoNecromancer);
                if (CustomGameOptions.NeoNecromancerOn > 0) ColorMapping.Add("Apparitionist", Colors.NeoNecromancer);
                if (CustomGameOptions.VampireOn > 0) ColorMapping.Add("Vampire", Colors.Vampire);
            }
            if (CustomGameOptions.AssassinGuessNeutralApocalypse && !PlayerControl.LocalPlayer.Is(Faction.NeutralApocalypse) && !PlayerControl.LocalPlayer.Is(AllianceEnum.Crewpocalypse))
            {
                if (CustomGameOptions.PlaguebearerOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.Plaguebearer)) ColorMapping.Add("Plaguebearer", Colors.RegularApoc);
                if (CustomGameOptions.BakerOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.Baker)) ColorMapping.Add("Baker", Colors.RegularApoc);
                if (CustomGameOptions.SoulCollectorOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.SoulCollector)) ColorMapping.Add("Soul Collector", Colors.RegularApoc);
                if (CustomGameOptions.BerserkerOn > 0 && !PlayerControl.LocalPlayer.Is(RoleEnum.Berserker)) ColorMapping.Add("Berserker", Colors.RegularApoc);
            }

        if (CustomGameOptions.AssassinGuessImpostors && !PlayerControl.LocalPlayer.Is(Faction.Impostors) && !PlayerControl.LocalPlayer.Is(AllianceEnum.Crewpostor))
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
        }

        // Add vanilla crewmate if enabled
        if (CustomGameOptions.AssassinCrewmateGuess) ColorMapping.Add("Crewmate", Colors.Crewmate);
        //Add modifiers if enabled
        if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.BaitOn > 0) ColorMapping.Add("Bait", Colors.Bait);
        if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.AftermathOn > 0) ColorMapping.Add("Aftermath", Colors.Aftermath);
        if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.DiseasedOn > 0) ColorMapping.Add("Diseased", Colors.Diseased);
        if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.FrostyOn > 0) ColorMapping.Add("Frosty", Colors.Frosty);
        if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.MultitaskerOn > 0) ColorMapping.Add("Multitasker", Colors.Multitasker);
        if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.TorchOn > 0) ColorMapping.Add("Torch", Colors.Torch);
        if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.EclipsedOn > 0) ColorMapping.Add("Eclipsed", Colors.Eclipsed);
        if (CustomGameOptions.AssassinGuessLovers && CustomGameOptions.LoversOn > 0) ColorMapping.Add("Lover", Colors.Lovers);

                if (CustomGameOptions.AssassinGuessRecruits && !PlayerControl.LocalPlayer.Is(RoleEnum.Jackal) && !player.Is(AllianceEnum.Recruit) && CustomGameOptions.JackalOn > 0) ColorMapping.Add(Utils.GradientColorText("B7B9BA", "5E576B", "Recruit"), Colors.Recruit);
                if (CustomGameOptions.AssassinGuessEvilCrew && !PlayerControl.LocalPlayer.Is(Faction.Impostors) && !player.Is(AllianceEnum.Crewpostor) && CustomGameOptions.CrewpostorOn > 0) ColorMapping.Add("Crewpostor", Colors.Impostor);
                if (CustomGameOptions.AssassinGuessEvilCrew && !PlayerControl.LocalPlayer.Is(Faction.NeutralApocalypse) && !player.Is(AllianceEnum.Crewpocalypse) && CustomGameOptions.CrewpocalypseOn > 0) ColorMapping.Add("Crewpocalypse", Colors.RegularApoc);
        // Sorts the list alphabetically. 
        SortedColorMapping = ColorMapping.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
    }

    public bool GuessedThisMeeting { get; set; } = false;

    public int RemainingKills { get; set; }

    public List<string> PossibleGuesses => SortedColorMapping.Keys.ToList();
}
}
