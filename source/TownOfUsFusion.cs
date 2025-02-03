using System;
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
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using TownOfUsFusion.Patches.ScreenEffects;
using TownOfUsFusion.CrewmateRoles.InvestigatorMod;
using TownOfUsFusion.NeutralRoles.SoulCollectorMod;
using System.IO;
using Reactor.Utilities;

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
        public const string VersionString = "0.4.3";
        public const string TouVersionString = "5.2.0";
        public static System.Version Version = System.Version.Parse(VersionString);
        public const string VersionTag = "<color=#ff33fc></color>";
        public const bool isDevBuild = true;
        public const string DevBuildVersion = "1";

        public static readonly string DataPath = Path.GetDirectoryName(Application.dataPath);
        public static readonly string Assets = Path.Combine(DataPath, "FusionAssets");
        public static readonly string Hats = Path.Combine(Assets, "CustomHats");
        public static readonly string Visors = Path.Combine(Assets, "CustomVisors");
        public static readonly string Nameplates = Path.Combine(Assets, "CustomNameplates");
        public static readonly string Sounds = Path.Combine(Assets, "CustomSounds");
        public static readonly string Other = Path.Combine(Assets, "Other");
        public static readonly string ModsFolder = Path.Combine(DataPath, "BepInEx", "plugins");
        public static AssetLoader bundledAssets;

        public static Sprite JesterVent;
        public static Sprite VampireVent;
        public static Sprite SkKill;
        public static Sprite SkVent;
        public static Sprite WerewolfVent;
        public static Sprite GlitchVent;
        public static Sprite GlitchKill;
        public static Sprite WerewolfKill;
        public static Sprite EngineerVent;
        public static Sprite EngineerFix;

        public static Sprite JanitorClean;
        public static Sprite SwapperSwitch;
        public static Sprite SwapperSwitchDisabled;
        public static Sprite Footprint;
        public static Sprite NormalKill;
        public static Sprite MedicSprite;
        public static Sprite PsychicSprite;
        public static Sprite SampleSprite;
        public static Sprite MorphSprite;
        public static Sprite Arrow;
        public static Sprite MineSprite;
        public static Sprite SwoopSprite;
        public static Sprite SwoopingSprite;
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
        public static Sprite GuardSprite;
        public static Sprite MirrorAbsorbSprite;
        public static Sprite MirrorUnleashSprite;
        public static Sprite BlackmailSprite;
        public static Sprite BlackmailLetterSprite;
        public static Sprite BlackmailOverlaySprite;
        public static Sprite LighterSprite;
        public static Sprite DarkerSprite;
        public static Sprite InfectSprite;
        public static Sprite RampageSprite;
        public static Sprite TrapSprite;
        public static Sprite AutopsySprite;
        public static Sprite ExamineSprite;
        public static Sprite EscapeSprite;
        public static Sprite MarkSprite;
        public static Sprite ImitateSelectSprite;
        public static Sprite ImitateDeselectSprite;
        public static Sprite ObserveSprite;
        public static Sprite BiteSprite;
        public static Sprite BitSprite;
        public static Sprite RevealSprite;
        public static Sprite NoAbilitySprite;
        public static Sprite CamouflageSprite;
        public static Sprite CamoSprintSprite;
        public static Sprite CamoSprintFreezeSprite;
        public static Sprite HackSprite;
        public static Sprite MimicSprite;
        public static Sprite LockSprite;
        public static Sprite StalkSprite;
        public static Sprite CrimeSceneSprite;
        public static Sprite CampaignSprite;
        public static Sprite BlessSprite;
        public static Sprite HypnotiseSprite;
        public static Sprite HysteriaSprite;
        public static Sprite JailSprite;
        public static Sprite InJailSprite;
        public static Sprite ExecuteSprite;
        public static Sprite CollectSprite;
        public static Sprite ReapSprite;
        public static Sprite SoulSprite;
        public static Sprite PerceptSprite;
        public static Sprite WatchSprite;
        public static Sprite InquisKill;
        public static Sprite SheriffKill;
        public static Sprite PoisonSprite;
        public static Sprite PoisonedSprite;
        public static Sprite CampSprite;
        public static Sprite ShootSprite;
        public static Sprite ConsumeSprite;

        public static Sprite SettingsButtonSprite;
        public static Sprite CrewSettingsButtonSprite;
        public static Sprite NeutralSettingsButtonSprite;
        public static Sprite ImposterSettingsButtonSprite;
        public static Sprite ModifierSettingsButtonSprite;
        public static Sprite ToUBanner;
        public static Sprite UpdateTOUButton;
        public static Sprite UpdateSubmergedButton;

        public static Sprite ZoomPlusButton;
        public static Sprite ZoomMinusButton;
        public static Sprite ZoomPlusActiveButton;
        public static Sprite ZoomMinusActiveButton;

        public static Vector3 ButtonPosition { get; private set; } = new Vector3(2.6f, 0.7f, -9f);

        private static DLoadImage _iCallLoadImage;


        private Harmony _harmony;

        public static ConfigEntry<bool> DeadSeeGhosts { get; set; }
        public static ConfigEntry<bool> ColoredMap { get; set; }

        public static string RuntimeLocation;
        
        public override void Load()
        {
            RuntimeLocation = Path.GetDirectoryName(Assembly.GetAssembly(typeof(TownOfUsFusion)).Location);
            ReactorCredits.Register<TownOfUsFusion>(ReactorCredits.AlwaysShow);
            System.Console.WriteLine("000.000.000.000/000000000000000000");

            _harmony = new Harmony("com.fusionstudios.TownOfUsFusion");

            Generate.GenerateAll();

            bundledAssets = new();
            // Investigative Roles
                PerceptSprite = CreateScaledSprite("TownOfUsFusion.Resources.Percept.png");
                WatchSprite = CreateScaledSprite("TownOfUsFusion.Resources.Watch.png");
                MediateSprite = CreateScaledSprite("TownOfUsFusion.Resources.Mediate.png");
                AutopsySprite = CreateScaledSprite("TownOfUsFusion.Resources.Autopsy.png");
                ExamineSprite = CreateScaledSprite("TownOfUsFusion.Resources.Examine.png");
                PsychicSprite = CreateScaledSprite("TownOfUsFusion.Resources.Psychic.png");
                TrackSprite = CreateScaledSprite("TownOfUsFusion.Resources.Track.png");
                TrapSprite = CreateScaledSprite("TownOfUsFusion.Resources.Trap.png");
            // Killing Roles
                CampSprite = CreateSprite("TownOfUsFusion.Resources.Camp.png");
                ShootSprite = CreateSprite("TownOfUsFusion.Resources.Shoot.png");
                SheriffKill = CreateScaledSprite("TownOfUsFusion.Resources.SheriffKill.png");
                AlertSprite = CreateSprite("TownOfUsFusion.Resources.Alert.png");
                StalkSprite = CreateSprite("TownOfUsFusion.Resources.Stalk.png");
                JailSprite = CreateScaledSprite("TownOfUsFusion.Resources.Jail.png");
                InJailSprite = CreateSprite("TownOfUsFusion.Resources.InJail.png");
                ExecuteSprite = CreateSprite("TownOfUsFusion.Resources.Execute.png");
            // Protective Roles
                ReviveSprite = CreateSprite("TownOfUsFusion.Resources.Revive.png");
                MedicSprite = CreateSprite("TownOfUsFusion.Resources.Medic.png");
                GuardSprite = CreateSprite("TownOfUsFusion.Resources.Guard.png");
                MirrorAbsorbSprite = CreateScaledSprite("TownOfUsFusion.Resources.MirrorAbsorb.png");
                MirrorUnleashSprite = CreateScaledSprite("TownOfUsFusion.Resources.MirrorUnleash.png");
                BlessSprite = CreateSprite("TownOfUsFusion.Resources.Fortify.png");
            // Sovereign Roles
                CampaignSprite = CreateScaledSprite("TownOfUsFusion.Resources.Campaign.png");
                RevealSprite = CreateSprite("TownOfUsFusion.Resources.Reveal.png");
                SwapperSwitch = CreateSprite("TownOfUsFusion.Resources.SwapperSwitch.png");
                SwapperSwitchDisabled = CreateSprite("TownOfUsFusion.Resources.SwapperSwitchDisabled.png");
            // Utility Roles
                EngineerFix = CreateScaledSprite("TownOfUsFusion.Resources.Engineer.png");
                EngineerVent = CreateVentSprite("TownOfUsFusion.Resources.VentEngineer.png");
                ImitateSelectSprite = CreateMeetingSprite("TownOfUsFusion.Resources.ImitateSelect.png");
                ImitateDeselectSprite = CreateMeetingSprite("TownOfUsFusion.Resources.ImitateDeselect.png");
                TransportSprite = CreateScaledSprite("TownOfUsFusion.Resources.Transport.png");

            // Benign Roles
                RememberSprite = CreateSprite("TownOfUsFusion.Resources.Remember.png");
                ProtectSprite = CreateScaledSprite("TownOfUsFusion.Resources.Protect.png");
                VestSprite = CreateSprite("TownOfUsFusion.Resources.Vest.png");
            // Evil Roles
                ObserveSprite = CreateSprite("TownOfUsFusion.Resources.Observe.png");
                JesterVent = CreateVentSprite("TownOfUsFusion.Resources.JesterVent.png");
            // Chaos Roles
                InquisKill = CreateScaledSprite("TownOfUsFusion.Resources.InquisKill.png");
                ConsumeSprite = CreateScaledSprite("TownOfUsFusion.Resources.Consume.png");
            // Killing Roles
                DouseSprite = CreateSprite("TownOfUsFusion.Resources.Douse.png");
                IgniteSprite = CreateSprite("TownOfUsFusion.Resources.Ignite.png");
                SkKill = CreateScaledSprite("TownOfUsFusion.Resources.SerialKill.png");
                SkVent = CreateVentSprite("TownOfUsFusion.Resources.SerialVent.png");
                HackSprite = CreateSprite("TownOfUsFusion.Resources.Hack.png");
                MimicSprite = CreateSprite("TownOfUsFusion.Resources.Mimic.png");
                GlitchKill = CreateScaledSprite("TownOfUsFusion.Resources.GlitchKill.png");
                GlitchVent = CreateVentSprite("TownOfUsFusion.Resources.GlitchVent.png");
                RampageSprite = CreateSprite("TownOfUsFusion.Resources.Rampage.png");
                WerewolfKill = CreateScaledSprite("TownOfUsFusion.Resources.WerewolfKill.png");
                WerewolfVent = CreateVentSprite("TownOfUsFusion.Resources.WerewolfVent.png");
            // Neophyte Roles
                BiteSprite = CreateSprite("TownOfUsFusion.Resources.Bite.png");
                BitSprite = CreateSprite("TownOfUsFusion.Resources.Bit.png");
                VampireVent = CreateVentSprite("TownOfUsFusion.Resources.VampireVent.png");
            // Apocalypse Roles
                InfectSprite = CreateSprite("TownOfUsFusion.Resources.Infect.png");
                CollectSprite = CreateSprite("TownOfUsFusion.Resources.Collect.png");
                ReapSprite = CreateSprite("TownOfUsFusion.Resources.Reap.png");
                SoulSprite = CreateSprite("TownOfUsFusion.Resources.Soul.png");

            // Concealing Roles
                EscapeSprite = CreateScaledSprite("TownOfUsFusion.Resources.Recall.png");
                MarkSprite = CreateScaledSprite("TownOfUsFusion.Resources.Mark.png");
                SampleSprite = CreateScaledSprite("TownOfUsFusion.Resources.Sample.png");
                MorphSprite = CreateScaledSprite("TownOfUsFusion.Resources.Morph.png");
                SwoopSprite = CreateScaledSprite("TownOfUsFusion.Resources.Swoop.png");
                SwoopingSprite = CreateScaledSprite("TownOfUsFusion.Resources.Swooping.png");
                FlashSprite = CreateScaledSprite("TownOfUsFusion.Resources.Flash.png");
                NoAbilitySprite = CreateScaledSprite("TownOfUsFusion.Resources.VenNone.png");
                CamouflageSprite = CreateScaledSprite("TownOfUsFusion.Resources.VenCamo.png");
                CamoSprintSprite = CreateScaledSprite("TownOfUsFusion.Resources.VenCamoSprint.png");
                CamoSprintFreezeSprite = CreateScaledSprite("TownOfUsFusion.Resources.VenCamoSprintFreeze.png");
            // Killing Roles
                PlantSprite = CreateScaledSprite("TownOfUsFusion.Resources.Plant.png");
                DetonateSprite = CreateScaledSprite("TownOfUsFusion.Resources.Detonate.png");
                PoisonSprite = CreateScaledSprite("TownOfUsFusion.Resources.Poison.png");
                PoisonedSprite = CreateScaledSprite("TownOfUsFusion.Resources.Poisoned.png");
            // Support Roles
                BlackmailSprite = CreateScaledSprite("TownOfUsFusion.Resources.Blackmail.png");
                BlackmailLetterSprite = CreateSprite("TownOfUsFusion.Resources.BlackmailLetter.png");
                BlackmailOverlaySprite = CreateSprite("TownOfUsFusion.Resources.BlackmailOverlay.png");
                HypnotiseSprite = CreateScaledSprite("TownOfUsFusion.Resources.Hypnotise.png");
                HysteriaSprite = CreateSprite("TownOfUsFusion.Resources.Hysteria.png");
                JanitorClean = CreateScaledSprite("TownOfUsFusion.Resources.Janitor.png");
                MineSprite = CreateScaledSprite("TownOfUsFusion.Resources.Mine.png");
                DragSprite = CreateScaledSprite("TownOfUsFusion.Resources.Drag.png");
                DropSprite = CreateScaledSprite("TownOfUsFusion.Resources.Drop.png");

                ButtonSprite = CreateScaledSprite("TownOfUsFusion.Resources.Button.png");
                DisperseSprite = CreateSprite("TownOfUsFusion.Resources.Disperse.png");

            Footprint = CreateSprite("TownOfUsFusion.Resources.Footprint.png");
            NormalKill = CreateSprite("TownOfUsFusion.Resources.NormalKill.png");
            Arrow = CreateSprite("TownOfUsFusion.Resources.Arrow.png");
            CycleBackSprite = CreateSprite("TownOfUsFusion.Resources.CycleBack.png");
            CycleForwardSprite = CreateSprite("TownOfUsFusion.Resources.CycleForward.png");
            GuessSprite = CreateSprite("TownOfUsFusion.Resources.Guess.png");
            LighterSprite = CreateSprite("TownOfUsFusion.Resources.Lighter.png");
            DarkerSprite = CreateSprite("TownOfUsFusion.Resources.Darker.png");
            LockSprite = CreateSprite("TownOfUsFusion.Resources.Lock.png");
            CrimeSceneSprite = CreateSprite("TownOfUsFusion.Resources.CrimeScene.png");

            SettingsButtonSprite = CreateSprite("TownOfUsFusion.Resources.SettingsButton.png");
            CrewSettingsButtonSprite = CreateSprite("TownOfUsFusion.Resources.Crewmate.png");
            NeutralSettingsButtonSprite = CreateSprite("TownOfUsFusion.Resources.Neutral.png");
            ImposterSettingsButtonSprite = CreateSprite("TownOfUsFusion.Resources.Impostor.png");
            ModifierSettingsButtonSprite = CreateSprite("TownOfUsFusion.Resources.Modifiers.png");
            ToUBanner = CreateSprite("TownOfUsFusion.Resources.TownOfUsFusionBanner.png");
            UpdateTOUButton = CreateSprite("TownOfUsFusion.Resources.UpdateToUButton.png");
            UpdateSubmergedButton = CreateSprite("TownOfUsFusion.Resources.UpdateSubmergedButton.png");

            ZoomPlusButton = CreateSprite("TownOfUsFusion.Resources.Plus.png");
            ZoomMinusButton = CreateSprite("TownOfUsFusion.Resources.Minus.png");
            ZoomPlusActiveButton = CreateSprite("TownOfUsFusion.Resources.PlusActive.png");
            ZoomMinusActiveButton = CreateSprite("TownOfUsFusion.Resources.MinusActive.png");

            PalettePatch.Load();
            ClassInjector.RegisterTypeInIl2Cpp<RainbowBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<CrimeScene>();
            ClassInjector.RegisterTypeInIl2Cpp<Soul>();

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    var filePath = Application.persistentDataPath;
                    var file = filePath + $"/GameSettings-Slot{i}";
                    if (File.Exists(file))
                    {
                        string newFile = Path.Combine(filePath, $"Saved Settings {i}.txt");
                        File.Move(file, newFile);
                    }
                }
                catch { }
            }

            // RegisterInIl2CppAttribute.Register();

            DeadSeeGhosts = Config.Bind("Settings", "Dead See Other Ghosts", true, "Whether you see other dead player's ghosts while your dead");
            ColoredMap = Config.Bind("Settings", "Colored Map", true, "Whether your map is colored based off role color or not");

            _harmony.PatchAll();
            SubmergedCompatibility.Initialize();

            ServerManager.DefaultRegions = new Il2CppReferenceArray<IRegionInfo>(new IRegionInfo[0]);
        }

        public static Sprite CreateSprite(string name)
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
        public static Sprite CreateMeetingSprite(string name)
        {
            var pixelsPerUnit = 300f;
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
        public static Sprite CreateScaledSprite(string name)
        {
            var pixelsPerUnit = 200f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            // this allows the icons to be any size and still look good
            pixelsPerUnit = tex.width;
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }
        public static Sprite CreateVentSprite(string name)
        {
            var pixelsPerUnit = 200f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            pixelsPerUnit = (int)(tex.height * 1.5);
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
