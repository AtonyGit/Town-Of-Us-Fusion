using System;
using System.Collections.Generic;

namespace TownOfUsFusion.CustomOption
{
    public class Generate
    {

        private static readonly List<string> roleBuckets = new List<string>
            {
            "<color=#66FFFFFF>Crew</color> Investigative",
            "<color=#66FFFFFF>Crew</color> Killing",
            "<color=#66FFFFFF>Crew</color> Protective",
            "<color=#66FFFFFF>Crew</color> Sovereign",
            "<color=#66FFFFFF>Crew</color> Utility",
            "Common <color=#66FFFFFF>Crew</color>",
            "Random <color=#66FFFFFF>Crew</color>",
            "<color=#999999FF>Neutral</color> Benign",
            "<color=#999999FF>Neutral</color> Evil",
            "<color=#999999FF>Neutral</color> Chaos",
            "<color=#999999FF>Neutral</color> Killing",
            "<color=#999999FF>Neutral</color> Neophyte",
            "<color=#999999FF>Neutral</color> Apocalypse",
            "Common <color=#999999FF>Neutral</color>",
            "Random <color=#999999FF>Neutral</color>",
            "<color=#FF0000FF>Imp</color> Concealing",
            "<color=#FF0000FF>Imp</color> Killing",
            "<color=#FF0000FF>Imp</color> Support",
            "Common <color=#FF0000FF>Imp</color>",
            "Random <color=#FF0000FF>Imp</color>",
            "Non-<color=#FF0000FF>Imp</color>",
            "Any"
            };
        public static CustomHeaderOption CrewInvestigativeRoles;
        public static CustomNumberOption AurialOn;
        public static CustomNumberOption HaunterOn;
        public static CustomNumberOption InvestigatorOn;
        public static CustomNumberOption LookoutOn;
        public static CustomNumberOption MediumOn;
        public static CustomNumberOption PsychicOn;
        public static CustomNumberOption SpyOn;
        public static CustomNumberOption TrackerOn;
        public static CustomNumberOption TrapperOn;

        public static CustomHeaderOption CrewKillingRoles;
        public static CustomNumberOption DeputyOn;
        public static CustomNumberOption HunterOn;
        public static CustomNumberOption SheriffOn;
        public static CustomNumberOption VeteranOn;
        public static CustomNumberOption VigilanteOn;

        public static CustomHeaderOption CrewProtectiveRoles;
        public static CustomNumberOption AltruistOn;
        public static CustomNumberOption BodyguardOn;
        public static CustomNumberOption MedicOn;
        public static CustomNumberOption MirrorMasterOn;
        public static CustomNumberOption OracleOn;

        public static CustomHeaderOption CrewSovereignRoles;
        public static CustomNumberOption CaptainOn;
        public static CustomNumberOption JailorOn;
        public static CustomNumberOption PoliticianOn;
        public static CustomNumberOption ProsecutorOn;
        public static CustomNumberOption SwapperOn;

        public static CustomHeaderOption CrewUtilityRoles;
        public static CustomNumberOption EngineerOn;
        public static CustomNumberOption ImitatorOn;
        public static CustomNumberOption TimeLordOn;
        public static CustomNumberOption TransporterOn;

        public static CustomHeaderOption NeutralBenignRoles;
        public static CustomNumberOption AmnesiacOn;
        public static CustomNumberOption GuardianAngelOn;
        public static CustomNumberOption LawyerOn;
        public static CustomNumberOption SurvivorOn;

        public static CustomHeaderOption NeutralEvilRoles;
        public static CustomNumberOption DoomsayerOn;
        public static CustomNumberOption ExecutionerOn;
        public static CustomNumberOption JesterOn;
        public static CustomNumberOption PhantomOn;

        public static CustomHeaderOption NeutralChaosRoles;
        public static CustomNumberOption InquisitorOn;
        public static CustomNumberOption TyrantOn;
        public static CustomNumberOption CannibalOn;

        public static CustomHeaderOption NeutralKillingRoles;
        public static CustomNumberOption ArsonistOn;
        public static CustomNumberOption SerialKillerOn;
        public static CustomNumberOption GlitchOn;
        public static CustomNumberOption WerewolfOn;
        
        public static CustomHeaderOption NeutralNeophyteRoles;
        public static CustomNumberOption VampireOn;

        public static CustomHeaderOption NeutralApocalypseRoles;
        public static CustomNumberOption JuggernautOn;
        public static CustomNumberOption PlaguebearerOn;
        public static CustomNumberOption SoulCollectorOn;

        public static CustomHeaderOption ImpostorConcealingRoles;
        public static CustomNumberOption EscapistOn;
        public static CustomNumberOption GrenadierOn;
        public static CustomNumberOption MorphlingOn;
        public static CustomNumberOption SwooperOn;
        public static CustomNumberOption VenererOn;

        public static CustomHeaderOption ImpostorKillingRoles;
        public static CustomNumberOption BomberOn;
        public static CustomNumberOption PoisonerOn;
        public static CustomNumberOption TraitorOn;
        public static CustomNumberOption WarlockOn;

        public static CustomHeaderOption ImpostorSupportRoles;
        public static CustomNumberOption BlackmailerOn;
        public static CustomNumberOption HypnotistOn;
        public static CustomNumberOption JanitorOn;
        public static CustomNumberOption MinerOn;
        public static CustomNumberOption UndertakerOn;

        public static CustomHeaderOption CrewmateModifiers;
        public static CustomNumberOption AftermathOn;
        public static CustomNumberOption BaitOn;
        public static CustomNumberOption DiseasedOn;
        public static CustomNumberOption FrostyOn;
        public static CustomNumberOption MultitaskerOn;
        public static CustomNumberOption TorchOn;

        public static CustomHeaderOption GlobalModifiers;
        public static CustomNumberOption ButtonBarryOn;
        public static CustomNumberOption DrunkOn;
        public static CustomNumberOption FlashOn;
        public static CustomNumberOption GiantOn;
        public static CustomNumberOption ObliviousOn;
        public static CustomNumberOption MiniOn;
        public static CustomNumberOption RadarOn;
        public static CustomNumberOption ShyOn;
        public static CustomNumberOption SixthSenseOn;
        public static CustomNumberOption SleuthOn;
        public static CustomNumberOption TiebreakerOn;

        public static CustomHeaderOption CrewmateAlliances;
        public static CustomNumberOption EgotistOn;
        public static CustomNumberOption CrewpocalypseOn;
        public static CustomNumberOption CrewpostorOn;
        public static CustomHeaderOption GlobalAlliances;
        public static CustomNumberOption LoversOn;

        public static CustomHeaderOption ImpostorModifiers;
        public static CustomNumberOption DisperserOn;
        public static CustomNumberOption DoubleShotOn;
        public static CustomNumberOption SaboteurOn;
        public static CustomNumberOption UnderdogOn;

        public static CustomHeaderOption MapSettings;
        public static CustomToggleOption RandomMapEnabled;
        public static CustomNumberOption RandomMapSkeld;
        public static CustomNumberOption RandomMapMira;
        public static CustomNumberOption RandomMapPolus;
        public static CustomNumberOption RandomMapAirship;
        public static CustomNumberOption RandomMapFungle;
        public static CustomNumberOption RandomMapSubmerged;
        public static CustomNumberOption RandomMapLevelImpostor;
        public static CustomToggleOption SmallMapHalfVision;
        public static CustomNumberOption SmallMapDecreasedCooldown;
        public static CustomNumberOption LargeMapIncreasedCooldown;
        public static CustomNumberOption SmallMapIncreasedShortTasks;
        public static CustomNumberOption SmallMapIncreasedLongTasks;
        public static CustomNumberOption LargeMapDecreasedShortTasks;
        public static CustomNumberOption LargeMapDecreasedLongTasks;

        public static CustomHeaderOption CustomGameSettings;
        public static CustomToggleOption ColourblindComms;
        public static CustomToggleOption CamoCommsKillAnyone;
        public static CustomToggleOption ImpostorSeeRoles;
        public static CustomToggleOption DeadSeeRoles;
        public static CustomNumberOption InitialCooldowns;
        public static CustomToggleOption ParallelMedScans;
        public static CustomStringOption SkipButtonDisable;
        public static CustomToggleOption LocationReports;
        public static CustomToggleOption FirstDeathShield;
        public static CustomToggleOption NeutralEvilWinEndsGame;
        public static CustomToggleOption CrewKillersContinue;
        public static CustomToggleOption ShowRecentBodies;

        public static CustomHeaderOption BetterPolusSettings;
        public static CustomToggleOption VentImprovements;
        public static CustomToggleOption VitalsLab;
        public static CustomToggleOption ColdTempDeathValley;
        public static CustomToggleOption WifiChartCourseSwap;

        public static CustomHeaderOption RoleListSettings;
        public static CustomToggleOption UniqueCrewInvestRoles;
        public static CustomToggleOption UniqueCrewKillingRoles;
        public static CustomToggleOption UniqueCrewProtectRoles;
        public static CustomToggleOption UniqueCrewUtilRoles;
        public static CustomToggleOption UniqueNeutBenignRoles;
        public static CustomToggleOption UniqueNeutEvilRoles;
        public static CustomToggleOption UniqueNeutKillingRoles;
        public static CustomToggleOption UniqueImpRoles;
        public static CustomStringOption Slot1;
        public static CustomStringOption Slot2;
        public static CustomStringOption Slot3;
        public static CustomStringOption Slot4;
        public static CustomStringOption Slot5;
        public static CustomStringOption Slot6;
        public static CustomStringOption Slot7;
        public static CustomStringOption Slot8;
        public static CustomStringOption Slot9;
        public static CustomStringOption Slot10;
        public static CustomStringOption Slot11;
        public static CustomStringOption Slot12;
        public static CustomStringOption Slot13;
        public static CustomStringOption Slot14;
        public static CustomStringOption Slot15;

