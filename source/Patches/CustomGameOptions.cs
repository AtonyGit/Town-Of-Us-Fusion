using TownOfUsFusion.CrewmateRoles.BodyguardMod;
using TownOfUsFusion.CrewmateRoles.MedicMod;
using TownOfUsFusion.CustomOption;
using TownOfUsFusion.NeutralRoles.ExecutionerMod;
using TownOfUsFusion.NeutralRoles.JokerMod;
using TownOfUsFusion.CrewmateRoles.HaunterMod;
using TownOfUsFusion.CrewmateRoles.MediumMod;
using TownOfUsFusion.CrewmateRoles.VampireHunterMod;
using TownOfUsFusion.NeutralRoles.GuardianAngelMod;

namespace TownOfUsFusion
{
    public enum DisableSkipButtonMeetings
{
    No,
    Emergency,
    Always
}
public enum GameMode
{
    Classic,
    AllAny,
    KillingOnly,
    Cultist
}
public enum AdminDeadPlayers
{
    Nobody,
    Spy,
    EveryoneButSpy,
    Everyone
}
public static class CustomGameOptions
{
    public static int MayorOn => (int)Generate.MayorOn.Get();
    public static int JesterOn => (int)Generate.JesterOn.Get();
    public static int SheriffOn => (int)Generate.SheriffOn.Get();
    public static int JanitorOn => (int)Generate.JanitorOn.Get();
    public static int EngineerOn => (int)Generate.EngineerOn.Get();
    public static int SwapperOn => (int)Generate.SwapperOn.Get();
    public static int AmnesiacOn => (int)Generate.AmnesiacOn.Get();
    public static int InvestigatorOn => (int)Generate.InvestigatorOn.Get();
    public static int MedicOn => (int)Generate.MedicOn.Get();
    public static int SeerOn => (int)Generate.SeerOn.Get();
    public static int GlitchOn => (int)Generate.GlitchOn.Get();
    public static int MorphlingOn => (int)Generate.MorphlingOn.Get();
    public static int ExecutionerOn => (int)Generate.ExecutionerOn.Get();
    public static int SpyOn => (int)Generate.SpyOn.Get();
    public static int SnitchOn => (int)Generate.SnitchOn.Get();
    public static int MinerOn => (int)Generate.MinerOn.Get();
    public static int SwooperOn => (int)Generate.SwooperOn.Get();
    public static int ArsonistOn => (int)Generate.ArsonistOn.Get();
    public static int AltruistOn => (int)Generate.AltruistOn.Get();
    public static int UndertakerOn => (int)Generate.UndertakerOn.Get();
    public static int PhantomOn => (int)Generate.PhantomOn.Get();
    public static int VigilanteOn => (int)Generate.VigilanteOn.Get();
    public static int HaunterOn => (int)Generate.HaunterOn.Get();
    public static int GrenadierOn => (int)Generate.GrenadierOn.Get();
    public static int VeteranOn => (int)Generate.VeteranOn.Get();
    public static int TrackerOn => (int)Generate.TrackerOn.Get();
    public static int TrapperOn => (int)Generate.TrapperOn.Get();
    public static int TraitorOn => (int)Generate.TraitorOn.Get();
    public static int TransporterOn => (int)Generate.TransporterOn.Get();
    public static int MediumOn => (int)Generate.MediumOn.Get();
    public static int SurvivorOn => (int)Generate.SurvivorOn.Get();
    public static int GuardianAngelOn => (int)Generate.GuardianAngelOn.Get();
    public static int MysticOn => (int)Generate.MysticOn.Get();
    public static int BlackmailerOn => (int)Generate.BlackmailerOn.Get();
    public static int PlaguebearerOn => (int)Generate.PlaguebearerOn.Get();
    public static int WerewolfOn => (int)Generate.WerewolfOn.Get();
    public static int DetectiveOn => (int)Generate.DetectiveOn.Get();
    public static int EscapistOn => (int)Generate.EscapistOn.Get();
    public static int ImitatorOn => (int)Generate.ImitatorOn.Get();
    public static int BomberOn => (int)Generate.BomberOn.Get();
    public static int DoomsayerOn => (int)Generate.DoomsayerOn.Get();
    public static int VampireOn => (int)Generate.VampireOn.Get();
    public static int VampireHunterOn => (int)Generate.VampireHunterOn.Get();
    public static int ProsecutorOn => (int)Generate.ProsecutorOn.Get();
    public static int WarlockOn => (int)Generate.WarlockOn.Get();
    public static int OracleOn => (int)Generate.OracleOn.Get();
    public static int VenererOn => (int)Generate.VenererOn.Get();
    public static int AurialOn => (int)Generate.AurialOn.Get();
    public static int HunterOn => (int)Generate.HunterOn.Get();
    // TOU FUSION ROLES
    public static int BodyguardOn => (int)Generate.BodyguardOn.Get();
    public static int MageOn => (int)Generate.MageOn.Get();
    public static int CaptainOn => (int)Generate.CaptainOn.Get();
    public static int MonarchOn => (int)Generate.MonarchOn.Get();
    public static int BartenderOn => (int)Generate.BartenderOn.Get();

