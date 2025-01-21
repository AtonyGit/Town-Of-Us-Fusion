using System;
using TownOfUsFusion.Patches;

namespace TownOfUsFusion.CustomOption
{
    public class Generate
    {
//      MAP SETTINGS
        public static CustomHeaderOption MapSettings;
        public static CustomStringOption Map;
        public static CustomToggleOption RandomMapEnabled;
        public static CustomNumberOption RandomMapSkeld;
        public static CustomNumberOption RandomMapdlekS;
        public static CustomNumberOption RandomMapMira;
        public static CustomNumberOption RandomMapPolus;
        public static CustomNumberOption RandomMapAirship;
        public static CustomNumberOption RandomMapFungle;
        public static CustomNumberOption RandomMapSubmerged;
        public static CustomNumberOption RandomMapLevelImp;
        public static CustomHeaderOption MapAdjust;
        public static CustomNumberOption RandomVentSpawn;
        public static CustomToggleOption AutoAdjustSettings;
        public static CustomToggleOption SmallMapHalfVision;
        public static CustomNumberOption SmallMapDecreasedCooldown;
        public static CustomNumberOption LargeMapIncreasedCooldown;
        public static CustomNumberOption SmallMapIncreasedShortTasks;
        public static CustomNumberOption SmallMapIncreasedLongTasks;
        public static CustomNumberOption LargeMapDecreasedShortTasks;
        public static CustomNumberOption LargeMapDecreasedLongTasks;

        public static CustomHeaderOption BetterSkeldSettings;
        public static CustomToggleOption BetterSkeldEnabled;
        public static CustomToggleOption BSVentImprovements;

        public static CustomHeaderOption BetterMiraSettings;
        public static CustomToggleOption BetterMiraEnabled;
        public static CustomToggleOption BMVentImprovements;

        public static CustomHeaderOption BetterPolusSettings;
        public static CustomToggleOption BetterPolusEnabled;
        public static CustomToggleOption BPVentImprovements;
        public static CustomToggleOption BPVitalsLab;
        public static CustomToggleOption BPColdTempDeathValley;
        public static CustomToggleOption BPWifiChartCourseSwap;
        
        public static CustomHeaderOption BetterAirshipSettings;
        public static CustomToggleOption BetterAirshipEnabled;
        public static CustomStringOption BAMoveAdmin;
        public static CustomStringOption BAMoveElectrical;
        public static CustomToggleOption BAMoveVitals;
        public static CustomToggleOption BAMoveFuel;
        public static CustomToggleOption BAMoveDivert;

//      CREWMATE STUFF
        public static CustomHeaderOption CrewAstralRoles;
        public static CustomNumberOption AurialOn;
        public static CustomNumberOption HaunterOn;
        public static CustomNumberOption MediumOn;
        public static CustomNumberOption MysticOn;
        public static CustomNumberOption OracleOn;

        public static CustomHeaderOption CrewInvestigativeRoles;
        public static CustomNumberOption DetectiveOn;
        public static CustomNumberOption InvestigatorOn;
        public static CustomNumberOption SeerOn;
        public static CustomNumberOption SnitchOn;
        public static CustomNumberOption SpyOn;
        public static CustomNumberOption TrackerOn;
        public static CustomNumberOption TrapperOn;

        public static CustomHeaderOption CrewProtectiveRoles;
        public static CustomNumberOption AltruistOn;
        public static CustomNumberOption BodyguardOn;
        public static CustomNumberOption MedicOn;

        public static CustomHeaderOption CrewKillingRoles;
        public static CustomNumberOption HunterOn;
        public static CustomNumberOption SheriffOn;
        public static CustomNumberOption TricksterOn;
        public static CustomNumberOption VampireHunterOn;
        public static CustomNumberOption VeteranOn;
        public static CustomNumberOption VigilanteOn;

        public static CustomHeaderOption CrewSovereignRoles;
        public static CustomNumberOption CaptainOn;
        public static CustomNumberOption MayorOn;
        public static CustomNumberOption MonarchOn;
        public static CustomNumberOption ProsecutorOn;
        public static CustomNumberOption SwapperOn;

        public static CustomHeaderOption CrewSupportRoles;
        public static CustomNumberOption BartenderOn;
        public static CustomNumberOption EngineerOn;
        public static CustomNumberOption ImitatorOn;
        public static CustomNumberOption MageOn;
        public static CustomNumberOption TransporterOn;
        public static CustomNumberOption TaskmasterOn;

        public static CustomHeaderOption NeutralBenignRoles;
        public static CustomNumberOption AmnesiacOn;
        public static CustomNumberOption GuardianAngelOn;
        public static CustomNumberOption SurvivorOn;

        public static CustomHeaderOption NeutralEvilRoles;
        public static CustomNumberOption DoomsayerOn;
        public static CustomNumberOption ExecutionerOn;
        public static CustomNumberOption JesterOn;
        public static CustomNumberOption PhantomOn;
        public static CustomNumberOption PirateOn;

        public static CustomHeaderOption NeutralChaosRoles;
        public static CustomNumberOption CursedSoulOn;
        public static CustomNumberOption TempestOn;
        public static CustomNumberOption InquisitorOn;
        public static CustomNumberOption JokerOn;
        public static CustomNumberOption TyrantOn;
        public static CustomNumberOption CannibalOn;
        public static CustomNumberOption WitchOn;

        public static CustomHeaderOption NeutralKillingRoles;
        public static CustomNumberOption ArsonistOn;
        public static CustomNumberOption GhoulOn;
        public static CustomNumberOption GlitchOn;
        public static CustomNumberOption SentinelOn;
        public static CustomNumberOption SerialKillerOn;
        public static CustomNumberOption WerewolfOn;

        public static CustomHeaderOption NeutralNeophyteRoles;
        public static CustomNumberOption VampireOn;
        public static CustomNumberOption NeoNecromancerOn;
        public static CustomNumberOption JackalOn;

        public static CustomHeaderOption NeutralApocalypseRoles;
        public static CustomNumberOption BakerOn;
        public static CustomNumberOption BerserkerOn;
        public static CustomNumberOption PlaguebearerOn;
        public static CustomNumberOption SoulCollectorOn;

        public static CustomHeaderOption ImpostorConcealingRoles;
        public static CustomNumberOption EscapistOn;
        public static CustomNumberOption MorphlingOn;
        public static CustomNumberOption SwooperOn;
        public static CustomNumberOption GrenadierOn;
        public static CustomNumberOption VenererOn;

        public static CustomHeaderOption ImpostorKillingRoles;
        public static CustomNumberOption BomberOn;
        public static CustomNumberOption PoisonerOn;
        public static CustomNumberOption TraitorOn;
        public static CustomNumberOption WarlockOn;

        public static CustomHeaderOption ImpostorSupportRoles;
        public static CustomNumberOption BlackmailerOn;
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
        public static CustomNumberOption DrunkOn;
        public static CustomNumberOption ButtonBarryOn;
        public static CustomNumberOption DwarfOn;
        public static CustomNumberOption EclipsedOn;
        public static CustomNumberOption GiantOn;
        public static CustomNumberOption NumbOn;
        public static CustomNumberOption NinjaOn;
        public static CustomNumberOption ObliviousOn;
        public static CustomNumberOption RadarOn;
        public static CustomNumberOption SleuthOn;
        public static CustomNumberOption TiebreakerOn;
        public static CustomNumberOption TrollOn;

        public static CustomHeaderOption ImpostorModifiers;
        public static CustomNumberOption DisperserOn;
        public static CustomNumberOption DoubleShotOn;
        public static CustomNumberOption UnderdogOn;

        public static CustomHeaderOption CrewmateAlliances;
        public static CustomNumberOption EgotistOn;
        public static CustomNumberOption CrewpocalypseOn;
        public static CustomNumberOption CrewpostorOn;
        public static CustomHeaderOption GlobalAlliances;
        public static CustomNumberOption LoversOn;
        public static CustomHeaderOption Lovers;
        public static CustomToggleOption BothLoversDie;
        public static CustomNumberOption LovingImpPercent;
        public static CustomToggleOption NeutralLovers;

        public static CustomHeaderOption CustomGameSettings;
        // April Fools
        //public static CustomToggleOption EnableAprilFoolsMode;
        public static CustomToggleOption ColourblindComms;
        public static CustomToggleOption ImpostorSeeRoles;
        public static CustomToggleOption DeadSeeRoles;
        public static CustomNumberOption InitialCooldowns;
        public static CustomToggleOption ButtonBarryGetsCooldown;
        public static CustomToggleOption ParallelMedScans;
        public static CustomStringOption SkipButtonDisable;
        public static CustomToggleOption FirstDeathShield;
        public static CustomToggleOption NeutralEvilWinEndsGame;
        public static CustomToggleOption GhostsDoTasks;

        public static CustomHeaderOption GameModeSettings;
        public static CustomStringOption GameMode;