        public static CustomHeaderOption TaskTrackingSettings;
        public static CustomToggleOption SeeTasksDuringRound;
        public static CustomToggleOption SeeTasksDuringMeeting;
        public static CustomToggleOption SeeTasksWhenDead;

        public static CustomHeaderOption Sheriff;
        public static CustomToggleOption SheriffKillOther;
        public static CustomToggleOption SheriffKillsNE;
        public static CustomToggleOption SheriffKillsNK;
        public static CustomNumberOption SheriffKillCd;
        public static CustomToggleOption SheriffBodyReport;

        public static CustomHeaderOption Hunter;
        public static CustomNumberOption HunterKillCd;
        public static CustomNumberOption HunterStalkCd;
        public static CustomNumberOption HunterStalkDuration;
        public static CustomNumberOption HunterStalkUses;
        public static CustomToggleOption RetributionOnVote;
        public static CustomToggleOption HunterBodyReport;

        public static CustomHeaderOption Engineer;
        public static CustomNumberOption MaxFixes;


        public static CustomHeaderOption Bodyguard;
        public static CustomNumberOption GuardCd;
        public static CustomNumberOption GuardDuration;
        public static CustomStringOption ShowGuarding;
        public static CustomNumberOption MaxGuards;

        public static CustomHeaderOption Medic;
        public static CustomStringOption ShowShielded;
        public static CustomStringOption WhoGetsNotification;
        public static CustomToggleOption ShieldBreaks;
        public static CustomToggleOption MedicReportSwitch;
        public static CustomNumberOption MedicReportNameDuration;
        public static CustomNumberOption MedicReportColorDuration;

        public static CustomHeaderOption MirrorMaster;
        public static CustomNumberOption MaxMirrors;
        public static CustomStringOption ShowMirrorShielded;
        public static CustomStringOption WhoGetsMirrorNotification;
        public static CustomNumberOption MirrorAbsorbCd;
        public static CustomNumberOption MirrorUnleashCd;
        public static CustomToggleOption MirrorBreaks;

        public static CustomHeaderOption Psychic;
        public static CustomNumberOption PsychicCooldown;
        public static CustomToggleOption CrewKillingRed;
        public static CustomToggleOption NeutBenignRed;
        public static CustomToggleOption NeutEvilRed;
        public static CustomToggleOption NeutKillingRed;
        public static CustomToggleOption TraitorColourSwap;

        public static CustomHeaderOption Swapper;
        public static CustomToggleOption SwapperButton;

        public static CustomHeaderOption TimeLord;
        public static CustomToggleOption RewindRevive;
        public static CustomNumberOption RewindDuration;
        public static CustomNumberOption RewindCooldown;
        public static CustomToggleOption TimeLordVitals;

        public static CustomHeaderOption Transporter;
        public static CustomNumberOption TransportCooldown;
        public static CustomNumberOption TransportMaxUses;
        public static CustomToggleOption TransporterVitals;

        public static CustomHeaderOption Jester;
        public static CustomToggleOption JesterButton;
        public static CustomToggleOption JesterVent;
        public static CustomToggleOption JesterImpVision;
        public static CustomToggleOption JesterHaunt;

        public static CustomHeaderOption TheGlitch;
        public static CustomNumberOption MimicCooldownOption;
        public static CustomNumberOption MimicDurationOption;
        public static CustomNumberOption HackCooldownOption;
        public static CustomNumberOption HackDurationOption;
        public static CustomNumberOption GlitchKillCooldownOption;
        public static CustomStringOption GlitchHackDistanceOption;
        public static CustomToggleOption GlitchVent;

        public static CustomHeaderOption Juggernaut;
        public static CustomNumberOption JuggKillCooldown;
        public static CustomNumberOption ReducedKCdPerKill;
        public static CustomNumberOption KillsNeeded;
        public static CustomToggleOption JuggVent;
        public static CustomNumberOption ArmKillCooldown;
        public static CustomToggleOption ArmVent;
        public static CustomNumberOption DeathRevealDelay;

        public static CustomHeaderOption Morphling;
        public static CustomNumberOption MorphlingCooldown;
        public static CustomNumberOption MorphlingDuration;
        public static CustomToggleOption MorphlingVent;

        public static CustomHeaderOption Executioner;
        public static CustomStringOption OnTargetDead;
        public static CustomToggleOption ExecutionerButton;
        public static CustomToggleOption ExecutionerTorment;

        public static CustomHeaderOption Phantom;
        public static CustomNumberOption PhantomTasksRemaining;
        public static CustomToggleOption PhantomSpook;

        public static CustomHeaderOption Spy;
        public static CustomNumberOption AdminCooldown;
        public static CustomStringOption WhoSeesDead;
        public static CustomToggleOption SpySeesNeutrals;
        public static CustomNumberOption SpyTasksRemaining;
        public static CustomToggleOption SpySeesImpInMeeting;
        public static CustomToggleOption SpySeesTraitor;

        public static CustomHeaderOption Altruist;
        public static CustomNumberOption AltruistMaxRevives;
        public static CustomNumberOption ReviveCooldown;
        public static CustomNumberOption ReviveDuration;
        public static CustomToggleOption AltruistTargetBody;

        public static CustomHeaderOption Miner;
        public static CustomNumberOption MineCooldown;

        public static CustomHeaderOption Swooper;
        public static CustomNumberOption SwoopCooldown;
        public static CustomNumberOption SwoopDuration;
        public static CustomToggleOption SwooperVent;

        public static CustomHeaderOption Arsonist;
        public static CustomNumberOption DouseCooldown;
        public static CustomNumberOption MaxDoused;
        public static CustomToggleOption ArsoImpVision;
        public static CustomToggleOption IgniteCdRemoved;

        public static CustomHeaderOption Undertaker;
        public static CustomNumberOption DragCooldown;
        public static CustomNumberOption UndertakerDragSpeed;
        public static CustomToggleOption UndertakerVent;
        public static CustomToggleOption UndertakerVentWithBody;

        public static CustomHeaderOption Assassin;
        public static CustomNumberOption NumberOfImpostorAssassins;
        public static CustomNumberOption NumberOfNeutralAssassins;
        public static CustomToggleOption AmneTurnImpAssassin;
        public static CustomToggleOption AmneTurnNeutAssassin;
        public static CustomToggleOption TraitorCanAssassin;
        public static CustomNumberOption AssassinKills;
        public static CustomToggleOption AssassinMultiKill;
        public static CustomToggleOption AssassinCrewmateGuess;
        public static CustomToggleOption AssassinGuessCrewInvestigative;
        public static CustomToggleOption AssassinGuessNeutralBenign;
        public static CustomToggleOption AssassinGuessNeutralEvil;
        public static CustomToggleOption AssassinGuessNeutralChaos;
        public static CustomToggleOption AssassinGuessNeutralKilling;
        public static CustomToggleOption AssassinGuessNeutralNeophyte;
        public static CustomToggleOption AssassinGuessNeutralApocalypse;
        public static CustomToggleOption AssassinGuessImpostors;
        public static CustomToggleOption AssassinGuessModifiers;
        public static CustomToggleOption AssassinGuessLovers;

        public static CustomHeaderOption Underdog;
        public static CustomNumberOption UnderdogKillBonus;
        public static CustomToggleOption UnderdogIncreasedKC;

        public static CustomHeaderOption Vigilante;
        public static CustomNumberOption VigilanteKills;
        public static CustomToggleOption VigilanteMultiKill;
        public static CustomToggleOption VigilanteGuessNeutralBenign;
        public static CustomToggleOption VigilanteGuessNeutralEvil;
        public static CustomToggleOption VigilanteGuessNeutralChaos;
        public static CustomToggleOption VigilanteGuessNeutralKilling;
        public static CustomToggleOption VigilanteGuessNeutralNeophyte;
        public static CustomToggleOption VigilanteGuessNeutralApocalypse;
        public static CustomToggleOption VigilanteGuessModifiers;
        public static CustomToggleOption VigilanteGuessLovers;

        public static CustomHeaderOption Haunter;
        public static CustomNumberOption HaunterTasksRemainingClicked;
        public static CustomNumberOption HaunterTasksRemainingAlert;
        public static CustomToggleOption HaunterRevealsNeutrals;
        public static CustomStringOption HaunterCanBeClickedBy;

        public static CustomHeaderOption Grenadier;
        public static CustomNumberOption GrenadeCooldown;
        public static CustomNumberOption GrenadeDuration;
        public static CustomToggleOption GrenadierIndicators;
        public static CustomToggleOption GrenadierVent;
        public static CustomNumberOption FlashRadius;

        public static CustomHeaderOption Veteran;
        public static CustomToggleOption KilledOnAlert;
        public static CustomNumberOption AlertCooldown;
        public static CustomNumberOption AlertDuration;
        public static CustomNumberOption MaxAlerts;

        public static CustomHeaderOption Tracker;
        public static CustomNumberOption UpdateInterval;
        public static CustomNumberOption TrackCooldown;
        public static CustomToggleOption ResetOnNewRound;
        public static CustomNumberOption MaxTracks;
        public static CustomNumberOption FootprintSize;
        public static CustomNumberOption FootprintInterval;
        public static CustomNumberOption FootprintDuration;
        public static CustomToggleOption AnonymousFootPrint;
        public static CustomToggleOption VentFootprintVisible;
        public static CustomToggleOption SeeOnlyTrackedPrints;

