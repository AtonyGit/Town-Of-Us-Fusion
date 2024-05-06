using System.Collections.Generic;
using System.Linq;
using TMPro;
using TownOfUsFusion.Patches;
using UnityEngine;
using TownOfUsFusion.NeutralRoles.ExecutionerMod;
using TownOfUsFusion.NeutralRoles.GuardianAngelMod;
using TownOfUsFusion.CrewmateRoles.VampireHunterMod;

namespace TownOfUsFusion.Roles
{
    public class Vigilante : Role, IGuesser
{
    public Dictionary<byte, (GameObject, GameObject, GameObject, TMP_Text)> Buttons { get; set; } = new();

    private Dictionary<string, Color> ColorMapping = new();

    public Dictionary<string, Color> SortedColorMapping;

    public Dictionary<byte, string> Guesses = new();

    public Vigilante(PlayerControl player) : base(player)
    {
        Name = "Vigilante";
        ImpostorText = () => "Kill Impostors If You Can Guess Their Roles";
        TaskText = () => "Guess the roles of impostors mid-meeting to kill them!";
        AlignmentText = () => "Crew Killing";
        Color = Patches.Colors.Vigilante;
        RoleType = RoleEnum.Vigilante;
        AddToRoleHistory(RoleType);

        RemainingKills = CustomGameOptions.VigilanteKills;

        if (CustomGameOptions.GameMode == GameMode.Classic || CustomGameOptions.GameMode == GameMode.AllAny)
        {
            var IsAllied = player.Is(AllianceEnum.Crewpostor) || player.Is(AllianceEnum.Crewpocalypse) || player.Is(AllianceEnum.Recruit);
            if (IsAllied)
            {
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
            if (CustomGameOptions.ImitatorOn > 0) ColorMapping.Add("Imitator", Colors.Imitator);
            if (CustomGameOptions.VampireHunterOn > 0 && CustomGameOptions.VampireOn > 0) ColorMapping.Add("Vampire Hunter", Colors.VampireHunter);
            if (CustomGameOptions.ProsecutorOn > 0) ColorMapping.Add("Prosecutor", Colors.Prosecutor);
            if (CustomGameOptions.OracleOn > 0) ColorMapping.Add("Oracle", Colors.Oracle);
            if (CustomGameOptions.AurialOn > 0) ColorMapping.Add("Aurial", Colors.Aurial);
            }
            if (!player.Is(AllianceEnum.Crewpostor))
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
            if (player.Is(AllianceEnum.Crewpostor) || player.Is(AllianceEnum.Crewpocalypse))
            {
        if (CustomGameOptions.AssassinGuessCrewInvestigative && IsAllied)
        {
        if (CustomGameOptions.TrackerOn > 0) ColorMapping.Add("Tracker", Colors.Tracker);
        if (CustomGameOptions.TrapperOn > 0) ColorMapping.Add("Trapper", Colors.Trapper);
        if (CustomGameOptions.DetectiveOn > 0) ColorMapping.Add("Detective", Colors.Detective);
        if (CustomGameOptions.InvestigatorOn > 0) ColorMapping.Add("Investigator", Colors.Investigator);
        if (CustomGameOptions.SeerOn > 0) ColorMapping.Add("Seer", Colors.Seer);
        if (CustomGameOptions.SnitchOn > 0) ColorMapping.Add("Snitch", Colors.Snitch);
        if (CustomGameOptions.SpyOn > 0) ColorMapping.Add("Spy", Colors.Spy);
        }

            }

            if (CustomGameOptions.VigilanteGuessNeutralBenign || (IsAllied && CustomGameOptions.AssassinGuessNeutralBenign))
            {
                if (CustomGameOptions.AmnesiacOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Amnesiac) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Amnesiac)) ColorMapping.Add("Amnesiac", Colors.Amnesiac);
                if (CustomGameOptions.GuardianAngelOn > 0) ColorMapping.Add("Guardian Angel", Colors.GuardianAngel);
                if (CustomGameOptions.SurvivorOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Survivor) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Survivor)) ColorMapping.Add("Survivor", Colors.Survivor);
            }
            if (CustomGameOptions.VigilanteGuessNeutralEvil || (IsAllied && CustomGameOptions.AssassinGuessNeutralEvil))
            {
                if (CustomGameOptions.DoomsayerOn > 0) ColorMapping.Add("Doomsayer", Colors.Doomsayer);
                if (CustomGameOptions.ExecutionerOn > 0) ColorMapping.Add("Executioner", Colors.Executioner);
                if (CustomGameOptions.JesterOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Jester) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Jester)) ColorMapping.Add("Jester", Colors.Jester);
            }
            if (CustomGameOptions.VigilanteGuessNeutralChaos || (IsAllied && CustomGameOptions.AssassinGuessNeutralChaos))
            {
                if (CustomGameOptions.TyrantOn > 0) ColorMapping.Add("Tyrant", Colors.Tyrant);
                if (CustomGameOptions.JokerOn > 0) ColorMapping.Add("Joker", Colors.Joker);
                if (CustomGameOptions.CannibalOn > 0) ColorMapping.Add("Cannibal", Colors.Cannibal);
            //    if (CustomGameOptions.JesterOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Jester) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Jester)) ColorMapping.Add("Jester", Colors.Jester);
            }
            if (CustomGameOptions.VigilanteGuessNeutralKilling || (IsAllied && CustomGameOptions.AssassinGuessNeutralKilling))
            {
                if (CustomGameOptions.ArsonistOn > 0) ColorMapping.Add("Arsonist", Colors.Arsonist);
                if (CustomGameOptions.GlitchOn > 0) ColorMapping.Add("The Glitch", Colors.Glitch);
                if (CustomGameOptions.WerewolfOn > 0) ColorMapping.Add("Werewolf", Colors.Werewolf);
            }
            if (CustomGameOptions.VigilanteGuessNeutralNeophyte || (IsAllied && CustomGameOptions.AssassinGuessNeutralNeophyte))
            {
                if (CustomGameOptions.NeoNecromancerOn > 0) ColorMapping.Add("Necromancer", Colors.NeoNecromancer);
                if (CustomGameOptions.NeoNecromancerOn > 0) ColorMapping.Add("Husk", Colors.NeoNecromancer);
                if (CustomGameOptions.NeoNecromancerOn > 0) ColorMapping.Add("Scourge", Colors.NeoNecromancer);
                if (CustomGameOptions.NeoNecromancerOn > 0) ColorMapping.Add("Enchanter", Colors.NeoNecromancer);
                if (CustomGameOptions.NeoNecromancerOn > 0) ColorMapping.Add("Apparitionist", Colors.NeoNecromancer);
                if (!player.Is(AllianceEnum.Recruit) && CustomGameOptions.JackalOn > 0) ColorMapping.Add(Utils.GradientColorText("B7B9BA", "5E576B", "Jackal"), Colors.Recruit);
                if (CustomGameOptions.VampireOn > 0) ColorMapping.Add("Vampire", Colors.Vampire);
            }
            if (CustomGameOptions.VigilanteGuessNeutralApocalypse && !player.Is(AllianceEnum.Crewpocalypse))
            {
                if (CustomGameOptions.PlaguebearerOn > 0) ColorMapping.Add("Plaguebearer", Colors.RegularApoc);
                if (CustomGameOptions.BakerOn > 0) ColorMapping.Add("Baker", Colors.RegularApoc);
                if (CustomGameOptions.SoulCollectorOn > 0) ColorMapping.Add("Soul Collector", Colors.RegularApoc);
                if (CustomGameOptions.BerserkerOn > 0) ColorMapping.Add("Berserker", Colors.RegularApoc);
            }
                if (CustomGameOptions.VigilanteGuessRecruits && !player.Is(AllianceEnum.Recruit) && CustomGameOptions.JackalOn > 0) ColorMapping.Add(Utils.GradientColorText("B7B9BA", "5E576B", "Recruit"), Colors.Recruit);
                if (CustomGameOptions.VigilanteGuessEvilCrew && !player.Is(AllianceEnum.Crewpostor) && CustomGameOptions.CrewpostorOn > 0) ColorMapping.Add("Crewpostor", Colors.Impostor);
                if (CustomGameOptions.VigilanteGuessEvilCrew && !player.Is(AllianceEnum.Crewpocalypse) && CustomGameOptions.CrewpocalypseOn > 0) ColorMapping.Add("Crewpocalypse", Colors.RegularApoc);
            if (CustomGameOptions.VigilanteGuessLovers && CustomGameOptions.LoversOn > 0) ColorMapping.Add("Lover", Colors.Lovers);
        }
        else if (CustomGameOptions.GameMode == GameMode.KillingOnly)
        {
            ColorMapping.Add("Morphling", Colors.Impostor);
            ColorMapping.Add("Miner", Colors.Impostor);
            ColorMapping.Add("Swooper", Colors.Impostor);
            ColorMapping.Add("Undertaker", Colors.Impostor);
            ColorMapping.Add("Grenadier", Colors.Impostor);
            ColorMapping.Add("Traitor", Colors.Impostor);
            ColorMapping.Add("Escapist", Colors.Impostor);

            if (CustomGameOptions.VigilanteGuessNeutralKilling)
            {
                if (CustomGameOptions.AddArsonist) ColorMapping.Add("Arsonist", Colors.Arsonist);
                if (CustomGameOptions.AddPlaguebearer) ColorMapping.Add("Plaguebearer", Colors.RegularApoc);
                ColorMapping.Add("The Glitch", Colors.Glitch);
                ColorMapping.Add("Werewolf", Colors.Werewolf);
            }
        }
        else
        {
            ColorMapping.Add("Necromancer", Colors.Impostor);
            ColorMapping.Add("Whisperer", Colors.Impostor);
            if (CustomGameOptions.MaxChameleons > 0) ColorMapping.Add("Swooper", Colors.Impostor);
            if (CustomGameOptions.MaxEngineers > 0) ColorMapping.Add("Demolitionist", Colors.Impostor);
            if (CustomGameOptions.MaxInvestigators > 0) ColorMapping.Add("Consigliere", Colors.Impostor);
            if (CustomGameOptions.MaxMystics > 0) ColorMapping.Add("Clairvoyant", Colors.Impostor);
            if (CustomGameOptions.MaxSnitches > 0) ColorMapping.Add("Informant", Colors.Impostor);
            if (CustomGameOptions.MaxSpies > 0) ColorMapping.Add("Rogue Agent", Colors.Impostor);
            if (CustomGameOptions.MaxTransporters > 0) ColorMapping.Add("Escapist", Colors.Impostor);
            if (CustomGameOptions.MaxVigilantes > 1) ColorMapping.Add("Assassin", Colors.Impostor);
            ColorMapping.Add("Impostor", Colors.Impostor);
        }

        SortedColorMapping = ColorMapping.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
    }

    public bool GuessedThisMeeting { get; set; } = false;

    public int RemainingKills { get; set; }

    public List<string> PossibleGuesses => SortedColorMapping.Keys.ToList();
}
}