    public static int JokerOn => (int)Generate.JokerOn.Get();
    public static int PirateOn => (int)Generate.PirateOn.Get();

    public static int FraudOn => (int)Generate.FraudOn.Get();
    public static int TempestOn => (int)Generate.TempestOn.Get();
    public static int TyrantOn => (int)Generate.TyrantOn.Get();
    public static int CannibalOn => (int)Generate.CannibalOn.Get();
    public static int WitchOn => (int)Generate.WitchOn.Get();

    public static int InfiltratorOn => (int)Generate.InfiltratorOn.Get();
    public static int GhoulOn => (int)Generate.GhoulOn.Get();
    public static int MercenaryOn => (int)Generate.MercenaryOn.Get();
    public static int SerialKillerOn => (int)Generate.SerialKillerOn.Get();

    public static int JackalOn => (int)Generate.JackalOn.Get();
    public static int NeoNecromancerOn => (int)Generate.NeoNecromancerOn.Get();

    public static int BakerOn => (int)Generate.BakerOn.Get();
    public static int BerserkerOn => (int)Generate.BerserkerOn.Get();
    public static int SoulCollectorOn => (int)Generate.SoulCollectorOn.Get();

    public static int PoisonerOn => (int)Generate.PoisonerOn.Get();
    // TOU FUSION MODIFIERS
    
    public static int DrunkOn => (int)Generate.DrunkOn.Get();
    public static int NumbOn => (int)Generate.NumbOn.Get();
    public static int NinjaOn => (int)Generate.NinjaOn.Get();
    public static int ObliviousOn => (int)Generate.ObliviousOn.Get();
    public static int TrollOn => (int)Generate.TrollOn.Get();
    public static int EclipsedOn => (int)Generate.EclipsedOn.Get();
    public static float EclipsedWithLights => Generate.EclipsedWithLights.Get();
    public static float EclipsedWithoutLights => Generate.EclipsedWithoutLights.Get();

    public static int TorchOn => (int)Generate.TorchOn.Get();
    public static int DiseasedOn => (int)Generate.DiseasedOn.Get();
    public static int DwarfOn => (int)Generate.DwarfOn.Get();
    public static int TiebreakerOn => (int)Generate.TiebreakerOn.Get();
    public static int GiantOn => (int)Generate.GiantOn.Get();
    public static int ButtonBarryOn => (int)Generate.ButtonBarryOn.Get();
    public static int BaitOn => (int)Generate.BaitOn.Get();
    public static int SleuthOn => (int)Generate.SleuthOn.Get();
    public static int AftermathOn => (int)Generate.AftermathOn.Get();
    public static int RadarOn => (int)Generate.RadarOn.Get();
    public static int DisperserOn => (int)Generate.DisperserOn.Get();
    public static int MultitaskerOn => (int)Generate.MultitaskerOn.Get();
    public static int DoubleShotOn => (int)Generate.DoubleShotOn.Get();
    public static int UnderdogOn => (int)Generate.UnderdogOn.Get();
    public static int FrostyOn => (int)Generate.FrostyOn.Get();
    public static float InitialCooldowns => Generate.InitialCooldowns.Get();
    public static bool ButtonBarryGetsCooldown => Generate.ButtonBarryGetsCooldown.Get();
    public static bool SheriffShootRoundOne => Generate.SheriffShootRoundOne.Get();
    public static bool SheriffKillOther => Generate.SheriffKillOther.Get();
    public static bool SheriffKillsDoomsayer => Generate.SheriffKillsDoomsayer.Get();
    public static bool SheriffKillsExecutioner => Generate.SheriffKillsExecutioner.Get();
    public static bool SheriffKillsJester => Generate.SheriffKillsJester.Get();

    public static bool SheriffKillsChaos => Generate.SheriffKillsChaos.Get();

    public static bool SheriffKillsArsonist => Generate.SheriffKillsArsonist.Get();
    public static bool SheriffKillsApocalypse => Generate.SheriffKillsApocalypse.Get();
    public static bool SheriffKillsGlitch => Generate.SheriffKillsGlitch.Get();
    public static bool SheriffKillsWerewolf => Generate.SheriffKillsWerewolf.Get();

    public static bool SheriffKillsNeophyte => Generate.SheriffKillsNeophyte.Get();
    public static bool SheriffKillsAlliedCrew => Generate.SheriffKillsAlliedCrew.Get();
    
    public static float SheriffKillCd => Generate.SheriffKillCd.Get();
    public static bool SwapperButton => Generate.SwapperButton.Get();
    public static float FootprintSize => Generate.FootprintSize.Get();
    public static float FootprintInterval => Generate.FootprintInterval.Get();
    public static float FootprintDuration => Generate.FootprintDuration.Get();
    public static bool AnonymousFootPrint => Generate.AnonymousFootPrint.Get();
    public static bool VentFootprintVisible => Generate.VentFootprintVisible.Get();
    public static bool JesterButton => Generate.JesterButton.Get();
    public static bool JesterVent => Generate.JesterVent.Get();
    public static bool JesterImpVision => Generate.JesterImpVision.Get();
    public static bool JesterHaunt => Generate.JesterHaunt.Get();
    public static GuardOptions ShowGuarded => (GuardOptions)Generate.ShowGuarded.Get();