        public static CustomHeaderOption Trapper;
        public static CustomNumberOption TrapCooldown;
        public static CustomToggleOption TrapsRemoveOnNewRound;
        public static CustomNumberOption MaxTraps;
        public static CustomNumberOption MinAmountOfTimeInTrap;
        public static CustomNumberOption TrapSize;
        public static CustomNumberOption MinAmountOfPlayersInTrap;

        public static CustomHeaderOption Traitor;
        public static CustomNumberOption LatestSpawn;
        public static CustomToggleOption NeutralKillingStopsTraitor;

        public static CustomHeaderOption Amnesiac;
        public static CustomToggleOption RememberArrows;
        public static CustomNumberOption RememberArrowDelay;

        public static CustomHeaderOption Medium;
        public static CustomNumberOption MediateCooldown;
        public static CustomToggleOption ShowMediatePlayer;
        public static CustomToggleOption ShowMediumToDead;
        public static CustomStringOption DeadRevealed;
        public static CustomNumberOption MediumArrowDuration;

        public static CustomHeaderOption Survivor;
        public static CustomNumberOption VestCd;
        public static CustomNumberOption VestDuration;
        public static CustomNumberOption VestKCReset;
        public static CustomNumberOption MaxVests;

        public static CustomHeaderOption Lawyer;
        public static CustomToggleOption LawyerDies;
        public static CustomNumberOption DefendantImpPercent;
        public static CustomToggleOption NeutralDefendant;
        public static CustomToggleOption LawyerCanTalkDefendant;
        public static CustomStringOption OnDefendantDead;

        public static CustomHeaderOption GuardianAngel;
        public static CustomNumberOption ProtectCd;
        public static CustomNumberOption ProtectDuration;
        public static CustomNumberOption ProtectKCReset;
        public static CustomNumberOption MaxProtects;
        public static CustomStringOption ShowProtect;
        public static CustomStringOption GaOnTargetDeath;
        public static CustomToggleOption GATargetKnows;
        public static CustomToggleOption GAKnowsTargetRole;
        public static CustomNumberOption EvilTargetPercent;

        public static CustomHeaderOption Blackmailer;
        public static CustomNumberOption BlackmailCooldown;
        public static CustomToggleOption BlackmailInvisible;
        public static CustomNumberOption LatestNonVote;

        public static CustomHeaderOption Plaguebearer;
        public static CustomNumberOption InfectCooldown;
        public static CustomNumberOption PestKillCooldown;
        public static CustomNumberOption PestKillDelay;
        public static CustomToggleOption PestVent;

        public static CustomHeaderOption Werewolf;
        public static CustomNumberOption RampageCooldown;
        public static CustomNumberOption RampageDuration;
        public static CustomNumberOption RampageKillCooldown;
        public static CustomToggleOption WerewolfVent;

        public static CustomHeaderOption Investigator;
        public static CustomNumberOption ExamineCooldown;
        public static CustomToggleOption InvestigatorReportOn;
        public static CustomNumberOption InvestigatorRoleDuration;
        public static CustomNumberOption InvestigatorFactionDuration;

        public static CustomHeaderOption Imitator;
        public static CustomToggleOption ImitatorCanBecomeMayor;

        public static CustomHeaderOption Escapist;
        public static CustomNumberOption EscapeCooldown;
        public static CustomToggleOption EscapistVent;

        public static CustomHeaderOption Bomber;
        public static CustomNumberOption MaxKillsInDetonation;
        public static CustomNumberOption DetonateDelay;
        public static CustomNumberOption DetonateRadius;
        public static CustomToggleOption BomberVent;
        public static CustomToggleOption AllImpsSeeBomb;

        public static CustomHeaderOption Poisoner;
        public static CustomNumberOption PoisonDuration;
        public static CustomToggleOption PoisonerVent;

        public static CustomHeaderOption Doomsayer;
        public static CustomNumberOption ObserveCooldown;
        public static CustomToggleOption DoomsayerGuessCrewInvestigative;
        public static CustomToggleOption DoomsayerGuessNeutralBenign;
        public static CustomToggleOption DoomsayerGuessNeutralEvil;
        public static CustomToggleOption DoomsayerGuessNeutralChaos;
        public static CustomToggleOption DoomsayerGuessNeutralKilling;
        public static CustomToggleOption DoomsayerGuessNeutralNeophyte;
        public static CustomToggleOption DoomsayerGuessNeutralApocalypse;
        public static CustomToggleOption DoomsayerGuessImpostors;
        public static CustomToggleOption DoomsayerCantObserve;

        public static CustomHeaderOption Cannibal;
        public static CustomToggleOption CannibalArrows;
        public static CustomNumberOption CannibalArrowDelay;
        public static CustomNumberOption BodiesNeededToWin;

        public static CustomHeaderOption Inquisitor;
        public static CustomNumberOption InquireCooldown;
        public static CustomToggleOption VanquishEnabled;
        public static CustomNumberOption VanquishCooldown;
        public static CustomToggleOption VanquishRoundOne;

        public static CustomHeaderOption Tyrant;
        public static CustomNumberOption TyrantVoteBank;
        public static CustomToggleOption TyrantAnonymous;

        public static CustomHeaderOption Vampire;
        public static CustomNumberOption BiteCooldown;
        public static CustomNumberOption BiteDuration;
        public static CustomToggleOption VampImpVision;
        public static CustomToggleOption VampVent;
        public static CustomToggleOption NewVampCanAssassin;
        public static CustomNumberOption MaxVampiresPerGame;
        public static CustomToggleOption CanBiteNeutralBenign;
        public static CustomToggleOption CanBiteNeutralEvil;
        public static CustomToggleOption CanBiteNeutralChaos;

        public static CustomHeaderOption Captain;
        public static CustomNumberOption MaxTribunals;
        public static CustomNumberOption MaxTribunalEjects;

        public static CustomHeaderOption Prosecutor;
        public static CustomToggleOption ProsDiesOnIncorrectPros;
        public static CustomNumberOption MaxProsecutes;

        public static CustomHeaderOption Warlock;
        public static CustomNumberOption ChargeUpDuration;
        public static CustomNumberOption ChargeUseDuration;

        public static CustomHeaderOption Oracle;
        public static CustomNumberOption ConfessCooldown;
        public static CustomNumberOption RevealAccuracy;
        public static CustomToggleOption NeutralBenignShowsEvil;
        public static CustomToggleOption NeutralEvilShowsEvil;
        public static CustomToggleOption NeutralKillingShowsEvil;

        public static CustomHeaderOption Venerer;
        public static CustomNumberOption AbilityCooldown;
        public static CustomNumberOption AbilityDuration;
        public static CustomNumberOption SprintSpeed;
        public static CustomNumberOption FreezeSpeed;

        public static CustomHeaderOption Aurial;
        public static CustomNumberOption AuraInnerRadius;
        public static CustomNumberOption AuraOuterRadius;
        public static CustomNumberOption SenseDuration;

        public static CustomHeaderOption Politician;
        public static CustomNumberOption CampaignCooldown;

        public static CustomHeaderOption Hypnotist;
        public static CustomNumberOption HypnotiseCooldown;

        public static CustomHeaderOption Jailor;
        public static CustomNumberOption JailCooldown;
        public static CustomNumberOption MaxExecutes;

        public static CustomHeaderOption SoulCollector;
        public static CustomNumberOption ReapCooldown;
        public static CustomToggleOption PassiveSoulCollection;
        public static CustomNumberOption SoulsToWin;

        public static CustomHeaderOption Lookout;
        public static CustomNumberOption WatchCooldown;
        public static CustomNumberOption PerceptCd;
        public static CustomNumberOption PerceptDuration;
        public static CustomToggleOption LoResetOnNewRound;
        public static CustomNumberOption MaxWatches;
        public static CustomNumberOption MaxPercepts;

        public static CustomHeaderOption SerialKiller;
        public static CustomToggleOption SkImpVision;
        public static CustomToggleOption SkVent;
        public static CustomNumberOption SkKillCooldown;
        public static CustomNumberOption BloodlustDuration;
        public static CustomNumberOption BloodlustIncreaseDuration;
        public static CustomNumberOption BloodlustCorrectKillCooldown;
        public static CustomNumberOption BloodlustIncorrectKillCooldown;

        public static CustomHeaderOption Giant;
        public static CustomNumberOption GiantSlow;

        public static CustomHeaderOption Flash;
        public static CustomNumberOption FlashSpeed;

        public static CustomHeaderOption Diseased;
        public static CustomNumberOption DiseasedKillMultiplier;

        public static CustomHeaderOption Bait;
        public static CustomNumberOption BaitMinDelay;
        public static CustomNumberOption BaitMaxDelay;

        public static CustomHeaderOption Oblivious;
        public static CustomToggleOption ObliviousCanReport;

        public static CustomHeaderOption Lovers;
        public static CustomToggleOption BothLoversDie;
        public static CustomNumberOption LovingImpPercent;
        public static CustomToggleOption NeutralLovers;
        public static CustomToggleOption ImpLoverKillTeammate;

        public static CustomHeaderOption Frosty;
        public static CustomNumberOption ChillDuration;
        public static CustomNumberOption ChillStartSpeed;

        public static CustomHeaderOption Shy;
        public static CustomNumberOption InvisDelay;
        public static CustomNumberOption TransformInvisDuration;
        public static CustomNumberOption FinalTransparency;