        public static CustomHeaderOption ClassicSettings;
        public static CustomNumberOption MinNeutralBenignRoles;
        public static CustomNumberOption MaxNeutralBenignRoles;
        public static CustomNumberOption MinNeutralEvilRoles;
        public static CustomNumberOption MaxNeutralEvilRoles;
        public static CustomNumberOption MinNeutralChaosRoles;
        public static CustomNumberOption MaxNeutralChaosRoles;
        public static CustomNumberOption MinNeutralKillingRoles;
        public static CustomNumberOption MaxNeutralKillingRoles;
        public static CustomNumberOption MinNeutralNeophyteRoles;
        public static CustomNumberOption MaxNeutralNeophyteRoles;
        public static CustomNumberOption MinNeutralApocalypseRoles;
        public static CustomNumberOption MaxNeutralApocalypseRoles;
        public static CustomNumberOption MinImpostorRoles;
        public static CustomNumberOption MaxImpostorRoles;

        public static CustomHeaderOption AllAnySettings;
        public static CustomToggleOption RandomNumberImps;

        public static CustomHeaderOption KillingOnlySettings;
        public static CustomNumberOption NeutralRoles;
        public static CustomNumberOption VeteranCount;
        public static CustomNumberOption VigilanteCount;
        public static CustomToggleOption AddArsonist;
        public static CustomToggleOption AddPlaguebearer;

        public static CustomHeaderOption CultistSettings;
        public static CustomNumberOption MayorCultistOn;
        public static CustomNumberOption SeerCultistOn;
        public static CustomNumberOption SheriffCultistOn;
        public static CustomNumberOption SurvivorCultistOn;
        public static CustomNumberOption NumberOfSpecialRoles;
        public static CustomNumberOption MaxChameleons;
        public static CustomNumberOption MaxEngineers;
        public static CustomNumberOption MaxInvestigators;
        public static CustomNumberOption MaxMystics;
        public static CustomNumberOption MaxSnitches;
        public static CustomNumberOption MaxSpies;
        public static CustomNumberOption MaxTransporters;
        public static CustomNumberOption MaxVigilantes;
        public static CustomNumberOption WhisperCooldown;
        public static CustomNumberOption IncreasedCooldownPerWhisper;
        public static CustomNumberOption WhisperRadius;
        public static CustomNumberOption ConversionPercentage;
        public static CustomNumberOption DecreasedPercentagePerConversion;
        public static CustomNumberOption ReviveCooldown;
        public static CustomNumberOption IncreasedCooldownPerRevive;
        public static CustomNumberOption MaxReveals;

        public static CustomHeaderOption TaskTrackingSettings;
        public static CustomToggleOption SeeTasksDuringRound;
        public static CustomToggleOption SeeTasksDuringMeeting;
        public static CustomToggleOption SeeTasksWhenDead;

        public static CustomHeaderOption Sheriff;
        public static CustomToggleOption SheriffShootRoundOne;
        public static CustomToggleOption SheriffKillOther;
        public static CustomToggleOption SheriffKillsDoomsayer;
        public static CustomToggleOption SheriffKillsExecutioner;
        public static CustomToggleOption SheriffKillsJester;
        public static CustomToggleOption SheriffKillsChaos;

        public static CustomToggleOption SheriffKillsArsonist;
        public static CustomToggleOption SheriffKillsApocalypse;
        public static CustomToggleOption SheriffKillsGlitch;
        public static CustomToggleOption SheriffKillsWerewolf;

        public static CustomToggleOption SheriffKillsNeophyte;
        public static CustomToggleOption SheriffKillsAlliedCrew;

        public static CustomNumberOption SheriffKillCd;
        public static CustomToggleOption SheriffBodyReport;

        public static CustomHeaderOption Engineer;
        public static CustomNumberOption MaxFixes;

        public static CustomHeaderOption Investigator;
        public static CustomNumberOption FootprintSize;
        public static CustomNumberOption FootprintInterval;
        public static CustomNumberOption FootprintDuration;
        public static CustomToggleOption AnonymousFootPrint;
        public static CustomToggleOption VentFootprintVisible;

        public static CustomHeaderOption Bodyguard;
        public static CustomNumberOption GuardCd;
        public static CustomNumberOption GuardDuration;
        public static CustomNumberOption MaxGuards;

        public static CustomHeaderOption Medic;
        public static CustomStringOption ShowShielded;
        public static CustomStringOption WhoGetsNotification;
        public static CustomToggleOption ShieldBreaks;
        public static CustomToggleOption MedicReportSwitch;
        public static CustomNumberOption MedicReportNameDuration;
        public static CustomNumberOption MedicReportColorDuration;

        public static CustomHeaderOption Seer;
        public static CustomNumberOption SeerCooldown;
        public static CustomToggleOption CrewKillingRed;
        public static CustomToggleOption NeutBenignRed;
        public static CustomToggleOption NeutEvilRed;
        public static CustomToggleOption NeutChaosRed;
        public static CustomToggleOption NeutKillingRed;
        public static CustomToggleOption NeutNeophyteRed;
        public static CustomToggleOption NeutApocalypseRed;
        public static CustomToggleOption TraitorColourSwap;

        public static CustomHeaderOption Spy;
        public static CustomStringOption WhoSeesDead;

        public static CustomHeaderOption Swapper;
        public static CustomToggleOption SwapperButton;

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

        public static CustomHeaderOption Berserker;
        public static CustomNumberOption JuggKillCooldown;
        public static CustomNumberOption ReducedKCdPerKill;
        public static CustomToggleOption JuggVent;

        public static CustomHeaderOption Morphling;
        public static CustomNumberOption MorphlingCooldown;
        public static CustomNumberOption MorphlingDuration;
        public static CustomToggleOption MorphlingVent;

        public static CustomHeaderOption Executioner;
        public static CustomStringOption OnTargetDead;
        public static CustomToggleOption ExecutionerButton;
        public static CustomToggleOption ExecutionerTorment;
        
        public static CustomHeaderOption Joker;
        public static CustomStringOption JokerOnTargetDead;
        public static CustomToggleOption JokerButton;

        public static CustomHeaderOption Phantom;
        public static CustomNumberOption PhantomTasksRemaining;
        public static CustomToggleOption PhantomSpook;

        public static CustomHeaderOption Snitch;
        public static CustomToggleOption SnitchSeesNeutrals;
        public static CustomNumberOption SnitchTasksRemaining;
        public static CustomToggleOption SnitchSeesImpInMeeting;
        public static CustomToggleOption SnitchSeesTraitor;

        public static CustomHeaderOption Taskmaster;
        public static CustomToggleOption TMSeesNeutrals;
        public static CustomNumberOption TMTasksRemaining;
        public static CustomNumberOption TMShortTasks;
        public static CustomNumberOption TMLongTasks;
        public static CustomNumberOption TMCommonTasks;

        public static CustomHeaderOption Altruist;
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
        public static CustomToggleOption AssassinGuessNeutralBenign;
        public static CustomToggleOption AssassinGuessCrewInvestigative;
        public static CustomToggleOption AssassinGuessNeutralEvil;
        public static CustomToggleOption AssassinGuessNeutralChaos;
        public static CustomToggleOption AssassinGuessNeutralKilling;
        public static CustomToggleOption AssassinGuessNeutralNeophyte;
        public static CustomToggleOption AssassinGuessNeutralApocalypse;
        public static CustomToggleOption AssassinGuessImpostors;
        public static CustomToggleOption AssassinGuessModifiers;
        public static CustomToggleOption AssassinGuessLovers;
        public static CustomToggleOption AssassinGuessRecruits;
        public static CustomToggleOption AssassinGuessEvilCrew;
        public static CustomToggleOption AssassinateAfterVoting;

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
        public static CustomToggleOption VigilanteGuessLovers;
        public static CustomToggleOption VigilanteGuessRecruits;
        public static CustomToggleOption VigilanteGuessEvilCrew;
        public static CustomToggleOption VigilanteAfterVoting;

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

        public static CustomHeaderOption Survivor;
        public static CustomNumberOption VestCd;
        public static CustomNumberOption VestDuration;
        public static CustomNumberOption VestKCReset;
        public static CustomNumberOption MaxVests;

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

        public static CustomHeaderOption Mystic;
        public static CustomNumberOption MysticArrowDuration;

        public static CustomHeaderOption Blackmailer;
        public static CustomNumberOption BlackmailCooldown;
        public static CustomToggleOption CanSeeBlackmailed;

        public static CustomHeaderOption Plaguebearer;
        public static CustomNumberOption InfectCooldown;
        public static CustomNumberOption PestKillCooldown;
        public static CustomToggleOption PestVent;

        public static CustomHeaderOption Sentinel;
        public static CustomNumberOption SentinelKillCooldown;
        public static CustomNumberOption SentinelSoloPercent;
        public static CustomNumberOption SentinelDuration;
        public static CustomNumberOption SentinelChargeCooldown;
        public static CustomNumberOption MaxKillsInCharge;
        public static CustomNumberOption ChargeDelay;
        public static CustomNumberOption ChargeRadius;
        public static CustomNumberOption MaxChargeUses;
        public static CustomNumberOption SentinelPlaceCooldown;
        public static CustomNumberOption MaxKillsInPlaced;
        public static CustomNumberOption PlaceRadius;
        public static CustomNumberOption MaxPlaceUses;
        public static CustomNumberOption SentinelStunCooldown;
        public static CustomNumberOption StunDelay;
        public static CustomNumberOption StunDuration;
        public static CustomNumberOption MaxStunUses;
        public static CustomToggleOption StunInverts;
        public static CustomToggleOption SentinelVent;

