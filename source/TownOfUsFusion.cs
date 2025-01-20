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
using TownOfUsFusion.CrewmateRoles.DetectiveMod;
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
        public const string VersionString = "0.4.0";
        public const string TouVersionString = "5.0.4";
        public static System.Version Version = System.Version.Parse(VersionString);
        public const string VersionTag = "<color=#ff33fc></color>";
        public const bool isDevBuild = true;
        public const string DevBuildVersion = "2";

        public static AssetLoader bundledAssets;

        public static Sprite EngineerVent;
        public static Sprite EngineerFix;

        public static Sprite JanitorClean;
        public static Sprite SwapperSwitch;
        public static Sprite SwapperSwitchDisabled;
        public static Sprite Footprint;
        public static Sprite NormalKill;
        public static Sprite MedicSprite;
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
        public static Sprite AutopsySprite;
        public static Sprite ExamineSprite;
        public static Sprite EscapeSprite;
        public static Sprite MarkSprite;
        public static Sprite ImitateSelectSprite;
        public static Sprite ImitateDeselectSprite;
        public static Sprite ObserveSprite;
        public static Sprite BiteSprite;
        public static Sprite RevealSprite;
        public static Sprite ConfessSprite;
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
        public static Sprite FortifySprite;
        public static Sprite HypnotiseSprite;
        public static Sprite HysteriaSprite;
        public static Sprite JailSprite;
        public static Sprite InJailSprite;
        public static Sprite ExecuteSprite;
        public static Sprite CollectSprite;
        public static Sprite ReapSprite;
        public static Sprite SoulSprite;

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

        public static string RuntimeLocation;
        
        public override void Load()
        {
            RuntimeLocation = Path.GetDirectoryName(Assembly.GetAssembly(typeof(TownOfUsFusion)).Location);
            ReactorCredits.Register<TownOfUsFusion>(ReactorCredits.AlwaysShow);
            System.Console.WriteLine("000.000.000.000/000000000000000000");

            _harmony = new Harmony("com.fusionstudios.TownOfUsFusion");

            Generate.GenerateAll();

            bundledAssets = new();
            // Astral Roles
            TrackSprite = CreateScaledSprite("TownOfUsFusion.Resources.Track.png");
            MediateSprite = CreateScaledSprite("TownOfUsFusion.Resources.Mediate.png");
            AutopsySprite = CreateScaledSprite("TownOfUsFusion.Resources.Autopsy.png");
            ExamineSprite = CreateScaledSprite("TownOfUsFusion.Resources.Examine.png");
            EngineerVent = CreateVentSprite("TownOfUsFusion.Resources.VentEngineer.png");
            // Investigative Roles
            ProtectSprite = CreateScaledSprite("TownOfUsFusion.Resources.Protect.png");
            MorphSprite = CreateScaledSprite("TownOfUsFusion.Resources.Morph.png");
            SwoopSprite = CreateScaledSprite("TownOfUsFusion.Resources.Swoop.png");

            JanitorClean = CreateSprite("TownOfUsFusion.Resources.Janitor.png");
            EngineerFix = CreateSprite("TownOfUsFusion.Resources.Engineer.png");
            SwapperSwitch = CreateSprite("TownOfUsFusion.Resources.SwapperSwitch.png");
            SwapperSwitchDisabled = CreateSprite("TownOfUsFusion.Resources.SwapperSwitchDisabled.png");
            Footprint = CreateSprite("TownOfUsFusion.Resources.Footprint.png");
            NormalKill = CreateSprite("TownOfUsFusion.Resources.NormalKill.png");
            MedicSprite = CreateSprite("TownOfUsFusion.Resources.Medic.png");
            SeerSprite = CreateSprite("TownOfUsFusion.Resources.Seer.png");
            SampleSprite = CreateSprite("TownOfUsFusion.Resources.Sample.png");
            Arrow = CreateSprite("TownOfUsFusion.Resources.Arrow.png");
            MineSprite = CreateSprite("TownOfUsFusion.Resources.Mine.png");
            DouseSprite = CreateSprite("TownOfUsFusion.Resources.Douse.png");
            IgniteSprite = CreateSprite("TownOfUsFusion.Resources.Ignite.png");
            ReviveSprite = CreateSprite("TownOfUsFusion.Resources.Revive.png");
            ButtonSprite = CreateSprite("TownOfUsFusion.Resources.Button.png");
            DisperseSprite = CreateSprite("TownOfUsFusion.Resources.Disperse.png");
            DragSprite = CreateSprite("TownOfUsFusion.Resources.Drag.png");
            DropSprite = CreateSprite("TownOfUsFusion.Resources.Drop.png");
            CycleBackSprite = CreateSprite("TownOfUsFusion.Resources.CycleBack.png");
            CycleForwardSprite = CreateSprite("TownOfUsFusion.Resources.CycleForward.png");
            GuessSprite = CreateSprite("TownOfUsFusion.Resources.Guess.png");
            FlashSprite = CreateSprite("TownOfUsFusion.Resources.Flash.png");
            AlertSprite = CreateSprite("TownOfUsFusion.Resources.Alert.png");
            RememberSprite = CreateSprite("TownOfUsFusion.Resources.Remember.png");
            PlantSprite = CreateSprite("TownOfUsFusion.Resources.Plant.png");
            DetonateSprite = CreateSprite("TownOfUsFusion.Resources.Detonate.png");
            TransportSprite = CreateSprite("TownOfUsFusion.Resources.Transport.png");
            VestSprite = CreateSprite("TownOfUsFusion.Resources.Vest.png");
            BlackmailSprite = CreateSprite("TownOfUsFusion.Resources.Blackmail.png");
            BlackmailLetterSprite = CreateSprite("TownOfUsFusion.Resources.BlackmailLetter.png");
            BlackmailOverlaySprite = CreateSprite("TownOfUsFusion.Resources.BlackmailOverlay.png");
            LighterSprite = CreateSprite("TownOfUsFusion.Resources.Lighter.png");
            DarkerSprite = CreateSprite("TownOfUsFusion.Resources.Darker.png");
            InfectSprite = CreateSprite("TownOfUsFusion.Resources.Infect.png");
            RampageSprite = CreateSprite("TownOfUsFusion.Resources.Rampage.png");
            TrapSprite = CreateSprite("TownOfUsFusion.Resources.Trap.png");
            EscapeSprite = CreateSprite("TownOfUsFusion.Resources.Recall.png");
            MarkSprite = CreateSprite("TownOfUsFusion.Resources.Mark.png");
            ImitateSelectSprite = CreateSprite("TownOfUsFusion.Resources.ImitateSelect.png");
            ImitateDeselectSprite = CreateSprite("TownOfUsFusion.Resources.ImitateDeselect.png");
            ObserveSprite = CreateSprite("TownOfUsFusion.Resources.Observe.png");
            BiteSprite = CreateSprite("TownOfUsFusion.Resources.Bite.png");
            RevealSprite = CreateSprite("TownOfUsFusion.Resources.Reveal.png");
            ConfessSprite = CreateSprite("TownOfUsFusion.Resources.Confess.png");
            NoAbilitySprite = CreateSprite("TownOfUsFusion.Resources.NoAbility.png");
            CamouflageSprite = CreateSprite("TownOfUsFusion.Resources.Camouflage.png");
            CamoSprintSprite = CreateSprite("TownOfUsFusion.Resources.CamoSprint.png");
            CamoSprintFreezeSprite = CreateSprite("TownOfUsFusion.Resources.CamoSprintFreeze.png");
            HackSprite = CreateSprite("TownOfUsFusion.Resources.Hack.png");
            MimicSprite = CreateSprite("TownOfUsFusion.Resources.Mimic.png");
            LockSprite = CreateSprite("TownOfUsFusion.Resources.Lock.png");
            StalkSprite = CreateSprite("TownOfUsFusion.Resources.Stalk.png");
            CrimeSceneSprite = CreateSprite("TownOfUsFusion.Resources.CrimeScene.png");
            CampaignSprite = CreateSprite("TownOfUsFusion.Resources.Campaign.png");
            FortifySprite = CreateSprite("TownOfUsFusion.Resources.Fortify.png");
            HypnotiseSprite = CreateSprite("TownOfUsFusion.Resources.Hypnotise.png");
            HysteriaSprite = CreateSprite("TownOfUsFusion.Resources.Hysteria.png");
            JailSprite = CreateSprite("TownOfUsFusion.Resources.Jail.png");
            InJailSprite = CreateSprite("TownOfUsFusion.Resources.InJail.png");
            ExecuteSprite = CreateSprite("TownOfUsFusion.Resources.Execute.png");
            CollectSprite = CreateSprite("TownOfUsFusion.Resources.Collect.png");
            ReapSprite = CreateSprite("TownOfUsFusion.Resources.Reap.png");
            SoulSprite = CreateSprite("TownOfUsFusion.Resources.Soul.png");

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

            // RegisterInIl2CppAttribute.Register();

            DeadSeeGhosts = Config.Bind("Settings", "Dead See Other Ghosts", true, "Whether you see other dead player's ghosts while your dead");

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
            pixelsPerUnit = tex.height;
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
            pixelsPerUnit = tex.height / 2;
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