        public static CustomHeaderOption Saboteur;
        public static CustomNumberOption ReducedSaboCooldown;

        public static Func<object, string> PercentFormat { get; } = value => $"{value:0}%";
        private static Func<object, string> CooldownFormat { get; } = value => $"{value:0.0#}s";
        private static Func<object, string> MultiplierFormat { get; } = value => $"{value:0.0#}x";


        public static void GenerateAll()
        {
            var num = 0;

            CrewInvestigativeRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Investigative Roles");
                
                AurialOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#B34D99FF>Aurial</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                HaunterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#D3D3D3FF>Haunter</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                InvestigatorOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#00B3B3FF>Investigator</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                LookoutOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#33FF66FF>Lookout</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                MediumOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#A680FFFF>Medium</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SpyOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#CCA3CCFF>Spy</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                PsychicOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#9900FFFF>Psychic</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                TrackerOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#009900FF>Tracker</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                TrapperOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#A7D1B3FF>Trapper</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            CrewKillingRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Killing Roles");
                DeputyOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#FFCC00FF>Deputy</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                HunterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#29AB87FF>Hunter</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SheriffOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#FFFF00FF>Sheriff</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                VeteranOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#998040FF>Veteran</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                VigilanteOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#FFFF99FF>Vigilante</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            CrewProtectiveRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Protective Roles");
                AltruistOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#660000FF>Altruist</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                BodyguardOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#80D3ABFF>Bodyguard</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                MedicOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#006600FF>Medic</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                MirrorMasterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#90A2C4FF>Mirror Master</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                OracleOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#BF00BFFF>Oracle</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            CrewSovereignRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Sovereign Roles");
                CaptainOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#C3E2EDFF>Captain</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                JailorOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#A6A6A6FF>Jailor</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                PoliticianOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#660099FF>Politician</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                ProsecutorOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#B38000FF>Prosecutor</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SwapperOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#66E666FF>Swapper</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            CrewUtilityRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Support Roles");
                EngineerOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#FFA60AFF>Engineer</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                ImitatorOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#B3D94DFF>Imitator</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                TimeLordOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#0000FFFF>Time Lord</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                TransporterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#00EEFFFF>Transporter</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);


            NeutralBenignRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Benign Roles");
                AmnesiacOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#80B2FFFF>Amnesiac</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                GuardianAngelOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#B3FFFFFF>Guardian Angel</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                LawyerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#D2B48CFF>Lawyer</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SurvivorOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#FFE64DFF>Survivor</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            NeutralEvilRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Evil Roles");
                DoomsayerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#00FF80FF>Doomsayer</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                ExecutionerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#8C4005FF>Executioner</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                JesterOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#FFBFCCFF>Jester</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                PhantomOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#662962FF>Phantom</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            NeutralChaosRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Chaos Roles");
                CannibalOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#8C4005FF>Cannibal</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                InquisitorOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#DA4291FF>Inquisitor</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                TyrantOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#EA535BFF>Tyrant</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            NeutralKillingRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Killing Roles");
                ArsonistOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#FF4D00FF>Arsonist</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SerialKillerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#003FFFFF>Serial Killer</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                GlitchOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#00FF00FF>The Glitch</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                WerewolfOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#A86629FF>Werewolf</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            NeutralNeophyteRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Neophyte Roles");
                VampireOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#262626FF>Vampire</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            NeutralApocalypseRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Apocalypse Roles");
                JuggernautOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#8C004DFF>Juggernaut</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                PlaguebearerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#8C004DFF>Plaguebearer</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SoulCollectorOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#8C004DFF>Soul Collector</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            ImpostorConcealingRoles = new CustomHeaderOption(num++, MultiMenu.imposter, "Impostor Concealing Roles");
                EscapistOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Escapist</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                GrenadierOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Grenadier</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                MorphlingOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Morphling</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SwooperOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Swooper</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                VenererOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Venerer</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            ImpostorKillingRoles = new CustomHeaderOption(num++, MultiMenu.imposter, "Impostor Killing Roles");
                BomberOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Bomber</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                PoisonerOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Poisoner</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                TraitorOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Traitor</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                WarlockOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Warlock</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            ImpostorSupportRoles = new CustomHeaderOption(num++, MultiMenu.imposter, "Impostor Support Roles");
                BlackmailerOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Blackmailer</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                HypnotistOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Hypnotist</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                JanitorOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Janitor</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                MinerOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Miner</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                UndertakerOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Undertaker</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            CrewmateModifiers = new CustomHeaderOption(num++, MultiMenu.modifiers, "Crewmate Modifiers");
                AftermathOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#A6FFA6FF>Aftermath</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                BaitOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#33B3B3FF>Bait</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                DiseasedOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#808080FF>Diseased</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                FrostyOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#99FFFFFF>Frosty</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                MultitaskerOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF804DFF>Multitasker</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                TorchOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FFFF99FF>Torch</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            GlobalModifiers = new CustomHeaderOption(num++, MultiMenu.modifiers, "Global Modifiers");
                ButtonBarryOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#E600FFFF>Button Barry</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                DrunkOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#758000FF>Drunk</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                FlashOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF8080FF>Flash</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                GiantOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FFB34DFF>Giant</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                MiniOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#CCFFE6FF>Mini</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                ObliviousOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#AAAAAAFF>Oblivious</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                RadarOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0080FF>Radar</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                ShyOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FFB3CCFF>Shy</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SixthSenseOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#D9FF8CFF>Sixth Sense</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SleuthOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#803333FF>Sleuth</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                TiebreakerOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#99E699FF>Tiebreaker</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            ImpostorModifiers = new CustomHeaderOption(num++, MultiMenu.modifiers, "Impostor Modifiers");
                DisperserOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Disperser</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                DoubleShotOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Double Shot</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                SaboteurOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Saboteur</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                UnderdogOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Underdog</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);


            RoleListSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Role List Settings");

            UniqueCrewInvestRoles = new CustomToggleOption(num++, MultiMenu.main, "Unique Crew Investigatives", true);
            UniqueCrewKillingRoles = new CustomToggleOption(num++, MultiMenu.main, "Unique Crew Killers", true);
            UniqueCrewProtectRoles = new CustomToggleOption(num++, MultiMenu.main, "Unique Crew Protectives", true);
            UniqueCrewUtilRoles = new CustomToggleOption(num++, MultiMenu.main, "Unique Crew Utility", true);
            UniqueNeutBenignRoles = new CustomToggleOption(num++, MultiMenu.main, "Unique Neutral Benigns", true);
            UniqueNeutEvilRoles = new CustomToggleOption(num++, MultiMenu.main, "Unique Neutral Evils", true);
            UniqueNeutKillingRoles = new CustomToggleOption(num++, MultiMenu.main, "Unique Neutral Killers ", true);
            UniqueImpRoles = new CustomToggleOption(num++, MultiMenu.main, "Unique Impostors", true);


            Slot1 = new CustomStringOption(num++, MultiMenu.main, "Slot 1", roleBuckets.ToArray(), 20);
            Slot2 = new CustomStringOption(num++, MultiMenu.main, "Slot 2", roleBuckets.ToArray(), 20);
            Slot3 = new CustomStringOption(num++, MultiMenu.main, "Slot 3", roleBuckets.ToArray(), 20);
            Slot4 = new CustomStringOption(num++, MultiMenu.main, "Slot 4", roleBuckets.ToArray(), 19);
            Slot5 = new CustomStringOption(num++, MultiMenu.main, "Slot 5", roleBuckets.ToArray(), 20);
            Slot6 = new CustomStringOption(num++, MultiMenu.main, "Slot 6", roleBuckets.ToArray(), 20);
            Slot7 = new CustomStringOption(num++, MultiMenu.main, "Slot 7", roleBuckets.ToArray(), 20);
            Slot8 = new CustomStringOption(num++, MultiMenu.main, "Slot 8", roleBuckets.ToArray(), 20);
            Slot9 = new CustomStringOption(num++, MultiMenu.main, "Slot 9", roleBuckets.ToArray(), 19);
            Slot10 = new CustomStringOption(num++, MultiMenu.main, "Slot 10", roleBuckets.ToArray(), 20);
            Slot11 = new CustomStringOption(num++, MultiMenu.main, "Slot 11", roleBuckets.ToArray(), 20);
            Slot12 = new CustomStringOption(num++, MultiMenu.main, "Slot 12", roleBuckets.ToArray(), 20);
            Slot13 = new CustomStringOption(num++, MultiMenu.main, "Slot 13", roleBuckets.ToArray(), 20);
            Slot14 = new CustomStringOption(num++, MultiMenu.main, "Slot 14", roleBuckets.ToArray(), 19);
            Slot15 = new CustomStringOption(num++, MultiMenu.main, "Slot 15", roleBuckets.ToArray(), 20);