    public static ShieldOptions ShowShielded => (ShieldOptions)Generate.ShowShielded.Get();

    public static NotificationOptions NotificationShield =>
        (NotificationOptions)Generate.WhoGetsNotification.Get();

    public static bool ShieldBreaks => Generate.ShieldBreaks.Get();
    public static float MedicReportNameDuration => Generate.MedicReportNameDuration.Get();
    public static float MedicReportColorDuration => Generate.MedicReportColorDuration.Get();
    public static bool ShowReports => Generate.MedicReportSwitch.Get();
    public static float SeerCd => Generate.SeerCooldown.Get();
    public static bool CrewKillingRed => Generate.CrewKillingRed.Get();
    public static bool NeutBenignRed => Generate.NeutBenignRed.Get();
    public static bool NeutEvilRed => Generate.NeutEvilRed.Get();
    public static bool NeutChaosRed => Generate.NeutChaosRed.Get();
    public static bool NeutKillingRed => Generate.NeutKillingRed.Get();
    public static bool NeutNeophyteRed => Generate.NeutNeophyteRed.Get();
    public static bool NeutApocalypseRed => Generate.NeutApocalypseRed.Get();
    public static bool TraitorColourSwap => Generate.TraitorColourSwap.Get();
    public static float MimicCooldown => Generate.MimicCooldownOption.Get();
    public static float MimicDuration => Generate.MimicDurationOption.Get();
    public static float HackCooldown => Generate.HackCooldownOption.Get();
    public static float HackDuration => Generate.HackDurationOption.Get();
    public static float GlitchKillCooldown => Generate.GlitchKillCooldownOption.Get();
    public static int GlitchHackDistance => Generate.GlitchHackDistanceOption.Get();
    public static bool GlitchVent => Generate.GlitchVent.Get();
    public static float JuggKCd => Generate.JuggKillCooldown.Get();
    public static float ReducedKCdPerKill => Generate.ReducedKCdPerKill.Get();
    public static bool JuggVent => Generate.JuggVent.Get();
    public static float MorphlingCd => Generate.MorphlingCooldown.Get();
    public static float MorphlingDuration => Generate.MorphlingDuration.Get();
    public static bool MorphlingVent => Generate.MorphlingVent.Get();
    public static bool ColourblindComms => Generate.ColourblindComms.Get();
    public static OnTargetDead OnTargetDead => (OnTargetDead)Generate.OnTargetDead.Get();
    public static bool ExecutionerButton => Generate.ExecutionerButton.Get();
    public static bool ExecutionerTorment => Generate.ExecutionerTorment.Get();
    public static JokerOnTargetDead JokerOnTargetDead => (JokerOnTargetDead)Generate.JokerOnTargetDead.Get();
    public static bool JokerButton => Generate.JokerButton.Get();
    public static bool SnitchSeesNeutrals => Generate.SnitchSeesNeutrals.Get();
    public static int SnitchTasksRemaining => (int)Generate.SnitchTasksRemaining.Get();
    public static bool SnitchSeesImpInMeeting => Generate.SnitchSeesImpInMeeting.Get();
    public static bool SnitchSeesTraitor => Generate.SnitchSeesTraitor.Get();
    public static float MineCd => Generate.MineCooldown.Get();
    public static float SwoopCd => Generate.SwoopCooldown.Get();
    public static float SwoopDuration => Generate.SwoopDuration.Get();
    public static bool SwooperVent => Generate.SwooperVent.Get();
    public static bool ImpostorSeeRoles => Generate.ImpostorSeeRoles.Get();
    public static bool DeadSeeRoles => Generate.DeadSeeRoles.Get();
    public static bool FirstDeathShield => Generate.FirstDeathShield.Get();
    public static bool NeutralEvilWinEndsGame => Generate.NeutralEvilWinEndsGame.Get();
    public static bool SeeTasksDuringRound => Generate.SeeTasksDuringRound.Get();
    public static bool SeeTasksDuringMeeting => Generate.SeeTasksDuringMeeting.Get();
    public static bool SeeTasksWhenDead => Generate.SeeTasksWhenDead.Get();
    public static float DouseCd => Generate.DouseCooldown.Get();
    public static int MaxDoused => (int)Generate.MaxDoused.Get();
    public static bool ArsoImpVision => Generate.ArsoImpVision.Get();
    public static bool IgniteCdRemoved => Generate.IgniteCdRemoved.Get();
    public static int MinNeutralBenignRoles => (int)Generate.MinNeutralBenignRoles.Get();
    public static int MaxNeutralBenignRoles => (int)Generate.MaxNeutralBenignRoles.Get();
    public static int MinNeutralEvilRoles => (int)Generate.MinNeutralEvilRoles.Get();
    public static int MaxNeutralEvilRoles => (int)Generate.MaxNeutralEvilRoles.Get();
    public static int MinNeutralChaosRoles => (int)Generate.MinNeutralChaosRoles.Get();
    public static int MaxNeutralChaosRoles => (int)Generate.MaxNeutralChaosRoles.Get();
    public static int MinNeutralKillingRoles => (int)Generate.MinNeutralKillingRoles.Get();
    public static int MaxNeutralKillingRoles => (int)Generate.MaxNeutralKillingRoles.Get();
    public static int MinNeutralNeophyteRoles => (int)Generate.MinNeutralNeophyteRoles.Get();
    public static int MaxNeutralNeophyteRoles => (int)Generate.MaxNeutralNeophyteRoles.Get();
    public static int MinNeutralApocalypseRoles => (int)Generate.MinNeutralApocalypseRoles.Get();
    public static int MaxNeutralApocalypseRoles => (int)Generate.MaxNeutralApocalypseRoles.Get();
    public static int MinImpostorRoles => (int)Generate.MinImpostorRoles.Get();
    public static int MaxImpostorRoles => (int)Generate.MaxImpostorRoles.Get();
    public static bool RandomNumberImps => Generate.RandomNumberImps.Get();
    public static int NeutralRoles => (int)Generate.NeutralRoles.Get();
    public static int VeteranCount => (int)Generate.VeteranCount.Get();
    public static int VigilanteCount => (int)Generate.VigilanteCount.Get();
    public static bool AddArsonist => Generate.AddArsonist.Get();
    public static bool AddPlaguebearer => Generate.AddPlaguebearer.Get();
    public static bool ParallelMedScans => Generate.ParallelMedScans.Get();
    public static int MaxFixes => (int)Generate.MaxFixes.Get();
    public static float ReviveDuration => Generate.ReviveDuration.Get();
    public static bool AltruistTargetBody => Generate.AltruistTargetBody.Get();
    public static bool SheriffBodyReport => Generate.SheriffBodyReport.Get();
    public static float DragCd => Generate.DragCooldown.Get();
    public static float UndertakerDragSpeed => Generate.UndertakerDragSpeed.Get();
    public static bool UndertakerVent => Generate.UndertakerVent.Get();
    public static bool UndertakerVentWithBody => Generate.UndertakerVentWithBody.Get();
    public static bool AssassinGuessNeutralBenign => Generate.AssassinGuessNeutralBenign.Get();
    public static bool AssassinGuessNeutralEvil => Generate.AssassinGuessNeutralEvil.Get();
    public static bool AssassinGuessNeutralChaos => Generate.AssassinGuessNeutralChaos.Get();
    public static bool AssassinGuessNeutralKilling => Generate.AssassinGuessNeutralKilling.Get();
    public static bool AssassinGuessNeutralNeophyte => Generate.AssassinGuessNeutralNeophyte.Get();
    public static bool AssassinGuessNeutralApocalypse => Generate.AssassinGuessNeutralApocalypse.Get();
    public static bool AssassinGuessImpostors => Generate.AssassinGuessImpostors.Get();
    public static bool AssassinGuessModifiers => Generate.AssassinGuessModifiers.Get();
    public static bool AssassinGuessLovers => Generate.AssassinGuessLovers.Get();
    public static bool AssassinGuessRecruits => Generate.AssassinGuessRecruits.Get();
    public static bool AssassinGuessEvilCrew => Generate.AssassinGuessEvilCrew.Get();
    public static bool AssassinCrewmateGuess => Generate.AssassinCrewmateGuess.Get();
    public static bool AssassinGuessCrewInvestigative => Generate.AssassinGuessCrewInvestigative.Get();
    public static int AssassinKills => (int)Generate.AssassinKills.Get();
    public static int NumberOfImpostorAssassins => (int)Generate.NumberOfImpostorAssassins.Get();
    public static int NumberOfNeutralAssassins => (int)Generate.NumberOfNeutralAssassins.Get();
    public static bool AmneTurnImpAssassin => Generate.AmneTurnImpAssassin.Get();
    public static bool AmneTurnNeutAssassin => Generate.AmneTurnNeutAssassin.Get();
    public static bool TraitorCanAssassin => Generate.TraitorCanAssassin.Get();
    public static bool AssassinMultiKill => Generate.AssassinMultiKill.Get();
    public static bool AssassinateAfterVoting => Generate.AssassinateAfterVoting.Get();
    public static float UnderdogKillBonus => Generate.UnderdogKillBonus.Get();
    public static bool UnderdogIncreasedKC => Generate.UnderdogIncreasedKC.Get();
    public static int PhantomTasksRemaining => (int)Generate.PhantomTasksRemaining.Get();
    public static bool PhantomSpook => Generate.PhantomSpook.Get();
    public static bool VigilanteGuessNeutralBenign => Generate.VigilanteGuessNeutralBenign.Get();
    public static bool VigilanteGuessNeutralEvil => Generate.VigilanteGuessNeutralEvil.Get();
    public static bool VigilanteGuessNeutralChaos => Generate.VigilanteGuessNeutralChaos.Get();
    public static bool VigilanteGuessNeutralKilling => Generate.VigilanteGuessNeutralKilling.Get();
    public static bool VigilanteGuessNeutralNeophyte => Generate.VigilanteGuessNeutralNeophyte.Get();
    public static bool VigilanteGuessNeutralApocalypse => Generate.VigilanteGuessNeutralApocalypse.Get();
    public static bool VigilanteGuessLovers => Generate.VigilanteGuessLovers.Get();
    public static bool VigilanteGuessRecruits => Generate.VigilanteGuessRecruits.Get();
    public static bool VigilanteGuessEvilCrew => Generate.VigilanteGuessEvilCrew.Get();
    public static int VigilanteKills => (int)Generate.VigilanteKills.Get();
    public static bool VigilanteMultiKill => Generate.VigilanteMultiKill.Get();
    public static bool VigilanteAfterVoting => Generate.VigilanteAfterVoting.Get();
    public static int HaunterTasksRemainingClicked => (int)Generate.HaunterTasksRemainingClicked.Get();
    public static int HaunterTasksRemainingAlert => (int)Generate.HaunterTasksRemainingAlert.Get();
    public static bool HaunterRevealsNeutrals => Generate.HaunterRevealsNeutrals.Get();
    public static HaunterCanBeClickedBy HaunterCanBeClickedBy => (HaunterCanBeClickedBy)Generate.HaunterCanBeClickedBy.Get();
    public static float GrenadeCd => Generate.GrenadeCooldown.Get();
    public static float GrenadeDuration => Generate.GrenadeDuration.Get();
    public static bool GrenadierIndicators => Generate.GrenadierIndicators.Get();
    public static bool GrenadierVent => Generate.GrenadierVent.Get();
    public static float FlashRadius => Generate.FlashRadius.Get();
    public static int LovingImpPercent => (int)Generate.LovingImpPercent.Get();
    public static bool KilledOnAlert => Generate.KilledOnAlert.Get();
    public static float AlertCd => Generate.AlertCooldown.Get();
    public static float AlertDuration => Generate.AlertDuration.Get();
    public static int MaxAlerts => (int)Generate.MaxAlerts.Get();
    public static float UpdateInterval => Generate.UpdateInterval.Get();
    public static float TrackCd => Generate.TrackCooldown.Get();
    public static bool ResetOnNewRound => Generate.ResetOnNewRound.Get();
    public static int MaxTracks => (int)Generate.MaxTracks.Get();
    // POISONER SHIT
    public static float PoisonDuration => Generate.PoisonDuration.Get();
    public static bool PoisonerVent => Generate.PoisonerVent.Get();


