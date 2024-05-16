using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Utilities.Extensions;
using Reactor.Networking.Attributes;
using TownOfUsFusion.CustomOption;
using TownOfUsFusion.Patches;
using TownOfUsFusion.RainbowMod;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.CrewmateRoles.DetectiveMod;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using UnityEngine.SceneManagement;
using TownOfUsFusion.Patches.ScreenEffects;
using System.IO;

namespace TownOfUsFusion
{
    [BepInPlugin(Id, "Town Of Us Fusion", VersionString)]
    [BepInDependency(ReactorPlugin.Id)]
    [BepInDependency(SubmergedCompatibility.SUBMERGED_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(LevelImpCheck.LEVELIMP_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    [ReactorModFlags(Reactor.Networking.ModFlags.RequireOnAllClients)]
    public class TownOfUsFusion : BasePlugin
    {
        public const string Id = "com.FusionStudios.TownOfUsFusion";
        public const string VersionString = "0.2.3";
        public const string TouVersionString = "5.0.4";
        public static System.Version Version = System.Version.Parse(VersionString);
        public static string STR_DiscordText = "Lobby";

        public static AssetLoader bundledAssets;

        public static Sprite JanitorClean;
        public static Sprite EngineerFix;
        public static Sprite SwapperSwitch;
        public static Sprite SwapperSwitchDisabled;
        public static Sprite Footprint;
        public static Sprite NormalKill;
        public static Sprite MedicSprite;
        public static Sprite StalkSprite;
        public static Sprite SeerSprite;
        public static Sprite SampleSprite;
        public static Sprite MorphSprite;
        public static Sprite Arrow;
        public static Sprite MineSprite;
        public static Sprite SwoopSprite;
        public static Sprite DouseSprite;
        public static Sprite IgniteSprite;
        public static Sprite ReviveSprite;
        public static Sprite ButtonSprite;
        public static Sprite DisperseSprite;
        public static Sprite CycleBackSprite;
        public static Sprite CycleForwardSprite;
        public static Sprite GuessSprite;
        public static Sprite DragSprite;
        public static Sprite DropSprite;
        public static Sprite FlashSprite;
        public static Sprite AlertSprite;
        public static Sprite RememberSprite;
        public static Sprite TrackSprite;
        public static Sprite PlantSprite;
        public static Sprite DetonateSprite;
        public static Sprite TransportSprite;
        public static Sprite MediateSprite;
        public static Sprite VestSprite;
        public static Sprite ProtectSprite;
        public static Sprite BlackmailSprite;
        public static Sprite BlackmailLetterSprite;
        public static Sprite BlackmailOverlaySprite;
        public static Sprite LighterSprite;
        public static Sprite DarkerSprite;
        public static Sprite InfectSprite;
        public static Sprite RampageSprite;
        public static Sprite TrapSprite;
        public static Sprite InspectSprite;
        public static Sprite ExamineSprite;
        public static Sprite EscapeSprite;
        public static Sprite MarkSprite;
        public static Sprite Revive2Sprite;
        public static Sprite WhisperSprite;
        public static Sprite ImitateSelectSprite;
        public static Sprite ImitateDeselectSprite;
        public static Sprite ObserveSprite;
        public static Sprite BiteSprite;
        public static Sprite BittenSprite;
        public static Sprite StakeSprite;
        public static Sprite RevealSprite;
        public static Sprite ConfessSprite;
        public static Sprite NoAbilitySprite;
        public static Sprite CamouflageSprite;
        public static Sprite CamoSprintSprite;
        public static Sprite CamoSprintFreezeSprite;
        public static Sprite RadiateSprite;
        public static Sprite HackSprite;
        public static Sprite MimicSprite;
        public static Sprite LockSprite;
        public static Sprite ResurrectSprite;
        public static Sprite ConsumeSprite;
        public static Sprite MapSettingsButtonSprite;
        public static Sprite SettingsButtonSprite;
        public static Sprite CrewSettingsButtonSprite;
        public static Sprite NeutralSettingsButtonSprite;
        public static Sprite ImposterSettingsButtonSprite;
        public static Sprite ModifierSettingsButtonSprite;
        public static Sprite AllianceSettingsButtonSprite;
        public static Sprite VisorColorButtonSprite;

        public static Sprite ToUBanner;
        public static Sprite UpdateTOUButton;
        public static Sprite UpdateSubmergedButton;

        public static Sprite ZoomPlusButton;
        public static Sprite ZoomMinusButton;

//      FUSION SHIT
        public static Sprite PoisonSprite;
        public static Sprite PoisonedSprite;
        public static Sprite CrimeSceneSprite;
        public static Sprite BugSprite;
        
        public static Sprite PortableAdSprite;
        public static Sprite GuardSprite;
        public static Sprite SpellbookSprite;
        public static Sprite CastSprite;
        public static Sprite TauntSprite;
        public static Sprite KnightSprite;
        public static Sprite SlashSprite;

        public static Sprite GuideButtonSprite;
        public static Sprite GuidePlaceholderSprite;
        public static Sprite Phone;
        public static Sprite GreyedOption;
        public static Sprite GuideOverlay;
        public static Sprite CrewGuideTab;
        public static Sprite NeutralGuideTab;
        public static Sprite ImpostorGuideTab;
        public static Sprite ModifierGuideTab;
        public static Sprite Plate;

        public static Vector3 ButtonPosition { get; private set; } = new Vector3(2.6f, 0.7f, -9f);

        private static DLoadImage _iCallLoadImage;


        private Harmony _harmony;

        public ConfigEntry<string> Ip { get; set; }

        public ConfigEntry<ushort> Port { get; set; }
        public static string RuntimeLocation;

        public override void Load()
        {
            RuntimeLocation = Path.GetDirectoryName(Assembly.GetAssembly(typeof(TownOfUsFusion)).Location);
            System.Console.WriteLine("000.000.000.000/000000000000000000");

            _harmony = new Harmony("com.fusionstudios.TownOfUsFusion");

            Generate.GenerateAll();

            bundledAssets = new();
            // CREW (ASTRAL)
            RadiateSprite = CreateSprite("TownOfUsFusion.Resources.Radiate.png");
            MediateSprite = CreateSprite("TownOfUsFusion.Resources.Mediate.png");
            ConfessSprite = CreateSprite("TownOfUsFusion.Resources.Confess.png");
            // CREW (INVESTIGATIVE)
            Footprint = CreateLegacySprite("TownOfUsFusion.Resources.Footprint.png");
            InspectSprite = CreateSprite("TownOfUsFusion.Resources.Inspect.png");
            ExamineSprite = CreateSprite("TownOfUsFusion.Resources.Examine.png");
            CrimeSceneSprite = CreateLegacySprite("TownOfUsFusion.Resources.CrimeScene.png");
            SeerSprite = CreateSprite("TownOfUsFusion.Resources.Seer.png");
            BugSprite = CreateSprite("TownOfUsFusion.Resources.Bug.png");
            PortableAdSprite = CreateSprite("TownOfUsFusion.Resources.PortableAdmin.png");
            TrackSprite = CreateSprite("TownOfUsFusion.Resources.Track.png");
            TrapSprite = CreateSprite("TownOfUsFusion.Resources.Trap.png");
            // CREW (KILLING)
            StalkSprite = CreateLegacySprite("TownOfUsFusion.Resources.Stalk.png");
            StakeSprite = CreateSprite("TownOfUsFusion.Resources.Stake.png");
            AlertSprite = CreateSprite("TownOfUsFusion.Resources.Alert.png");
            // CREW (PROTECTIVE)
            ReviveSprite = CreateSprite("TownOfUsFusion.Resources.Revive.png");
            GuardSprite = CreateSprite("TownOfUsFusion.Resources.Guard.png");
            MedicSprite = CreateSprite("TownOfUsFusion.Resources.Medic.png");
            LighterSprite = CreateLegacySprite("TownOfUsFusion.Resources.Lighter.png");
            DarkerSprite = CreateLegacySprite("TownOfUsFusion.Resources.Darker.png");
            // CREW (CHAOS)
            SpellbookSprite = CreateSprite("TownOfUsFusion.Resources.Spellbook.png");
            CastSprite = CreateSprite("TownOfUsFusion.Resources.Cast.png");
            SwapperSwitch = CreateLegacySprite("TownOfUsFusion.Resources.SwapperSwitch.png");
            SwapperSwitchDisabled = CreateLegacySprite("TownOfUsFusion.Resources.SwapperSwitchDisabled.png");
            TransportSprite = CreateSprite("TownOfUsFusion.Resources.Transport.png");
            // CREW (POWER)
            RevealSprite = CreateLegacySprite("TownOfUsFusion.Resources.Reveal.png");
            KnightSprite = CreateSprite("TownOfUsFusion.Resources.Knight.png");
            // CREW (SUPPORT)
            EngineerFix = CreateSprite("TownOfUsFusion.Resources.Engineer.png");
            ImitateSelectSprite = CreateLegacySprite("TownOfUsFusion.Resources.ImitateSelect.png");
            ImitateDeselectSprite = CreateLegacySprite("TownOfUsFusion.Resources.ImitateDeselect.png");
            // NEUTRAL (BENIGN)
            RememberSprite = CreateSprite("TownOfUsFusion.Resources.Remember.png");
            ProtectSprite = CreateSprite("TownOfUsFusion.Resources.Protect.png");
            VestSprite = CreateSprite("TownOfUsFusion.Resources.Vest.png");
            // NEUTRAL (EVIL)
            ObserveSprite = CreateSprite("TownOfUsFusion.Resources.Observe.png");
            // NEUTRAL (CHAOS)
            TauntSprite = CreateSprite("TownOfUsFusion.Resources.Taunt.png");
            // NEUTRAL (KILLING)
            DouseSprite = CreateSprite("TownOfUsFusion.Resources.Douse.png");
            IgniteSprite = CreateSprite("TownOfUsFusion.Resources.Ignite.png");
            InfectSprite = CreateSprite("TownOfUsFusion.Resources.Infect.png");
            HackSprite = CreateSprite("TownOfUsFusion.Resources.Hack.png");
            MimicSprite = CreateSprite("TownOfUsFusion.Resources.Mimic.png");
            LockSprite = CreateLegacySprite("TownOfUsFusion.Resources.Lock.png");
            SlashSprite = CreateSprite("TownOfUsFusion.Resources.Slash.png");
            RampageSprite = CreateSprite("TownOfUsFusion.Resources.Rampage.png");
            // NEUTRAL (NEOPHYTE)
            BiteSprite = CreateSprite("TownOfUsFusion.Resources.Bite.png");
            BittenSprite = CreateSprite("TownOfUsFusion.Resources.Bitten.png");
            ResurrectSprite = CreateSprite("TownOfUsFusion.Resources.Resurrect.png");
            ConsumeSprite = CreateSprite("TownOfUsFusion.Resources.Consume.png");
            // IMPOSTOR (CONCEALING)
            EscapeSprite = CreateSprite("TownOfUsFusion.Resources.Recall.png");
            MarkSprite = CreateSprite("TownOfUsFusion.Resources.Mark.png");
            FlashSprite = CreateSprite("TownOfUsFusion.Resources.Flash.png");
            SampleSprite = CreateSprite("TownOfUsFusion.Resources.Sample.png");
            MorphSprite = CreateSprite("TownOfUsFusion.Resources.Morph.png");
            SwoopSprite = CreateSprite("TownOfUsFusion.Resources.Swoop.png");
            NoAbilitySprite = CreateLegacySprite("TownOfUsFusion.Resources.NoAbility.png");
            CamouflageSprite = CreateLegacySprite("TownOfUsFusion.Resources.Camouflage.png");
            CamoSprintSprite = CreateLegacySprite("TownOfUsFusion.Resources.CamoSprint.png");
            CamoSprintFreezeSprite = CreateLegacySprite("TownOfUsFusion.Resources.CamoSprintFreeze.png");
            // IMPOSTOR (KILLING)
            PlantSprite = CreateLegacySprite("TownOfUsFusion.Resources.Plant.png");
            DetonateSprite = CreateLegacySprite("TownOfUsFusion.Resources.Detonate.png");
            PoisonSprite = CreateLegacySprite("TownOfUsFusion.Resources.Poison.png");
            PoisonedSprite = CreateLegacySprite("TownOfUsFusion.Resources.Poisoned.png");
            // IMPOSTOR (SUPPORT)
            BlackmailSprite = CreateSprite("TownOfUsFusion.Resources.Blackmail.png");
            BlackmailLetterSprite = CreateLegacySprite("TownOfUsFusion.Resources.BlackmailLetter.png");
            BlackmailOverlaySprite = CreateLegacySprite("TownOfUsFusion.Resources.BlackmailOverlay.png");
            JanitorClean = CreateSprite("TownOfUsFusion.Resources.Janitor.png");
            MineSprite = CreateSprite("TownOfUsFusion.Resources.Mine.png");
            DragSprite = CreateSprite("TownOfUsFusion.Resources.Drag.png");
            DropSprite = CreateSprite("TownOfUsFusion.Resources.Drop.png");
            // CULTIST GAMEMODE
            Revive2Sprite = CreateLegacySprite("TownOfUsFusion.Resources.Revive2.png");
            WhisperSprite = CreateLegacySprite("TownOfUsFusion.Resources.Whisper.png");
            // MODIFIERS
            DisperseSprite = CreateSprite("TownOfUsFusion.Resources.Disperse.png");
            ButtonSprite = CreateSprite("TownOfUsFusion.Resources.Button.png");
            // OTHER SPRITES
            NormalKill = CreateLegacySprite("TownOfUsFusion.Resources.NormalKill.png");
            Arrow = CreateLegacySprite("TownOfUsFusion.Resources.Arrow.png");
            CycleBackSprite = CreateLegacySprite("TownOfUsFusion.Resources.CycleBack.png");
            CycleForwardSprite = CreateLegacySprite("TownOfUsFusion.Resources.CycleForward.png");
            GuessSprite = CreateLegacySprite("TownOfUsFusion.Resources.Guess.png");
            // SETTINGS
            MapSettingsButtonSprite = CreateSettingsSprite("TownOfUsFusion.Resources.Map.png");
            SettingsButtonSprite = CreateSettingsSprite("TownOfUsFusion.Resources.SettingsButton.png");
            CrewSettingsButtonSprite = CreateSettingsSprite("TownOfUsFusion.Resources.Crewmate.png");
            NeutralSettingsButtonSprite = CreateSettingsSprite("TownOfUsFusion.Resources.Neutral.png");
            ImposterSettingsButtonSprite = CreateSettingsSprite("TownOfUsFusion.Resources.Impostor.png");
            ModifierSettingsButtonSprite = CreateSettingsSprite("TownOfUsFusion.Resources.Modifiers.png");
            AllianceSettingsButtonSprite = CreateSettingsSprite("TownOfUsFusion.Resources.Alliances.png");
            
            VisorColorButtonSprite = CreateSettingsSprite("TownOfUsFusion.Resources.VisorColor.png");
            // MAIN MENU
            ToUBanner = CreateLegacySprite("TownOfUsFusion.Resources.TownOfUsFusionBanner.png");
            UpdateTOUButton = CreateLegacySprite("TownOfUsFusion.Resources.UpdateToUButton.png");
            UpdateSubmergedButton = CreateLegacySprite("TownOfUsFusion.Resources.UpdateSubmergedButton.png");
            // UI BUTTONS
            ZoomPlusButton = CreateLegacySprite("TownOfUsFusion.Resources.Plus.png");
            ZoomMinusButton = CreateLegacySprite("TownOfUsFusion.Resources.Minus.png");
            // GUIDE STUFF
            GuideButtonSprite = CreateLegacySprite("TownOfUsFusion.Resources.Guide.png");
            GuidePlaceholderSprite = CreateLegacySprite("TownOfUsFusion.Resources.GuidePlaceholder.png");
            Plate = CreateLegacySprite("TownOfUsFusion.Resources.Plate.png");
            Phone = CreateSettingsSprite("TownOfUsFusion.Resources.Phone.png");
            GreyedOption = CreateTabSprite("TownOfUsFusion.Resources.GreyedOption.png");
            GuideOverlay = CreateSettingsSprite("TownOfUsFusion.Resources.GuideOverlay.png");
            CrewGuideTab = CreateTabSmallSprite("TownOfUsFusion.Resources.Crewmate.png");
            NeutralGuideTab = CreateTabSmallSprite("TownOfUsFusion.Resources.Neutral.png");
            ImpostorGuideTab = CreateTabSmallSprite("TownOfUsFusion.Resources.Impostor.png");
            ModifierGuideTab = CreateTabSmallSprite("TownOfUsFusion.Resources.Modifiers.png");

            PalettePatch.Load();
            ClassInjector.RegisterTypeInIl2Cpp<RainbowBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<CrimeScene>();

            // RegisterInIl2CppAttribute.Register();

            Ip = Config.Bind("Custom", "Ipv4 or Hostname", "127.0.0.1");
            Port = Config.Bind("Custom", "Port", (ushort) 22023);
            var defaultRegions = ServerManager.DefaultRegions.ToList();
            var ip = Ip.Value;
            if (Uri.CheckHostName(Ip.Value).ToString() == "Dns")
                foreach (var address in Dns.GetHostAddresses(Ip.Value))
                {
                    if (address.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    ip = address.ToString();
                    break;
                }

            ServerManager.DefaultRegions = defaultRegions.ToArray();

            SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>) ((scene, loadSceneMode) =>
            {
                try { ModManager.Instance.ShowModStamp(); }
                catch { }
            }));

            _harmony.PatchAll();
            SubmergedCompatibility.Initialize();
        }

        public static Sprite CreateSprite(string name)
        {
            var pixelsPerUnit = 416f;
            var pivot = new Vector2(0.5f, 0.575f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }
        public static Sprite CreateTabSprite(string name)
        {
            var pixelsPerUnit = 140f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }
        public static Sprite CreateTabSmallSprite(string name)
        {
            var pixelsPerUnit = 240f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }
        public static Sprite CreateSettingsSprite(string name)
        {
            var pixelsPerUnit = 180f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }
        public static Sprite CreateLegacySprite(string name)
        {
            var pixelsPerUnit = 100f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }

        public static void LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
        {
            _iCallLoadImage ??= IL2CPP.ResolveICall<DLoadImage>("UnityEngine.ImageConversion::LoadImage");
            var il2CPPArray = (Il2CppStructArray<byte>) data;
            _iCallLoadImage.Invoke(tex.Pointer, il2CPPArray.Pointer, markNonReadable);
        }

        private delegate bool DLoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
    }
}