        public static CustomHeaderOption Werewolf;
        public static CustomNumberOption RampageCooldown;
        public static CustomNumberOption RampageDuration;
        public static CustomNumberOption RampageKillCooldown;
        public static CustomToggleOption WerewolfVent;

        public static CustomHeaderOption Detective;
        public static CustomNumberOption ExamineCooldown;
        public static CustomToggleOption DetectiveReportOn;
        public static CustomNumberOption DetectiveRoleDuration;
        public static CustomNumberOption DetectiveFactionDuration;
        public static CustomToggleOption CanDetectLastKiller;

        public static CustomHeaderOption Escapist;
        public static CustomNumberOption EscapeCooldown;
        public static CustomToggleOption EscapistVent;

        public static CustomHeaderOption Bomber;
        public static CustomNumberOption MaxKillsInDetonation;
        public static CustomNumberOption DetonateDelay;
        public static CustomNumberOption DetonateRadius;
        public static CustomToggleOption BomberVent;

        public static CustomHeaderOption Poisoner;
        public static CustomNumberOption PoisonDuration;
        public static CustomToggleOption PoisonerVent;

        public static CustomHeaderOption Cannibal;
        public static CustomToggleOption CannibalArrows;
        public static CustomNumberOption CannibalArrowDelay;
        public static CustomNumberOption BodiesNeededToWin;

        public static CustomHeaderOption CursedSoul;
        public static CustomNumberOption SoulSwapCooldown;
        public static CustomNumberOption SoulSwapGuarantee;

        public static CustomHeaderOption Inquisitor;
        public static CustomNumberOption InquireCooldown;
        public static CustomToggleOption VanquishEnabled;
        public static CustomNumberOption VanquishCooldown;
        public static CustomToggleOption VanquishRoundOne;

        public static CustomHeaderOption Tyrant;
        public static CustomNumberOption TyrantVoteBank;
        public static CustomToggleOption TyrantAnonymous;

        public static CustomHeaderOption Doomsayer;
        public static CustomNumberOption ObserveCooldown;
        public static CustomToggleOption DoomsayerGuessNeutralBenign;
        public static CustomToggleOption DoomsayerGuessNeutralEvil;
        public static CustomToggleOption DoomsayerGuessNeutralChaos;
        public static CustomToggleOption DoomsayerGuessNeutralKilling;
        public static CustomToggleOption DoomsayerGuessNeutralNeophyte;
        public static CustomToggleOption DoomsayerGuessNeutralApocalypse;
        public static CustomToggleOption DoomsayerGuessImpostors;
        public static CustomToggleOption DoomsayerAfterVoting;
        public static CustomNumberOption DoomsayerGuessesToWin;
        public static CustomToggleOption DoomsayerCantObserve;

        public static CustomHeaderOption Jackal;
        public static CustomNumberOption JackalKillCooldown;
        public static CustomToggleOption DoJackalRecruitsDie;
        public static CustomToggleOption JackalCanAlwaysKill;

        public static CustomHeaderOption NeoNecromancer;
        public static CustomNumberOption NecroKillCooldown;
        public static CustomNumberOption NecroResurrectCooldown;
        public static CustomNumberOption NecroIncreasedCooldownPerResurrect;
        public static CustomNumberOption AppaResurrectCooldown;
        public static CustomNumberOption AppaIncreasedCooldownPerResurrect;
        public static CustomNumberOption ScourgeKillCooldown;
        public static CustomToggleOption CanHuskVent;
        public static CustomToggleOption CanHuskAssassinate;
        public static CustomNumberOption HuskAssassinKills;
        public static CustomToggleOption HuskAssassinMultiKill;

        public static CustomHeaderOption Vampire;
        public static CustomNumberOption BiteCooldown;
        public static CustomNumberOption BiteDuration;
        public static CustomToggleOption VampImpVision;
        public static CustomToggleOption VampVent;
        public static CustomToggleOption RememberedVampireStaysVamp;
        public static CustomToggleOption NewVampCanAssassin;
        public static CustomNumberOption MaxVampiresPerGame;
        public static CustomToggleOption CanBiteNeutralBenign;
        public static CustomToggleOption CanBiteNeutralEvil;
        public static CustomToggleOption CanBiteNeutralChaos;

        public static CustomHeaderOption VampireHunter;
        public static CustomNumberOption StakeCooldown;
        public static CustomNumberOption MaxFailedStakesPerGame;
        public static CustomToggleOption CanStakeRoundOne;
        public static CustomToggleOption SelfKillAfterFinalStake;
        public static CustomStringOption BecomeOnVampDeaths;

        public static CustomHeaderOption Trickster;
        public static CustomNumberOption TrickCooldown;
        public static CustomNumberOption MaxFailedTricksPerGame;
        public static CustomToggleOption CanTrickRoundOne;
        public static CustomToggleOption SelfKillAfterFinalTrick;

        public static CustomHeaderOption Hunter;
        public static CustomNumberOption HunterKillCd;
        public static CustomNumberOption HunterStalkCd;
        public static CustomNumberOption HunterStalkDuration;
        public static CustomNumberOption HunterStalkUses;
        public static CustomToggleOption HunterBodyReport;
        public static CustomHeaderOption Prosecutor;
        public static CustomToggleOption ProsDiesOnIncorrectPros;

        public static CustomHeaderOption Warlock;
        public static CustomNumberOption ChargeUpDuration;
        public static CustomNumberOption ChargeUseDuration;

        public static CustomHeaderOption Oracle;
        public static CustomNumberOption ConfessCooldown;
        public static CustomNumberOption RevealAccuracy;
        public static CustomToggleOption NeutralBenignShowsEvil;
        public static CustomToggleOption NeutralEvilShowsEvil;
        public static CustomToggleOption NeutralChaosShowsEvil;
        public static CustomToggleOption NeutralKillingShowsEvil;
        public static CustomToggleOption NeutralNeophyteShowsEvil;
        public static CustomToggleOption NeutralApocalypseShowsEvil;

        public static CustomHeaderOption Venerer;
        public static CustomNumberOption AbilityCooldown;
        public static CustomNumberOption AbilityDuration;
        public static CustomNumberOption SprintSpeed;
        public static CustomNumberOption FreezeSpeed;

        public static CustomHeaderOption Aurial;
        public static CustomNumberOption RadiateRange;
        public static CustomNumberOption RadiateCooldown;
        public static CustomNumberOption RadiateSucceedChance;
        public static CustomNumberOption RadiateCount;
        public static CustomNumberOption RadiateInvis;

        public static CustomHeaderOption Giant;
        public static CustomNumberOption GiantSlow;

        public static CustomHeaderOption Dwarf;
        public static CustomNumberOption DwarfSpeed;

        public static CustomHeaderOption Diseased;
        public static CustomNumberOption DiseasedKillMultiplier;

        public static CustomHeaderOption Eclipsed;
        public static CustomNumberOption EclipsedWithLights;
        public static CustomNumberOption EclipsedWithoutLights;

        public static CustomHeaderOption Bait;
        public static CustomNumberOption BaitMinDelay;
        public static CustomNumberOption BaitMaxDelay;
        public static CustomHeaderOption Oblivious;
        public static CustomToggleOption ObliviousCanReport;

        public static CustomHeaderOption Frosty;
        public static CustomNumberOption ChillDuration;
        public static CustomNumberOption ChillStartSpeed;

        public static Func<object, string> PercentFormat { get; } = value => $"{value:0}%";

        private static Func<object, string> CooldownFormat { get; } = value => $"{value:0.0#}s";
        private static Func<object, string> MultiplierFormat { get; } = value => $"{value:0.0#}x";