    public static int LatestSpawn => (int)Generate.LatestSpawn.Get();
    public static bool NeutralKillingStopsTraitor => Generate.NeutralKillingStopsTraitor.Get();
    public static float TransportCooldown => Generate.TransportCooldown.Get();
    public static int TransportMaxUses => (int)Generate.TransportMaxUses.Get();
    public static bool TransporterVitals => Generate.TransporterVitals.Get();
    public static bool RememberArrows => Generate.RememberArrows.Get();
    public static float RememberArrowDelay => Generate.RememberArrowDelay.Get();

    public static bool CannibalArrows => Generate.CannibalArrows.Get();
    public static float CannibalArrowDelay => Generate.CannibalArrowDelay.Get();
    public static int BodiesNeededToWin => (int)Generate.BodiesNeededToWin.Get();

    public static float MediateCooldown => Generate.MediateCooldown.Get();
    public static bool ShowMediatePlayer => Generate.ShowMediatePlayer.Get();
    public static bool ShowMediumToDead => Generate.ShowMediumToDead.Get();
    public static DeadRevealed DeadRevealed => (DeadRevealed)Generate.DeadRevealed.Get();
    public static float VestCd => Generate.VestCd.Get();
    public static float VestDuration => Generate.VestDuration.Get();
    public static float VestKCReset => Generate.VestKCReset.Get();
    public static int MaxVests => (int)Generate.MaxVests.Get();
    public static float ProtectCd => Generate.ProtectCd.Get();
    public static float ProtectDuration => Generate.ProtectDuration.Get();
    public static float ProtectKCReset => Generate.ProtectKCReset.Get();
    public static int MaxProtects => (int)Generate.MaxProtects.Get();
    public static ProtectOptions ShowProtect => (ProtectOptions)Generate.ShowProtect.Get();
    public static BecomeOptions GaOnTargetDeath => (BecomeOptions)Generate.GaOnTargetDeath.Get();
    public static bool GATargetKnows => Generate.GATargetKnows.Get();
    public static bool GAKnowsTargetRole => Generate.GAKnowsTargetRole.Get();
    public static int EvilTargetPercent => (int)Generate.EvilTargetPercent.Get();
    public static float MysticArrowDuration => Generate.MysticArrowDuration.Get();
    public static float BlackmailCd => Generate.BlackmailCooldown.Get();
    public static bool CanSeeBlackmailed => Generate.CanSeeBlackmailed.Get();
    public static float GiantSlow => Generate.GiantSlow.Get();
    public static bool ObliviousCanReport => Generate.ObliviousCanReport.Get();
    public static float DwarfSpeed => Generate.DwarfSpeed.Get();
    public static float DiseasedMultiplier => Generate.DiseasedKillMultiplier.Get();
    public static float BaitMinDelay => Generate.BaitMinDelay.Get();
    public static float BaitMaxDelay => Generate.BaitMaxDelay.Get();
    public static float InfectCd => Generate.InfectCooldown.Get();
    public static float PestKillCd => Generate.PestKillCooldown.Get();
    public static bool PestVent => Generate.PestVent.Get();
    public static float RampageCd => Generate.RampageCooldown.Get();
    public static float RampageDuration => Generate.RampageDuration.Get();
    public static float RampageKillCd => Generate.RampageKillCooldown.Get();
    public static bool WerewolfVent => Generate.WerewolfVent.Get();
    public static float TrapCooldown => Generate.TrapCooldown.Get();
    public static bool TrapsRemoveOnNewRound => Generate.TrapsRemoveOnNewRound.Get();
    public static int MaxTraps => (int)Generate.MaxTraps.Get();
    public static float MinAmountOfTimeInTrap => Generate.MinAmountOfTimeInTrap.Get();
    public static float TrapSize => Generate.TrapSize.Get();
    public static int MinAmountOfPlayersInTrap => (int)Generate.MinAmountOfPlayersInTrap.Get();
    public static float ExamineCd => Generate.ExamineCooldown.Get();
    public static bool DetectiveReportOn => Generate.DetectiveReportOn.Get();
    public static float DetectiveRoleDuration => Generate.DetectiveRoleDuration.Get();
    public static float DetectiveFactionDuration => Generate.DetectiveFactionDuration.Get();
    public static bool CanDetectLastKiller => Generate.CanDetectLastKiller.Get();
    public static float EscapeCd => Generate.EscapeCooldown.Get();
    public static bool EscapistVent => Generate.EscapistVent.Get();
    public static float DetonateDelay => Generate.DetonateDelay.Get();
    public static int MaxKillsInDetonation => (int)Generate.MaxKillsInDetonation.Get();
    public static float DetonateRadius => Generate.DetonateRadius.Get();
    public static bool BomberVent => Generate.BomberVent.Get();

