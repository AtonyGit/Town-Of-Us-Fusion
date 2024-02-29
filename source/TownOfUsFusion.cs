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
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using UnityEngine.SceneManagement;
using TownOfUsFusion.Patches.ScreenEffects;

namespace TownOfUsFusion
{
    [BepInPlugin(Id, "Town Of Us Fusion", VersionString)]
    [BepInDependency(ReactorPlugin.Id)]
    [BepInDependency(SubmergedCompatibility.SUBMERGED_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    [ReactorModFlags(Reactor.Networking.ModFlags.RequireOnAllClients)]
    public class TownOfUsFusion : BasePlugin
    {
        public const string Id = "com.FusionStudios.TownOfUsFusion";
        public const string VersionString = "5.0.3";
        public static System.Version Version = System.Version.Parse(VersionString);

        public static AssetLoader bundledAssets;

        public static Sprite JanitorClean;
        public static Sprite EngineerFix;
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

        public static Vector3 ButtonPosition { get; private set; } = new Vector3(2.6f, 0.7f, -9f);

        private static DLoadImage _iCallLoadImage;


        private Harmony _harmony;

        public ConfigEntry<string> Ip { get; set; }

        public ConfigEntry<ushort> Port { get; set; }

        public override void Load()
        {
            System.Console.WriteLine("000.000.000.000/000000000000000000");

            _harmony = new Harmony("com.slushiegoose.TownOfUsFusion");

            Generate.GenerateAll();

            bundledAssets = new();

            JanitorClean = CreateSprite("TownOfUsFusion.Resources.Janitor.png");
            EngineerFix = CreateSprite("TownOfUsFusion.Resources.Engineer.png");
            SwapperSwitch = CreateSprite("TownOfUsFusion.Resources.SwapperSwitch.png");
            SwapperSwitchDisabled = CreateSprite("TownOfUsFusion.Resources.SwapperSwitchDisabled.png");
            Footprint = CreateSprite("TownOfUsFusion.Resources.Footprint.png");
            NormalKill = CreateSprite("TownOfUsFusion.Resources.NormalKill.png");
            MedicSprite = CreateSprite("TownOfUsFusion.Resources.Medic.png");
            SeerSprite = CreateSprite("TownOfUsFusion.Resources.Seer.png");
            SampleSprite = CreateSprite("TownOfUsFusion.Resources.Sample.png");
            MorphSprite = CreateSprite("TownOfUsFusion.Resources.Morph.png");
            Arrow = CreateSprite("TownOfUsFusion.Resources.Arrow.png");
            MineSprite = CreateSprite("TownOfUsFusion.Resources.Mine.png");
            SwoopSprite = CreateSprite("TownOfUsFusion.Resources.Swoop.png");
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
            TrackSprite = CreateSprite("TownOfUsFusion.Resources.Track.png");
            PlantSprite = CreateSprite("TownOfUsFusion.Resources.Plant.png");
            DetonateSprite = CreateSprite("TownOfUsFusion.Resources.Detonate.png");
            TransportSprite = CreateSprite("TownOfUsFusion.Resources.Transport.png");
            MediateSprite = CreateSprite("TownOfUsFusion.Resources.Mediate.png");
            VestSprite = CreateSprite("TownOfUsFusion.Resources.Vest.png");
            ProtectSprite = CreateSprite("TownOfUsFusion.Resources.Protect.png");
            BlackmailSprite = CreateSprite("TownOfUsFusion.Resources.Blackmail.png");
            BlackmailLetterSprite = CreateSprite("TownOfUsFusion.Resources.BlackmailLetter.png");
            BlackmailOverlaySprite = CreateSprite("TownOfUsFusion.Resources.BlackmailOverlay.png");
            LighterSprite = CreateSprite("TownOfUsFusion.Resources.Lighter.png");
            DarkerSprite = CreateSprite("TownOfUsFusion.Resources.Darker.png");
            InfectSprite = CreateSprite("TownOfUsFusion.Resources.Infect.png");
            RampageSprite = CreateSprite("TownOfUsFusion.Resources.Rampage.png");
            TrapSprite = CreateSprite("TownOfUsFusion.Resources.Trap.png");
            InspectSprite = CreateSprite("TownOfUsFusion.Resources.Inspect.png");
            ExamineSprite = CreateSprite("TownOfUsFusion.Resources.Examine.png");
            EscapeSprite = CreateSprite("TownOfUsFusion.Resources.Recall.png");
            MarkSprite = CreateSprite("TownOfUsFusion.Resources.Mark.png");
            Revive2Sprite = CreateSprite("TownOfUsFusion.Resources.Revive2.png");
            WhisperSprite = CreateSprite("TownOfUsFusion.Resources.Whisper.png");
            ImitateSelectSprite = CreateSprite("TownOfUsFusion.Resources.ImitateSelect.png");
            ImitateDeselectSprite = CreateSprite("TownOfUsFusion.Resources.ImitateDeselect.png");
            ObserveSprite = CreateSprite("TownOfUsFusion.Resources.Observe.png");
            BiteSprite = CreateSprite("TownOfUsFusion.Resources.Bite.png");
            StakeSprite = CreateSprite("TownOfUsFusion.Resources.Stake.png");
            RevealSprite = CreateSprite("TownOfUsFusion.Resources.Reveal.png");
            ConfessSprite = CreateSprite("TownOfUsFusion.Resources.Confess.png");
            NoAbilitySprite = CreateSprite("TownOfUsFusion.Resources.NoAbility.png");
            CamouflageSprite = CreateSprite("TownOfUsFusion.Resources.Camouflage.png");
            CamoSprintSprite = CreateSprite("TownOfUsFusion.Resources.CamoSprint.png");
            CamoSprintFreezeSprite = CreateSprite("TownOfUsFusion.Resources.CamoSprintFreeze.png");
            RadiateSprite = CreateSprite("TownOfUsFusion.Resources.Radiate.png");
            HackSprite = CreateSprite("TownOfUsFusion.Resources.Hack.png");
            MimicSprite = CreateSprite("TownOfUsFusion.Resources.Mimic.png");
            LockSprite = CreateSprite("TownOfUsFusion.Resources.Lock.png");

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

            PalettePatch.Load();
            ClassInjector.RegisterTypeInIl2Cpp<RainbowBehaviour>();

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
