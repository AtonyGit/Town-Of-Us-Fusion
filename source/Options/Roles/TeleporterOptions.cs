namespace TownOfUsFusion.Options.Roles;

public class TeleporterOptions : AbstractOptionGroup
{
    public override string GroupName => "Teleporter";

    public override Type AdvancedRole => typeof(TeleporterRole);

    public ModdedNumberOption TeleportCooldown { get; set; } = new("Teleport Cooldown", 10, 5, 60, 2.5f, MiraNumberSuffixes.Seconds);

    [ModdedNumberOption("Teleport Duration", 5, 25, 1, MiraNumberSuffixes.Seconds)]
    public float TeleportDuration { get; set; } = 10;

    [ModdedNumberOption("Zoom Distance", 4, 15)]
    public float ZoomDistance { get; set; } = 6;
}
