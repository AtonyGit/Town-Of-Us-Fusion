namespace TownOfUsFusion.Options.Roles;

public class ArsonistRoleSettings : AbstractOptionGroup
{
    public override string GroupName => "Arsonist";

    public override Type AdvancedRole => typeof(ArsonistRole);

    public ModdedNumberOption DouseCooldown { get; set; } = new("Douse Cooldown", 25, 10, 60, 2.5f, MiraNumberSuffixes.Seconds);

    [ModdedNumberOption("Max Douses In Total", 1, 15)]
    public float DouseUses { get; set; } = 5;

    [ModdedToggleOption("Arsonist Has Full Vision")]
    public bool ArsonistImpVision { get; set; } = false;

    [ModdedToggleOption("Ignite Cooldown Removed When Arso Is The Final Killer")]
    public bool IgniteCdRemoved { get; set; } = false;
}
