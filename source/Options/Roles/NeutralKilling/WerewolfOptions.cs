namespace TownOfUsFusion.Options.Roles;

public class WerewolfOptions : AbstractOptionGroup
{
    public override string GroupName => "Werewolf";

    public override Type AdvancedRole => typeof(WerewolfRole);

    public ModdedNumberOption RampageCooldown { get; set; } = new("Rampage Cooldown", 25, 10, 60, 2.5f, MiraNumberSuffixes.Seconds);

    [ModdedNumberOption("Rampage Duration", 10, 60, 2.5f, MiraNumberSuffixes.Seconds)]
    public float RampageDuration { get; set; } = 10;

    [ModdedNumberOption("Rampage Kill Cooldown", 0.5f, 15f, 0.5f, MiraNumberSuffixes.Seconds)]
    public float RampageKillCooldown { get; set; } = 0.5f;

    [ModdedToggleOption("Can Vent When Rampaging")]
    public bool WerewolfVent { get; set; } = true;
}