    public static float ObserveCooldown => Generate.ObserveCooldown.Get();
    public static bool DoomsayerGuessNeutralBenign => Generate.DoomsayerGuessNeutralBenign.Get();
    public static bool DoomsayerGuessNeutralEvil => Generate.DoomsayerGuessNeutralEvil.Get();
    public static bool DoomsayerGuessNeutralChaos => Generate.DoomsayerGuessNeutralChaos.Get();
    public static bool DoomsayerGuessNeutralKilling => Generate.DoomsayerGuessNeutralKilling.Get();
    public static bool DoomsayerGuessNeutralNeophyte => Generate.DoomsayerGuessNeutralNeophyte.Get();
    public static bool DoomsayerGuessNeutralApocalypse => Generate.DoomsayerGuessNeutralApocalypse.Get();
    public static bool DoomsayerGuessImpostors => Generate.DoomsayerGuessImpostors.Get();
    public static bool DoomsayerAfterVoting => Generate.DoomsayerAfterVoting.Get();
    public static int DoomsayerGuessesToWin => (int)Generate.DoomsayerGuessesToWin.Get();

    public static float BiteCd => Generate.BiteCooldown.Get();
    public static bool VampImpVision => Generate.VampImpVision.Get();
    public static bool VampVent => Generate.VampVent.Get();
    public static float BiteDuration => Generate.BiteDuration.Get();
    public static bool NewVampCanAssassin => Generate.NewVampCanAssassin.Get();
    public static int MaxVampiresPerGame => (int)Generate.MaxVampiresPerGame.Get();
    public static bool RememberedVampireStaysVamp => Generate.RememberedVampireStaysVamp.Get();
    public static bool CanBiteNeutralBenign => Generate.CanBiteNeutralBenign.Get();
    public static bool CanBiteNeutralEvil => Generate.CanBiteNeutralEvil.Get();
    public static bool CanBiteNeutralChaos => Generate.CanBiteNeutralChaos.Get();
    public static float StakeCd => Generate.StakeCooldown.Get();
    public static int MaxFailedStakesPerGame => (int)Generate.MaxFailedStakesPerGame.Get();
    public static bool CanStakeRoundOne => Generate.CanStakeRoundOne.Get();
    public static bool SelfKillAfterFinalStake => Generate.SelfKillAfterFinalStake.Get();
    public static BecomeEnum BecomeOnVampDeaths => (BecomeEnum)Generate.BecomeOnVampDeaths.Get();

