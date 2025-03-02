using MiraAPI.Events.Vanilla.Gameplay;
using MiraAPI.Networking;
using MiraAPI.PluginLoading;
using TownOfUsFusion.Modifiers.Camped;

namespace TownOfUsFusion;

    [BepInPlugin(Id, "Town Of Us Fusion", VersionString)]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)] // Required for networking
[BepInDependency(MiraApiPlugin.Id)]
[ReactorModFlags(ModFlags.RequireOnAllClients)]
public partial class TownOfUsFusion : BasePlugin, IMiraPlugin
{
    public const string Id = "com.FusionStudios.TownOfUsFusion";
    public const string VersionString = "1.0.0";
    public const string TouVersionString = "5.2.0";
    public static System.Version Version = System.Version.Parse(VersionString);
    public const string VersionTag = "<color=#ff33fc></color>";
    public const bool isDevBuild = true;
    public const string DevBuildVersion = "4";
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Town of Us\nFusion";
    public ConfigFile GetConfigFile() => Config;

    public static readonly string DataPath = Path.GetDirectoryName(Application.dataPath);
    public static readonly string Assets = Path.Combine(DataPath, "FusionAssets");
    public static readonly string Hats = Path.Combine(Assets, "CustomHats");
    public static readonly string Visors = Path.Combine(Assets, "CustomVisors");
    public static readonly string Nameplates = Path.Combine(Assets, "CustomNameplates");
    public static readonly string Sounds = Path.Combine(Assets, "CustomSounds");
    public static readonly string Other = Path.Combine(Assets, "Other");
    public static readonly string ModsFolder = Path.Combine(DataPath, "BepInEx", "plugins");
    public static string RuntimeLocation;
        
    public override void Load()
    {
        RuntimeLocation = Path.GetDirectoryName(Assembly.GetAssembly(typeof(TownOfUsFusion)).Location);
        ReactorCredits.Register<TownOfUsFusion>(ReactorCredits.AlwaysShow);
        
        Harmony.PatchAll();
    }  
}