            MapSettings = new CustomHeaderOption(num++, MultiMenu.main, "Map Settings");
                RandomMapEnabled = new CustomToggleOption(num++, MultiMenu.main, "Choose Random Map", false);
                RandomMapSkeld = new CustomNumberOption(num++, MultiMenu.main, "Skeld Chance", 0f, 0f, 100f, 10f, PercentFormat);
                RandomMapMira = new CustomNumberOption(num++, MultiMenu.main, "Mira Chance", 0f, 0f, 100f, 10f, PercentFormat);
                RandomMapPolus = new CustomNumberOption(num++, MultiMenu.main, "Polus Chance", 0f, 0f, 100f, 10f, PercentFormat);
                RandomMapAirship = new CustomNumberOption(num++, MultiMenu.main, "Airship Chance", 0f, 0f, 100f, 10f, PercentFormat);
                RandomMapFungle = new CustomNumberOption(num++, MultiMenu.main, "Fungle Chance", 0f, 0f, 100f, 10f, PercentFormat);
                RandomMapSubmerged = new CustomNumberOption(num++, MultiMenu.main, "Submerged Chance", 0f, 0f, 100f, 10f, PercentFormat);
                RandomMapLevelImpostor = new CustomNumberOption(num++, MultiMenu.main, "Level Impostor Chance", 0f, 0f, 100f, 10f, PercentFormat);
                SmallMapHalfVision = new CustomToggleOption(num++, MultiMenu.main, "Half Vision On Skeld/Mira HQ", false);
                SmallMapDecreasedCooldown =
                    new CustomNumberOption(num++, MultiMenu.main, "Mira HQ Decreased Cooldowns", 0f, 0f, 7.5f, 2.5f, CooldownFormat);
                LargeMapIncreasedCooldown =
                    new CustomNumberOption(num++, MultiMenu.main, "Airship/Submerged Increased Cooldowns", 0f, 0f, 7.5f, 2.5f, CooldownFormat);
                SmallMapIncreasedShortTasks =
                    new CustomNumberOption(num++, MultiMenu.main, "Skeld/Mira HQ Increased Short Tasks", 0, 0, 5, 1);
                SmallMapIncreasedLongTasks =
                    new CustomNumberOption(num++, MultiMenu.main, "Skeld/Mira HQ Increased Long Tasks", 0, 0, 3, 1);
                LargeMapDecreasedShortTasks =
                    new CustomNumberOption(num++, MultiMenu.main, "Airship/Submerged Decreased Short Tasks", 0, 0, 5, 1);
                LargeMapDecreasedLongTasks =
                    new CustomNumberOption(num++, MultiMenu.main, "Airship/Submerged Decreased Long Tasks", 0, 0, 3, 1);

            BetterPolusSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Better Polus Settings");
                VentImprovements = new CustomToggleOption(num++, MultiMenu.main, "Better Polus Vent Layout", false);
                VitalsLab = new CustomToggleOption(num++, MultiMenu.main, "Vitals Moved To Lab", false);
                ColdTempDeathValley = new CustomToggleOption(num++, MultiMenu.main, "Cold Temp Moved To Death Valley", false);
                WifiChartCourseSwap =
                    new CustomToggleOption(num++, MultiMenu.main, "Reboot Wifi And Chart Course Swapped", false);

            CustomGameSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Custom Game Settings");
                ColourblindComms = new CustomToggleOption(num++, MultiMenu.main, "Camouflaged Comms", true);
                CamoCommsKillAnyone = new CustomToggleOption(num++, MultiMenu.main, "Kill Anyone During Camouflaged Comms", true);
                ImpostorSeeRoles = new CustomToggleOption(num++, MultiMenu.main, "Impostors Can See The Roles Of Their Team", true);
                DeadSeeRoles =
                    new CustomToggleOption(num++, MultiMenu.main, "Dead Can See Everyone's Roles/Votes", true);
                InitialCooldowns =
                    new CustomNumberOption(num++, MultiMenu.main, "Game Start Cooldowns", 10f, 10f, 30f, 2.5f, CooldownFormat);
                ParallelMedScans = new CustomToggleOption(num++, MultiMenu.main, "Parallel Medbay Scans", false);
                SkipButtonDisable = new CustomStringOption(num++, MultiMenu.main, "Disable Meeting Skip Button", new[] { "No", "Emergency", "Always" });
                FirstDeathShield = new CustomToggleOption(num++, MultiMenu.main, "First Death Shield Next Game", false);
                NeutralEvilWinEndsGame = new CustomToggleOption(num++, MultiMenu.main, "Neutral Evil Win Ends Game", true);
                CrewKillersContinue = new CustomToggleOption(num++, MultiMenu.main, "Crew Killers Continue Game", false);
                ShowRecentBodies = new(num++, MultiMenu.main, "Indicate Recent Deaths In Meetings", true);
                LocationReports = new CustomToggleOption(num++, MultiMenu.main, "State Locations Of Body Reports In Chat", true);

            TaskTrackingSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Task Tracking Settings");
                SeeTasksDuringRound = new CustomToggleOption(num++, MultiMenu.main, "See Tasks During Round", false);
                SeeTasksDuringMeeting = new CustomToggleOption(num++, MultiMenu.main, "See Tasks During Meetings", true);
                SeeTasksWhenDead = new CustomToggleOption(num++, MultiMenu.main, "See Tasks When Dead", true);

            Assassin = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Assassin Ability</color>");
                NumberOfImpostorAssassins = new CustomNumberOption(num++, MultiMenu.imposter, "Number Of Impostor Assassins", 1, 0, 4, 1);
                NumberOfNeutralAssassins = new CustomNumberOption(num++, MultiMenu.imposter, "Number Of Neutral Assassins", 1, 0, 5, 1);
                AmneTurnImpAssassin = new CustomToggleOption(num++, MultiMenu.imposter, "Amnesiac Turned Impostor Gets Ability", false);
                AmneTurnNeutAssassin = new CustomToggleOption(num++, MultiMenu.imposter, "Amnesiac Turned Neutral Killing Gets Ability", false);
                TraitorCanAssassin = new CustomToggleOption(num++, MultiMenu.imposter, "Traitor Gets Ability", false);
                AssassinKills = new CustomNumberOption(num++, MultiMenu.imposter, "Number Of Assassin Kills", 1, 1, 15, 1);
                AssassinMultiKill = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Kill More Than Once Per Meeting", false);
                AssassinGuessCrewInvestigative = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Crewmate Investigative Roles", false);
                AssassinCrewmateGuess = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess \"Crewmate\"", false);
                AssassinGuessNeutralBenign = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Benign Roles", false);
                AssassinGuessNeutralEvil = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Evil Roles", false);
                AssassinGuessNeutralChaos = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Chaos Roles", false);
                AssassinGuessNeutralKilling = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Killing Roles", false);
                AssassinGuessNeutralNeophyte = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Neophyte Roles", false);
                AssassinGuessNeutralApocalypse = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Apocalypse Roles", false);
                AssassinGuessImpostors = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Impostor Roles", false);
                AssassinGuessModifiers = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Crewmate Modifiers", false);
                AssassinGuessLovers = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Lovers", false);