    public static bool ProsDiesOnIncorrectPros => Generate.ProsDiesOnIncorrectPros.Get();

    public static float ChargeUpDuration => Generate.ChargeUpDuration.Get();
    public static float ChargeUseDuration => Generate.ChargeUseDuration.Get();
    public static float ConfessCd => Generate.ConfessCooldown.Get();
    public static float RevealAccuracy => Generate.RevealAccuracy.Get();
    public static bool NeutralBenignShowsEvil => Generate.NeutralBenignShowsEvil.Get();
    public static bool NeutralEvilShowsEvil => Generate.NeutralEvilShowsEvil.Get();
    public static bool NeutralChaosShowsEvil => Generate.NeutralChaosShowsEvil.Get();
    public static bool NeutralKillingShowsEvil => Generate.NeutralKillingShowsEvil.Get();
    public static bool NeutralNeophyteShowsEvil => Generate.NeutralNeophyteShowsEvil.Get();
    public static bool NeutralApocalypseShowsEvil => Generate.NeutralApocalypseShowsEvil.Get();

    public static float AbilityCd => Generate.AbilityCooldown.Get();
    public static float AbilityDuration => Generate.AbilityDuration.Get();
    public static float SprintSpeed => Generate.SprintSpeed.Get();
    public static float FreezeSpeed => Generate.FreezeSpeed.Get();
    public static float ChillDuration => Generate.ChillDuration.Get();
    public static float ChillStartSpeed => Generate.ChillStartSpeed.Get();
    public static float RadiateRange => (float)Generate.RadiateRange.Get();
    public static float RadiateCooldown => (float)Generate.RadiateCooldown.Get();
    public static float RadiateInvis => (float)Generate.RadiateInvis.Get();
    public static int RadiateCount => (int)Generate.RadiateCount.Get();
    public static int RadiateChance => (int)Generate.RadiateSucceedChance.Get();
        public static float HunterKillCd => Generate.HunterKillCd.Get();
        public static float HunterStalkCd => Generate.HunterStalkCd.Get();
        public static float HunterStalkDuration => Generate.HunterStalkDuration.Get();
        public static int HunterStalkUses => (int)Generate.HunterStalkUses.Get();
        public static bool HunterBodyReport => Generate.HunterBodyReport.Get();
    public static AdminDeadPlayers WhoSeesDead => (AdminDeadPlayers)Generate.WhoSeesDead.Get();
    public static DisableSkipButtonMeetings SkipButtonDisable =>
        (DisableSkipButtonMeetings)Generate.SkipButtonDisable.Get();
    public static GameMode GameMode =>
        (GameMode)Generate.GameMode.Get();
    public static GameModeEnum GM
    {
        get
        {
            var gm = Generate.GameMode.GetInt();
            var gmname = Generate.GameMode.GetString();

        //    if (gm is 0 or 1 or 2 or 3 or 4 or 5)
                return (GameModeEnum)gm;
            /*else if (gmname == "Submerged" && Patches.SubmergedCompatibility.Loaded)
                return GameModeEnum.Submerged;
            else if (gmname == "LevelImpostor" && Patches.LevelImpCheck.Loaded)
                return GameModeEnum.LevelImpostor;
            else
                return GameModeEnum.Random;*/
        }
    }
    public static int MayorCultistOn => (int)Generate.MayorCultistOn.Get();
    public static int SeerCultistOn => (int)Generate.SeerCultistOn.Get();
    public static int SheriffCultistOn => (int)Generate.SheriffCultistOn.Get();
    public static int SurvivorCultistOn => (int)Generate.SurvivorCultistOn.Get();
    public static int SpecialRoleCount => (int)Generate.NumberOfSpecialRoles.Get();
    public static int MaxChameleons => (int)Generate.MaxChameleons.Get();
    public static int MaxEngineers => (int)Generate.MaxEngineers.Get();
    public static int MaxInvestigators => (int)Generate.MaxInvestigators.Get();
    public static int MaxMystics => (int)Generate.MaxMystics.Get();
    public static int MaxSnitches => (int)Generate.MaxSnitches.Get();
    public static int MaxSpies => (int)Generate.MaxSpies.Get();
    public static int MaxTransporters => (int)Generate.MaxTransporters.Get();
    public static int MaxVigilantes => (int)Generate.MaxVigilantes.Get();
    public static float WhisperCooldown => Generate.WhisperCooldown.Get();
    public static float IncreasedCooldownPerWhisper => Generate.IncreasedCooldownPerWhisper.Get();
    public static float WhisperRadius => Generate.WhisperRadius.Get();
    public static int ConversionPercentage => (int)Generate.ConversionPercentage.Get();
    public static int DecreasedPercentagePerConversion => (int)Generate.DecreasedPercentagePerConversion.Get();
    public static float ReviveCooldown => Generate.ReviveCooldown.Get();
    public static float IncreasedCooldownPerRevive => Generate.IncreasedCooldownPerRevive.Get();
    public static int MaxReveals => (int)Generate.MaxReveals.Get();
    public static bool GhostsDoTasks => Generate.GhostsDoTasks.Get();
    public static bool DoomsayerCantObserve => Generate.DoomsayerCantObserve.Get();