        public static void GenerateAll()
        {
            var num = 0;

            Patches.ExportButton = new Export(num++);
            Patches.ImportButton = new Import(num++);

            MapSettings = new CustomHeaderOption(num++, MultiMenu.map, "Map Chance Config");
            RandomMapEnabled = new CustomToggleOption(num++, MultiMenu.map, "Choose Random Map", false);
            RandomMapSkeld = new CustomNumberOption(num++, MultiMenu.map, "Skeld Chance", 0f, 0f, 100f, 10f, PercentFormat);
            RandomMapdlekS = new CustomNumberOption(num++, MultiMenu.map, "dlekS Chance", 0f, 0f, 100f, 10f, PercentFormat);
            RandomMapMira = new CustomNumberOption(num++, MultiMenu.map, "Mira Chance", 0f, 0f, 100f, 10f, PercentFormat);
            RandomMapPolus = new CustomNumberOption(num++, MultiMenu.map, "Polus Chance", 0f, 0f, 100f, 10f, PercentFormat);
            RandomMapAirship = new CustomNumberOption(num++, MultiMenu.map, "Airship Chance", 0f, 0f, 100f, 10f, PercentFormat);
            RandomMapFungle = new CustomNumberOption(num++, MultiMenu.map, "Fungle Chance", 0f, 0f, 100f, 10f, PercentFormat);
            RandomMapSubmerged = new CustomNumberOption(num++, MultiMenu.map, "Submerged Chance", 0f, 0f, 100f, 10f, PercentFormat);
            RandomMapLevelImp = new CustomNumberOption(num++, MultiMenu.map, "Random Level Imposter Map Chance", 0f, 0f, 200f, 20f, PercentFormat);
            MapAdjust = new CustomHeaderOption(num++, MultiMenu.map, "Map Adjustments");
            RandomVentSpawn = new CustomNumberOption(num++, MultiMenu.map, "Random Vent Spawn Chance", 0f, 0f, 100f, 5f, PercentFormat);
            AutoAdjustSettings = new CustomToggleOption(num++, MultiMenu.map, "Auto Adjust Settings", false);
            SmallMapHalfVision = new CustomToggleOption(num++, MultiMenu.map, "Half Vision On Skeld/Mira HQ", false);
            SmallMapDecreasedCooldown =
                new CustomNumberOption(num++, MultiMenu.map, "Mira HQ Decreased Cooldowns", 0f, 0f, 15f, 2.5f, CooldownFormat);
            LargeMapIncreasedCooldown =
                new CustomNumberOption(num++, MultiMenu.map, "Airship/Submerged Increased Cooldowns", 0f, 0f, 15f, 2.5f, CooldownFormat);
            SmallMapIncreasedShortTasks =
                 new CustomNumberOption(num++, MultiMenu.map, "Skeld/Mira HQ Increased Short Tasks", 0, 0, 5, 1);
            SmallMapIncreasedLongTasks =
                 new CustomNumberOption(num++, MultiMenu.map, "Skeld/Mira HQ Increased Long Tasks", 0, 0, 3, 1);
            LargeMapDecreasedShortTasks =
                 new CustomNumberOption(num++, MultiMenu.map, "Airship/Submerged Decreased Short Tasks", 0, 0, 5, 1);
            LargeMapDecreasedLongTasks =
                 new CustomNumberOption(num++, MultiMenu.map, "Airship/Submerged Decreased Long Tasks", 0, 0, 3, 1);

            BetterSkeldSettings =
                new CustomHeaderOption(num++, MultiMenu.map, "Better Skeld Settings");
            BetterSkeldEnabled = new CustomToggleOption(num++, MultiMenu.map, "Enable Better Skeld", false);
            BSVentImprovements = new CustomToggleOption(num++, MultiMenu.map, "Better Skeld Vent Layout");

            BetterMiraSettings =
                new CustomHeaderOption(num++, MultiMenu.map, "Better Mira HQ Settings");
            BetterMiraEnabled = new CustomToggleOption(num++, MultiMenu.map, "Enable Better Mira HQ", false);
            BMVentImprovements = new CustomToggleOption(num++, MultiMenu.map, "Better Mira HQ Vent Layout");

            BetterPolusSettings =
                new CustomHeaderOption(num++, MultiMenu.map, "Better Polus Settings");
            BetterPolusEnabled = new CustomToggleOption(num++, MultiMenu.map, "Enable Better Polus", false);
            BPVentImprovements = new CustomToggleOption(num++, MultiMenu.map, "Better Polus Vent Layout", false);
            BPVitalsLab = new CustomToggleOption(num++, MultiMenu.map, "Vitals Moved To Lab", false);
            BPColdTempDeathValley = new CustomToggleOption(num++, MultiMenu.map, "Cold Temp Moved To Death Valley", false);
            BPWifiChartCourseSwap =
                new CustomToggleOption(num++, MultiMenu.map, "Reboot Wifi And Chart Course Swapped", false);

            BetterAirshipSettings =
                new CustomHeaderOption(num++, MultiMenu.map, "Better Airship Settings");
            BetterAirshipEnabled = new CustomToggleOption(num++, MultiMenu.map, "Enable Better Airship", false);
            BAMoveAdmin =
                 new CustomStringOption(num++, MultiMenu.map, "Move Admin Table", new[] { "Don't Move", "Cockpit", "Main Hall" });
            BAMoveElectrical =
                 new CustomStringOption(num++, MultiMenu.map, "Move Electrical Outlet", new[] { "Don't Move", "Vault", "Electrical" });
            BAMoveVitals = new CustomToggleOption(num++, MultiMenu.map, "Move Airship Vitals", false);
            BAMoveFuel = new CustomToggleOption(num++, MultiMenu.map, "Move Airship Fuel", false);
            BAMoveDivert = new CustomToggleOption(num++, MultiMenu.map, "Move Divert Power", false);

            CrewAstralRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Astral Roles");
            AurialOn = new CustomNumberOption(num++, MultiMenu.crewmate, $"<color=#B34D99FF>Aurial</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            HaunterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#D3D3D3FF>Haunter</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MediumOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#A680FFFF>Medium</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MysticOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#4D99E6FF>Mystic</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            OracleOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#BF00BFFF>Oracle</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            CrewInvestigativeRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Investigative Roles");
            DetectiveOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#4D4DFFFF>Detective</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            InvestigatorOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#00B3B3FF>Investigator</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SeerOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#FFCC80FF>Seer</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SnitchOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#D4AF37FF>Snitch</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SpyOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#CCA3CCFF>Spy</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TrackerOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#009900FF>Tracker</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TrapperOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#A7D1B3FF>Trapper</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            CrewKillingRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Killing Roles");
            HunterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#29AB87FF>Hunter</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SheriffOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#FFFF00FF>Sheriff</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TricksterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#EAA299FF>Trickster</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            VampireHunterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#B3B3E6FF>Vampire Hunter</color>", 0f, 0f, 100f, 10f,
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

            CrewSupportRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Sovereign Roles");
            CaptainOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#D2F3FFFF>Captain (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MayorOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#704FA8FF>Mayor</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MonarchOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#DC9E3FFF>Monarch (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            ProsecutorOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#B38000FF>Prosecutor</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SwapperOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#66E666FF>Swapper</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            CrewSupportRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Support Roles");
            BartenderOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#770169FF>Bartender (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            EngineerOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#FFA60AFF>Engineer</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            ImitatorOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#B3D94DFF>Imitator</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MageOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#6064D8FF>Mage (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TransporterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#00EEFFFF>Transporter</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TaskmasterOn = new CustomNumberOption(num++, MultiMenu.crewmate, "<color=#C3FFFFFF>Taskmaster</color>", 0f, 0f, 100f, 10f,
                PercentFormat);


            NeutralBenignRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Benign Roles");
            AmnesiacOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#80B2FFFF>Amnesiac</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            GuardianAngelOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#B3FFFFFF>Guardian Angel</color>", 0f, 0f, 100f, 10f,
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
            PirateOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#DBAF57FF>Pirate (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            NeutralChaosRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Chaos Roles");
            CursedSoulOn = new CustomNumberOption(num++, MultiMenu.neutral, Utils.GradientColorText("79FFB3", "B579FF", "Cursed Soul"), 0f, 0f, 100f, 10f,
                PercentFormat);
            InquisitorOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#DA4291FF>Inquisitor</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            JokerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#C0FF85FF>Joker (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TempestOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#A2A6F0FF>Tempest (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TyrantOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#EA535BFF>Tyrant</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            CannibalOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#8C4005FF>Cannibal</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            WitchOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#5514E1FF>Witch (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            NeutralKillingRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Killing Roles");
            ArsonistOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#FF4D00FF>Arsonist</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            GhoulOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#4EA4EBFF>Ghoul (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            GlitchOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#00FF00FF>The Glitch</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SentinelOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#8FA28DFF>The Sentinel</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SerialKillerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#002AFFFF>Serial Killer (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            WerewolfOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#A86629FF>Werewolf</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            NeutralNeophyteRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Neophyte Roles");
            JackalOn = new CustomNumberOption(num++, MultiMenu.neutral, Utils.GradientColorText("B7B9BA", "5E576B", "Jackal"), 0f, 0f, 100f, 10f,
                PercentFormat);
            NeoNecromancerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#E22759FF>Necromancer</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            VampireOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#262626FF>Vampire</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            NeutralApocalypseRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Apocalypse Roles");
            BakerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#FFE8B3FF>Baker (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            BerserkerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#FFE8B3FF>Berserker</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            PlaguebearerOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#FFE8B3FF>Plaguebearer</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SoulCollectorOn = new CustomNumberOption(num++, MultiMenu.neutral, "<color=#FFE8B3FF>Soul Collector (WIP)</color>", 0f, 0f, 100f, 10f,
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
            JanitorOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Janitor</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MinerOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Miner</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            UndertakerOn = new CustomNumberOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Undertaker</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            CrewmateModifiers = new CustomHeaderOption(num++, MultiMenu.modifiers, "Crewmate Modifiers");
            AftermathOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#A6FFA6FF>Aftermath</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            BaitOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#00B3B3FF>Bait</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            DiseasedOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#808080FF>Diseased</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            EclipsedOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#D98E70>Eclipsed</color>", 0f, 0f, 100f, 10f,
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
            DwarfOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF8080FF>Dwarf</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            GiantOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FFB34DFF>Giant</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            NumbOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#662529FF>Numb (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            NinjaOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#304A3EFF>Ninja (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            ObliviousOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#AAAAAAFF>Oblivious</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            RadarOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0080FF>Radar</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SleuthOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#803333FF>Sleuth</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TiebreakerOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#99E699FF>Tiebreaker</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TrollOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#00571AFF>Troll (WIP)</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            ImpostorModifiers = new CustomHeaderOption(num++, MultiMenu.modifiers, "Impostor Modifiers");
            DisperserOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Disperser</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            DoubleShotOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Double Shot</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            UnderdogOn = new CustomNumberOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Underdog</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            CrewmateAlliances = new CustomHeaderOption(num++, MultiMenu.alliance, "Crewmate Alliances");
            CrewpostorOn = new CustomNumberOption(num++, MultiMenu.alliance, "<color=#FF0000FF>Crewpostor</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            CrewpocalypseOn = new CustomNumberOption(num++, MultiMenu.alliance, "<color=#FFE8B3FF>Crewpocalypse</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            EgotistOn = new CustomNumberOption(num++, MultiMenu.alliance, "<color=#3A6041FF>Egotist</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            GlobalAlliances = new CustomHeaderOption(num++, MultiMenu.alliance, "Global Alliances");
            LoversOn = new CustomNumberOption(num++, MultiMenu.alliance, "<color=#FF66CCFF>Lovers</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            Lovers =
                new CustomHeaderOption(num++, MultiMenu.alliance, "<color=#FF66CCFF>Lovers</color>");
            BothLoversDie = new CustomToggleOption(num++, MultiMenu.alliance, "Both Lovers Die");
            LovingImpPercent = new CustomNumberOption(num++, MultiMenu.alliance, "Loving Impostor Probability", 20f, 0f, 100f, 10f,
                PercentFormat);
            NeutralLovers = new CustomToggleOption(num++, MultiMenu.alliance, "Neutral Roles Can Be Lovers");

            GameModeSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Game Mode Settings");
            GameMode = new CustomStringOption(num++, MultiMenu.main, "Game Mode", new[] {"Classic", "All Any", "Killing Only", "Cultist" });

            ClassicSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Classic Game Mode Settings");
            MinNeutralBenignRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Min Neutral Benign Roles", 1, 0, 3, 1);
            MaxNeutralBenignRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Max Neutral Benign Roles", 1, 0, 3, 1);
            MinNeutralEvilRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Min Neutral Evil Roles", 1, 0, 3, 1);
            MaxNeutralEvilRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Max Neutral Evil Roles", 1, 0, 3, 1);
            MinNeutralChaosRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Min Neutral Chaos Roles", 1, 0, 3, 1);
            MaxNeutralChaosRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Max Neutral Chaos Roles", 1, 0, 3, 1);
            MinNeutralKillingRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Min Neutral Killing Roles", 1, 0, 5, 1);
            MaxNeutralKillingRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Max Neutral Killing Roles", 1, 0, 5, 1);
            MinNeutralNeophyteRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Min Neutral Neophyte Roles", 1, 0, 5, 1);
            MaxNeutralNeophyteRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Max Neutral Neophyte Roles", 1, 0, 5, 1);
            MinNeutralApocalypseRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Min Neutral Apocalypse Roles", 1, 0, 5, 1);
            MaxNeutralApocalypseRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Max Neutral Apocalypse Roles", 1, 0, 5, 1);
            MinImpostorRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Min Impostor Roles", 1, 0, 3, 1);
            MaxImpostorRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Max Impostor Roles", 1, 0, 3, 1);

            AllAnySettings =
                new CustomHeaderOption(num++, MultiMenu.main, "All Any Settings");
            RandomNumberImps = new CustomToggleOption(num++, MultiMenu.main, "Random Number Of Impostors", true);

            KillingOnlySettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Killing Only Settings");
            NeutralRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Neutral Roles", 1, 0, 5, 1);
            VeteranCount =
                new CustomNumberOption(num++, MultiMenu.main, "Veteran Count", 1, 0, 5, 1);
            VigilanteCount =
                new CustomNumberOption(num++, MultiMenu.main, "Vigilante Count", 1, 0, 5, 1);
            AddArsonist = new CustomToggleOption(num++, MultiMenu.main, "Add Arsonist", true);
            AddPlaguebearer = new CustomToggleOption(num++, MultiMenu.main, "Add Plaguebearer", true);

            CultistSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Cultist Settings");
            MayorCultistOn = new CustomNumberOption(num++, MultiMenu.main, "<color=#704FA8FF>Mayor</color> (Cultist Mode)", 100f, 0f, 100f, 10f,
                PercentFormat);
            SeerCultistOn = new CustomNumberOption(num++, MultiMenu.main, "<color=#FFCC80FF>Seer</color> (Cultist Mode)", 100f, 0f, 100f, 10f,
                PercentFormat);
            SheriffCultistOn = new CustomNumberOption(num++, MultiMenu.main, "<color=#FFFF00FF>Sheriff</color> (Cultist Mode)", 100f, 0f, 100f, 10f,
                PercentFormat);
            SurvivorCultistOn = new CustomNumberOption(num++, MultiMenu.main, "<color=#FFE64DFF>Survivor</color> (Cultist Mode)", 100f, 0f, 100f, 10f,
                PercentFormat);
            NumberOfSpecialRoles =
                new CustomNumberOption(num++, MultiMenu.main, "Number Of Special Roles", 4, 0, 4, 1);
            MaxChameleons =
                new CustomNumberOption(num++, MultiMenu.main, "Max Chameleons", 3, 0, 5, 1);
            MaxEngineers =
                new CustomNumberOption(num++, MultiMenu.main, "Max Engineers", 3, 0, 5, 1);
            MaxInvestigators =
                new CustomNumberOption(num++, MultiMenu.main, "Max Investigators", 3, 0, 5, 1);
            MaxMystics =
                new CustomNumberOption(num++, MultiMenu.main, "Max Mystics", 3, 0, 5, 1);
            MaxSnitches =
                new CustomNumberOption(num++, MultiMenu.main, "Max Snitches", 3, 0, 5, 1);
            MaxSpies =
                new CustomNumberOption(num++, MultiMenu.main, "Max Spies", 3, 0, 5, 1);
            MaxTransporters =
                new CustomNumberOption(num++, MultiMenu.main, "Max Transporters", 3, 0, 5, 1);
            MaxVigilantes =
                new CustomNumberOption(num++, MultiMenu.main, "Max Vigilantes", 3, 0, 5, 1);
            WhisperCooldown =
                new CustomNumberOption(num++, MultiMenu.main, "Initial Whisper Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            IncreasedCooldownPerWhisper =
                new CustomNumberOption(num++, MultiMenu.main, "Increased Cooldown Per Whisper", 5f, 0f, 15f, 0.5f, CooldownFormat);
            WhisperRadius =
                new CustomNumberOption(num++, MultiMenu.main, "Whisper Radius", 1f, 0.25f, 5f, 0.25f, MultiplierFormat);
            ConversionPercentage = new CustomNumberOption(num++, MultiMenu.main, "Conversion Percentage", 25f, 0f, 100f, 5f,
                PercentFormat);
            DecreasedPercentagePerConversion = new CustomNumberOption(num++, MultiMenu.main, "Decreased Conversion Percentage Per Conversion", 5f, 0f, 15f, 1f,
                PercentFormat);
            ReviveCooldown =
                new CustomNumberOption(num++, MultiMenu.main, "Initial Revive Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            IncreasedCooldownPerRevive =
                new CustomNumberOption(num++, MultiMenu.main, "Increased Cooldown Per Revive", 25f, 10f, 60f, 2.5f, CooldownFormat);
            MaxReveals = new CustomNumberOption(num++, MultiMenu.main, "Maximum Number Of Reveals", 5, 1, 15, 1);

            CustomGameSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Custom Game Settings");
            ColourblindComms = new CustomToggleOption(num++, MultiMenu.main, "Camouflaged Comms", false);
            ImpostorSeeRoles = new CustomToggleOption(num++, MultiMenu.main, "Impostors Can See The Roles Of Their Team", false);
            DeadSeeRoles =
                new CustomToggleOption(num++, MultiMenu.main, "Dead Can See Everyone's Roles/Votes", false);
            InitialCooldowns =
                new CustomNumberOption(num++, MultiMenu.main, "Game Start Cooldowns", 10f, 10f, 30f, 2.5f, CooldownFormat);
            ButtonBarryGetsCooldown = new CustomToggleOption(num++, MultiMenu.main, "Button Barry Has Initial Cooldown", false);
            ParallelMedScans = new CustomToggleOption(num++, MultiMenu.main, "Parallel Medbay Scans", false);
            SkipButtonDisable = new CustomStringOption(num++, MultiMenu.main, "Disable Meeting Skip Button", new[] { "No", "Emergency", "Always" });
            FirstDeathShield = new CustomToggleOption(num++, MultiMenu.main, "First Death Shield Next Game", false);
            NeutralEvilWinEndsGame = new CustomToggleOption(num++, MultiMenu.main, "Neutral Evil Win Ends Game", true);
            GhostsDoTasks = new CustomToggleOption(num++, MultiMenu.main, "Ghosts Do Tasks", true);
            //EnableAprilFoolsMode = new CustomToggleOption(num++, MultiMenu.main, "Enable April Fools Mode", false);

            TaskTrackingSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Task Tracking Settings");
            SeeTasksDuringRound = new CustomToggleOption(num++, MultiMenu.main, "See Tasks During Round", false);
            SeeTasksDuringMeeting = new CustomToggleOption(num++, MultiMenu.main, "See Tasks During Meetings", false);
            SeeTasksWhenDead = new CustomToggleOption(num++, MultiMenu.main, "See Tasks When Dead", true);

            Assassin = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#FF0000FF>Assassin Ability</color>");
            NumberOfImpostorAssassins = new CustomNumberOption(num++, MultiMenu.imposter, "Number Of Impostor Assassins", 1, 0, 4, 1);
            NumberOfNeutralAssassins = new CustomNumberOption(num++, MultiMenu.imposter, "Number Of Neutral Assassins", 1, 0, 5, 1);
            AmneTurnImpAssassin = new CustomToggleOption(num++, MultiMenu.imposter, "Amnesiac Turned Impostor Gets Ability", false);
            AmneTurnNeutAssassin = new CustomToggleOption(num++, MultiMenu.imposter, "Amnesiac Turned Neutral Killing Gets Ability", false);
            TraitorCanAssassin = new CustomToggleOption(num++, MultiMenu.imposter, "Traitor Gets Ability", false);
            AssassinKills = new CustomNumberOption(num++, MultiMenu.imposter, "Number Of Assassin Kills", 1, 1, 15, 1);
            AssassinMultiKill = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Kill More Than Once Per Meeting", false);
            AssassinCrewmateGuess = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess \"Crewmate\"", false);
            AssassinGuessCrewInvestigative = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Crew Investigative", false);
            AssassinGuessNeutralBenign = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Benign Roles", false);
            AssassinGuessNeutralEvil = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Evil Roles", false);
            AssassinGuessNeutralChaos = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Chaos Roles", false);
            AssassinGuessNeutralKilling = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Killing Roles", false);
            AssassinGuessNeutralNeophyte = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Neophyte Roles", false);
            AssassinGuessNeutralApocalypse = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Neutral Apocalypse Roles", false);
            AssassinGuessImpostors = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Impostor Roles", false);
            AssassinGuessModifiers = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Crewmate Modifiers", false);
            AssassinGuessLovers = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Lovers", false);
            AssassinGuessRecruits = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Recruits", false);
            AssassinGuessEvilCrew = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess Allied Crew", false);
            AssassinateAfterVoting = new CustomToggleOption(num++, MultiMenu.imposter, "Assassin Can Guess After Voting", false);

            Aurial =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#B34D99FF>Aurial</color>");
            RadiateRange =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Radiate Range", 1f, 0.25f, 5f, 0.25f, MultiplierFormat);
            RadiateCooldown =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Radiate Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            RadiateInvis =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Radiate See Delay", 10f, 0f, 15f, 1f, CooldownFormat);
            RadiateCount =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Radiate Uses To See", 3, 1, 5, 1);
            RadiateSucceedChance =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Radiate Succeed Chance", 100f, 0f, 100f, 10f, PercentFormat);

            Detective =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#4D4DFFFF>Detective</color>");
            ExamineCooldown =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Examine Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            DetectiveReportOn = new CustomToggleOption(num++, MultiMenu.crewmate, "Show Detective Reports", true);
            DetectiveRoleDuration =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Time Where Detective Will Have Role", 15f, 0f, 60f, 2.5f,
                    CooldownFormat);
            DetectiveFactionDuration =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Time Where Detective Will Have Faction", 30f, 0f, 60f, 2.5f,
                    CooldownFormat);
            CanDetectLastKiller = new CustomToggleOption(num++, MultiMenu.crewmate, "Can Detect Last Killer", false);

            Haunter =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#d3d3d3FF>Haunter</color>");
            HaunterTasksRemainingClicked =
                 new CustomNumberOption(num++, MultiMenu.crewmate, "Tasks Remaining When Haunter Can Be Clicked", 5, 1, 15, 1);
            HaunterTasksRemainingAlert =
                 new CustomNumberOption(num++, MultiMenu.crewmate, "Tasks Remaining When Alert Is Sent", 1, 1, 5, 1);
            HaunterRevealsNeutrals = new CustomToggleOption(num++, MultiMenu.crewmate, "Haunter Reveals Neutral Roles", false);
            HaunterCanBeClickedBy = new CustomStringOption(num++, MultiMenu.crewmate, "Who Can Click Haunter", new[] { "All", "Non-Crew", "Imps Only" });

            Investigator =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#00B3B3FF>Investigator</color>");
            FootprintSize = new CustomNumberOption(num++, MultiMenu.crewmate, "Footprint Size", 4f, 1f, 10f, 1f);
            FootprintInterval =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Footprint Interval", 0.1f, 0.05f, 1f, 0.05f, CooldownFormat);
            FootprintDuration = new CustomNumberOption(num++, MultiMenu.crewmate, "Footprint Duration", 10f, 1f, 15f, 0.5f, CooldownFormat);
            AnonymousFootPrint = new CustomToggleOption(num++, MultiMenu.crewmate, "Anonymous Footprint", false);
            VentFootprintVisible = new CustomToggleOption(num++, MultiMenu.crewmate, "Footprint Vent Visible", false);

            Mystic =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#4D99E6FF>Mystic</color>");
            MysticArrowDuration =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Dead Body Arrow Duration", 0.1f, 0f, 1f, 0.05f, CooldownFormat);

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
            NeutralChaosShowsEvil =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Chaos Roles Show Evil", false);
            NeutralKillingShowsEvil =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Killing Roles Show Evil", true);
            NeutralNeophyteShowsEvil =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Neophyte Roles Show Evil", true);
            NeutralApocalypseShowsEvil =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Apocalypse Roles Show Evil", true);

            Seer =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#FFCC80FF>Seer</color>");
            SeerCooldown =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Seer Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            CrewKillingRed =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Crewmate Killing Roles Are Red", false);
            NeutBenignRed =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Benign Roles Are Red", false);
            NeutEvilRed =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Evil Roles Are Red", false);
            NeutChaosRed =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Chaos Roles Are Red", false);
            NeutKillingRed =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Killing Roles Are Red", true);
            NeutNeophyteRed =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Neophyte Roles Are Red", true);
            NeutApocalypseRed =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Neutral Apocalypse Roles Are Red", true);
            TraitorColourSwap =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Traitor Does Not Swap Colours", false);

            Snitch = new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#D4AF37FF>Snitch</color>");
            SnitchSeesNeutrals = new CustomToggleOption(num++, MultiMenu.crewmate, "Snitch Sees Neutral Roles", false);
            SnitchTasksRemaining =
                 new CustomNumberOption(num++, MultiMenu.crewmate, "Tasks Remaining When Revealed", 1, 1, 5, 1);
            SnitchSeesImpInMeeting = new CustomToggleOption(num++, MultiMenu.crewmate, "Snitch Sees Impostors In Meetings", true);
            SnitchSeesTraitor = new CustomToggleOption(num++, MultiMenu.crewmate, "Snitch Sees Traitor", true);

            Spy =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#CCA3CCFF>Spy</color>");
            WhoSeesDead = new CustomStringOption(num++, MultiMenu.crewmate, "Who Sees Dead Bodies On Admin",
                new[] { "Nobody", "Spy", "Everyone But Spy", "Everyone" });

            Tracker =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#009900FF>Tracker</color>");
            UpdateInterval =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Arrow Update Interval", 5f, 0.5f, 15f, 0.5f, CooldownFormat);
            TrackCooldown =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Track Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            ResetOnNewRound = new CustomToggleOption(num++, MultiMenu.crewmate, "Tracker Arrows Reset After Each Round", false);
            MaxTracks = new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Tracks Per Round", 5, 1, 15, 1);

            Trapper =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#A7D1B3FF>Trapper</color>");
            MinAmountOfTimeInTrap =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Min Amount Of Time In Trap To Register", 1f, 0f, 15f, 0.5f, CooldownFormat);
            TrapCooldown =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Trap Cooldown", 25f, 10f, 40f, 2.5f, CooldownFormat);
            TrapsRemoveOnNewRound =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Traps Removed After Each Round", true);
            MaxTraps =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Traps Per Game", 5, 1, 15, 1);
            TrapSize =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Trap Size", 0.25f, 0.05f, 1f, 0.05f, MultiplierFormat);
            MinAmountOfPlayersInTrap =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Minimum Number Of Roles Required To Trigger Trap", 3, 1, 5, 1);

            Hunter =
               new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#29AB87FF>Hunter</color>");
            HunterKillCd =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Hunter Kill Cooldown", 25f, 10f, 40f, 2.5f, CooldownFormat);
            HunterStalkCd =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Hunter Stalk Cooldown", 10f, 0f, 40f, 2.5f, CooldownFormat);
            HunterStalkDuration =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Hunter Stalk Duration", 25f, 5f, 40f, 1f, CooldownFormat);
            HunterStalkUses =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Stalk Uses", 5, 1, 15, 1);
            HunterBodyReport =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Hunter Can Report Who They've Killed");
            Sheriff =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#FFFF00FF>Sheriff</color>");
            SheriffShootRoundOne =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Can Shoot Round One", false);
            SheriffKillOther =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Miskill Kills Crewmate", false);
            SheriffKillsDoomsayer =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Doomsayer", false);
            SheriffKillsExecutioner =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Executioner", false);
            SheriffKillsJester =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Jester", false);

            SheriffKillsChaos =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Chaos Neutrals", false);

            SheriffKillsArsonist =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Arsonist", false);
            SheriffKillsGlitch =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills The Glitch", false);
            SheriffKillsWerewolf =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Werewolf", false);

            SheriffKillsNeophyte =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Neophyte Neutrals", false);
            SheriffKillsApocalypse =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Apocalypse Neutrals", false);
            SheriffKillsAlliedCrew =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Kills Allied Players", true);

            SheriffKillCd =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Sheriff Kill Cooldown", 25f, 10f, 40f, 2.5f, CooldownFormat);
            SheriffBodyReport = new CustomToggleOption(num++, MultiMenu.crewmate, "Sheriff Can Report Who They've Killed");

            Trickster =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#EAA299FF>Trickster</color>");
            TrickCooldown =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Trick Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            MaxFailedTricksPerGame = new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Failed Tricks Per Game", 3, 1, 15, 1);
            CanTrickRoundOne = new CustomToggleOption(num++, MultiMenu.crewmate, "Can Trick Round One", false);
            SelfKillAfterFinalTrick = new CustomToggleOption(num++, MultiMenu.crewmate, "Suicide If All Tricks Fail", true);

            VampireHunter =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#B3B3E6FF>Vampire Hunter</color>");
            StakeCooldown =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Stake Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            MaxFailedStakesPerGame = new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Failed Stakes Per Game", 5, 1, 15, 1);
            CanStakeRoundOne = new CustomToggleOption(num++, MultiMenu.crewmate, "Can Stake Round One", false);
            SelfKillAfterFinalStake = new CustomToggleOption(num++, MultiMenu.crewmate, "Self Kill On Failure To Kill A Vamp With All Stakes", false);
            BecomeOnVampDeaths =
                new CustomStringOption(num++, MultiMenu.crewmate, "What Vampire Hunter Becomes On All Vampire Deaths", new[] { "Crewmate", "Sheriff", "Veteran", "Vigilante", "Hunter"});

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
            VigilanteGuessLovers = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Lovers", false);
            VigilanteGuessRecruits = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Recruits", false);
            VigilanteGuessEvilCrew = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess Allied Crew", false);
            VigilanteAfterVoting = new CustomToggleOption(num++, MultiMenu.crewmate, "Vigilante Can Guess After Voting", false);

            Altruist = new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#660000FF>Altruist</color>");
            ReviveDuration =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Altruist Revive Duration", 10f, 1f, 15f, 1f, CooldownFormat);
            AltruistTargetBody =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Target's Body Disappears On Beginning Of Revive", false);

            Bodyguard =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#80D3ABFF>Bodyguard</color>");
            GuardCd =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Guard Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            GuardDuration =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Guard Duration", 10f, 5f, 15f, 1f, CooldownFormat);
            MaxGuards =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Times Allowed To Guard", 5, 1, 15, 1);

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

            Engineer =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#FFA60AFF>Engineer</color>");
            MaxFixes =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Fixes", 5, 1, 15, 1);

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

            Prosecutor =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#B38000FF>Prosecutor</color>");
            ProsDiesOnIncorrectPros =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Prosecutor Dies When They Exile A Crewmate", false);

            Swapper =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#66E666FF>Swapper</color>");
            SwapperButton =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Swapper Can Button", true);

            Transporter =
                new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#00EEFFFF>Transporter</color>");
            TransportCooldown =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Transport Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            TransportMaxUses =
                new CustomNumberOption(num++, MultiMenu.crewmate, "Maximum Number Of Transports", 5, 1, 15, 1);
            TransporterVitals =
                new CustomToggleOption(num++, MultiMenu.crewmate, "Transporter Cannot Use Vitals", false);

            Taskmaster = new CustomHeaderOption(num++, MultiMenu.crewmate, "<color=#C3FFFFFF>Taskmaster</color>");
            TMSeesNeutrals = new CustomToggleOption(num++, MultiMenu.crewmate, "Taskmaster Sees Neutral Roles", true);
            TMTasksRemaining =
                 new CustomNumberOption(num++, MultiMenu.crewmate, "Tasks Remaining When Revealed", 2, 1, 5, 1);
            TMCommonTasks =
                 new CustomNumberOption(num++, MultiMenu.crewmate, "Common Master Tasks Added", 3, 1, 5, 1);
            TMLongTasks =
                 new CustomNumberOption(num++, MultiMenu.crewmate, "Long Master Tasks Added", 2, 1, 5, 1);
            TMShortTasks =
                 new CustomNumberOption(num++, MultiMenu.crewmate, "Short Master Tasks Added", 3, 1, 5, 1);

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
            DoomsayerGuessNeutralBenign = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Benign Roles", false);
            DoomsayerGuessNeutralEvil = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Evil Roles", false);
            DoomsayerGuessNeutralChaos = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Chaos Roles", false);
            DoomsayerGuessNeutralKilling = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Killing Roles", false);
            DoomsayerGuessNeutralNeophyte = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Neophyte Roles", false);
            DoomsayerGuessNeutralApocalypse = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Neutral Apocalypse Roles", false);
            DoomsayerGuessImpostors = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess Impostor Roles", false);
            DoomsayerAfterVoting = new CustomToggleOption(num++, MultiMenu.neutral, "Doomsayer Can Guess After Voting", false);
            DoomsayerGuessesToWin = new CustomNumberOption(num++, MultiMenu.neutral, "Number Of Doomsayer Kills To Win", 3, 1, 5, 1);
            DoomsayerCantObserve = new CustomToggleOption(num++, MultiMenu.neutral, "(Experienced) Doomsayer Can't Observe", false);

            Executioner =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#8C4005FF>Executioner</color>");
            OnTargetDead = new CustomStringOption(num++, MultiMenu.neutral, "Executioner Becomes On Target Dead",
                new[] { "Crew", "Amnesiac", "Survivor", "Jester" });
            ExecutionerButton =
                new CustomToggleOption(num++, MultiMenu.neutral, "Executioner Can Button", true);
            ExecutionerTorment =
                new CustomToggleOption(num++, MultiMenu.neutral, "Executioner Torments Player On Victory", true);

            CursedSoul = new CustomHeaderOption(num++, MultiMenu.neutral, Utils.GradientColorText("79FFB3", "B579FF", "Cursed Soul"));
            SoulSwapCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Soul Swap Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            SoulSwapGuarantee =
                new CustomNumberOption(num++, MultiMenu.neutral, "Indirect Soul Swap Chance", 40f, 0f, 100f, 10f, PercentFormat);

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

            Joker =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#C0FF85FF>Joker</color>");
            JokerOnTargetDead = new CustomStringOption(num++, MultiMenu.neutral, "Joker Becomes On Target Dead",
                new[] { "Crew", "Amnesiac", "Survivor", "Jester", "Joker" });
            JokerButton =
                new CustomToggleOption(num++, MultiMenu.neutral, "Joker Can Button", true);

            Arsonist = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#FF4D00FF>Arsonist</color>");
            DouseCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Douse Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            MaxDoused =
                new CustomNumberOption(num++, MultiMenu.neutral, "Maximum Alive Players Doused", 5, 1, 15, 1);
            ArsoImpVision =
                new CustomToggleOption(num++, MultiMenu.neutral, "Arsonist Has Impostor Vision", false);
            IgniteCdRemoved =
                new CustomToggleOption(num++, MultiMenu.neutral, "Ignite Cooldown Removed When Arsonist Is Last Killer", false);

            TheGlitch =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#00FF00FF>The Glitch</color>");
            MimicCooldownOption = new CustomNumberOption(num++, MultiMenu.neutral, "Mimic Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            MimicDurationOption = new CustomNumberOption(num++, MultiMenu.neutral, "Mimic Duration", 10f, 1f, 15f, 1f, CooldownFormat);
            HackCooldownOption = new CustomNumberOption(num++, MultiMenu.neutral, "Hack Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            HackDurationOption = new CustomNumberOption(num++, MultiMenu.neutral, "Hack Duration", 10f, 1f, 15f, 1f, CooldownFormat);
            GlitchKillCooldownOption =
                new CustomNumberOption(num++, MultiMenu.neutral, "Glitch Kill Cooldown", 25f, 10f, 120f, 2.5f, CooldownFormat);
            GlitchHackDistanceOption =
                new CustomStringOption(num++, MultiMenu.neutral, "Glitch Hack Distance", new[] { "Short", "Normal", "Long" });
            GlitchVent =
                new CustomToggleOption(num++, MultiMenu.neutral, "Glitch Can Vent", false);
            
            Sentinel = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#8FA28DFF>The Sentinel</color>");
            SentinelKillCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Sentinel Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            SentinelSoloPercent = new CustomNumberOption(num++, MultiMenu.neutral, "Sentinel Solo Probability", 20f, 0f, 100f, 10f,
                PercentFormat);

            SentinelChargeCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Sentinel Charge Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            ChargeDelay =
                new CustomNumberOption(num++, MultiMenu.neutral, "Charge Delay", 5f, 1f, 15f, 1f, CooldownFormat);
            MaxKillsInCharge =
                new CustomNumberOption(num++, MultiMenu.neutral, "Max Kills In Charge", 5, 1, 15, 1);
            ChargeRadius =
                new CustomNumberOption(num++, MultiMenu.neutral, "Charge Radius", 0.25f, 0.05f, 1f, 0.05f, MultiplierFormat);
            MaxChargeUses = new CustomNumberOption(num++, MultiMenu.neutral, "Maximum Number Of Charges", 5, 1, 15, 1);

            SentinelPlaceCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Sentinel Dynamite Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            MaxKillsInPlaced =
                new CustomNumberOption(num++, MultiMenu.neutral, "Max Kills With Dynamite", 5, 1, 15, 1);
            PlaceRadius =
                new CustomNumberOption(num++, MultiMenu.neutral, "Dynamite Radius", 0.25f, 0.05f, 1f, 0.05f, MultiplierFormat);
            MaxPlaceUses = new CustomNumberOption(num++, MultiMenu.neutral, "Maximum Number Of Dynamite", 5, 1, 15, 1);

            SentinelStunCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Sentinel Stun Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            StunDelay =
                new CustomNumberOption(num++, MultiMenu.neutral, "Stun Delay", 5f, 1f, 15f, 1f, CooldownFormat);
            StunDuration =
                new CustomNumberOption(num++, MultiMenu.neutral, "Stun Duration", 25f, 10f, 60f, 2.5f, CooldownFormat);
            StunInverts =
                new CustomToggleOption(num++, MultiMenu.neutral, "Stun Inverts Controls", true);
            MaxStunUses = new CustomNumberOption(num++, MultiMenu.neutral, "Maximum Number Of Stuns", 5, 1, 15, 1);

            SentinelVent =
                new CustomToggleOption(num++, MultiMenu.neutral, "Sentinel Can Vent", false);

            Werewolf = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#A86629FF>Werewolf</color>");
            RampageCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Rampage Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            RampageDuration =
                new CustomNumberOption(num++, MultiMenu.neutral, "Rampage Duration", 25f, 10f, 60f, 2.5f, CooldownFormat);
            RampageKillCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Rampage Kill Cooldown", 10f, 0.5f, 15f, 0.5f, CooldownFormat);
            WerewolfVent =
                new CustomToggleOption(num++, MultiMenu.neutral, "Werewolf Can Vent When Rampaged", false);

            Jackal = new CustomHeaderOption(num++, MultiMenu.neutral, Utils.GradientColorText("B7B9BA", "5E576B", "Jackal"));
            JackalKillCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Jackal Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            DoJackalRecruitsDie =
                new CustomToggleOption(num++, MultiMenu.neutral, "Do Both Jackal Recruits Die", true);
            JackalCanAlwaysKill =
                new CustomToggleOption(num++, MultiMenu.neutral, "Jackal Can Always Kill", false);

            NeoNecromancer = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#E22759FF>Necromancer Team</color>");
            NecroKillCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Necromancer: Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            NecroResurrectCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Necromancer: Initial Resurrect Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            NecroIncreasedCooldownPerResurrect =
                new CustomNumberOption(num++, MultiMenu.neutral, "Necromancer: Increased Cooldown Per Resurrection", 25f, 10f, 60f, 2.5f, CooldownFormat);
            AppaResurrectCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Apparitionist: Initial Resurrect Cooldown", 35f, 10f, 90f, 2.5f, CooldownFormat);
            AppaIncreasedCooldownPerResurrect =
                new CustomNumberOption(num++, MultiMenu.neutral, "Apparitionist: Increased Cooldown Per Resurrection", 35f, 10f, 90f, 2.5f, CooldownFormat);
            ScourgeKillCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Scourge Kill Cooldown", 35f, 10f, 90f, 2.5f, CooldownFormat);
            CanHuskVent =
                new CustomToggleOption(num++, MultiMenu.neutral, "Can Husk Vent", true);
            CanHuskAssassinate =
                new CustomToggleOption(num++, MultiMenu.neutral, "Can Husk Assassinate (Uses Assassin Settings)", false);
            HuskAssassinKills = new CustomNumberOption(num++, MultiMenu.neutral, "Number Of Husk Assassin Kills", 1, 1, 15, 1);
            HuskAssassinMultiKill = new CustomToggleOption(num++, MultiMenu.neutral, "Husk Can Kill More Than Once Per Meeting", false);

            Vampire = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#262626FF>Vampire</color>");
            BiteCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Vampire Bite Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            BiteDuration =
                new CustomNumberOption(num++, MultiMenu.neutral, "Vampire Bite Duration", 5f, 0f, 10f, 0.5f, CooldownFormat);
            VampImpVision =
                new CustomToggleOption(num++, MultiMenu.neutral, "Vampires Have Impostor Vision", false);
            VampVent =
                new CustomToggleOption(num++, MultiMenu.neutral, "Vampires Can Vent", false);
            RememberedVampireStaysVamp =
                new CustomToggleOption(num++, MultiMenu.neutral, "Remembered Vampire Stays Vampire", false);
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

            Berserker =
                new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#FFE8B3FF>Berserker</color>");
            JuggKillCooldown = new CustomNumberOption(num++, MultiMenu.neutral, "Berserker Initial Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            ReducedKCdPerKill = new CustomNumberOption(num++, MultiMenu.neutral, "Reduced Kill Cooldown Per Kill", 5f, 2.5f, 10f, 2.5f, CooldownFormat);
            JuggVent =
                new CustomToggleOption(num++, MultiMenu.neutral, "Berserker Can Vent", false);

            Plaguebearer = new CustomHeaderOption(num++, MultiMenu.neutral, "<color=#FFE8B3FF>Plaguebearer</color>");
            InfectCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Infect Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            PestKillCooldown =
                new CustomNumberOption(num++, MultiMenu.neutral, "Pestilence Kill Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            PestVent =
                new CustomToggleOption(num++, MultiMenu.neutral, "Pestilence Can Vent", false);
                
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
            CanSeeBlackmailed =
                new CustomToggleOption(num++, MultiMenu.imposter, "Players Can See Blackmailer Player", true);

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

            Bait = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#00B3B3FF>Bait</color>");
            BaitMinDelay = new CustomNumberOption(num++, MultiMenu.modifiers, "Minimum Delay for the Bait Report", 0f, 0f, 15f, 0.5f, CooldownFormat);
            BaitMaxDelay = new CustomNumberOption(num++, MultiMenu.modifiers, "Maximum Delay for the Bait Report", 1f, 0f, 15f, 0.5f, CooldownFormat);

            Diseased = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#808080FF>Diseased</color>");
            DiseasedKillMultiplier = new CustomNumberOption(num++, MultiMenu.modifiers, "Diseased Kill Multiplier", 3f, 1.5f, 5f, 0.5f, MultiplierFormat);

            Eclipsed = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#D98E70>Eclipsed</color>");
            EclipsedWithLights = new CustomNumberOption(num++, MultiMenu.modifiers, "Eclipsed Vision Decrease With Lights On", 3f, 1.5f, 5f, 0.5f, MultiplierFormat);
            EclipsedWithoutLights = new CustomNumberOption(num++, MultiMenu.modifiers, "Eclipsed Vision Increase With Lights Off", 3f, 1.5f, 5f, 0.5f, MultiplierFormat);

            Frosty = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#99FFFFFF>Frosty</color>");
            ChillDuration = new CustomNumberOption(num++, MultiMenu.modifiers, "Chill Duration", 10f, 1f, 15f, 1f, CooldownFormat);
            ChillStartSpeed = new CustomNumberOption(num++, MultiMenu.modifiers, "Chill Start Speed", 0.75f, 0.25f, 0.95f, 0.05f, MultiplierFormat);

            Dwarf = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FF8080FF>Dwarf</color>");
            DwarfSpeed = new CustomNumberOption(num++, MultiMenu.modifiers, "Dwarf Speed", 1.25f, 1.05f, 2.5f, 0.05f, MultiplierFormat);

            Giant = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FFB34DFF>Giant</color>");
            GiantSlow = new CustomNumberOption(num++, MultiMenu.modifiers, "Giant Speed", 0.75f, 0.25f, 1f, 0.05f, MultiplierFormat);

            Oblivious = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#AAAAAAFF>Oblivious</color>");
            ObliviousCanReport = new CustomToggleOption(num++, MultiMenu.modifiers, "Oblivious Can Report", true);

            Underdog = new CustomHeaderOption(num++, MultiMenu.modifiers, "<color=#FF0000FF>Underdog</color>");
            UnderdogKillBonus = new CustomNumberOption(num++, MultiMenu.modifiers, "Kill Cooldown Bonus", 5f, 2.5f, 10f, 2.5f, CooldownFormat);
            UnderdogIncreasedKC = new CustomToggleOption(num++, MultiMenu.modifiers, "Increased Kill Cooldown When 2+ Imps", true);
        }
    }
}