namespace TownOfUsFusion.Options;

public class ExampleOptions : AbstractOptionGroup
{
    public override string GroupName => "Example Options 1";
    public override Color GroupColor => Color.green;

    [ModdedToggleOption("Toggle Opt 1")]
    public bool ToggleOpt { get; set; } = false;

    [ModdedToggleOption("Toggle Opt 2")]
    public bool ToggleOpt2 { get; set; } = true;

    [ModdedNumberOption("Number Opt", min: 0, max: 10, increment: .25f, formatString: "0.00", suffixType: MiraNumberSuffixes.Percent)]
    public float NumberOpt { get; set; } = 4f;

    [ModdedEnumOption("Best API", typeof(BestApi), ["Mira API", "Mitochondria", "Reactor"])]
    public BestApi Opt { get; set; } = BestApi.MiraAPI;
}

public enum BestApi
{
    MiraAPI,
    Mitochondria,
    Reactor,
}