    public static float JackalKillCooldown => Generate.JackalKillCooldown.Get();
    public static bool DoJackalRecruitsDie => Generate.DoJackalRecruitsDie.Get();
    public static bool JackalCanAlwaysKill => Generate.JackalCanAlwaysKill.Get();

    public static float NecroKillCooldown => Generate.NecroKillCooldown.Get();
    public static float NecroResurrectCooldown => Generate.NecroResurrectCooldown.Get();
    public static float NecroIncreasedCooldownPerResurrect => Generate.NecroIncreasedCooldownPerResurrect.Get();
    public static float AppaResurrectCooldown => Generate.AppaResurrectCooldown.Get();
    public static float AppaIncreasedCooldownPerResurrect => Generate.AppaIncreasedCooldownPerResurrect.Get();
    public static float ScourgeKillCooldown => Generate.ScourgeKillCooldown.Get();
    public static float EnchantRevealCooldown => Generate.EnchantRevealCooldown.Get();
    public static int EnchantMaxReveals => (int)Generate.EnchantMaxReveals.Get();
    public static bool CanHuskVent => Generate.CanHuskVent.Get();
    public static bool CanHuskAssassinate => Generate.CanHuskAssassinate.Get();
    public static bool HuskAssassinMultiKill => Generate.HuskAssassinMultiKill.Get();
    public static int HuskAssassinKills => (int)Generate.HuskAssassinKills.Get();