            Aurial =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#B34D99FF>Aurial</color>");
                AuraInnerRadius =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Radiate Colour Range", 0.5f, 0f, 1f, 0.25f, MultiplierFormat);
                AuraOuterRadius =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Radiate Max Range", 1.5f, 1f, 5f, 0.25f, MultiplierFormat);
                SenseDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Sense Duration", 10f, 1f, 15f, 1f, CooldownFormat);
            Haunter =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#D3D3D3FF>Haunter</color>");
                HaunterTasksRemainingClicked =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Tasks Remaining When Haunter Can Be Clicked", 5, 1, 15, 1);
                HaunterTasksRemainingAlert =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Tasks Remaining When Alert Is Sent", 1, 1, 5, 1);
                HaunterRevealsNeutrals = new CustomToggleOption(num++, MultiMenu.crewmate, "Haunter Reveals Neutral Roles", false);
                HaunterCanBeClickedBy = new CustomStringOption(num++, MultiMenu.crewmate, "Who Can Click Haunter", new[] { "All", "Non-Crew", "Imps Only" });
            Investigator =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#00B3B3FF>Investigator</color>");
                ExamineCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Examine Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                InvestigatorReportOn = new CustomToggleOption(num++, MultiMenu.crewmate, "Show Investigator Reports", true);
                InvestigatorRoleDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Time Where Investigator Will Have Role", 15f, 0f, 60f, 2.5f,
                        CooldownFormat);
                InvestigatorFactionDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Time Where Investigator Will Have Faction", 30f, 0f, 60f, 2.5f,
                        CooldownFormat);
            Lookout =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#33FF66FF>Lookout</color>");
                WatchCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Watch Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                PerceptCd =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Eagle Eye Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                PerceptDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Eagle Eye Duration", 10f, 5f, 60f, 2.5f, CooldownFormat);
                LoResetOnNewRound = new CustomToggleOption(num++, MultiMenu.crewmate, "Lookout Watches Reset After Each Round", true);
                MaxWatches = new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Players That Can Be Watched", 5, 1, 15, 1);
                MaxPercepts = new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Eagle Eye Uses", 5, 1, 20, 1);
            Medium =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#A680FFFF>Medium</color>");
                MediateCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Mediate Cooldown", 10f, 1f, 15f, 1f, CooldownFormat);
                ShowMediatePlayer =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Reveal Appearance Of Mediate Target", true);
                ShowMediumToDead =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Reveal The Medium To The Mediate Target", true);
                DeadRevealed =
                    new CustomStringOption(num++, MultiMenu.crewmate, "Who Is Revealed With Mediate", new[] { "Oldest Dead", "Newest Dead", "All Dead" });
                MediumArrowDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Dead Body Arrow Duration", 0.1f, 0f, 1f, 0.05f, CooldownFormat);
            Psychic =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#9900FFFF>Psychic</color>");
                PsychicCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Psychic Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                CrewKillingRed =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Crewmate Killing Roles Are Red", false);
                NeutBenignRed =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Benign Roles Are Red", false);
                NeutEvilRed =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Evil Roles Are Red", false);
                NeutKillingRed =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Killing Roles Are Red", true);
                TraitorColourSwap =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Traitor Does Not Swap Colours", false);
                RevealAccuracy = new CustomNumberOption(num++, MultiMenu.crewmate, "Reveal Accuracy", 80f, 0f, 100f, 10f,
                    PercentFormat);
            Spy =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#CCA3CCFF>Spy</color>");
                AdminCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Portable Admin Cooldown", 10f, 1f, 15f, 1f, CooldownFormat);
                WhoSeesDead = new CustomStringOption(num++, MultiMenu.crewmate, "Who Sees Dead Bodies On Admin",
                    new[] { "Nobody", "Spy", "Everyone But Spy", "Everyone" });
                SpySeesNeutrals = new CustomToggleOption(num++, MultiMenu.crewmate, "Spy Sees Neutral Roles", false);
                SpyTasksRemaining =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Tasks Remaining When Revealed", 1, 1, 5, 1);
                SpySeesImpInMeeting = new CustomToggleOption(num++, MultiMenu.crewmate, "Spy Sees Impostors In Meetings", true);
                SpySeesTraitor = new CustomToggleOption(num++, MultiMenu.crewmate, "Spy Sees Traitor", true);
            Tracker =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#009900FF>Tracker</color>");
                UpdateInterval =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Arrow Update Interval", 5f, 0.5f, 15f, 0.5f, CooldownFormat);
                TrackCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Track Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                ResetOnNewRound = new CustomToggleOption(num++, MultiMenu.crewmate, "Tracker Arrows Reset After Each Round", true);
                MaxTracks = new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Tracks", 5, 1, 15, 1);
                FootprintSize = new CustomNumberOption(num++, MultiMenu.crewmate, "Footprint Size", 4f, 1f, 10f, 1f);
                FootprintInterval =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Footprint Interval", 0.1f, 0.05f, 1f, 0.05f, CooldownFormat);
                FootprintDuration = new CustomNumberOption(num++, MultiMenu.crewmate, "Footprint Duration", 15f, 1.5f, 60f, 2.5f, CooldownFormat);
                AnonymousFootPrint = new CustomToggleOption(num++, MultiMenu.crewmate, "Anonymous Footprint", false);
                VentFootprintVisible = new CustomToggleOption(num++, MultiMenu.crewmate, "Footprint Vent Visible", false);
                SeeOnlyTrackedPrints = new CustomToggleOption(num++, MultiMenu.crewmate, "Only Tracked Player Footprints Are Visible", true);
            Trapper =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#A7D1B3FF>Trapper</color>");
                MinAmountOfTimeInTrap =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Min Amount Of Time In Trap To Register", 1f, 0f, 15f, 0.5f, CooldownFormat);
                TrapCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Trap Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                TrapsRemoveOnNewRound =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Traps Removed After Each Round", true);
                MaxTraps =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Traps", 5, 1, 15, 1);
                TrapSize =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Trap Size", 0.25f, 0.05f, 1f, 0.05f, MultiplierFormat);
                MinAmountOfPlayersInTrap =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Minimum Number Of Roles Required To Trigger Trap", 3, 1, 5, 1);


            Hunter =
               new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#29AB87FF>Hunter</color>");
                HunterKillCd =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Hunter Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                HunterStalkCd =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Hunter Stalk Cooldown", 10f, 1f, 15f, 1f, CooldownFormat);
                HunterStalkDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Hunter Stalk Duration", 25f, 5f, 60f, 2.5f, CooldownFormat);
                HunterStalkUses =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Stalk Uses", 5, 1, 15, 1);
                RetributionOnVote =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Hunter Kills Last Voter If Voted Out", false);
                HunterBodyReport =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Hunter Can Report Who They've Killed");
            Sheriff =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#FFFF00FF>Sheriff</color>");
                SheriffKillOther =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Miskill Kills Crewmate", false);
                SheriffKillsNE =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Neutral Evil Roles", false);
                SheriffKillsNK =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Neutral Killing Roles", false);
                SheriffKillCd =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Sheriff Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                SheriffBodyReport = new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Can Report Who They've Killed");
            Veteran =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#998040FF>Veteran</color>");
                KilledOnAlert =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Can Be Killed On Alert", false);
                AlertCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Alert Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                AlertDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Alert Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                MaxAlerts = new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Alerts", 5, 1, 15, 1);
            Vigilante = new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#FFFF99FF>Vigilante</color>");
                VigilanteKills = new CustomNumberOption(num++, MultiMenu.crewmate, "Number Of Vigilante Kills", 1, 1, 15, 1);
                VigilanteMultiKill = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Kill More Than Once Per Meeting", false);
                VigilanteGuessNeutralBenign = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Neutral Benign Roles", false);
                VigilanteGuessNeutralEvil = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Neutral Evil Roles", false);
                VigilanteGuessNeutralChaos = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Neutral Chaos Roles", false);
                VigilanteGuessNeutralKilling = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Neutral Killing Roles", false);
                VigilanteGuessNeutralNeophyte = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Neutral Neophyte Roles", false);
                VigilanteGuessNeutralApocalypse = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Neutral Apocalypse Roles", false);
                VigilanteGuessModifiers = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Impostor Modifiers", false);
                VigilanteGuessLovers = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Lovers", false);


            Altruist = new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#660000FF>Altruist</color>");
                ReviveCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Altruist Revive Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                ReviveDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Altruist Revive Duration", 10f, 1f, 15f, 1f, CooldownFormat);
                AltruistMaxRevives =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Revives Before Suicide", 3, 1, 5, 1);
                AltruistTargetBody =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Target's Body Disappears On Beginning Of Revive", false);
            Bodyguard =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#80D3ABFF>Bodyguard</color>");
                GuardCd =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Guard Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                GuardDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Guard Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                MaxGuards =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Guards", 5, 1, 15, 1);
                ShowGuarding =
                    new CustomStringOption(num++, MultiMenu.crewmate, "Show Guarded Player",
                        new[] { "Self", "Bodyguard", "Self+BG", "Everyone" });
            Medic =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#006600FF>Medic</color>");
                ShowShielded =
                    new CustomStringOption(num++, MultiMenu.crewmate, "Show Shielded Player",
                        new[] { "Self", "Medic", "Self+Medic", "Everyone" });
                WhoGetsNotification =
                    new CustomStringOption(num++, MultiMenu.crewmate, "Who Gets Murder Attempt Indicator",
                        new[] { "Medic", "Shielded", "Everyone", "Nobody" });
                ShieldBreaks = new CustomToggleOption(num++, MultiMenu.crewmate, "Shield Breaks On Murder Attempt", false);
                MedicReportSwitch = new CustomToggleOption(num++, MultiMenu.crewmate, "Show Medic Reports");
                MedicReportNameDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Time Where Medic Will Have Name", 0f, 0f, 60f, 2.5f,
                        CooldownFormat);
                MedicReportColorDuration =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Time Where Medic Will Have Color Type", 15f, 0f, 60f, 2.5f,
                        CooldownFormat);
            MirrorMaster =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#90A2C4FF>Mirror Master</color>");
                MaxMirrors =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Mirror Uses", 5, 1, 20, 1);
                ShowMirrorShielded =
                    new CustomStringOption(num++, MultiMenu.crewmate, "Show Mirrored Player",
                        new[] { "Mirror Master", "Self", "Self+MM", "Everyone" });
                WhoGetsMirrorNotification =
                    new CustomStringOption(num++, MultiMenu.crewmate, "Who Gets Absorb Indicator",
                        new[] { "Mirror Master", "Shielded", "MM+Shielded", "Everyone", "Nobody" });
                MirrorAbsorbCd =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Absorb Cooldown", 25f, 0f, 60f, 2.5f, CooldownFormat);
                MirrorUnleashCd =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Unleash Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                MirrorBreaks = new CustomToggleOption(num++, MultiMenu.crewmate, "Mirror Shatters On Murder Attempt", true);
            Oracle =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#BF00BFFF>Oracle</color>");
                ConfessCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Confess Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                RevealAccuracy = new CustomNumberOption(num++, MultiMenu.crewmate, "Reveal Accuracy", 80f, 0f, 100f, 10f,
                    PercentFormat);
                NeutralBenignShowsEvil =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Benign Roles Show Evil", false);
                NeutralEvilShowsEvil =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Evil Roles Show Evil", false);
                NeutralKillingShowsEvil =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Killing Roles Show Evil", true);


            Captain =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#C3E2EDFF>Captain</color>");
            MaxTribunals =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Tribunals", 2, 1, 3, 1);
            MaxTribunalEjects =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Ejections Per Tribunal", 2, 2, 4, 1);
            Jailor =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#A6A6A6FF>Jailor</color>");
                JailCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Jail Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                MaxExecutes =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Executes", 3, 1, 5, 1);
            Politician =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#660099FF>Politician</color>");
                CampaignCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Campaign Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            Prosecutor =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#B38000FF>Prosecutor</color>");
            ProsDiesOnIncorrectPros =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Prosecutor Dies When They Exile A Crewmate", false);
            MaxProsecutes =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Prosecutes", 2, 1, 3, 1);
            Swapper =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#66E666FF>Swapper</color>");
                SwapperButton =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Swapper Can Button", true);

            Engineer =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#FFA60AFF>Engineer</color>");
                MaxFixes =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Fixes", 5, 1, 15, 1);
            Imitator =
                    new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#B3D94DFF>Imitator</color>");
                ImitatorCanBecomeMayor =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Imitator Can Become Mayor", true);
            TimeLord =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#0000FFFF>Time Lord</color>");
                RewindRevive = new CustomToggleOption(num++, MultiMenu.crewmate, "Revive During Rewind", false);
                RewindDuration = new CustomNumberOption(num++, MultiMenu.crewmate, "Rewind Duration", 3f, 3f, 15f, 0.5f, CooldownFormat);
                RewindCooldown = new CustomNumberOption(num++, MultiMenu.crewmate, "Rewind Cooldown", 25f, 10f, 40f, 2.5f, CooldownFormat);
                TimeLordVitals =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Time Lord can use Vitals", false);

            Transporter =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#00EEFFFF>Transporter</color>");
                TransportCooldown =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Transport Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                TransportMaxUses =
                    new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Transports", 5, 1, 15, 1);
                TransporterVitals =
                    new CustomToggleOption(num++, MultiMenu.crewmate, "Transporter Can Use Vitals", false);



            Amnesiac = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#80B2FFFF>Amnesiac</color>");
                RememberArrows =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Amnesiac Gets Arrows Pointing To Dead Bodies", false);
                RememberArrowDelay =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Time After Death Arrow Appears", 5f, 0f, 15f, 1f, CooldownFormat);
            GuardianAngel =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#B3FFFFFF>Guardian Angel</color>");
                ProtectCd =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Protect Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                ProtectDuration =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Protect Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                ProtectKCReset =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Kill Cooldown Reset When Protected", 2.5f, 0f, 15f, 0.5f, CooldownFormat);
                MaxProtects =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Maximum Number Of Protects", 5, 1, 15, 1);
                ShowProtect =
                    new CustomStringOption(num++, MultiMenu.neutral, "Show Protected Player",
                        new[] { "Self", "Guardian Angel", "Self+GA", "Everyone" });
                GaOnTargetDeath = new CustomStringOption(num++, MultiMenu.neutral, "GA Becomes On Target Dead",
                    new[] { "Crew", "Amnesiac", "Survivor", "Jester" });
                GATargetKnows =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Target Knows GA Exists", false);
                GAKnowsTargetRole =
                    new CustomToggleOption(num++, MultiMenu.neutral, "GA Knows Targets Role", false);
                EvilTargetPercent = new CustomNumberOption(num++, MultiMenu.neutral, "Odds Of Target Being Evil", 20f, 0f, 100f, 10f,
                    PercentFormat);
            Lawyer =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#D2B48CFF>Lawyer</color>");
                LawyerDies = new CustomToggleOption(num++, MultiMenu.neutral, "Lawyer Dies With Defendant");
                DefendantImpPercent = new CustomNumberOption(num++, MultiMenu.neutral, "Killer Defendant Probability", 20f, 0f, 100f, 10f,
                    PercentFormat);
                NeutralDefendant = new CustomToggleOption(num++, MultiMenu.neutral, "Neutral Evil Roles Can Be Defendants", false);
                LawyerCanTalkDefendant = new CustomToggleOption(num++, MultiMenu.neutral, "Lawyer And Defendant Can Talk", false);
                OnDefendantDead = new CustomStringOption(num++, MultiMenu.neutral, "Lawyer Becomes On Defendant Dead",
                    new[] { "Crew", "Amnesiac", "Survivor", "Jester" });
            Survivor =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#FFE64DFF>Survivor</color>");
                VestCd =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Vest Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                VestDuration =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Vest Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                VestKCReset =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Kill Cooldown Reset On Attack", 2.5f, 0f, 15f, 0.5f, CooldownFormat);
                MaxVests =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Maximum Number Of Vests", 5, 1, 15, 1);

            Doomsayer = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#00FF80FF>Doomsayer</color>");
                ObserveCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Observe Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                DoomsayerGuessCrewInvestigative = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Crewmate Investigative Roles", false);
                DoomsayerGuessNeutralBenign = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Benign Roles", false);
                DoomsayerGuessNeutralEvil = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Evil Roles", false);
                DoomsayerGuessNeutralChaos = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Chaos Roles", false);
                DoomsayerGuessNeutralKilling = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Killing Roles", false);
                DoomsayerGuessNeutralNeophyte = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Neophyte Roles", false);
                DoomsayerGuessNeutralApocalypse = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Apocalypse Roles", false);
                DoomsayerGuessImpostors = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Impostor Roles", false);
                DoomsayerCantObserve = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can't Observe", false);
            Executioner =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#8C4005FF>Executioner</color>");
                OnTargetDead = new CustomStringOption(num++, MultiMenu.neutral, "Executioner Becomes On Target Dead",
                    new[] { "Crew", "Amnesiac", "Survivor", "Jester" });
                ExecutionerButton =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Executioner Can Button", true);
                ExecutionerTorment =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Executioner Torments Player On Victory", true);
            Jester =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#FFBFCCFF>Jester</color>");
                JesterButton =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Jester Can Button", true);
                JesterVent =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Jester Can Hide In Vents", false);
                JesterImpVision =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Jester Has Impostor Vision", false);
                JesterHaunt =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Jester Haunts Player On Victory", true);
            Phantom =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#662962FF>Phantom</color>");
                PhantomTasksRemaining =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Tasks Remaining When Phantom Can Be Clicked", 5, 1, 15, 1);
                PhantomSpook =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Phantom Spooks Player On Victory", true);


            Cannibal = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#8C4005FF>Cannibal</color>");
                CannibalArrows =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Cannibal Gets Arrows Pointing To Dead Bodies", false);
                CannibalArrowDelay =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Time After Death Arrow Appears", 5f, 0f, 15f, 1f, CooldownFormat);
                BodiesNeededToWin =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Bodies Needed To Win", 3, 1, 6, 1);
            Inquisitor = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#DA4291FF>Inquisitor</color>");
                InquireCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Inquire Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                VanquishEnabled =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Inquisitor Can Vanquish", true);
                VanquishRoundOne =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Inquisitor Can Vanquish Round One", false);
                VanquishCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Vanquish Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            Tyrant = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#EA535BFF>Tyrant</color>");
                TyrantVoteBank =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Initial Tyrant Vote Bank", 1, 1, 15, 1);
                TyrantAnonymous =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Tyrant Votes Show Anonymous", false);


            Arsonist = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#FF4D00FF>Arsonist</color>");
                DouseCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Douse Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                MaxDoused =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Maximum Alive Players Doused", 5, 1, 15, 1);
                ArsoImpVision =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Arsonist Has Impostor Vision", false);
                IgniteCdRemoved =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Ignite Cooldown Removed When Arsonist Is Last Killer", false);
            SerialKiller = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#003FFFFF>Serial Killer</color>");
                SkKillCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Base Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                SkVent =
                    new CustomToggleOption(num++, MultiMenu.neutral, "SK Can Vent", false);
                SkImpVision =
                    new CustomToggleOption(num++, MultiMenu.neutral, "SK Has Impostor Vision", false);
                BloodlustDuration =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Bloodlust Duration", 25f, 10f, 60f, 2.5f, CooldownFormat);
                BloodlustIncreaseDuration =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Bloodlust Duration Increase Per Kill", 10f, 5f, 15f, 0.5f, CooldownFormat);
                BloodlustCorrectKillCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Bloodlust Kill Cooldown On Correct Kill", 10f, 5f, 15f, 0.5f, CooldownFormat);
                BloodlustIncorrectKillCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Kill Cooldown Multiplier On Incorrect Kill", 3f, 1.25f, 5f, 0.25f, MultiplierFormat);
            TheGlitch =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#00FF00FF>The Glitch</color>");
                MimicCooldownOption = new CustomNumberOption(num++, MultiMenu.neutral, "Mimic Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                MimicDurationOption = new CustomNumberOption(num++, MultiMenu.neutral, "Mimic Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                HackCooldownOption = new CustomNumberOption(num++, MultiMenu.neutral, "Hack Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                HackDurationOption = new CustomNumberOption(num++, MultiMenu.neutral, "Hack Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                GlitchKillCooldownOption =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Glitch Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                GlitchHackDistanceOption =
                    new CustomStringOption(num++, MultiMenu.neutral, "Glitch Hack Distance", new[] { "Short", "Normal", "Long" });
                GlitchVent =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Glitch Can Vent", false);
            Werewolf = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#A86629FF>Werewolf</color>");
                RampageCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Rampage Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                RampageDuration =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Rampage Duration", 25f, 10f, 60f, 2.5f, CooldownFormat);
                RampageKillCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Rampage Kill Cooldown", 10f, 0.5f, 15f, 0.5f, CooldownFormat);
                WerewolfVent =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Werewolf Can Vent When Rampaged", false);


            Vampire = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#262626FF>Vampire</color>");
                BiteCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Vampire Bite Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                BiteDuration =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Vampire Bite Duration", 5f, 0f, 10f, 0.5f, CooldownFormat);
                VampImpVision =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Vampires Have Impostor Vision", false);
                VampVent =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Vampires Can Vent", false);
                NewVampCanAssassin =
                    new CustomToggleOption(num++, MultiMenu.neutral, "New Vampire Can Assassinate", false);
                MaxVampiresPerGame =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Maximum Vampires Per Game", 2, 2, 5, 1);
                CanBiteNeutralBenign =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Can Convert Neutral Benign Roles", false);
                CanBiteNeutralEvil =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Can Convert Neutral Evil Roles", false);
                CanBiteNeutralChaos =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Can Convert Neutral Chaos Roles", false);
                

            Juggernaut =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#8C004DFF>Juggernaut</color>");
                JuggKillCooldown = new CustomNumberOption(num++, MultiMenu.neutral, "Juggernaut Initial Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                ReducedKCdPerKill = new CustomNumberOption(num++, MultiMenu.neutral, "Reduced Kill Cooldown Per Kill", 5f, 2.5f, 10f, 2.5f, CooldownFormat);
                KillsNeeded = new CustomNumberOption(num++, MultiMenu.neutral, "Kills Needed Before Armaggeddon Transformation", 2, 2, 5, 1);
                ArmKillCooldown = new CustomNumberOption(num++, MultiMenu.neutral, "Armaggeddon Kill Cooldown", 10f, 0f, 60f, 2.5f, CooldownFormat);
                JuggVent =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Juggernaut Can Vent", false);
                ArmVent =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Armaggeddon Can Vent", true);
            Plaguebearer = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#8C004DFF>Plaguebearer</color>");
                InfectCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Infect Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                PestKillCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Pestilence Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                PestKillDelay =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Pestilence Kill Delay", 30f, 0f, 60f, 2.5f, CooldownFormat);
                PestVent =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Pestilence Can Vent", false);
            SoulCollector =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#8C004DFF>Soul Collector</color>");
                ReapCooldown =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Reap Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                PassiveSoulCollection =
                    new CustomToggleOption(num++, MultiMenu.neutral, "Passively Collect A Soul Each Round", true);
                SoulsToWin =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Amount Of Souls Required To Transform", 5, 1, 15, 1);
                DeathRevealDelay =
                    new CustomNumberOption(num++, MultiMenu.neutral, "Time Before A Death Meeting Is Called", 30f, 0f, 60f, 2.5f, CooldownFormat);



            Escapist =
                new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Escapist</color>");
                EscapeCooldown =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Recall Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                EscapistVent =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Escapist Can Vent", false);
            Grenadier =
                new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Grenadier</color>");
                GrenadeCooldown =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Flash Grenade Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                GrenadeDuration =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Flash Grenade Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                FlashRadius =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Flash Radius", 1f, 0.25f, 5f, 0.25f, MultiplierFormat);
                GrenadierIndicators =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Indicate Flashed Crewmates", false);
                GrenadierVent =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Grenadier Can Vent", false);
            Morphling =
                new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Morphling</color>");
                MorphlingCooldown =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Morphling Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                MorphlingDuration =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Morphling Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                MorphlingVent =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Morphling Can Vent", false);
            Swooper = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Swooper</color>");
                SwoopCooldown =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Swoop Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                SwoopDuration =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Swoop Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                SwooperVent =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Swooper Can Vent", false);
            Venerer = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Venerer</color>");
                AbilityCooldown =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Ability Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                AbilityDuration =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Ability Duration", 10f, 5f, 15f, 1f, CooldownFormat);
                SprintSpeed = new CustomNumberOption(num++, MultiMenu.imposter, "Sprint Speed", 1.25f, 1.05f, 2.5f, 0.05f, MultiplierFormat);
                FreezeSpeed = new CustomNumberOption(num++, MultiMenu.imposter, "Freeze Speed", 0.75f, 0.25f, 1f, 0.05f, MultiplierFormat);


            Bomber =
                new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Bomber</color>");
                DetonateDelay =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Detonate Delay", 5f, 1f, 15f, 1f, CooldownFormat);
                MaxKillsInDetonation =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Max Kills In Detonation", 5, 1, 15, 1);
                DetonateRadius =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Detonate Radius", 0.25f, 0.05f, 1f, 0.05f, MultiplierFormat);
                BomberVent =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Bomber Can Vent", false);
                AllImpsSeeBomb =
                    new CustomToggleOption(num++, MultiMenu.imposter, "All Impostors See Bomb", false);
            Poisoner =
                new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Poisoner</color>");
                PoisonDuration =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Poison Kill Delay", 5, 1, 15, 1f, CooldownFormat);
                PoisonerVent =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Poisoner Can Vent", false);
            Traitor = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Traitor</color>");
                LatestSpawn = new CustomNumberOption(num++, MultiMenu.imposter, "Minimum People Alive When Traitor Can Spawn", 5, 3, 15, 1);
                NeutralKillingStopsTraitor =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Traitor Won't Spawn If Any Neutral Killing Is Alive", false);
            Warlock = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Warlock</color>");
                ChargeUpDuration =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Time It Takes To Fully Charge", 25f, 10f, 60f, 2.5f, CooldownFormat);
                ChargeUseDuration =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Time It Takes To Use Full Charge", 1f, 0.05f, 5f, 0.05f, CooldownFormat);


            Blackmailer = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Blackmailer</color>");
                BlackmailCooldown =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Initial Blackmail Cooldown", 10f, 1f, 15f, 1f, CooldownFormat);
                BlackmailInvisible =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Only Target Sees Blackmail", false);
                LatestNonVote = new CustomNumberOption(num++, MultiMenu.imposter, "Maximum People Alive Where Blackmailed Can Vote", 5, 1, 15, 1);
            Hypnotist = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Hypnotist</color>");
                HypnotiseCooldown =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Hypnotize Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);

                Miner = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Miner</color>");
                MineCooldown =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Mine Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            Undertaker = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Undertaker</color>");
                DragCooldown = new CustomNumberOption(num++, MultiMenu.imposter, "Drag Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
                UndertakerDragSpeed =
                    new CustomNumberOption(num++, MultiMenu.imposter, "Undertaker Drag Speed", 0.75f, 0.25f, 1f, 0.05f, MultiplierFormat);
                UndertakerVent =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Undertaker Can Vent", false);
                UndertakerVentWithBody =
                    new CustomToggleOption(num++, MultiMenu.imposter, "Undertaker Can Vent While Dragging", false);



            Bait = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#33B3B3FF>Bait</color>");
                BaitMinDelay = new CustomNumberOption(num++, MultiMenu.modifiers, "Minimum Delay for the Bait Report", 0f, 0f, 15f, 0.5f, CooldownFormat);
                BaitMaxDelay = new CustomNumberOption(num++, MultiMenu.modifiers, "Maximum Delay for the Bait Report", 1f, 0f, 15f, 0.5f, CooldownFormat);
            Diseased = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#808080FF>Diseased</color>");
                DiseasedKillMultiplier = new CustomNumberOption(num++, MultiMenu.modifiers, "Diseased Kill Multiplier", 3f, 1.5f, 5f, 0.5f, MultiplierFormat);
            Frosty = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#99FFFFFF>Frosty</color>");
                ChillDuration = new CustomNumberOption(num++, MultiMenu.modifiers, "Chill Duration", 10f, 1f, 15f, 1f, CooldownFormat);
                ChillStartSpeed = new CustomNumberOption(num++, MultiMenu.modifiers, "Chill Start Speed", 0.75f, 0.25f, 0.95f, 0.05f, MultiplierFormat);
            Flash = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FF8080FF>Flash</color>");
                FlashSpeed = new CustomNumberOption(num++, MultiMenu.modifiers, "Flash Speed", 1.25f, 1.05f, 2.5f, 0.05f, MultiplierFormat);
            Giant = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FFB34DFF>Giant</color>");
                GiantSlow = new CustomNumberOption(num++, MultiMenu.modifiers, "Giant Speed", 0.75f, 0.25f, 1f, 0.05f, MultiplierFormat);
            Oblivious = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#AAAAAAFF>Oblivious</color>");
                ObliviousCanReport = new CustomToggleOption(num++, MultiMenu.modifiers, "Oblivious Can Report", true);
            Shy = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FFB3CCFF>Shy</color>");
                InvisDelay = new CustomNumberOption(num++, MultiMenu.modifiers, "Transparency Delay", 5f, 1f, 15f, 1f, CooldownFormat);
                TransformInvisDuration = new CustomNumberOption(num++, MultiMenu.modifiers, "Turn Transparent Duration", 5f, 1f, 15f, 1f, CooldownFormat);
                FinalTransparency = new CustomNumberOption(num++, MultiMenu.modifiers, "Final Opacity", 20f, 0f, 80f, 10f, PercentFormat);


            Saboteur = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Saboteur</color>");
                ReducedSaboCooldown = new CustomNumberOption(num++, MultiMenu.modifiers, "Reduced Sabotage Bonus", 10f, 5f, 15f, 1f, CooldownFormat);
            Underdog = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Underdog</color>");
                UnderdogKillBonus = new CustomNumberOption(num++, MultiMenu.modifiers, "Kill Cooldown Bonus", 5f, 2.5f, 10f, 2.5f, CooldownFormat);
                UnderdogIncreasedKC = new CustomToggleOption(num++, MultiMenu.modifiers, "Increased Kill Cooldown When 2+ Imps", true);
            


            CrewmateAlliances = new CustomHeaderOption(num++, MultiMenu.modifiers, "Crewmate Alliances");
                CrewpostorOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Crewpostor</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                CrewpocalypseOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FFE8B3FF>Crewpocalypse</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                EgotistOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#3A6041FF>Egotist</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
                GlobalAlliances = new CustomHeaderOption(num++, MultiMenu.modifiers, "Global Alliances");
                LoversOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF66CCFF>Lovers</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);

            Lovers =
                new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FF66CCFF>Lovers</color>");
                BothLoversDie = new CustomToggleOption(num++, MultiMenu.modifiers, "Both Lovers Die");
                LovingImpPercent = new CustomNumberOption(num++, MultiMenu.modifiers, "Loving Killer Probability", 20f, 0f, 100f, 10f,
                    PercentFormat);
                NeutralLovers = new CustomToggleOption(num++, MultiMenu.modifiers, "Neutral Roles Can Be Lovers");
                ImpLoverKillTeammate = new CustomToggleOption(num++, MultiMenu.modifiers, "Impostor Lover Can Kill Teammate", false);
        }
    }
}