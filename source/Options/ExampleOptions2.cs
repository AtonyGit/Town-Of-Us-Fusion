namespace TownOfUsFusion.Options;

public class ExampleOptions2 : AbstractOptionGroup
{
    public override string GroupName => "Example Options 2";

    public override uint GroupPriority => 0; // This group will be displayed first. The default value is uint.MaxValue.

    public ModdedToggleOption ToggleOpt1 { get; } = new("Toggle Option 1", false);

    public ModdedToggleOption ToggleOpt2 { get; } = new("Toggle Option 2", false)
    {
        Visible = () => OptionGroupSingleton<ExampleOptions2>.Instance.ToggleOpt1, // implicit cast from ModdedToggleOption to bool
    };

    public ModdedEnumOption<TestingData> EnumOpt { get; } = new("Enum Opt", 0)
    {
        ChangedEvent = x => Logger<TownOfUsFusion>.Info($"changed Enum Opt to {x}"),
    };
}

public enum TestingData : ulong
{
    Happy,
    Sad,
    Neutral,
}