    public static bool EnableAprilFoolsMode => Generate.EnableAprilFoolsMode.Get();

    //              MAP SETTINGSSSS
    public static MapEnum Map
    {
        get
        {
            var map = Generate.Map.GetInt();
            var name = Generate.Map.GetString();

            if (map is 0 or 1 or 2 or 3 or 4 or 5)
                return (MapEnum)map;
            else if (name == "Submerged" && Patches.SubmergedCompatibility.Loaded)
                return MapEnum.Submerged;
            else if (name == "LevelImposter" && Patches.LevelImpCheck.Loaded)
                return MapEnum.LevelImposter;
            else
                return MapEnum.Random;
        }
    }
    public static bool RandomMapEnabled => Generate.RandomMapEnabled.Get();
    public static float RandomMapSkeld => Generate.RandomMapSkeld.Get();
    public static float RandomMapdlekS => Generate.RandomMapdlekS.Get();
    public static float RandomMapMira => Generate.RandomMapMira.Get();
    public static float RandomMapPolus => Generate.RandomMapPolus.Get();
    public static float RandomMapAirship => Generate.RandomMapAirship.Get();
    public static float RandomMapFungle => Generate.RandomMapFungle.Get();
    public static float RandomMapSubmerged => Patches.SubmergedCompatibility.Loaded ? Generate.RandomMapSubmerged.Get() : 0f;
    public static float RandomMapLevelImp => Patches.LevelImpCheck.Loaded ? Generate.RandomMapLevelImp.Get() : 0f;
    public static bool AutoAdjustSettings => Generate.AutoAdjustSettings.Get();
    public static bool SmallMapHalfVision => Generate.SmallMapHalfVision.Get();
    public static float SmallMapDecreasedCooldown => Generate.SmallMapDecreasedCooldown.Get();
    public static float LargeMapIncreasedCooldown => Generate.LargeMapIncreasedCooldown.Get();
    public static int SmallMapIncreasedShortTasks => (int)Generate.SmallMapIncreasedShortTasks.Get();
    public static int SmallMapIncreasedLongTasks => (int)Generate.SmallMapIncreasedLongTasks.Get();
    public static int LargeMapDecreasedShortTasks => (int)Generate.LargeMapDecreasedShortTasks.Get();
    public static int LargeMapDecreasedLongTasks => (int)Generate.LargeMapDecreasedLongTasks.Get();
    public static int CurMapVariant;
    //              BETTER SKELD
    public static bool BetterSkeldEnabled => Generate.BetterSkeldEnabled.Get();
    public static bool BSVentImprovements => Generate.BSVentImprovements.Get();
    //              BETTER MIRA HQ
    public static bool BetterMiraEnabled => Generate.BetterMiraEnabled.Get();
    public static bool BMVentImprovements => Generate.BMVentImprovements.Get();
    //              BETTER POLUS
    public static bool BetterPolusEnabled => Generate.BetterPolusEnabled.Get();
    public static bool BPVentImprovements => Generate.BPVentImprovements.Get();
    public static bool BPVitalsLab => Generate.BPVitalsLab.Get();
    public static bool BPColdTempDeathValley => Generate.BPColdTempDeathValley.Get();
    public static bool BPWifiChartCourseSwap => Generate.BPWifiChartCourseSwap.Get();

        public static bool BetterAirshipEnabled => Generate.BetterAirshipEnabled.Get();
        public static int BAMoveAdmin => (int)Generate.BAMoveAdmin.Get();
        public static int BAMoveElectrical => (int)Generate.BAMoveElectrical.Get();
        public static bool BAMoveVitals => Generate.BAMoveVitals.Get();
        public static bool BAMoveFuel => Generate.BAMoveFuel.Get();
        public static bool BAMoveDivert => Generate.BAMoveDivert.Get();

        
    public static int CrewpocalypseOn => (int)Generate.CrewpocalypseOn.Get();
    public static int CrewpostorOn => (int)Generate.CrewpostorOn.Get();
    public static int LoversOn => (int)Generate.LoversOn.Get();
    public static bool BothLoversDie => Generate.BothLoversDie.Get();
    public static bool NeutralLovers => Generate.NeutralLovers.Get();
}
}